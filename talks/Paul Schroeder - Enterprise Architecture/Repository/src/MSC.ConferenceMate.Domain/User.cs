using MSC.ConferenceMate.Domain.Interface;
using dtoCM = MSC.ConferenceMate.DTO.CM;
using entCM = MSC.ConferenceMate.Repository.Entities.CM;
using MSC.ConferenceMate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using cmEnums = MSC.ConferenceMate.Domain.Enums;
using MSC.ConferenceMate.Domain.Utils;
using System.Drawing;
using System.Linq;
using CodeGenHero.Logging;
using Microsoft.WindowsAzure.Storage;

namespace MSC.ConferenceMate.Domain
{
	public class User : DomainBase, IUser
	{
		public User(ILoggingService log, ICMRepository repository, IAzureStorageManager azureStorageManager)
			: base(log, repository)
		{
			AzureStorageManager = azureStorageManager;
		}

		private User() : base(null, null)
		{   // Prevent creation via parameterless constructor.
		}

		private IAzureStorageManager AzureStorageManager { get; set; }

		public async Task<byte[]> GetBlobBytesByBlobFileIdAsync(Guid blobFileId, cmEnums.BlobFileType blobFileType)
		{
			byte[] retVal = null;

			try
			{
				if (blobFileId == Guid.Empty)
					return retVal;

				var blobFile = Repo.GetQueryable_BlobFile().Where(x => x.BlobFileId == blobFileId && x.BlobFileTypeId == (int)blobFileType).FirstOrDefault();

				if (blobFile != null)
				{
					retVal = await AzureStorageManager.GetBlobBytesByPrimaryUriAsync(new Uri(blobFile.BlobUri));
				}
			}
			catch (StorageException sex)
			{
				Log.Error($"Unable to retrieve blobFileId: {blobFileId.ToString()} in {nameof(GetBlobBytesByBlobFileIdAsync)}.", LogMessageType.Instance.Exception_Domain, sex);
			}

			return retVal;
		}

		public async Task<entCM.UserProfile> GetUserProfileAsync(int userProfileId)
		{
			entCM.UserProfile retVal = null;

			try
			{
				var userProfile = await Repo.Get_UserProfileAsync(userProfileId, 1);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in {nameof(GetUserProfileAsync)} {ex.Message}", LogMessageType.Instance.Exception_Domain, ex, userName: userProfileId.ToString());
			}

			return retVal;
		}

