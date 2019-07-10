using System;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace MSC.ConferenceMate.API.Providers
{
	public class JWTFormat : ISecureDataFormat<AuthenticationTicket>
	{
		private readonly string _issuer = string.Empty;

		public JWTFormat(string issuer)
		{
			_issuer = issuer;
		}

		public string DigestAlgorithm
		{
			get { return "http://www.w3.org/2001/04/xmlenc#sha256"; }
		}

		public string SignatureAlgorithm
		{
			get { return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
		}

		public string Protect(AuthenticationTicket data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}

			string audienceId = Utils.Configuration.TokenAudienceId;
			string symmetricKeyAsBase64 = Utils.Configuration.TokenAudienceSecret;
			var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
			var signingKey = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray),
															SignatureAlgorithm,
															DigestAlgorithm);

			var issued = data.Properties.IssuedUtc;
			var expires = data.Properties.ExpiresUtc;
			var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(_issuer, audienceId, data.Identity.Claims,
				issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);
			var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
			var jwt = handler.WriteToken(token);

			return jwt;
		}

		public AuthenticationTicket Unprotect(string tokenString)
		{
			throw new NotImplementedException();
		}
	}
}