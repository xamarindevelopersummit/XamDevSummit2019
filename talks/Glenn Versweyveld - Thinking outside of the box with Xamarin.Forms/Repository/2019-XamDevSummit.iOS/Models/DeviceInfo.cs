using UIKit;
using Xamarin.Forms;
using XamDevSummit.iOS.Models;
using XamDevSummit.Models.Interfaces;

[assembly: Dependency(typeof(DeviceInfo))]
namespace XamDevSummit.iOS.Models
{
    public class DeviceInfo : IDeviceInfo
    {
        public float StatusbarHeight => (float)UIApplication.SharedApplication.StatusBarFrame.Size.Height;
    }
}