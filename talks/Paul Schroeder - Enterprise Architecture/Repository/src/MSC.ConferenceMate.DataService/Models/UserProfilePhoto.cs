using System;
using System.Collections.Generic;
using System.Text;

namespace MSC.ConferenceMate.DataService.Models
{
	public class UserProfilePhoto
	{
		public byte[] Data { get; set; }
		public string FileName { get; set; }
		public int UserProfileId { get; set; }
	}
}