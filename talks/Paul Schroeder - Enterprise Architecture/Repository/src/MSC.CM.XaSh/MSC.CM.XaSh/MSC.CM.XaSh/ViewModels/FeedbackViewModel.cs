using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MSC.CM.XaSh.ViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        private string _description;
        private string _feedbackTitle;
        private ObservableCollection<FeedbackType> _feedbackTypeList;
        private FeedbackType _selectedFeedbackType;

        public FeedbackViewModel(IDataStore store = null, IDataLoader loader = null, IDataUploader uploader = null)
        {
            DataStore = store;
            DataLoader = loader;
            DataUploader = uploader;
            Title = "Feedback";
            FeedbackTypeList = new ObservableCollection<FeedbackType>();
        }

        public string Description
        {
            get { return _description; }
            set { Set(nameof(Description), ref _description, value); }
        }

        public string FeedbackTitle
        {
            get { return _feedbackTitle; }
            set { Set(nameof(FeedbackTitle), ref _feedbackTitle, value); }
        }

        public ObservableCollection<FeedbackType> FeedbackTypeList
        {
            get { return _feedbackTypeList; }
            set { Set(nameof(FeedbackTypeList), ref _feedbackTypeList, value); }
        }

        public FeedbackType SelectedFeedbackType
        {
            get { return _selectedFeedbackType; }
            set { Set(nameof(SelectedFeedbackType), ref _selectedFeedbackType, value); }
        }

        public RelayCommand SubmitFeedbackCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        int currentUserProfileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0); // != 0 ? Preferences.Get(App.CURRENT_USER_ID, 0) as int? : null;

                        //build up a feedback data model - we don't need to build an obj model as this will go right into SQLite
                        var feedbackData = new MSC.CM.Xam.ModelData.CM.Feedback()
                        {
                            CreatedBy = "CurrentUser",
                            CreatedUtcDate = DateTime.UtcNow,
                            DataVersion = 1,
                            Description = Description,
                            Dispositioned = false,
                            FeedbackId = Guid.NewGuid(),
                            FeedbackTypeId = SelectedFeedbackType.FeedbackTypeId,
                            IsDeleted = false,
                            ModifiedBy = "CurrentUser",
                            ModifiedUtcDate = DateTime.UtcNow,
                            Title = FeedbackTitle,
                            Latitude = 0D,
                            Longitude = 0D,
                            UserProfileId = currentUserProfileId,
                            FeedbackInitiatorTypeId = 1, //customer
                            Source = "Mobile App",
                        };

                        //TODO: location stuff
                        //if (location != null && location.Latitude != 0D && location.Longitude != 0D)
                        //{
                        //    feedbackData.Longitude = location.Longitude;
                        //    feedbackData.Latitude = location.Latitude;
                        //}

                        //Write the data to SQLite
                        if (1 == await DataStore.WriteFeedbackRecord(feedbackData))
                        {
                            //Queue up the record for upload to the Azure Database
                            await DataUploader.QueueAsync(feedbackData.FeedbackId, QueueableObjects.Feedback);
                            //See if right now is a good time to upload the data - 10 records at a time
                            DataUploader.StartSafeQueuedUpdates();
                        }

                        //say thanks!
                        await Application.Current.MainPage.DisplayAlert("Thanks!", "We got your feedback!", "OK");

                        //does not work!
                        //Shell.Current.GoToAsync(new ShellNavigationState(new Uri("app://msctek.com/MSC.CM.XaSh")));
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                        await Application.Current.MainPage.DisplayAlert("Error", "Something happened, we may not have recieved your feedback!", "OK");
                    }
                });
            }
        }

        public async Task LoadData()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                if (base.IsConnected || App.UseSampleDataStore)
                {
                    //load SQLite from API or sample data
                    var count = await DataLoader.LoadFeedbackTypesAsync();
                    Debug.WriteLine($"Loaded {count} Feedback Types.");
                }

                //clear local list
                FeedbackTypeList.Clear();

                //populate local list
                var items = await DataStore.GetFeedbackTypesAsync();
                foreach (var item in items)
                {
                    FeedbackTypeList.Add(item);
                }

                SelectedFeedbackType = FeedbackTypeList.Where(z => z.Code == "GENERAL").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}