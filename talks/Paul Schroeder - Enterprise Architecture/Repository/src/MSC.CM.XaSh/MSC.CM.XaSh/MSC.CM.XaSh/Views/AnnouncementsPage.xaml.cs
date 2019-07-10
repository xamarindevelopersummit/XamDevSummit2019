using Microsoft.AppCenter.Analytics;
using MSC.CM.XaSh.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSC.CM.XaSh.Views
{
    public partial class AnnouncementsPage : ContentPage
    {
        private AnnouncementsViewModel viewModel;

        public AnnouncementsPage()
        {
            InitializeComponent();
            //TODO: Navigation is via the view, not the viewmodel... rework this eventually.
            //BindingContext = viewModel = Startup.ServiceProvider?.GetService<AnnouncementsViewModel>() ?? new AnnouncementsViewModel();
        }

        protected async override void OnAppearing()
        {
            Analytics.TrackEvent("AnnouncementsPage");
            base.OnAppearing();

            //Workaround here
            BindingContext = viewModel = Startup.ServiceProvider?.GetService<AnnouncementsViewModel>() ?? new AnnouncementsViewModel();
            await Refresh();
        }

        private async void MainListView_Refreshing(object sender, EventArgs e)
        {
            await Refresh();
        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string catName = (e.CurrentSelection.FirstOrDefault() as Animal).Name;
            // This works because route names are unique in this application.
            //await Shell.Current.GoToAsync($"catdetails?name={catName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/domestic/cats/catdetails?name={catName}");
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MainListView.SelectedItem = null; // de-select the row
        }

        private async Task Refresh()
        {
            MainListView.IsRefreshing = true;
            await viewModel.RefreshListViewData();
            MainListView.EndRefresh();
        }
    }
}