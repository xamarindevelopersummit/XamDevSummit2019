using MSC.ConferenceMate.Repository.Entities.CM;

namespace MSC.ConferenceMate.Domain.Interface
{
	public interface IUserProfilePhoto
	{
		BlobFile BlobFile { get; set; }
		byte[] Data { get; set; }
	}
}