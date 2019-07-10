using CodeGenHero.Logging;
using CodeGenHero.WebApi;
using MSC.ConferenceMate.Domain;
using MSC.ConferenceMate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using iDom = MSC.ConferenceMate.Domain.Interface;
using cmEnums = MSC.ConferenceMate.Domain.Enums;
using System.Net.Http.Headers;
using MSC.ConferenceMate.API.Models.Interface;

namespace MSC.ConferenceMate.API.Controllers.CM
{
	public partial class UserProfileImagesController : CMBaseApiControllerAuthorized
	{
		private readonly iDom.IUser _domUser = null;
		private readonly ISession _session = null;

		public UserProfileImagesController(ILoggingService log, ICMRepository repository, iDom.IUser domUser, ISession session)
			: base(log, repository)
		{
			_domUser = domUser;
			_session = session;
		}

		[HttpGet]
		[VersionedRoute(template: "UserProfileThumbnail/{userProfileId}", allowedVersion: 1, Name = "GetUserProfileThumbnail")]
		public async Task<HttpResponseMessage> Download(int userProfileId)
		{
			HttpResponseMessage retVal = new HttpResponseMessage(HttpStatusCode.OK);

			if (userProfileId <= 0)
				return Request.CreateResponse(HttpStatusCode.BadRequest);

			var userProfilePhoto = await _domUser.GetUserProfilePhotoAsync(userProfileId, cmEnums.BlobFileType.Thumbnail_Image);
			if (userProfilePhoto != null && userProfilePhoto.Data != null)
			{
				MemoryStream ms = new MemoryStream(userProfilePhoto.Data);
				retVal.Content = new StreamContent(ms);
				retVal.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
				retVal.Content.Headers.ContentDisposition.FileName = userProfilePhoto.BlobFile.Name;
				//retVal.Content.Headers.ContentType = new MediaTypeHeaderValue(userProfilePhoto.BlobFile.DiscreteMimeType);
				retVal.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(userProfilePhoto.BlobFile.Name));
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, $"Unable to retrieve userProfilePhoto using userProfileId: {userProfileId}.");
			}

			return retVal;
		}

		[HttpPost]
		[VersionedRoute(template: "UserProfileImage", allowedVersion: 1, Name = "CMImageUpload")]
		public async Task<HttpResponseMessage> Upload()
		{
			bool isUploaded = false;
			var updateTasks = new List<Task>();

			try
			{
				var httpRequest = HttpContext.Current.Request;
				string userProfileIdString = httpRequest.Form["userProfileId"];
				if (string.IsNullOrEmpty(userProfileIdString) || !int.TryParse(userProfileIdString, out int userProfileId))
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No valid userProfileId received in the upload.");
				}

				if (_session.CurrentUserProfileId == userProfileId
					|| _session.CurrentUserIsConferenceOrganizer)
				{
					if (httpRequest.Files.Count > 0)
					{
						var postedFile = httpRequest.Files[0];
						if (IsImage(postedFile.FileName))
						{
							if (postedFile.ContentLength > 0)
							{
								var claimsIdentity = RequestContext.Principal.Identity as ClaimsIdentity;
								var createdByOrModifiedByUser = claimsIdentity.Claims.FirstOrDefault(x => x.Type == Consts.CLAIM_USERPROFILEID).Value;

								MemoryStream ms = new MemoryStream();
								postedFile.InputStream.CopyTo(ms);
								postedFile.InputStream.Position = ms.Position = 0;

								try
								{
									isUploaded = await _domUser.SetUserProfilePhotoAsync(userProfileId, postedFile.FileName, postedFile.ContentLength, createdByOrModifiedByUser, ms);
								}
								catch (Exception ex)
								{
									Log.Error(ex.Message, LogMessageType.Instance.Exception_WebApi, ex);
									return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
								}
							}
						}
						else
						{
							return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This service only supports image files with an extension of '.jpg', '.png', '.gif', or '.jpeg'.");
						}

						await Task.WhenAll(updateTasks.ToArray()); // Allow all our async operations to complete.
						return Request.CreateResponse(HttpStatusCode.Created, postedFile.FileName);
					}
					else
					{
						return Request.CreateResponse(HttpStatusCode.BadRequest, "No files received in the upload.");
					}
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Only conference organizers may alter a profile picture for users other than themselves.");
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, LogMessageType.Instance.Exception_WebApiClient, ex);
				return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
			}
		}

		private bool IsImage(string fileName)
		{
			string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
			return formats.Any(x => fileName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
		}
	}
}