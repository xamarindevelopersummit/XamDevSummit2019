using Xamarin.Forms;
using XamDevSummit.Models.Interfaces;

namespace XamDevSummit.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            //Fix top page marging requirement depending on the current device running the app
            StatusRowDefinition.Height = DependencyService.Get<IDeviceInfo>().StatusbarHeight;
        }
    }
}