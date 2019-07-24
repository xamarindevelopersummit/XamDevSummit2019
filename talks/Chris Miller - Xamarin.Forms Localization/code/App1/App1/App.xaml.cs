using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Services;
using App1.Views;
using System.Threading;
using System.Globalization;


namespace App1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            ChangeCulture("es-PE");

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        private void ChangeCulture(string locale)
        {
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo(locale);

            Thread.CurrentThread.CurrentUICulture =
                Thread.CurrentThread.CurrentCulture;
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
