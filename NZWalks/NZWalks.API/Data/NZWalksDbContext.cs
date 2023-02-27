using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext : DbContext
	{
		public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User_Role>()
				.HasOne(x => x.Role)
				.WithMany(y => y.UserRoles)
				.HasForeignKey(x => x.RoleId);

			modelBuilder.Entity<User_Role>()
				.HasOne(x => x.User)
				.WithMany(y => y.UserRoles)
				.HasForeignKey(x => x.UserId);
		}

		public DbSet<Region> Regions { get; set; } //Create a table

		public DbSet<Walk> Walks { get; set; } //Create a table

		public DbSet<WalkDifficulty> WalkDifficulty { get; set; } //Its a lookUp table

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User_Role> Users_Roles { get; set; }
	}
}