		public async Task<IUserProfilePhoto> GetUserProfilePhotoAsync(int userProfileId, cmEnums.BlobFileType blobFileType)
		{
			IUserProfilePhoto retVal = null;

			try
			{
				var userProfile = await Repo.Get_UserProfileAsync(userProfileId, 1);

				if (userProfile == null)
					throw new InvalidOperationException($"Unable to find user profile in {nameof(GetUserProfilePhotoAsync)}() using userProfileId: {userProfileId}");

				if (!userProfile.PhotoBlobFileId.HasValue)
					return retVal;

				var blobFiles = Repo.GetQueryable_BlobFile().Where(x => x.BlobFileId == userProfile.PhotoBlobFileId.Value || x.ParentBlobFileId == userProfile.PhotoBlobFileId.Value)
					.ToList().OrderBy(x => x.SizeInBytes);

				if (blobFiles.Count() > 0)
				{
					entCM.BlobFile bestmatchBlobFile = blobFiles.FirstOrDefault();
					if (blobFileType == cmEnums.BlobFileType.Thumbnail_Image)
					{
						bestmatchBlobFile = blobFiles.FirstOrDefault(x => x.BlobFileTypeId == (int)cmEnums.BlobFileType.Thumbnail_Image);
					}
					else
					{
						bestmatchBlobFile = blobFiles.FirstOrDefault(x => x.BlobFileTypeId == (int)cmEnums.BlobFileType.Original_Image);
					}

					if (bestmatchBlobFile != null && !string.IsNullOrEmpty(bestmatchBlobFile.BlobUri))
					{
						retVal = new UserProfilePhoto()
						{
							BlobFile = bestmatchBlobFile
						};

						retVal.Data = await AzureStorageManager.GetBlobBytesByPrimaryUriAsync(new Uri(bestmatchBlobFile.BlobUri));
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, LogMessageType.Instance.Exception_WebApi, ex, userName: userProfileId.ToString());
			}

			return retVal;
		}

		public async Task<bool> SetUserProfilePhotoAsync(int userProfileId, string fileName, long? sizeInBytes, string createdByOrModifiedByUser, Stream msFullsizeImage)
		{
			bool retVal = false;
			try
			{
				var userProfile = await Repo.Get_UserProfileAsync(userProfileId, 1);
				var runningTasks = new List<Task>();

				if (userProfile == null)
					throw new InvalidOperationException($"Unable to find user profile in {nameof(SetUserProfilePhotoAsync)}() using userProfileId: {userProfileId}");

				var currentUtcDateTime = DateTime.UtcNow;
				// See if user has an existing photo blob that should be deleted.
				await DeleteExistingUserPhotoDataAsync(createdByOrModifiedByUser, runningTasks, currentUtcDateTime, userProfile.BlobFile);

				Guid newBlobFileId = Guid.NewGuid(); // Add full-size user profile image to Azure.
				var userProfileImage = Image.FromStream(msFullsizeImage);
				msFullsizeImage.Position = 0; // Reset stream position - ahead of the upload to avoid an AzureStorageException - "The requested number of bytes exceeds the length of the stream remaining from the specified position."

				Task<string> fullsizeImageUploadTask = AzureStorageManager.UploadFileToStorageAsync(fileStream: msFullsizeImage,
					blobName: newBlobFileId.ToString(), blobFileType: cmEnums.BlobFileType.Original_Image);
				runningTasks.Add(fullsizeImageUploadTask);

				// Add thumbnail user profile image to Azure.
				var thumbNailImage = ImageHelper.ResizeImage(userProfileImage, 150, 150);
				var msThumbNail = new MemoryStream();
				thumbNailImage.Save(msThumbNail, System.Drawing.Imaging.ImageFormat.Png);
				msThumbNail.Position = 0; // Reset stream position.
				Task<string> thumbnailImageUploadTask = AzureStorageManager.UploadFileToStorageAsync(fileStream: msThumbNail, blobName: newBlobFileId.ToString(),
					blobFileType: cmEnums.BlobFileType.Thumbnail_Image);
				runningTasks.Add(thumbnailImageUploadTask);

				var fullsizeImagePrimaryUri = await fullsizeImageUploadTask; // Create a new BlobFile DB record for the full-size image.
				entCM.BlobFile userProfilePhotoBlobFile = new entCM.BlobFile()
				{
					BlobFileId = newBlobFileId,
					BlobFileTypeId = (int)cmEnums.BlobFileType.Original_Image,
					BlobUri = fullsizeImagePrimaryUri,
					CreatedBy = createdByOrModifiedByUser,
					CreatedUtcDate = currentUtcDateTime,
					DiscreteMimeType = GetDiscreteMimeType(fileName),
					ModifiedBy = createdByOrModifiedByUser,
					ModifiedUtcDate = currentUtcDateTime,
					Name = fileName,
					RequiresResize = true,
					ResizeComplete = true,
					SizeInBytes = msFullsizeImage.Length,
				};
				await Repo.InsertAsync(userProfilePhotoBlobFile); // Note, EF only allows one execution per context at a time so we must await here instead of using runningTasks.Add();

				FileInfo fileInfo = new FileInfo(fileName);
				string thumbnailFileName = fileInfo.Name.Replace(fileInfo.Extension, ".png");
				var thumbnailImagePrimaryUri = await thumbnailImageUploadTask; // Create a new BlobFile DB record for the thumbnail image.
				entCM.BlobFile thumbnailBlobFile = new entCM.BlobFile()
				{
					BlobFileId = Guid.NewGuid(),
					BlobFileTypeId = (int)cmEnums.BlobFileType.Thumbnail_Image,
					BlobUri = thumbnailImagePrimaryUri,
					CreatedBy = createdByOrModifiedByUser,
					CreatedUtcDate = currentUtcDateTime,
					DiscreteMimeType = GetDiscreteMimeType(".Png"),
					ModifiedBy = createdByOrModifiedByUser,
					ModifiedUtcDate = currentUtcDateTime,
					Name = thumbnailFileName,
					ParentBlobFileId = userProfilePhotoBlobFile.BlobFileId,
					RequiresResize = false,
					ResizeComplete = false,
					SizeInBytes = msThumbNail.Length,
				};
				await Repo.InsertAsync(thumbnailBlobFile); // Note, EF only allows one execution per context at a time so we must await here instead of using runningTasks.Add();

				// Update our userProfile
				userProfile.PhotoBlobFileId = userProfilePhotoBlobFile.BlobFileId;
				userProfile.ModifiedBy = createdByOrModifiedByUser;
				userProfile.ModifiedUtcDate = currentUtcDateTime;
				await Repo.UpdateAsync(userProfile); // Note, EF only allows one execution per context at a time so we must await here instead of using runningTasks.Add();

				await Task.WhenAll(runningTasks);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, LogMessageType.Instance.Exception_WebApi, ex, userName: userProfileId.ToString());
				retVal = false;
			}

			return retVal;
		}

		private async Task<bool> DeleteExistingUserPhotoDataAsync(string createdByOrModifiedByUser, List<Task> runningTasks, DateTime currentUtcDateTime, entCM.BlobFile blobFile)
		{
			if (blobFile != null && !string.IsNullOrEmpty(blobFile.BlobUri))
			{   // Recursively cascade the delete to children of this BlobFile.
				var childBlobFiles = Repo.GetQueryable_BlobFile().Where(x => x.ParentBlobFileId == blobFile.BlobFileId).ToList();
				foreach (var childBlobFile in childBlobFiles)
				{
					await DeleteExistingUserPhotoDataAsync(createdByOrModifiedByUser, runningTasks, currentUtcDateTime, childBlobFile);
				}

				// Handle the original file that got loaded here.
				var blobUri = new Uri(blobFile.BlobUri);
				runningTasks.Add(AzureStorageManager.DeleteFileFromStorageAsync(blobUri));

				// Update the DB record associated with userProfile.BlobFile as deleted.
				blobFile.IsDeleted = true;
				blobFile.ModifiedBy = createdByOrModifiedByUser;
				blobFile.ModifiedUtcDate = currentUtcDateTime;
				await Repo.UpdateAsync(blobFile); // Note, EF only allows one execution per context at a time so we must await here instead of using runningTasks.Add();
			}

			return await Task.FromResult(true);
		}

		private string GetDiscreteMimeType(string fileName)
		{
			string retVal = null;

			if (string.IsNullOrEmpty(fileName))
				return null;

			fileName = fileName.ToLowerInvariant();
			if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg"))
			{
				retVal = "image/jpeg";
			}
			else if (fileName.EndsWith(".png"))
			{
				retVal = "image/png";
			}
			else if (fileName.EndsWith(".gif"))
			{
				retVal = "image/gif";
			}
			else if (fileName.EndsWith(".pdf"))
			{
				retVal = "application/pdf";
			}

			return retVal;
		}
	}
}