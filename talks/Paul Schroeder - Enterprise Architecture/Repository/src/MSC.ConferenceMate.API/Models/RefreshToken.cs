using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSC.ConferenceMate.API.Models
{
	[Table(name: "RefreshToken")]
	public class RefreshToken
	{
		public string AspNetUsersId { get; set; }

		public DateTime ExpiresUtc { get; set; }

		public DateTime IssuedUtc { get; set; }

		[Required]
		public string ProtectedTicket { get; set; }

		[Key]
		public int RefreshTokenId { get; set; }

		public string Token { get; set; }

		[ForeignKey("AspNetUsersId")]
		public ApplicationUser User { get; set; }
	}
}