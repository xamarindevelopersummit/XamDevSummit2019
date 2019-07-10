using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using MSC.CM.Xam.ModelData.CM;
using SQLite;

namespace MSC.CM.XaSh.Database
{
    public class ConferenceMateDatabase
    {
        public readonly SQLiteAsyncConnection conn;

        public ConferenceMateDatabase(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            if (conn != null)
            {
                CreateTables();
            }
        }

        public async Task<bool> DropCreateTables()
        {
            if (await DropTables())
            {
                CreateTables();
                return true;
            }
            return false;
        }

        private void CreateTables()
        {
            try
            {
                conn.CreateTableAsync<Announcement>().Wait();
                conn.CreateTableAsync<FeaturedEvent>().Wait();
                conn.CreateTableAsync<Feedback>().Wait();
                conn.CreateTableAsync<FeedbackInitiatorType>().Wait();
                conn.CreateTableAsync<FeedbackType>().Wait();
                conn.CreateTableAsync<GenderType>().Wait();
                conn.CreateTableAsync<LanguageType>().Wait();
                conn.CreateTableAsync<LookupList>().Wait();
                conn.CreateTableAsync<Room>().Wait();
                conn.CreateTableAsync<Session>().Wait();
                conn.CreateTableAsync<SessionCategoryType>().Wait();
                conn.CreateTableAsync<SessionLike>().Wait();
                conn.CreateTableAsync<SessionSessionCategoryType>().Wait();
                conn.CreateTableAsync<SessionSpeaker>().Wait();
                conn.CreateTableAsync<Sponsor>().Wait();
                conn.CreateTableAsync<SponsorFeaturedEvent>().Wait();
                conn.CreateTableAsync<SponsorType>().Wait();
                conn.CreateTableAsync<UserProfile>().Wait();

                conn.CreateTableAsync<MobileModelData.UploadQueue>().Wait();
                conn.CreateTableAsync<MobileModelData.LastUpdated>().Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Crashes.TrackError(ex);
            }
        }

        private async Task<bool> DropTables()
        {
            try
            {
                await conn.DropTableAsync<Announcement>();
                await conn.DropTableAsync<FeaturedEvent>();
                await conn.DropTableAsync<Feedback>();
                await conn.DropTableAsync<FeedbackInitiatorType>();
                await conn.DropTableAsync<FeedbackType>();
                await conn.DropTableAsync<GenderType>();
                await conn.DropTableAsync<LanguageType>();
                await conn.DropTableAsync<LookupList>();
                await conn.DropTableAsync<Room>();
                await conn.DropTableAsync<Session>();
                await conn.DropTableAsync<SessionCategoryType>();
                await conn.DropTableAsync<SessionLike>();
                await conn.DropTableAsync<SessionSessionCategoryType>();
                await conn.DropTableAsync<SessionSpeaker>();
                await conn.DropTableAsync<Sponsor>();
                await conn.DropTableAsync<SponsorFeaturedEvent>();
                await conn.DropTableAsync<SponsorType>();
                await conn.DropTableAsync<UserProfile>();

                await conn.DropTableAsync<MobileModelData.UploadQueue>();
                await conn.DropTableAsync<MobileModelData.LastUpdated>();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Crashes.TrackError(ex);
                return false;
            }
        }
    }
}