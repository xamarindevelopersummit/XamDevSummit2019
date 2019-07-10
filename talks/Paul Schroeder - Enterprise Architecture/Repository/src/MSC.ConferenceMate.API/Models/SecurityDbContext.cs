using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MSC.ConferenceMate.API.Models
{
	public class SecurityDbContext : IdentityDbContext<ApplicationUser>
	{
		public SecurityDbContext()
			: base("ConferenceMateDB", throwIfV1Schema: false)
		{
		}

		public DbSet<RefreshToken> RefreshToken { get; set; }

		public static SecurityDbContext Create()
		{
			return new SecurityDbContext();
		}

		public async Task<bool> AddRefreshToken(RefreshToken token)
		{
			var existingToken = RefreshToken.Where(r => r.AspNetUsersId == token.AspNetUsersId).SingleOrDefault();

			if (existingToken != null)
			{
				var result = await RemoveRefreshToken(existingToken);
			}

			RefreshToken.Add(token);

			return await SaveChangesAsync() > 0;
		}

		public async Task<RefreshToken> FindRefreshTokenAsync(string token)
		{
			return await RefreshToken.SingleOrDefaultAsync(i => i.Token == token);
		}

		public async Task<bool> RemoveRefreshToken(string refreshToken)
		{
			var refreshTokenModel = await RefreshToken.SingleAsync(i => i.Token == refreshToken);

			if (refreshToken != null)
			{
				RefreshToken.Remove(refreshTokenModel);
				return await SaveChangesAsync() > 0;
			}

			return false;
		}

		public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
		{
			RefreshToken.Remove(refreshToken);
			return await SaveChangesAsync() > 0;
		}
	}
}