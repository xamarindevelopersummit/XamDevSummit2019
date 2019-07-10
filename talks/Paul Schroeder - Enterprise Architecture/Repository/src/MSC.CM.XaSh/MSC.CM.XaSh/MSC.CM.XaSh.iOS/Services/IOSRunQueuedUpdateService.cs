using MSC.CM.XaSh.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace MSC.CM.XaSh.iOS.Services
{
    public class IOSRunQueuedUpdateService
    {
        private CancellationTokenSource _cts;
        private nint _taskId;

        public async Task StartAsync()
        {
            _cts = new CancellationTokenSource();

            _taskId = UIApplication.SharedApplication.BeginBackgroundTask(OnExpiration);

            try
            {
                //if you don't run this method in an iOS "Background Task" and the user gets a phone call or does something which backgrounds the app,
                //iOS will give the app ~5 seconds (at the very most), to finish or iOS will terminate the app. BeginBackgroundTask allows the method to run for another
                //180 seconds. iOS fires off OnExpiration when the 180 is almost over and you need to cut the method off by cancelling it.

                IDataUploader dataUploader = Startup.ServiceProvider?.GetService(typeof(IDataUploader)) as IDataUploader;
                await dataUploader.StartQueuedUpdatesAsync(_cts.Token);
                //await UploadDataService.Instance.RunQueuedUpdatesAsync(_cts.Token);
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    var message = new CancelledMessage();
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(message, "CancelledMessage"));
                }
            }

            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        private void OnExpiration()
        {
            _cts.Cancel();
        }
    }
}