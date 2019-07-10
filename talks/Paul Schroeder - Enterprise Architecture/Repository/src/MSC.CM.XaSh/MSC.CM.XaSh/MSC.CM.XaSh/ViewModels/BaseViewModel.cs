using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MSC.CM.XaSh.Services;
using GalaSoft.MvvmLight;
using Microsoft.AppCenter.Crashes;
using System.Diagnostics;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace MSC.CM.XaSh.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        private bool _isBusy;
        private string _title;

        public IDataLoader DataLoader { get; set; }

        public IDataStore DataStore { get; set; }

        public IDataUploader DataUploader { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { if (Set(ref _isBusy, value)) { OnIsBusyChanged(); } }
        }

        public string Title
        {
            get { return string.IsNullOrEmpty(_title) ? string.Empty : _title; }
            set { Set(ref _title, value); }
        }

        internal bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task CheckAppCenter()
        {
            if (IsConnected)
            {
                try
                {
                    Debug.WriteLine($"Analytics are Enabled? {await Analytics.IsEnabledAsync()}");
                    Debug.WriteLine($"Crash Reporting is Enabled? {await Crashes.IsEnabledAsync()}");
                    //Debug.WriteLine($"Distribution Notices are Enabled? {await Distribute.IsEnabledAsync()}");
                    //Debug.WriteLine($"Push Notifications are Enabled? {await Push.IsEnabledAsync()}");
                }
                catch (Exception ex)
                {
                    //ha ha - this probably won't work right away...but what the heck?
                    Crashes.TrackError(ex);
                    Debug.WriteLine("App Center enable check is Failing!");
                }
            }
        }

        protected virtual void OnIsBusyChanged()
        {
            Debug.WriteLine($"IsBusy: {IsBusy}");
        }
    }
}