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
    public class AnnouncementsViewModel : BaseViewModel
    {
        public AnnouncementsViewModel(IDataStore store = null, IDataLoader loader = null)
        {
            DataStore = store;
            DataLoader = loader;
            Title = "Announcements";
            Announcements = new ObservableCollection<Announcement>();
        }

        public ObservableCollection<Announcement> Announcements { get; private set; }

        public async Task RefreshListViewData()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                if (base.IsConnected || App.UseSampleDataStore)
                {
                    //load SQLite from API or sample data
                    var count = await DataLoader.LoadAnnouncementsAsync();
                    Debug.WriteLine($"Loaded {count} Announcements.");
                }

                //clear local list
                Announcements.Clear();

                //populate local list
                var items = await DataStore.GetAnnouncementsAsync();
                foreach (var item in items)
                {
                    Announcements.Add(item);
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