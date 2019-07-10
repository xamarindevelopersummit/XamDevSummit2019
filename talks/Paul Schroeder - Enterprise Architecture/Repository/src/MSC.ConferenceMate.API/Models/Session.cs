using MSC.ConferenceMate.API.Models.Interface;
using MSC.ConferenceMate.Domain;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Linq;

namespace MSC.ConferenceMate.API.Models
{
	public class Session : ISession
	{
		private ClaimsPrincipal _currentUser;

		private int _currentUserProfileId = -99;

		public Session(HttpRequestMessage httpRequest)
		{
			CurrentRequest = httpRequest;
		}

		public HttpRequestMessage CurrentRequest { get; }

		public ClaimsPrincipal CurrentUser
		{
			get
			{
				if (_currentUser == null)
				{
					_currentUser = CurrentRequest.GetOwinContext().Authentication.User;
				}

				return _currentUser;
			}
		}

		public bool CurrentUserIsConferenceOrganizer
		{
			get
			{
				bool retVal = false;

				if (CurrentUser != null && CurrentUser.IsInRole(Consts.ROLE_CONFERENCEORGANIZER))
				{
					retVal = true;
				}

				return retVal;
			}
		}

		public int CurrentUserProfileId
		{
			get
			{
				if (_currentUserProfileId == -99)
				{
					if (CurrentUser != null && CurrentUser.HasClaim(x => x.Type == Consts.CLAIM_USERPROFILEID))
					{
						var claimValue = CurrentUser.Claims.First(x => x.Type == Consts.CLAIM_USERPROFILEID).Value;
						int.TryParse(claimValue, out _currentUserProfileId);
					}
				}

				return _currentUserProfileId;
			}
		}
	}
}