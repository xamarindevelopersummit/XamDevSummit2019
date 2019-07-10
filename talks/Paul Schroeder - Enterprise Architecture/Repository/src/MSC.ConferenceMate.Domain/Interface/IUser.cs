using System;
using System.IO;
using System.Threading.Tasks;
using cmEnums = MSC.ConferenceMate.Domain.Enums;
using entCM = MSC.ConferenceMate.Repository.Entities.CM;

namespace MSC.ConferenceMate.Domain.Interface
{
	public interface IUser
	{
		Task<byte[]> GetBlobBytesByBlobFileIdAsync(Guid blobFileId, cmEnums.BlobFileType blobFileType);

		Task<entCM.UserProfile> GetUserProfileAsync(int userProfileId);

		Task<IUserProfilePhoto> GetUserProfilePhotoAsync(int userProfileId, cmEnums.BlobFileType blobFileType);

		Task<bool> SetUserProfilePhotoAsync(int userProfileId, string fileName, long? SizeInBytes, string createdByOrModifiedByUser, Stream ms);
	}
}