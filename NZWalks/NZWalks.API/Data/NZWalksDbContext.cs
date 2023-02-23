using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext : DbContext
	{
		public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base(options)
		{

		}

		public DbSet<Region> Regions { get; set; } //Create a table

		public DbSet<Walk> Walks { get; set; } //Create a table

		public DbSet<WalkDifficulty> WalkDifficulty { get; set; } //Its a lookUp table
	}
}
