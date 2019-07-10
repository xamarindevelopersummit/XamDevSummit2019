using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSC.CM.XaSh.Views
{
    public partial class SessionsByRoomPage : ContentPage
    {
        private SessionsByRoomViewModel viewModel;

        public SessionsByRoomPage()
        {
            InitializeComponent();
            //TODO: Navigation is via the view, not the viewmodel... rework this eventually.
            BindingContext = viewModel = Startup.ServiceProvider?.GetService<SessionsByRoomViewModel>() ?? new SessionsByRoomViewModel();
        }

        protected async override void OnAppearing()
        {
            Analytics.TrackEvent("SessionsByRoomPage");
            base.OnAppearing();

            //Workaround here
            BindingContext = viewModel = Startup.ServiceProvider?.GetService<SessionsByRoomViewModel>() ?? new SessionsByRoomViewModel();
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

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            /*try
            {
                Xamarin.Forms.Switch mySwitch = sender as Xamarin.Forms.Switch;
                var session = mySwitch.Parent.Parent.BindingContext as Session;
                viewModel.SetSessionLike(session.SessionId, e.Value);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }*/
        }
    }
}