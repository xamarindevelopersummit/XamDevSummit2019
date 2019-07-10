using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MSC.CM.XaSh.Helpers;
using MSC.CM.XaSh.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MSC.CM.XaSh.ViewModels
{
    public class GeneralInfoViewModel : BaseViewModel
    {
        private string _currentUserEmail;
        private int _currentUserId;
        private string _currentUserPassword;
        private string _dataMessage;
        private string _loginMessage;

        public GeneralInfoViewModel(IDataStore store = null, IDataLoader loader = null)
        {
            DataStore = store;
            DataLoader = loader;
            Title = "GeneralInfo";

            PrePopulateLoginFields();

            PopulateDataMessage();

            base.CheckAppCenter();
        }

        public string CurrentUserEmail
        {
            get { return _currentUserEmail; }
            set { Set(ref _currentUserEmail, value); }
        }

        public int CurrentUserId
        {
            get { return _currentUserId; }
            set { Set(ref _currentUserId, value); }
        }

        public string CurrentUserPassword
        {
            get { return _currentUserPassword; }
            set { Set(ref _currentUserPassword, value); }
        }

        public string DataMessage
        {
            get { return _dataMessage; }
            set { Set(ref _dataMessage, value); }
        }

        public bool IsUserLoggedIn
        {
            get
            {
                int profileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);
                return (profileId == 0) ? false : true;
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    IsBusy = true;
                    try
                    {
                        await Login();
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                    IsBusy = false;
                });
            }
        }

        public string LoginMessage
        {
            get { return _loginMessage; }
            set { Set(ref _loginMessage, value); }
        }

        public RelayCommand LogoutCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsBusy = true;
                    try
                    {
                        Logout();
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                    IsBusy = false;
                });
            }
        }

        public RelayCommand UseAPIDataCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    IsBusy = true;
                    try
                    {
                        Logout();
                        if (await App.Database.DropCreateTables())
                        {
                            Startup.InitForSwitchToAPIData();
                            DataLoader = (IDataLoader)Startup.ServiceProvider.GetService(typeof(IDataLoader));
                            //we need to login before we can pull fresh data
                            await Login();
                            PopulateDataMessage();
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                    IsBusy = false;
                });
            }
        }

        public RelayCommand UseSampleDataCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    IsBusy = true;
                    try
                    {
                        Logout();
                        if (await App.Database.DropCreateTables())
                        {
                            Startup.InitForSwitchToSampleData();
                            DataLoader = (IDataLoader)Startup.ServiceProvider.GetService(typeof(IDataLoader));
                            await Login();
                            PopulateDataMessage();
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                    IsBusy = false;
                });
            }
        }

        internal async Task RefreshUserData(bool forceRefresh = false)
        {
            if (base.IsConnected || App.UseSampleDataStore)
            {
                //load SQLite from API or sample data
                var countUsers = await DataLoader.LoadUsersAsync(forceRefresh: forceRefresh);
                Debug.WriteLine($"Loaded {countUsers} Users.");
                var countFeedbackInitTypes = await DataLoader.LoadFeedbackInitiatorTypesAsync(forceRefresh: forceRefresh);
                Debug.WriteLine($"Loaded {countFeedbackInitTypes} Feedback Initiator Types.");
                var countFeedbackTypes = await DataLoader.LoadFeedbackTypesAsync(forceRefresh: forceRefresh);
                Debug.WriteLine($"Loaded {countFeedbackTypes} Feedback Types.");
            }
        }

        private async Task Login()
        {
            await DataLoader.AuthGetToken(CurrentUserEmail, CurrentUserPassword);

            UpdateMessage();
            RaisePropertyChanged(nameof(IsUserLoggedIn));
            await RefreshUserData(true);
        }

        private void Logout()
        {
            DataLoader.AuthRemoveToken();
            UpdateMessage();
            RaisePropertyChanged(nameof(IsUserLoggedIn));
        }

        private void PopulateDataMessage()
        {
            DataMessage = (App.UseSampleDataStore) ? "Currently Using Sample Data" : "Currently Using API Data";
        }

        private bool PrePopulateLoginFields()
        {
            //TODO: Rework this a bit - this is set up for quick testing right now
            var currentId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);
            UpdateMessage();
            if (currentId == 0)
            {
                //no user logged in yet, but we can autofill these
                CurrentUserEmail = "David@example.com";
                CurrentUserPassword = "test"; //TODO: ONLY FOR TESTING

                return false;
            }
            else
            {
                //there is a user logged in
                CurrentUserEmail = Preferences.Get(Consts.CURRENT_USER_EMAIL, string.Empty);
                CurrentUserPassword = "test"; //TODO: ONLY FOR TESTING

                return true;
            }
        }

        private void UpdateMessage()
        {
            int profileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);
            LoginMessage = (profileId == 0) ? "User is not logged in" : $"User #{profileId} is logged in";
        }
    }
}