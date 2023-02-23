namespace NZWalks.API.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public double Length { get; set; }

		public Guid RegionId { get; set; }

		public Guid WalkDifficultyId { get; set; }

		//Navigation Property (Connection of tables in the DB is established during with help of EF core)
		public Region Region { get; set; }

		public WalkDifficulty WalkDifficulty { get; set; }

	}
}

//EF Core is the modern DB mapper for .NET core