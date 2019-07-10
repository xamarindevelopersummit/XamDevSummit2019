using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.Helpers;
using MSC.CM.XaSh.Services;
using MSC.ConferenceMate.DataService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MSC.ConferenceMate.DataService.Extensions;
using Xamarin.Forms;
using System.Collections.Generic;

namespace MSC.CM.XaSh.ViewModels
{
	public class MyProfileEditViewModel : MyProfileViewModelBase
	{
		private string _biography;
		private bool _canPickExistingPicture;
		private bool _canTakeNewPicture;
		private string _companyName;
		private UserProfile _currentUser;
		private string _firstName;
		private string _jobTitle;
		private string _lastName;
		private string _twitterUrl;

		public MyProfileEditViewModel(IDataStore store = null, IDataLoader loader = null, IDataUploader uploader = null)
		{
			DataStore = store;
			DataLoader = loader;
			DataUploader = uploader;
			Title = "Edit My Profile";
		}

		public string Biography
		{
			get { return _biography; }
			set { Set(ref _biography, value); }
		}

		public ICommand CancelCommand => new Command(() => ExecuteCancelCommand());

		public bool CanPickExistingPicture
		{
			get { return _canPickExistingPicture; }
			set { Set(ref _canPickExistingPicture, value); }
		}

		public bool CanTakeNewPicture
		{
			get { return _canTakeNewPicture; }
			set { Set(ref _canTakeNewPicture, value); }
		}

		public string CompanyName
		{
			get { return _companyName; }
			set { Set(ref _companyName, value); }
		}

		public string FirstName
		{
			get { return _firstName; }
			set { Set(ref _firstName, value); }
		}

		public string JobTitle
		{
			get { return _jobTitle; }
			set { Set(ref _jobTitle, value); }
		}

		public string LastName
		{
			get { return _lastName; }
			set { Set(ref _lastName, value); }
		}

		public ICommand SaveCommand => new Command(() => ExecuteSaveCommand());
		public ICommand SelectExistingPictureCommand => new Command(() => PickExistingImage());
		public ICommand TakeNewPictureCommand => new Command(() => CaptureAnImage());

		public string TwitterUrl
		{
			get { return _twitterUrl; }
			set { Set(ref _twitterUrl, value); }
		}

		public override async Task LoadVM()
		{
			await base.LoadVM();
			if (CurrentUser != null && CurrentUser.UserProfileId > 0)
			{
				Biography = CurrentUser.Biography;
				CompanyName = CurrentUser.CompanyName;
				FirstName = CurrentUser.FirstName;
				LastName = CurrentUser.LastName;
				JobTitle = CurrentUser.JobTitle;
				TwitterUrl = CurrentUser.TwitterUrl;
			}

			CanPickExistingPicture = await PhotoHelper.CheckPickPhoto();
			CanTakeNewPicture = await PhotoHelper.CheckTakePhoto();
		}

		internal async Task<bool> SetUserProfileImageAsync(int userProfileId, MediaFile file)
		{
			bool isUploaded = false;

			// Update image on displayed form immediately.
			MyProfileImage = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});

			var fileInfo = new FileInfo(file.Path);
			if (IsConnected && file != null)
			{   // Save file to API/Azure.
				var stream = file.GetStream();
				UserProfilePhoto userProfilePhoto = new UserProfilePhoto()
				{
					UserProfileId = userProfileId,
					FileName = fileInfo.Name,
					Data = stream.ToArray()
				};

				isUploaded = await DataLoader.SaveUserProfileImageAsync(userProfilePhoto);
				if (isUploaded)
				{   // Get the thumbnail that was just created by the server API.
					await RefreshUserProfileImageAsync(userProfileId);
				}
			}

			return isUploaded;
		}

		private async void CaptureAnImage()
		{
		}

		private void ExecuteCancelCommand()
		{
			Biography = CurrentUser.Biography;
			CompanyName = CurrentUser.CompanyName;
			//Email = CurrentUser.Email;
			FirstName = CurrentUser.FirstName;
			LastName = CurrentUser.LastName;
			JobTitle = CurrentUser.JobTitle;
			TwitterUrl = CurrentUser.TwitterUrl;
		}

		private async void ExecuteSaveCommand()
		{
			if (ValidateProfile())
			{
				var editedCurrentUser = new Xam.ModelData.CM.UserProfile()
				{
					AvatarUrl = CurrentUser.AvatarUrl,
					Biography = Biography,
					BirthDate = CurrentUser.BirthDate,
					BlogUrl = CurrentUser.BlogUrl,
					CompanyName = CompanyName,
					CompanyWebsiteUrl = CurrentUser.CompanyWebsiteUrl,
					CreatedBy = CurrentUser.CreatedBy,
					CreatedUtcDate = CurrentUser.CreatedUtcDate,
					//Email = Email,
					FirstName = FirstName,
					GenderTypeId = CurrentUser.GenderTypeId,
					IsDeleted = false,
					JobTitle = JobTitle,
					LastLogin = CurrentUser.LastLogin,
					LastName = LastName,
					LinkedInUrl = CurrentUser.LinkedInUrl,
					ModifiedBy = CurrentUser.UserProfileId.ToString(),
					ModifiedUtcDate = DateTime.UtcNow,
					//PhotoUrl  <= //TODO
					PreferredLanguageId = CurrentUser.PreferredLanguageId,
					TwitterUrl = TwitterUrl,
					UserProfileId = CurrentUser.UserProfileId,
					DataVersion = CurrentUser.DataVersion
				};
				if (await DataStore.UpdateUserRecord(editedCurrentUser) == 1)
				{
					await DataUploader.QueueAsync(CurrentUser.UserProfileId, QueueableObjects.UserProfileUpdate);
					DataUploader.StartSafeQueuedUpdates();
				}
			}
		}

		private void PickExistingImage()
		{
			//do something here
			Debug.WriteLine("Do Whatever We Need To Do To pick an existing Image");
		}

		private bool ValidateProfile()
		{
			if (string.IsNullOrEmpty(FirstName))
			{
				AppShell.Current.DisplayAlert("Error", "First Name is Required", "OK");
				return false;
			}

			if (string.IsNullOrEmpty(LastName))
			{
				AppShell.Current.DisplayAlert("Error", "Last Name is Required", "OK");
				return false;
			}

			//if (string.IsNullOrEmpty(Email))
			//{
			//    AppShell.Current.DisplayAlert("Error", "Email is Required", "OK");
			//    return false;
			//}
			//if (!Helpers.RegexUtilities.IsValidEmail(Email))
			//{
			//    AppShell.Current.DisplayAlert("Error", "Email is Not Valid", "OK");
			//    return false;
			//}

			return true;
		}

		//public ICommand SaveCommand => new Command(() => Device.OpenUri(new Uri(CurrentUser.TwitterUrl)));
	}
}