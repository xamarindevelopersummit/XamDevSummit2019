using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter.Analytics;
using MSC.CM.XaSh.Services;
using Xamarin.Forms;

namespace MSC.CM.XaSh.Droid.Services
{
    //https://github.com/RobGibbens/XamarinFormsBackgrounding
    [Service]
    public class DroidRunQueuedUpdateService : Service
    {
        private CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    IDataUploader dataUploader = Startup.ServiceProvider?.GetService(typeof(IDataUploader)) as IDataUploader;
                    await dataUploader.StartQueuedUpdatesAsync(_cts.Token);
                    //await App.Current.UploadDataService.Instance.RunQueuedUpdatesAsync(_cts.Token);
                }
                catch (System.OperationCanceledException)
                {
                    //you could log this, or not
                    Analytics.TrackEvent("System.OperationCanceledException in DroidRunQueuedUpdateService");
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, "CancelledMessage")
                        );
                    }
                }
            }, _cts.Token);

            return StartCommandResult.Sticky;
        }
    }
}