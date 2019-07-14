using System;
using UIKit;
using System.Diagnostics;
using CoreGraphics;

namespace UserPeers
{
    public partial class ViewController : UIViewController
    {
        CustomParentView parentView;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.NavigationItem.RightBarButtonItems = new[] {
                new UIBarButtonItem(UIBarButtonSystemItem.Trash, OnGarbageCollect),
                new UIBarButtonItem(UIBarButtonSystemItem.Stop, OnRemoveSubviews),
                new UIBarButtonItem(UIBarButtonSystemItem.Add, OnAddSubviews)
            };
        }

        void OnAddSubviews(object sender, EventArgs e)
        {
            if (this.parentView != null)
                return;

            parentView = new CustomParentView {
                BackgroundColor = UIColor.Yellow,
                Frame = new CGRect(0, 0, 200, 200),
                Center = this.View.Center
            };

            Add(parentView);
        }

        void OnRemoveSubviews(object sender, EventArgs e)
        {
            if (parentView != null) {
                parentView.RemoveFromSuperview ();
                parentView = null;
            }
        }

        void OnGarbageCollect(object sender, EventArgs args)
        {
            Debug.WriteLine("Triggering GC.");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}

