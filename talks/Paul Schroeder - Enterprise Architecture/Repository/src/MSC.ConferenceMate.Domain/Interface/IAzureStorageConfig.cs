namespace MSC.ConferenceMate.Domain.Interface
{
	public interface IAzureStorageConfig
	{
		string AccountKey { get; set; }
		string AccountName { get; set; }
		string ImageContainer { get; set; }
		string QueueName { get; set; }
		string ThumbnailContainer { get; set; }
	}
}