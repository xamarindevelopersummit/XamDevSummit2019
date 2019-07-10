using MSC.CM.Xam.ModelObj.CM;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using MSC.CM.Xam;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using MSC.CM.XaSh.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AppCenter.Crashes;
using GalaSoft.MvvmLight.Command;
using Xamarin.Essentials;

namespace MSC.CM.XaSh.ViewModels
{
    public class SpeakerViewModel : BaseViewModel
    {
        public SpeakerViewModel(IDataStore store = null, IDataLoader loader = null)
        {
            DataStore = store;
            DataLoader = loader;
            Title = "Speakers";
            Speakers = new ObservableCollection<UserProfile>();
        }

        public ObservableCollection<UserProfile> Speakers { get; private set; }

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
                    var ctSessions = await DataLoader.LoadSessionsAsync();
                    Debug.WriteLine($"Loaded {ctSessions} Sessions.");
                    var ctSessionSpeakers = await DataLoader.LoadSessionSpeakersAsync();
                    Debug.WriteLine($"Loaded {ctSessionSpeakers} SessionSpeakers.");
                }

                //clear local list
                Speakers.Clear();

                //populate local list
                var items = await DataStore.GetSpeakersAsync();
                foreach (var item in items)
                {
                    Speakers.Add(item);
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