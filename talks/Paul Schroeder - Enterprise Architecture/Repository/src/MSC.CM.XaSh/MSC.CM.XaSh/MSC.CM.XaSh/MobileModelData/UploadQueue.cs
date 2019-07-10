using SQLite;
using System;

namespace MSC.CM.XaSh.MobileModelData
{
    [Table("UploadQueue")]
    public class UploadQueue
    {
        public DateTime DateQueued { get; set; }

        public int NumAttempts { get; set; }

        public string QueueableObject { get; set; }

        public Guid? RecordIdGuid { get; set; }

        public int? RecordIdInt { get; set; }

        public string RecordIdStr { get; set; }

        public bool Success { get; set; }

        [PrimaryKey]
        public Guid UploadQueueId { get; set; }
    }
}