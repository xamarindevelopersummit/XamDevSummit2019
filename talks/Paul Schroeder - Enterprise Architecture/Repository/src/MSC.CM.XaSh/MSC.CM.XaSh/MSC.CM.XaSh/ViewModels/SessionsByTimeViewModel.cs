using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MSC.CM.Xam.ModelObj.CM;
using Xamarin.Forms;
using MSC.CM.Xam;
using MSC.CM.XaSh.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Diagnostics;
using Microsoft.AppCenter.Crashes;
using GalaSoft.MvvmLight.Command;
using MSC.CM.XaSh.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace MSC.CM.XaSh.ViewModels
{
    public class SessionsByTimeViewModel : BaseViewModel
    {
        private ObservableCollection<Grouping<string, Session>> _sessionsByTime;

        public SessionsByTimeViewModel(IDataStore store = null, IDataLoader loader = null, IDataUploader uploader = null)
        {
            DataStore = store;
            DataLoader = loader;
            DataUploader = uploader;
            Title = "Sessions By Time";
            SessionsByTime = new ObservableCollection<Grouping<string, Session>>();
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

                            await RefreshListViewData();

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

        public ObservableCollection<Grouping<string, Session>> SessionsByTime
        {
            get { return _sessionsByTime; }
            set { Set(nameof(SessionsByTime), ref _sessionsByTime, value); }
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

                await RefreshSessionList();
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

        private async Task RefreshSessionList()
        {
            var items = await DataStore.GetSessionsWithRoomsAsync();

            var sorted = from session in items
                         orderby session.StartTime
                         group session by session.DateDisplay into sessionGroup
                         select new Grouping<string, Session>(sessionGroup.Key, sessionGroup);

            SessionsByTime = new ObservableCollection<Grouping<string, Session>>(sorted);
        }
    }
}