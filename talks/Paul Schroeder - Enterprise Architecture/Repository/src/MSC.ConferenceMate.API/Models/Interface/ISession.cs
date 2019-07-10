using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace MSC.ConferenceMate.API.Models.Interface
{
	public interface ISession
	{
		HttpRequestMessage CurrentRequest { get; }
		ClaimsPrincipal CurrentUser { get; }
		bool CurrentUserIsConferenceOrganizer { get; }
		int CurrentUserProfileId { get; }
	}
}