using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using MSC.CM.Xam;
using MSC.CM.Xam.ModelData.CM;
using MSC.CM.XaSh.Helpers;
using MSC.ConferenceMate.DataService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MSC.CM.XaSh.Services
{
	public class AzureDataLoader : AzureDataLoaderBase, IDataLoader
	{
		private const int MAX_MINUTES_BETWEEN_UPDATES = 10;
		private const int MINUTES_REMAINING_FOR_TRIGGER_REFRESH_TOKEN = 2;

		public AzureDataLoader(IHttpClientFactory httpClientFactory = null, ILogger<AzureDataLoader> logger = null)
			: base(httpClientFactory, logger)
		{
		}

		public enum UpdateableTableNames
		{
			User,
			Session,
			SessionLike,
			SessionSpeaker
		}

		private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

		public async Task<bool> AuthCheckAndRenewTokenIfNeeded()
		{
			if (!AuthCheckTokenExpiration())
			{
				return await AuthRenewRefreshToken();
			}
			else
			{
				return true;
			}
		}

		public bool AuthCheckTokenExpiration()
		{
			string expStr = AuthenticationHelper.GetExpirationUTC();
			if (expStr != null)
			{
				DateTime expDate;
				if (DateTime.TryParse(expStr, out expDate))
				{
					if (expDate > DateTime.UtcNow.AddMinutes(MINUTES_REMAINING_FOR_TRIGGER_REFRESH_TOKEN))
					{
						//token is fine now and valid for more than the next ___ minutes, it doesn't need a refresh
						return true;
					}
					else
					{
						//token is expired or will expire in the next ____ minutes, needs a refresh
						return false;
					}
				}
				else
				{
					//can't parse expiration date, just clear and refresh everything and log it
					Analytics.TrackEvent($"Can't parse this token expiration date: {expStr}");
					AuthenticationHelper.ClearSecureStorageAuthValues();
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> AuthGetToken(string user, string pass)
		{
			try
			{
				var content = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("grant_type", "password"),
					new KeyValuePair<string, string>("username", user),
					new KeyValuePair<string, string>("password", pass)
				});

				var client = GetHttpClient(Consts.UNAUTHORIZED);
				var result = await client.PostAsync("token", content);

				if (result.IsSuccessStatusCode)
				{
					var resultContent = JsonConvert.DeserializeObject<AuthenticationResult>(await result.Content.ReadAsStringAsync());

					AuthenticationHelper.SetTokens(resultContent.access_token, resultContent.refresh_token, DateTime.UtcNow.AddSeconds((double)resultContent.expires_in).ToString());
					Preferences.Set(Consts.CURRENT_ASP_USER_ID, resultContent.userId);
					Preferences.Set(Consts.CURRENT_USER_EMAIL, resultContent.userName);
					Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, resultContent.userProfileId);

					Analytics.TrackEvent("Successful Login", new Dictionary<string, string> { { "user", user } });
					return true;
				}
				else
				{
					AuthenticationHelper.ClearSecureStorageAuthValues();
					Preferences.Set(Consts.CURRENT_ASP_USER_ID, string.Empty);
					Preferences.Remove(Consts.CURRENT_USER_EMAIL);
					Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, 0);

					Analytics.TrackEvent("Unsuccessful Login", new Dictionary<string, string> { { "user", user } });
					return false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				Crashes.TrackError(ex);
				return false;
			}
		}

		public void AuthRemoveToken()
		{
			try
			{
				AuthenticationHelper.ClearSecureStorageAuthValues();
				Preferences.Set(Consts.CURRENT_ASP_USER_ID, string.Empty);
				Preferences.Remove(Consts.CURRENT_USER_EMAIL);
				Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, 0);

				Analytics.TrackEvent("Successfully Logged Out");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				Crashes.TrackError(ex);
			}
		}

		public async Task<bool> AuthRenewRefreshToken()
		{
			try
			{
				var refToken = AuthenticationHelper.GetRefreshToken();

				var content = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("grant_type", "refresh_token"),
					new KeyValuePair<string, string>("refresh_token", refToken),
				});

				var client = GetHttpClient(Consts.UNAUTHORIZED);
				var result = await client.PostAsync("token", content);

				if (result.IsSuccessStatusCode)
				{
					var resultContent = JsonConvert.DeserializeObject<AuthenticationResult>(await result.Content.ReadAsStringAsync());

					AuthenticationHelper.SetTokens(resultContent.access_token, resultContent.refresh_token, DateTime.UtcNow.AddSeconds((double)resultContent.expires_in).ToString());
					Preferences.Set(Consts.CURRENT_ASP_USER_ID, resultContent.userId);
					Preferences.Set(Consts.CURRENT_USER_EMAIL, resultContent.userName);
					Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, resultContent.userProfileId);

					Analytics.TrackEvent("Successfully Refreshed Access Token");
					return true;
				}
				else
				{
					//refresh token call failed - clear everything
					AuthenticationHelper.ClearSecureStorageAuthValues();
					Preferences.Set(Consts.CURRENT_ASP_USER_ID, string.Empty);
					Preferences.Remove(Consts.CURRENT_USER_EMAIL);
					Preferences.Set(Consts.CURRENT_USER_PROFILE_ID, 0);

					Analytics.TrackEvent("Unsuccessfully Refreshed Access Token");
					return false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				Crashes.TrackError(ex);
				return false;
			}
		}

		public async Task<bool> CheckNetworkAndAPIHeartbeat()
		{
			if (IsConnected)
			{
				return await HeartbeatCheck();
			}
			else
			{
				return false;
			}
		}

		public async Task<UserProfilePhoto> GetUserProfileThumbnailAsync(int userProfileId)
		{
			UserProfilePhoto retVal = null;

			try
			{
				retVal = await GetWebAPIDataService(Consts.AUTHORIZED).GetUserProfileThumbnailAsync(userProfileId);
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}

			return retVal;
		}

		public async Task<bool> HeartbeatCheck()
		{
			try
			{
				Debug.WriteLine("Heartbeat Check");
				bool result = await GetWebAPIDataService(Consts.UNAUTHORIZED).IsServiceOnlineAsync();
				return result;
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
				return false;
			}
		}

		public async Task<int> LoadAnnouncementsAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				DateTime? lastUpdatedDate = null;
				if (!forceRefresh)
				{
					if (await _conn.Table<Announcement>().CountAsync() > 0)
					{
						var lastUpdated = await _conn.Table<Announcement>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
						lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
					}
				}
				else
				{
					//truncate the table
					await _conn.Table<Announcement>().Where(x => x.AnnouncementId != 0).DeleteAsync();
				}

				var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesAnnouncementsAsync(lastUpdatedDate);
				int count = 0;
				if (dtos.Any())
				{
					foreach (var r in dtos)
					{
						count += await _conn.InsertOrReplaceAsync(r.ToModelData());
					}
					return count;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadAnnouncementsAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadFeedbackInitiatorTypesAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				DateTime? lastUpdatedDate = null;
				if (!forceRefresh)
				{
					if (await _conn.Table<FeedbackInitiatorType>().CountAsync() > 0)
					{
						var lastUpdated = await _conn.Table<FeedbackInitiatorType>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
						lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
					}
				}
				else
				{
					//truncate the table
					await _conn.Table<FeedbackInitiatorType>().Where(x => x.FeedbackInitiatorTypeId != 0).DeleteAsync();
				}
				var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesFeedbackInitiatorTypesAsync(lastUpdatedDate);
				int count = 0;
				if (dtos.Any())
				{
					foreach (var r in dtos)
					{
						count += await _conn.InsertOrReplaceAsync(r.ToModelData());
					}
					return count;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadFeedbackInitiatorTypesAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadFeedbackTypesAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				DateTime? lastUpdatedDate = null;
				if (!forceRefresh)
				{
					if (await _conn.Table<FeedbackType>().CountAsync() > 0)
					{
						var lastUpdated = await _conn.Table<FeedbackType>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
						lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
					}
				}
				else
				{
					//truncate the table
					await _conn.Table<FeedbackType>().Where(x => x.FeedbackTypeId != 0).DeleteAsync();
				}
				var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesFeedbackTypesAsync(lastUpdatedDate);
				int count = 0;
				if (dtos.Any())
				{
					foreach (var r in dtos)
					{
						count += await _conn.InsertOrReplaceAsync(r.ToModelData());
					}
					return count;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadFeedbackTypesAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadRoomsAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				DateTime? lastUpdatedDate = null;
				if (!forceRefresh)
				{
					if (await _conn.Table<Room>().CountAsync() > 0)
					{
						var lastUpdated = await _conn.Table<Room>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
						lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
					}
				}
				else
				{
					//truncate the table
					await _conn.Table<Room>().Where(x => x.RoomId != 0).DeleteAsync();
				}
				var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesRoomsAsync(lastUpdatedDate);
				int count = 0;
				if (dtos.Any())
				{
					foreach (var r in dtos)
					{
						count += await _conn.InsertOrReplaceAsync(r.ToModelData());
					}
					return count;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadRoomsAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadSessionLikesAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				if (await NeedsDataRefresh(UpdateableTableNames.SessionLike))
				{
					DateTime? lastUpdatedDate = null;
					if (!forceRefresh)
					{
						if (await _conn.Table<SessionLike>().CountAsync() > 0)
						{
							var lastUpdated = await _conn.Table<SessionLike>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
							lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
						}
					}
					else
					{
						//truncate the table
						await _conn.Table<SessionLike>().Where(x => x.SessionIdUserProfileId != "0").DeleteAsync();
					}
					var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesSessionLikesAsync(lastUpdatedDate);
					int count = 0;
					if (dtos.Any())
					{
						foreach (var r in dtos)
						{
							count += await _conn.InsertOrReplaceAsync(r.ToModelData());
						}
						SetLastUpdatedNow(UpdateableTableNames.SessionLike);
						return count;
					}
					else
					{
						return 0;
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadSessionLikesAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadSessionsAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				if (await NeedsDataRefresh(UpdateableTableNames.Session))
				{
					DateTime? lastUpdatedDate = null;
					if (!forceRefresh)
					{
						if (await _conn.Table<Session>().CountAsync() > 0)
						{
							var lastUpdated = await _conn.Table<Session>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
							lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
						}
					}
					else
					{
						//truncate the table
						await _conn.Table<Session>().Where(x => x.SessionId != 0).DeleteAsync();
					}
					var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesSessionsAsync(lastUpdatedDate);
					int count = 0;
					if (dtos.Any())
					{
						foreach (var r in dtos)
						{
							count += await _conn.InsertOrReplaceAsync(r.ToModelData());
						}
						SetLastUpdatedNow(UpdateableTableNames.Session);
						return count;
					}
					else
					{
						return 0;
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadSessionsAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadSessionSpeakersAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				if (await NeedsDataRefresh(UpdateableTableNames.SessionSpeaker))
				{
					DateTime? lastUpdatedDate = null;
					if (!forceRefresh)
					{
						if (await _conn.Table<SessionSpeaker>().CountAsync() > 0)
						{
							var lastUpdated = await _conn.Table<SessionSpeaker>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
							lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
						}
					}
					else
					{
						//truncate the table
						await _conn.Table<SessionSpeaker>().Where(x => x.SessionIdUserProfileId != "0").DeleteAsync();
					}
					var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesSessionSpeakersAsync(lastUpdatedDate);
					int count = 0;
					if (dtos.Any())
					{
						foreach (var r in dtos)
						{
							count += await _conn.InsertOrReplaceAsync(r.ToModelData());
						}
						SetLastUpdatedNow(UpdateableTableNames.SessionSpeaker);
						return count;
					}
					else
					{
						return 0;
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadSessionSpeakersAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<int> LoadUsersAsync(bool forceRefresh = false, bool secondPass = false)
		{
			try
			{
				if (await NeedsDataRefresh(UpdateableTableNames.User))
				{
					DateTime? lastUpdatedDate = null;
					if (!forceRefresh)
					{
						if (await _conn.Table<UserProfile>().CountAsync() > 0)
						{
							var lastUpdated = await _conn.Table<UserProfile>().OrderByDescending(x => x.ModifiedUtcDate).FirstAsync();
							lastUpdatedDate = lastUpdated != null ? lastUpdated?.ModifiedUtcDate : null;
						}
					}
					else
					{
						//truncate the table
						await _conn.Table<UserProfile>().Where(x => x.UserProfileId != 0).DeleteAsync();
					}
					var dtos = await GetWebAPIDataService(Consts.AUTHORIZED).GetAllPagesUserProfilesAsync(lastUpdatedDate);
					int count = 0;
					if (dtos.Any())
					{
						foreach (var r in dtos)
						{
							count += await _conn.InsertOrReplaceAsync(r.ToModelData());
						}
						SetLastUpdatedNow(UpdateableTableNames.User);
						return count;
					}
					else
					{
						return 0;
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				if (secondPass == false && await AuthCheckAndRenewTokenIfNeeded())
				{
					//rerun
					await LoadUsersAsync(forceRefresh, true);
					return 0;
				}
				else
				{
					Crashes.TrackError(ex);
					return 0;
				}
			}
		}

		public async Task<bool> SaveUserProfileImageAsync(UserProfilePhoto userProfilePhoto)
		{
			bool retVal = false;

			try
			{
				retVal = await GetWebAPIDataService(Consts.AUTHORIZED).SaveUserProfileImageAsync(userProfilePhoto);
			}
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}

			return retVal;
		}

		private async Task<bool> NeedsDataRefresh(UpdateableTableNames updateableTableName)
		{
			string tableName = updateableTableName.ToString();
			var record = await _conn.Table<MobileModelData.LastUpdated>().Where(x => x.TableName == tableName).FirstOrDefaultAsync();
			if (record != null)
			{
				return (record.LastUpdatedUTC < DateTime.UtcNow.AddMinutes(MAX_MINUTES_BETWEEN_UPDATES)) ? true : false;
			}
			return true;
		}

		private async Task<bool> SetLastUpdatedNow(UpdateableTableNames updateableTableName)
		{
			return 1 == await _conn.InsertOrReplaceAsync(new MobileModelData.LastUpdated() { TableName = updateableTableName.ToString(), LastUpdatedUTC = DateTime.UtcNow });
		}
	}
}