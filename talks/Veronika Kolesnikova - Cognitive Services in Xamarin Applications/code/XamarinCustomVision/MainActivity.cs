using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using Android.Provider;
using Android.Graphics;
using Android.Runtime;
using Org.Tensorflow.Contrib.Android;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Util;
using System.Xml;
using System.Net.Http;
using System.Text;
using Android.Media;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace XamarinCustomVision
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
        ImageView imageView;
        static AccountDetails account = new AccountDetails();

        public struct Recognition
        {
            public string Label;
            public float Confidence;

            public override string ToString()
            {
                return string.Format($"[Label: {Label}, Confidence: {Confidence}]");
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            var btnCamera = FindViewById<Button>(Resource.Id.myButton);
            imageView = FindViewById<ImageView>(Resource.Id.image);

            btnCamera.Click += Take_Picture;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Take_Picture(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);

            var assets = Application.Context.Assets;
            var inferenceInterface = new TensorFlowInferenceInterface(assets, "model.pb");
            var sr = new StreamReader(assets.Open("labels.txt"));
            var labels = sr.ReadToEnd()
                           .Split('\n')
                           .Select(s => s.Trim())
                           .Where(s => !string.IsNullOrEmpty(s))
                           .ToList();

            var floatValues = GetBitmapPixels(bitmap);
            var outputs = new float[labels.Count];
            inferenceInterface.Feed("Placeholder", floatValues, 1, 224, 224, 3);
            inferenceInterface.Run(new[] { "loss" });
            inferenceInterface.Fetch("loss", outputs);

            var results = new Recognition[labels.Count];
            for (int i = 0; i < labels.Count; i++)
            {
                results[i] = new Recognition { Confidence = outputs[i], Label = labels[i] };
            }

            Array.Sort(results, (x, y) => y.Confidence.CompareTo(x.Confidence));
            var result = String.Format("I think the cat on this picture is: {0}. I'm {1} confident", results[0].Label, results[0].Confidence.ToString("P1"));
            ((TextView)FindViewById(Resource.Id.result)).Text = result;
            TextToSpeech.SpeakAsync(result).ContinueWith((b) => { Task.Run(() => TextToSpeechAsync(result)); });
        }

        public static async Task TextToSpeechAsync(string text)
        {
            string accessToken;

            Authentication auth = new Authentication("https://eastus.api.cognitive.microsoft.com/sts/v1.0/issuetoken", account.Key);

            try
            {
                accessToken = await auth.FetchTokenAsync().ConfigureAwait(false);
                Log.WriteLine(LogPriority.Info, "TTS Token", "Successfully obtained an access token. \n");
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogPriority.Error, "TTS Token", "Failed to obtain an access token. " +
                    "Error:" + ex.Message);
                return;
            }

            string host = "https://eastus.tts.speech.microsoft.com/cognitiveservices/v1";

            const string EnglishNeural = "en-US, JessaNeural";
            string body = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" "+
                "xmlns:mstts=\"http://www.w3.org/2001/mstts\" xml:lang=\"en-US\"><voice xml:lang=\"en-US\" name=\"Microsoft"+
                " Server Speech Text to Speech Voice (" + EnglishNeural + 
                ")\">" + text + "</voice></speak>";

            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(host);
                    // Set the content type header
                    request.Content = new StringContent(body.ToString(), System.Text.Encoding.UTF8, "application/ssml+xml");
                    // Set additional header, such as Authorization and User-Agent
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Connection", "Keep-Alive");
                    request.Headers.Add("User-Agent", "TTSClient");
                    // Audio output format. See API reference for full list.
                    request.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                    Console.WriteLine("Calling the TTS service. Please wait... \n");
                    using (HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        using (System.IO.Stream dataStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            var player = CrossSimpleAudioPlayer.Current;
                            player.Load(dataStream);
                            player.Play();
                        }
                    }
                }
            }
        }

        static float[] GetBitmapPixels(Bitmap bitmap)
        {
            var floatValues = new float[224 * 224 * 3];

            using (var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, 224, 224, false))
            {
                using (var resizedBitmap = scaledBitmap.Copy(Bitmap.Config.Argb8888, false))
                {
                    var intValues = new int[224 * 224];
                    resizedBitmap.GetPixels(intValues, 0, resizedBitmap.Width, 0, 0, resizedBitmap.Width, resizedBitmap.Height);

                    for (int i = 0; i < intValues.Length; ++i)
                    {
                        var val = intValues[i];

                        floatValues[i * 3 + 0] = ((val & 0xFF) - 104);
                        floatValues[i * 3 + 1] = (((val >> 8) & 0xFF) - 117);
                        floatValues[i * 3 + 2] = (((val >> 16) & 0xFF) - 123);
                    }

                    resizedBitmap.Recycle();
                }

                scaledBitmap.Recycle();
            }

            return floatValues;
        }
    }
}