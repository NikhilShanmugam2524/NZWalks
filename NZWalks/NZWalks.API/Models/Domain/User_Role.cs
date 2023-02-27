using System.Data;

namespace NZWalks.API.Models.Domain
{
	public class User_Role  //TO connect user with roles (i.e. connecting user with role)
	{						// 1 Role has many user and one user has only one role
		public Guid Id { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }

		public Guid RoleId { get; set; }
		public Role Role { get; set; }
	}
}
