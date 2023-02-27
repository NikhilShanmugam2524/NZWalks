using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly NZWalksDbContext nZWalksDbContext;

		public UserRepository(NZWalksDbContext nZWalksDbContext)
		{
			this.nZWalksDbContext = nZWalksDbContext;
		}


		public async Task<User> AuthenticateAsync(string username, string password)
		{
			//Checking the UN and pwd
			var user = await nZWalksDbContext.Users
				.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

			if (user == null)
			{
				return null;
			}

			//Getting userroles from the table
			var userRoles = await nZWalksDbContext.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();

			//if roles is not null
			if (userRoles.Any())
			{
				user.Roles = new List<string>();
				foreach (var userRole in userRoles)
				{
					var role = await nZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
					if (role != null)
					{
						user.Roles.Add(role.Name);
					}
				}
			}

			user.Password = null;  //This is done so that pwd does not float around the application (no One can see it while running app)
			return user;
		}
	}
}