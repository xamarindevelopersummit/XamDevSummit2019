using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace NetworkState
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage (new MainPage ());
        }
    }
}
