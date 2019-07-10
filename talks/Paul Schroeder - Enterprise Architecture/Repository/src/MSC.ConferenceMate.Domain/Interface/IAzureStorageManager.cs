using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MSC.ConferenceMate.Domain.Interface
{
	public interface IAzureStorageManager
	{
		Task<bool> DeleteFileFromStorageAsync(string blobName, Enums.BlobFileType containerType);

		Task<bool> DeleteFileFromStorageAsync(Uri blobUri);

		Task<byte[]> GetBlobBytesByPrimaryUriAsync(Uri blobUri);

		Task<List<string>> GetThumbNailUrlsAsync();

		Task<string> UploadFileToStorageAsync(Stream fileStream, string blobName, Enums.BlobFileType blobFileType);
	}
}