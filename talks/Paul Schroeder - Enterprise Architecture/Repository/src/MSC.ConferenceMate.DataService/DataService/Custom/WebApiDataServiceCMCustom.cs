using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using CodeGenHero.DataService;
using MSC.ConferenceMate.API.Client.Interface;
using MSC.ConferenceMate.DataService.Models;
using System.Linq;
using System.Net.Mime;
using Newtonsoft.Json;

namespace MSC.ConferenceMate.API.Client
{
	public partial class WebApiDataServiceCM : WebApiDataServiceBase, IWebApiDataServiceCM
	{
		public async Task<UserProfilePhoto> GetUserProfileThumbnailAsync(int userProfileId)
		{
			UserProfilePhoto retVal = new UserProfilePhoto() { UserProfileId = userProfileId };
			using (HttpResponseMessage response = await HttpClient.GetAsync($"CM/UserProfileThumbnail/{userProfileId}", HttpCompletionOption.ResponseHeadersRead))
			{
				if (response.IsSuccessStatusCode)
				{
					retVal.Data = await response.Content.ReadAsByteArrayAsync();
					string contentDispositionString = response.Content.Headers.GetValues("Content-Disposition").FirstOrDefault();
					ContentDisposition contentDisposition = new ContentDisposition(contentDispositionString);
					string filename = contentDisposition.FileName;
					retVal.FileName = filename;
				}
			}

			return retVal;
		}

		public async Task<bool> SaveUserProfileImageAsync(UserProfilePhoto userProfilePhoto)
		{
			if (userProfilePhoto == null || userProfilePhoto.UserProfileId <= 0)
				return false;

			bool retVal = false;
			MultipartFormDataContent content = new MultipartFormDataContent();
			ByteArrayContent baContent = new ByteArrayContent(userProfilePhoto.Data);
			content.Add(baContent, "File", userProfilePhoto.FileName);

			StringContent userProfileIdContent = new StringContent(userProfilePhoto.UserProfileId.ToString());
			content.Add(userProfileIdContent, "userProfileId");

			// Upload MultipartFormDataContent content
			using (var response = await HttpClient.PostAsync($"CM/UserProfileImage", content))
			{
				if (response.IsSuccessStatusCode)
				{
					retVal = true;
				}
			}

			return retVal;
		}
	}
}