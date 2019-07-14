using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace NetworkState
{
    public class ProblemPage : ContentPage
    {

        byte[] data = new byte[1024 * 1024*20]; //Allocate 20MB of data

        public ProblemPage()
        {
            Array.Clear(data, 0, data.Length);
            BackgroundColor = CrossConnectivity.Current.IsConnected ? Color.Green : Color.Red;
          
           
           
        }

        void OnConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            BackgroundColor = e.IsConnected ? Color.Green : Color.Red;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossConnectivity.Current.ConnectivityChanged += OnConnectionChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossConnectivity.Current.ConnectivityChanged -= OnConnectionChanged;
        }
    }
}
