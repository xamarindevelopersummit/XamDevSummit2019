using MSC.ConferenceMate.Domain.Interface;
using entCM = MSC.ConferenceMate.Repository.Entities.CM;

namespace MSC.ConferenceMate.Domain
{
	public class UserProfilePhoto : IUserProfilePhoto
	{
		public entCM.BlobFile BlobFile { get; set; }
		public byte[] Data { get; set; }
	}
}