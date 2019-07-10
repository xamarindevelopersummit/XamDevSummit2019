using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.Helpers;
using MSC.CM.XaSh.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MSC.CM.XaSh.ViewModels
{
    public class MyFavoritesViewModel : BaseViewModel
    {
        private ObservableCollection<Session> _sessions;

        public MyFavoritesViewModel(IDataStore store = null, IDataLoader loader = null, IDataUploader uploader = null)
        {
            DataStore = store;
            DataLoader = loader;
            DataUploader = uploader;
            Title = "My Favorites";
            Sessions = new ObservableCollection<Session>();
        }

        public RelayCommand<int> LikeCommand
        {
            get
            {
                return new RelayCommand<int>(async (sessionId) =>
                {
                    try
                    {
                        var pkSessionLike = $"{sessionId}{Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0).ToString()}";
                        if (await DataStore.ToggleSessionLikeAsync(sessionId, pkSessionLike))
                        {
                            //Queue the record for upload
                            await DataUploader.QueueAsync(pkSessionLike, QueueableObjects.SessionLikes);

                            //re populate local list from sqlite
                            Sessions = (await DataStore.GetFavoriteSessionsAsync()).ToObservableCollection();

                            DataUploader.StartSafeQueuedUpdates();
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                });
            }
        }

        public ObservableCollection<Session> Sessions
        {
            get { return _sessions; }
            set { Set(nameof(Sessions), ref _sessions, value); }
        }

        public async Task RefreshListViewData()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                if (base.IsConnected || App.UseSampleDataStore)
                {
                    //load SQLite from API or sample data
                    var ctUsers = await DataLoader.LoadUsersAsync();
                    Debug.WriteLine($"Loaded {ctUsers} Users.");
                    var ctRooms = await DataLoader.LoadRoomsAsync();
                    Debug.WriteLine($"Loaded {ctRooms} Rooms.");
                    var ctSessions = await DataLoader.LoadSessionsAsync();
                    Debug.WriteLine($"Loaded {ctSessions} Sessions.");
                    var ctSessionSpeakers = await DataLoader.LoadSessionSpeakersAsync();
                    Debug.WriteLine($"Loaded {ctSessionSpeakers} SessionSpeakers.");
                }

                //clear local list
                Sessions.Clear();

                //populate local list
                var items = await DataStore.GetFavoriteSessionsAsync();
                foreach (var item in items)
                {
                    Sessions.Add(item);
                }
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