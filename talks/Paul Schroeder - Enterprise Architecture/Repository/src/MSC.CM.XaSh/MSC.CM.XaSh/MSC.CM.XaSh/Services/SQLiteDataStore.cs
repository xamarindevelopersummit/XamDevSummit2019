using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using MSC.CM.Xam;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using dataModel = MSC.CM.Xam.ModelData.CM;
using objModel = MSC.CM.Xam.ModelObj.CM;

namespace MSC.CM.XaSh.Services
{
    public class SQLiteDataStore : IDataStore
    {
        private SQLiteAsyncConnection conn = App.Database.conn;
        private ILogger<SQLiteDataStore> logger;

        //TODO: PAUL to take advantage of this goodness, wire up the CGH httpclient to use the transient http error policy
        //private HttpClient client;
        //public AzureDataStore(ILogger<AzureDataStore> _logger = null, IHttpClientFactory _httpClientFactory = null)
        public SQLiteDataStore(ILogger<SQLiteDataStore> _logger = null)
        {
            logger = _logger;
            //client = _httpClientFactory == null ? new HttpClient() : _httpClientFactory.CreateClient("AzureWebsites");

            //if (_httpClientFactory == null)
            //    client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
        }

        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<IEnumerable<objModel.Announcement>> GetAnnouncementsAsync()
        {
            var returnMe = new List<objModel.Announcement>();
            var dataResults = await conn.Table<dataModel.Announcement>()
                .OrderBy(x => x.ModifiedUtcDate).ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    returnMe.Add(d.ToModelObj());
                }
            }
            return returnMe;
        }

        public async Task<IEnumerable<objModel.Session>> GetFavoriteSessionsAsync()
        {
            //includes session and user data
            var returnMe = new List<objModel.Session>();
            var dataResults = await conn.Table<dataModel.SessionLike>().Where(x => x.IsDeleted == false).ToListAsync();
            int currentUserProfileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    //var sessionObjMod = d.ToModelObj();
                    var sessionDataMod = await conn.Table<dataModel.Session>().Where(x => x.SessionId == d.SessionId && d.UserProfileId == currentUserProfileId).FirstOrDefaultAsync();
                    if (sessionDataMod != null)
                    {
                        var sessionObjMod = sessionDataMod.ToModelObj();
                        var sessionSpeakers = await conn.Table<dataModel.SessionSpeaker>()
                            .Where(x => x.SessionId == d.SessionId).ToListAsync();
                        foreach (var s in sessionSpeakers)
                        {
                            var speaker = s.ToModelObj();
                            var user = await conn.Table<dataModel.UserProfile>()
                                .Where(x => x.UserProfileId == s.UserProfileId).FirstOrDefaultAsync();
                            speaker.UserProfile = user != null ? user.ToModelObj() : null;

                            sessionObjMod.SessionSpeakers.Add(speaker);
                        }
                        var sessionLikes = await conn.Table<dataModel.SessionLike>()
                        .Where(x => x.SessionId == d.SessionId && x.UserProfileId == currentUserProfileId && x.IsDeleted == false).ToListAsync();
                        foreach (var s in sessionLikes)
                        {
                            sessionObjMod.SessionLikes.Add(s.ToModelObj());
                        }
                        returnMe.Add(sessionObjMod);
                    }
                }
            }
            return returnMe;
        }

        public async Task<IEnumerable<objModel.FeedbackType>> GetFeedbackTypesAsync()
        {
            //includes session and user data
            var returnMe = new List<objModel.FeedbackType>();
            var dataResults = await conn.Table<dataModel.FeedbackType>().ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    returnMe.Add(d.ToModelObj());
                }
            }
            return returnMe;
        }

        public async Task<objModel.Room> GetRoomById(int roomId)
        {
            var roomObj = await conn.Table<dataModel.Room>().Where(x => x.RoomId == roomId).FirstOrDefaultAsync();
            return roomObj != null ? roomObj.ToModelObj() : null;
        }

        public async Task<IEnumerable<objModel.Session>> GetSessionsAsync()
        {
            //includes session, speaker and favorite data
            var returnMe = new List<objModel.Session>();
            var dataResults = await conn.Table<dataModel.Session>().ToListAsync();
            int currentUserProfileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);
            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    var sessionObjMod = d.ToModelObj();
                    var sessionSpeakers = await conn.Table<dataModel.SessionSpeaker>()
                        .Where(x => x.SessionId == d.SessionId).ToListAsync();
                    foreach (var s in sessionSpeakers)
                    {
                        var speaker = s.ToModelObj();
                        var user = await conn.Table<dataModel.UserProfile>()
                            .Where(x => x.UserProfileId == s.UserProfileId).FirstOrDefaultAsync();
                        speaker.UserProfile = user != null ? user.ToModelObj() : null;

                        sessionObjMod.SessionSpeakers.Add(speaker);
                    }
                    var sessionLikes = await conn.Table<dataModel.SessionLike>()
                        .Where(x => x.SessionId == d.SessionId && x.UserProfileId == currentUserProfileId && x.IsDeleted == false).ToListAsync();
                    foreach (var s in sessionLikes)
                    {
                        sessionObjMod.SessionLikes.Add(s.ToModelObj());
                    }
                    returnMe.Add(sessionObjMod);
                }
            }
            return returnMe;
        }

        public async Task<IEnumerable<objModel.Session>> GetSessionsWithRoomsAsync()
        {
            //includes session and room data
            var returnMe = new List<objModel.Session>();
            var dataResults = await conn.Table<dataModel.Session>().ToListAsync();
            int currentUserProfileId = Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0);
            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    var sessionObjMod = d.ToModelObj();
                    var sessionRoom = await conn.Table<dataModel.Room>()
                        .Where(x => x.RoomId == d.RoomId).FirstOrDefaultAsync();
                    sessionObjMod.Room = sessionRoom.ToModelObj();
                    var sessionLikes = await conn.Table<dataModel.SessionLike>()
                       .Where(x => x.SessionId == d.SessionId && x.UserProfileId == currentUserProfileId && x.IsDeleted == false).ToListAsync();
                    foreach (var s in sessionLikes)
                    {
                        sessionObjMod.SessionLikes.Add(s.ToModelObj());
                    }
                    returnMe.Add(sessionObjMod);
                }
            }
            return returnMe;
        }

        public async Task<IEnumerable<objModel.UserProfile>> GetSpeakersAsync()
        {
            //includes session and user data
            var returnMe = new List<objModel.UserProfile>();

            //TODO: fix this
            //var dataResults = await conn.Table<dataModel.SessionSpeaker>().ToListAsync();
            //IEnumerable<int> dataResults = await conn.QueryAsync<int>("select distinct UserId from SessionSpeaker");
            //IEnumerable<int> dataResults = await conn.QueryAsync<int>("select UserId from SessionSpeaker");
            List<int> userProfileIds = (await conn.Table<dataModel.SessionSpeaker>().ToListAsync()).Select(x => x.UserProfileId).Distinct().ToList();

            if (userProfileIds.Any())
            {
                foreach (var userProfileId in userProfileIds)
                {
                    var user = await conn.Table<dataModel.UserProfile>()
                       .Where(x => x.UserProfileId == userProfileId).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        returnMe.Add(user.ToModelObj());
                    }
                }
            }
            return returnMe;
        }

        public async Task<objModel.UserProfile> GetUserByAspNetUsersIdAsync(string userId)
        {
            var dataResult = await conn.Table<dataModel.UserProfile>().Where(x => x.AspNetUsersId == userId).FirstOrDefaultAsync();
            return (dataResult != null) ? dataResult.ToModelObj() : null;
        }

        public async Task<objModel.UserProfile> GetUserByUserProfileIdAsync(int userId)
        {
            var dataResult = await conn.Table<dataModel.UserProfile>().Where(x => x.UserProfileId == userId).FirstOrDefaultAsync();
            return (dataResult != null) ? dataResult.ToModelObj() : null;
        }

        public async Task<bool> ToggleSessionLikeAsync(int sessionId, string sessionIdUserProfileId)
        {
            var dataResult = await conn.Table<dataModel.SessionLike>().Where(x => x.SessionIdUserProfileId == sessionIdUserProfileId).FirstOrDefaultAsync();
            if (dataResult != null)
            {   // Data record was previously created.
                dataResult.IsDeleted = !dataResult.IsDeleted;
                dataResult.ModifiedUtcDate = DateTime.UtcNow;

                return (1 == await conn.InsertOrReplaceAsync(dataResult));
            }
            else
            {   // Create new data record.
                if (Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0) != 0)
                {
                    var user = await GetUserByUserProfileIdAsync(Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0));
                    if (user != null)
                    {
                        dataResult = new dataModel.SessionLike()
                        {
                            CreatedBy = user.UserProfileId.ToString(),
                            CreatedUtcDate = DateTime.UtcNow,
                            DataVersion = 1,
                            IsDeleted = false,
                            ModifiedBy = user.UserProfileId.ToString(),
                            ModifiedUtcDate = DateTime.UtcNow,
                            UserProfileId = user.UserProfileId,
                            SessionId = sessionId,
                            SessionIdUserProfileId = sessionIdUserProfileId //$"{sessionId}{user.UserProfileId.ToString()}"
                        };
                        return (1 == await conn.InsertAsync(dataResult));
                    }
                    else
                    {
                        Debug.WriteLine("Can't find user in SQLite");
                        Analytics.TrackEvent($"SetSessionLikeAsync - Can't find user in SQLite - id: {Preferences.Get(Consts.CURRENT_USER_PROFILE_ID, 0)}");
                    }
                }
                else
                {
                    Debug.WriteLine("No User Logged In");
                    Analytics.TrackEvent($"SetSessionLikeAsync - No user is logged in");
                }
            }
            return false;
        }

        public async Task<int> UpdateUserRecord(dataModel.UserProfile editedCurrentUser)
        {
            return await conn.UpdateAsync(editedCurrentUser);
        }

        public async Task<int> WriteFeedbackRecord(dataModel.Feedback feedbackData)
        {
            return await conn.InsertAsync(feedbackData);
        }
    }
}