using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MSC.CM.XaSh.iOS.Services;
using MSC.CM.XaSh.Services;
using UIKit;
using Xamarin.Forms;

namespace MSC.CM.XaSh.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static Action BackgroundSessionCompletionHandler;

        private IOSRunQueuedUpdateService myiOSUploadDataService;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init();
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            LoadApplication(new App());

            SubscribeToMessages();

            return base.FinishedLaunching(app, options);
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<StartUploadDataMessage>(this, "StartUploadDataMessage", async message =>
            {
                myiOSUploadDataService = new IOSRunQueuedUpdateService();
                await myiOSUploadDataService.StartAsync();
            });

            MessagingCenter.Subscribe<StopUploadDataMessage>(this, "StopUploadDataMessage", message =>
            {
                myiOSUploadDataService.Stop();
            });
        }
    }
}