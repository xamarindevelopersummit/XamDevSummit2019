using System.Threading.Tasks;
using MSC.ConferenceMate.DataService.Models;

namespace MSC.CM.XaSh.Services
{
	public interface IDataLoader
	{
		Task<bool> AuthCheckAndRenewTokenIfNeeded();

		bool AuthCheckTokenExpiration();

		Task<bool> AuthGetToken(string user, string pass);

		void AuthRemoveToken();

		Task<bool> AuthRenewRefreshToken();

		Task<bool> CheckNetworkAndAPIHeartbeat();

		Task<ConferenceMate.DataService.Models.UserProfilePhoto> GetUserProfileThumbnailAsync(int userProfileId);

		Task<bool> HeartbeatCheck();

		Task<int> LoadAnnouncementsAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadFeedbackInitiatorTypesAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadFeedbackTypesAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadRoomsAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadSessionLikesAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadSessionsAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadSessionSpeakersAsync(bool forceRefresh = false, bool secondPass = false);

		Task<int> LoadUsersAsync(bool forceRefresh = false, bool secondPass = false);

		Task<bool> SaveUserProfileImageAsync(UserProfilePhoto userProfilePhoto);
	}
}