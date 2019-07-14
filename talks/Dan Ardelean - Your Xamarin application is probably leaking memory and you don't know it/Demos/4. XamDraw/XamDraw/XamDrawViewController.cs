using System;
using CoreGraphics;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace XamDraw
{
    public partial class XamDrawViewController : UIViewController
    {
        Random rand = new Random();
        Dictionary<IntPtr, UIColor> colors = new Dictionary<IntPtr, UIColor>();

        public XamDrawViewController(IntPtr handle) : base(handle)
        {

        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            foreach (UITouch touch in touches)
            {
                var color = GetRandomColor();
                colors.Add(touch.Handle, color);
            }
        }

        void DrawLine(CGPoint pt1, CGPoint pt2, UIColor color)
        {
            UIGraphics.BeginImageContext(imgDraw.Frame.Size);

            using(var g = UIGraphics.GetCurrentContext())
			{
                imgDraw.Layer.RenderInContext(g);
                var path = new CGPath();
                path.AddLines(new CGPoint[] { pt1, pt2 });
                g.SetLineWidth(3);
                color.SetStroke();
                g.AddPath(path);
                g.DrawPath(CGPathDrawingMode.Stroke);
                imgDraw.Image?.Dispose();
                imgDraw.Image = UIGraphics.GetImageFromCurrentImageContext();
            }

            UIGraphics.EndImageContext();
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            foreach (UITouch touch in touches)
            {
                UIColor color;
                colors.TryGetValue(touch.Handle, out color);

                DrawLine(touch.PreviousLocationInView(imgDraw),
                          touch.LocationInView(imgDraw), color);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            foreach (UITouch touch in touches)
            {
                colors.Remove(touch.Handle);
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            TouchesEnded(touches, evt);
        }

        void BtnClear_TouchUpInside(object sender, EventArgs e)
        {
            imgDraw.Image = null;
            GC.Collect();
        }

        UIColor GetRandomColor()
        {
            return new UIColor((float)rand.NextDouble(),
                                (float)rand.NextDouble(),
                                (float)rand.NextDouble(), 1.0f);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.MultipleTouchEnabled = true;
            this.View.UserInteractionEnabled = true;
            this.btnClear.TouchUpInside += BtnClear_TouchUpInside;
        }
    }
}

