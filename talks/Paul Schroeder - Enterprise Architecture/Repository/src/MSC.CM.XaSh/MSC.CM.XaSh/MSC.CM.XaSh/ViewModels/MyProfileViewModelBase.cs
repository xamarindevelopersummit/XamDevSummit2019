using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MSC.CM.XaSh.ViewModels
{
	public abstract class MyProfileViewModelBase : BaseViewModel
	{
		private UserProfile _currentUser;
		private bool _isUserLoggedIn;
		private ImageSource _myProfileImage = null;

		public virtual UserProfile CurrentUser
		{
			get { return _currentUser; }
			set { Set(ref _currentUser, value); }
		}

		public bool IsUserLoggedIn
		{
			get { return _isUserLoggedIn; }
			set { Set(ref _isUserLoggedIn, value); }
		}

		public virtual ImageSource MyProfileImage
		{
			get { return _myProfileImage; }
			set { Set(ref _myProfileImage, value); }
		}

		public virtual async Task LoadVM()
		{   // See if there is a user 'logged in'
			int currentUserProfileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);
			if (currentUserProfileId != 0)
			{
				CurrentUser = await DataStore.GetUserByUserProfileIdAsync(currentUserProfileId);
				IsUserLoggedIn = CurrentUser != null ? true : false;
				await RefreshUserProfileImageAsync(currentUserProfileId);
			}
		}

		protected async Task RefreshUserProfileImageAsync(int userProfileId)
		{
			if (IsUserLoggedIn)
			{
				ConferenceMate.DataService.Models.UserProfilePhoto userProfilePhoto = await DataLoader.GetUserProfileThumbnailAsync(userProfileId);
				if (userProfilePhoto != null && userProfilePhoto.Data != null && userProfilePhoto.Data.Length > 0)
				{
					MyProfileImage = ImageSource.FromStream(() => new MemoryStream(userProfilePhoto.Data));
				}
				else
				{
					MyProfileImage = (new Image { Source = "xamarinstore.jpg" }).Source;
				}
			}
		}
	}
}