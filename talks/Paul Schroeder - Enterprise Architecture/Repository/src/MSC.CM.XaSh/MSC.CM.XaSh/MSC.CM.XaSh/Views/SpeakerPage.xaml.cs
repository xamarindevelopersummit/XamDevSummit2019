using Microsoft.AppCenter.Analytics;
using MSC.CM.XaSh.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSC.CM.XaSh.Views
{
    public partial class SpeakerPage : ContentPage
    {
        private SpeakerViewModel viewModel;

        public SpeakerPage()
        {
            InitializeComponent();
            //TODO: Navigation is via the view, not the viewmodel... rework this eventually.
            //BindingContext = viewModel = Startup.ServiceProvider?.GetService<SpeakerViewModel>() ?? new SpeakerViewModel();
        }

        protected async override void OnAppearing()
        {
            Analytics.TrackEvent("SpeakerPage");
            base.OnAppearing();
            //Workaround here
            BindingContext = viewModel = Startup.ServiceProvider?.GetService<SpeakerViewModel>() ?? new SpeakerViewModel();
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