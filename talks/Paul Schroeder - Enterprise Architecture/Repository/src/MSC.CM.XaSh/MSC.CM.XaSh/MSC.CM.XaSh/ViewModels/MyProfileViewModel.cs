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
	public class MyProfileViewModel : MyProfileViewModelBase
	{
		public MyProfileViewModel(IDataStore store = null, IDataLoader loader = null)
		{
			DataStore = store;
			DataLoader = loader;
			Title = "My Profile";
		}

		public ICommand EditProfileCommand => new Command(() => Shell.Current.GoToAsync(new ShellNavigationState("profileedit")));

		public ICommand TwitterCommand => new Command(() => Device.OpenUri(new Uri("https://www.twitter.com"))); //if(CurrentUser != null) { Device.OpenUri(new Uri(CurrentUser.TwitterUrl)); });

		public override async Task LoadVM()
		{   // See if there is a user 'logged in'
			await base.LoadVM();
		}

		public async Task LoginWithUserId(int userProfileId)
		{
			if (userProfileId != 0)
			{
				CurrentUser = await DataStore.GetUserByUserProfileIdAsync(userProfileId);
				if (CurrentUser != null)
				{
					IsUserLoggedIn = true;
					Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, userProfileId);
				}
			}
		}

		public void Logout()
		{
			CurrentUser = null;
			IsUserLoggedIn = false;
			Preferences.Remove(Consts.CURRENT_USER_PROFILE_ID);
		}
	}
}