// <auto-generated> - Template:MvvmLightModelObject, Version:1.1, Id:c644a31c-7ebc-4383-bc7f-0ea7c5bf6ed4
using GalaSoft.MvvmLight;

namespace MSC.CM.Xam.ModelObj.CM
{
	public partial class BlobFile : ObservableObject
	{
		public BlobFile()
		{
			UserProfiles = new System.Collections.Generic.List<UserProfile>(); // Reverse Navigation

			InitializePartial();
		}

		private System.Guid _blobFileId;
		private int? _blobFileTypeId;
		private string _blobUri;
		private byte[] _content;
		private string _createdBy;
		private System.DateTime _createdUtcDate;
		private int _dataVersion;
		private string _discreteMimeType;
		private bool _isDeleted;
		private string _modifiedBy;
		private System.DateTime _modifiedUtcDate;
		private string _name;
		private System.Guid? _parentBlobFileId;
		private bool? _requiresResize;
		private bool? _resizeComplete;
		private long? _sizeInBytes;


		public System.Guid BlobFileId
		{
			get { return _blobFileId; }
			set
			{
				Set<System.Guid>(() => BlobFileId, ref _blobFileId, value);
				RunCustomLogicSetBlobFileId(value);
			}
		}

		public int? BlobFileTypeId
		{
			get { return _blobFileTypeId; }
			set
			{
				Set<int?>(() => BlobFileTypeId, ref _blobFileTypeId, value);
				RunCustomLogicSetBlobFileTypeId(value);
			}
		}

		public string BlobUri
		{
			get { return _blobUri; }
			set
			{
				Set<string>(() => BlobUri, ref _blobUri, value);
				RunCustomLogicSetBlobUri(value);
			}
		}

		public byte[] Content
		{
			get { return _content; }
			set
			{
				Set<byte[]>(() => Content, ref _content, value);
				RunCustomLogicSetContent(value);
			}
		}

		public string CreatedBy
		{
			get { return _createdBy; }
			set
			{
				Set<string>(() => CreatedBy, ref _createdBy, value);
				RunCustomLogicSetCreatedBy(value);
			}
		}

		public System.DateTime CreatedUtcDate
		{
			get { return _createdUtcDate; }
			set
			{
				Set<System.DateTime>(() => CreatedUtcDate, ref _createdUtcDate, value);
				RunCustomLogicSetCreatedUtcDate(value);
			}
		}

		public int DataVersion
		{
			get { return _dataVersion; }
			set
			{
				Set<int>(() => DataVersion, ref _dataVersion, value);
				RunCustomLogicSetDataVersion(value);
			}
		}

		public string DiscreteMimeType
		{
			get { return _discreteMimeType; }
			set
			{
				Set<string>(() => DiscreteMimeType, ref _discreteMimeType, value);
				RunCustomLogicSetDiscreteMimeType(value);
			}
		}

		public bool IsDeleted
		{
			get { return _isDeleted; }
			set
			{
				Set<bool>(() => IsDeleted, ref _isDeleted, value);
				RunCustomLogicSetIsDeleted(value);
			}
		}

		public string ModifiedBy
		{
			get { return _modifiedBy; }
			set
			{
				Set<string>(() => ModifiedBy, ref _modifiedBy, value);
				RunCustomLogicSetModifiedBy(value);
			}
		}

		public System.DateTime ModifiedUtcDate
		{
			get { return _modifiedUtcDate; }
			set
			{
				Set<System.DateTime>(() => ModifiedUtcDate, ref _modifiedUtcDate, value);
				RunCustomLogicSetModifiedUtcDate(value);
			}
		}

		public string Name
		{
			get { return _name; }
			set
			{
				Set<string>(() => Name, ref _name, value);
				RunCustomLogicSetName(value);
			}
		}

		public System.Guid? ParentBlobFileId
		{
			get { return _parentBlobFileId; }
			set
			{
				Set<System.Guid?>(() => ParentBlobFileId, ref _parentBlobFileId, value);
				RunCustomLogicSetParentBlobFileId(value);
			}
		}

		public bool? RequiresResize
		{
			get { return _requiresResize; }
			set
			{
				Set<bool?>(() => RequiresResize, ref _requiresResize, value);
				RunCustomLogicSetRequiresResize(value);
			}
		}

		public bool? ResizeComplete
		{
			get { return _resizeComplete; }
			set
			{
				Set<bool?>(() => ResizeComplete, ref _resizeComplete, value);
				RunCustomLogicSetResizeComplete(value);
			}
		}

		public long? SizeInBytes
		{
			get { return _sizeInBytes; }
			set
			{
				Set<long?>(() => SizeInBytes, ref _sizeInBytes, value);
				RunCustomLogicSetSizeInBytes(value);
			}
		}

		public virtual System.Collections.Generic.IList<UserProfile> UserProfiles { get; set; } // Many to many mapping
		public virtual BlobFileType BlobFileType { get; set; } 


		partial void InitializePartial();

		#region RunCustomLogicSet

		partial void RunCustomLogicSetBlobFileId(System.Guid value);
		partial void RunCustomLogicSetBlobFileTypeId(int? value);
		partial void RunCustomLogicSetBlobUri(string value);
		partial void RunCustomLogicSetContent(byte[] value);
		partial void RunCustomLogicSetCreatedBy(string value);
		partial void RunCustomLogicSetCreatedUtcDate(System.DateTime value);
		partial void RunCustomLogicSetDataVersion(int value);
		partial void RunCustomLogicSetDiscreteMimeType(string value);
		partial void RunCustomLogicSetIsDeleted(bool value);
		partial void RunCustomLogicSetModifiedBy(string value);
		partial void RunCustomLogicSetModifiedUtcDate(System.DateTime value);
		partial void RunCustomLogicSetName(string value);
		partial void RunCustomLogicSetParentBlobFileId(System.Guid? value);
		partial void RunCustomLogicSetRequiresResize(bool? value);
		partial void RunCustomLogicSetResizeComplete(bool? value);
		partial void RunCustomLogicSetSizeInBytes(long? value);

		#endregion RunCustomLogicSet

	}
}
