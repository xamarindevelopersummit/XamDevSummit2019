using Microsoft.AppCenter.Analytics;
using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSC.CM.XaSh.Views
{
    public partial class MyFavoritesPage : ContentPage
    {
        private MyFavoritesViewModel viewModel;

        public MyFavoritesPage()
        {
            InitializeComponent();
            //TODO: Navigation is via the view, not the viewmodel... rework this eventually.
            //BindingContext = viewModel = Startup.ServiceProvider?.GetService<MyFavoritesViewModel>() ?? new MyFavoritesViewModel();
        }

        protected async override void OnAppearing()
        {
            Analytics.TrackEvent("MyFavoritesPage");
            base.OnAppearing();

            //Workaround here
            BindingContext = viewModel = Startup.ServiceProvider?.GetService<MyFavoritesViewModel>() ?? new MyFavoritesViewModel();
            await Refresh();
        }

        private async void MainListView_Refreshing(object sender, EventArgs e)
        {
            await Refresh();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        private async Task Refresh()
        {
            MainListView.IsRefreshing = true;
            await viewModel.RefreshListViewData();
            MainListView.EndRefresh();
        }
    }
}