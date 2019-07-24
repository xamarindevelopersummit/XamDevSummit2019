using Foundation;
using System;
using UIKit;

namespace PeerPromotion
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController (IntPtr handle) : base (handle)
        {
        }

        partial void OnGarbageCollect (UIBarButtonItem sender)
        {
            GC.Collect ();
        }
    }
}