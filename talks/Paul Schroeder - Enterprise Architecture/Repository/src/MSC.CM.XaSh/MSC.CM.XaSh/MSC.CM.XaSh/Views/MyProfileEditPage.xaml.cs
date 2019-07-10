using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MSC.CM.XaSh.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSC.CM.XaSh.Views
{
	public partial class MyProfileEditPage : ContentPage
	{
		private MyProfileEditViewModel viewModel;

		public MyProfileEditPage()
		{
			InitializeComponent();
			//TODO: Navigation is via the view, not the viewmodel... rework this eventually.
			//BindingContext = viewModel = Startup.ServiceProvider?.GetService<MyProfileEditViewModel>() ?? new MyProfileEditViewModel();
		}

		protected async override void OnAppearing()
		{
			Analytics.TrackEvent("MyProfileEditPage");
			base.OnAppearing();

			//Workaround here
			BindingContext = viewModel = Startup.ServiceProvider?.GetService<MyProfileEditViewModel>() ?? new MyProfileEditViewModel();
			await Refresh();
		}

		private async void ExsitingPictureButton_Clicked(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
				return;
			}

			var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
			{
				PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
			});

			await SetUserProfileImageAsync(file);
		}

		private async void NewPictureButton_Clicked(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				DisplayAlert("No Camera", ":( No camera available.", "OK");
				return;
			}

			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "Test",
				SaveToAlbum = true,
				CompressionQuality = 75,
				CustomPhotoSize = 50,
				PhotoSize = PhotoSize.MaxWidthHeight,
				MaxWidthHeight = 2000,
				DefaultCamera = CameraDevice.Front
			});

			await SetUserProfileImageAsync(file);
		}

		private async Task Refresh()
		{
			await viewModel.LoadVM();
		}

		private async Task SetUserProfileImageAsync(MediaFile file)
		{
			if (file == null)
				return;

			var myProfileEditViewModel = ((MyProfileEditViewModel)BindingContext);
			var saveCanTakeNewPicture = myProfileEditViewModel.CanTakeNewPicture;
			var saveCanPickExistingPicture = myProfileEditViewModel.CanPickExistingPicture;

			try
			{
				myProfileEditViewModel.CanTakeNewPicture = false;
				myProfileEditViewModel.CanPickExistingPicture = false;

				bool isUploaded = await myProfileEditViewModel.SetUserProfileImageAsync(myProfileEditViewModel.CurrentUser.UserProfileId, file);
				if (!isUploaded)
				{
					DisplayAlert("File upload failed.", file.Path, "OK");
				}
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}
			finally
			{
				myProfileEditViewModel.CanTakeNewPicture = saveCanTakeNewPicture;
				myProfileEditViewModel.CanPickExistingPicture = saveCanPickExistingPicture;
			}
		}
	}
}