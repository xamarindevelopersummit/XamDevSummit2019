using System;
using Xamarin.Forms;

namespace NetworkState
{
    public partial class MainPage : ContentPage
    {
        public MainPage ()
        {
            InitializeComponent ();
        }

        void OnDoGC (object sender, EventArgs e)
        {
            GC.Collect ();
        }

        async void OnNextPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync (new ProblemPage ());
        }
    }
}

