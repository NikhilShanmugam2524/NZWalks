namespace NZWalks.API.Models.DTO
{
	public class UpdateRegionRequest
	{
		//Except ID all the other prperties can be updated by the user (ID is once given cant be updated)
		public string Code { get; set; }
		public string Name { get; set; }
		public double Area { get; set; }
		public double Lat { get; set; }
		public double Long { get; set; }
		public long Population { get; set; }
	}
}
