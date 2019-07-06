using Xamarin.Forms;
using XamDevSummit.Droid.Models;
using XamDevSummit.Models.Interfaces;

[assembly: Dependency(typeof(DeviceInfo))]
namespace XamDevSummit.Droid.Models
{
    public class DeviceInfo : IDeviceInfo
    {
        public float StatusbarHeight => 0;
    }
}