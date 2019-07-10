using MSC.CM.Xam.ModelData.CM;
using MSC.CM.XaSh.Database;
using MSC.ConferenceMate.DataService.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MSC.CM.XaSh.Services
{
	public class SampleDataLoader : IDataLoader
	{
		private SQLiteAsyncConnection conn = App.Database.conn;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task<bool> AuthCheckAndRenewTokenIfNeeded()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			return true;
		}

		public bool AuthCheckTokenExpiration()
		{
			return false;
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task<bool> AuthGetToken(string user, string pass)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			Preferences.Set(Consts.CURRENT_ASP_USER_ID, DemoUserProfile.SampleUserProfile00.AspNetUsersId);
			Preferences.Set(Consts.CURRENT_USER_EMAIL, user);
			Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, DemoUserProfile.SampleUserProfile00.UserProfileId);

			return true;
		}

		public void AuthRemoveToken()
		{
			Preferences.Set(Consts.CURRENT_ASP_USER_ID, string.Empty);
			Preferences.Remove(Consts.CURRENT_USER_EMAIL);
			Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, 0);
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task<bool> AuthRenewRefreshToken()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			return true;
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task<bool> CheckNetworkAndAPIHeartbeat()
		{
			return true;
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task<UserProfilePhoto> GetUserProfileThumbnailAsync(int userProfileId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			return null;
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task<bool> HeartbeatCheck()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			return true;
		}

		public async Task<int> LoadAnnouncementsAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<Announcement>().CountAsync() > 0)
			{
				await conn.Table<Announcement>().Where(x => x.AnnouncementId != 0).DeleteAsync();
			}

			var myList = new List<Announcement>() {
				DemoAnnouncement.SampleAnnouncement00,
				DemoAnnouncement.SampleAnnouncement01
			};

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadFeedbackInitiatorTypesAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<FeedbackInitiatorType>().CountAsync() > 0)
			{
				await conn.Table<FeedbackInitiatorType>().Where(x => x.FeedbackInitiatorTypeId != 0).DeleteAsync();
			}

			var myList = new List<FeedbackInitiatorType>() {
				DemoFeedbackInitiatorType.SampleFeedbackInitiatorType00,
				DemoFeedbackInitiatorType.SampleFeedbackInitiatorType01
			};

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadFeedbackTypesAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<FeedbackType>().CountAsync() > 0)
			{
				await conn.Table<FeedbackType>().Where(x => x.FeedbackTypeId != 0).DeleteAsync();
			}

			var myList = new List<FeedbackType>() {
				DemoFeedbackType.SampleFeedbackType00,
				DemoFeedbackType.SampleFeedbackType01
			};

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadRoomsAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<Room>().CountAsync() > 0)
			{
				await conn.Table<Room>().Where(x => x.RoomId != 0).DeleteAsync();
			}

			var myList = new List<Room>() {
				DemoRoom.SampleRoom00,
				DemoRoom.SampleRoom01
			};

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadSessionLikesAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<SessionLike>().CountAsync() > 0)
			{
				await conn.Table<SessionLike>().Where(x => x.SessionId != 0).DeleteAsync();
			}

			var myList = new List<SessionLike>() {
				DemoSessionLike.SampleSessionLike00,
				DemoSessionLike.SampleSessionLike01,
			};

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadSessionsAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<Session>().CountAsync() > 0)
			{
				await conn.Table<Session>().Where(x => x.SessionId != 0).DeleteAsync();
			}

			var myList = new List<Session>() {
				DemoSession.SampleSession00,
				DemoSession.SampleSession01,
                //DemoSession.SampleSession02,
                //DemoSession.SampleSession03,
                //DemoSession.SampleSession04,
                //DemoSession.SampleSession05
            };

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadSessionSpeakersAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<SessionSpeaker>().CountAsync() > 0)
			{
				await conn.Table<SessionSpeaker>().Where(x => x.SessionId != 0).DeleteAsync();
			}

			var myList = new List<SessionSpeaker>() {
				DemoSessionSpeaker.SampleSessionSpeaker00,
				DemoSessionSpeaker.SampleSessionSpeaker01
			};

			return await conn.InsertAllAsync(myList);
		}

		public async Task<int> LoadUsersAsync(bool forceRefresh = false, bool secondPass = false)
		{
			if (await conn.Table<UserProfile>().CountAsync() > 0)
			{
				await conn.Table<UserProfile>().Where(x => x.UserProfileId != 0).DeleteAsync();
			}

			var myList = new List<UserProfile>() {
				DemoUserProfile.SampleUserProfile00,
				DemoUserProfile.SampleUserProfile01
			};

			return await conn.InsertAllAsync(myList);
		}

		public Task<bool> SaveUserProfileImageAsync(UserProfilePhoto userProfilePhoto)
		{
			return Task.FromResult(true);
		}
	}
}