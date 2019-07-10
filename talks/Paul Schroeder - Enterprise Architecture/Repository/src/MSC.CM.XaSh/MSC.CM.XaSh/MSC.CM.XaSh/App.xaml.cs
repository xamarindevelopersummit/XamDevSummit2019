using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MSC.CM.XaSh.Services;
using MSC.CM.XaSh.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MSC.CM.XaSh.Database;
using System.IO;
using MSC.CM.XaSh.Helpers;
using Xamarin.Essentials;

namespace MSC.CM.XaSh
{
	public partial class App : Application
	{
		public static string AzureBackendUrl = "http://10.0.0.145:45455/api/";
		//public static string AzureBackendUrl = "https://conferencemate.azurewebsites.net/api/";

		// public static string AzureBackendUrl = "http://10.0.2.2:6677/api/"; //"http://10.0.2.2:6672/api/";
		// Android Emulator then asking for the localhost web service won't work, because you're looking at the localhost of the emulator.
		// Android Emulator has a magic address http://10.0.2.2:your_port that points to 127.0.0.1:your_port on your host machine.
		// If you're going to use the emulator trick in conjunction with IIS Express, run this along with your local API:
		// npx iisexpress-proxy 6672 to 6677
		// For details, see: https://github.com/icflorescu/iisexpress-proxy

		private static ConferenceMateDatabase database;

		public App()
		{
			InitializeComponent();

			Startup.Init();

			MainPage = new AppShell();
		}

		public static ConferenceMateDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new ConferenceMateDatabase(
					  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ConferenceMate.db3"));
				}
				return database;
			}
		}

		public static bool UseSampleDataStore { get; set; } = false;

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnStart()
		{
			// Handle when your app starts

			AppCenter.Start("android=eb56638a-6ad3-4c3f-b084-52f3b84633d6;" +
				 "ios=daee1b97-de5f-4f66-8982-5ee228fa79fe;",
				 typeof(Analytics), typeof(Crashes));
		}
	}
}