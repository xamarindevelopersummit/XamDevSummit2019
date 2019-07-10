using MSC.ConferenceMate.Domain.Interface;

namespace MSC.ConferenceMate.Domain
{
	public class AzureStorageConfig : IAzureStorageConfig
	{
		private string _accountName;
		private string _imageContainer;
		private string _queueName;
		private string _thumbnailContainer;
		public string AccountKey { get; set; }

		public string AccountName
		{
			get { return _accountName; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_accountName = null;
				}
				else
				{   // Limitation: Azure Storage names must be all lowercase. ie: "newqueueitem"
					_accountName = value.ToLowerInvariant();
				}
			}
		}

		public string ImageContainer
		{
			get { return _imageContainer; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_imageContainer = null;
				}
				else
				{   // Limitation: Azure Storage names must be all lowercase. ie: "newqueueitem"
					_imageContainer = value.ToLowerInvariant();
				}
			}
		}

		public string QueueName
		{
			get { return _queueName; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_queueName = null;
				}
				else
				{   // Limitation: Azure Storage names must be all lowercase. ie: "newqueueitem"
					_queueName = value.ToLowerInvariant();
				}
			}
		}

		public string ThumbnailContainer
		{
			get { return _thumbnailContainer; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_thumbnailContainer = null;
				}
				else
				{   // Limitation: Azure Storage names must be all lowercase. ie: "newqueueitem"
					_thumbnailContainer = value.ToLowerInvariant();
				}
			}
		}
	}
}