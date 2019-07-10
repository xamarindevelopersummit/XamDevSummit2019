using Microsoft.AppCenter.Analytics;
using MSC.CM.XaSh.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSC.CM.XaSh.Views
{
    public partial class MyProfilePage : ContentPage
    {
        private MyProfileViewModel viewModel;

        public MyProfilePage()
        {
            InitializeComponent();
            //TODO: Navigation is via the view, not the viewmodel... rework this eventually.
            //BindingContext = viewModel = Startup.ServiceProvider?.GetService<MyProfileViewModel>() ?? new MyProfileViewModel();
        }

        protected async override void OnAppearing()
        {
            Analytics.TrackEvent("MyProfilePage");
            base.OnAppearing();
            //Workaround here
            BindingContext = viewModel = Startup.ServiceProvider?.GetService<MyProfileViewModel>() ?? new MyProfileViewModel();
            await Refresh();
        }

        private async Task Refresh()
        {
            await viewModel.LoadVM();
        }
    }
}