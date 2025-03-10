﻿namespace NZWalks.API.Models.DTO
{
	public class AddRegionRequest
	{
		//Except ID all the other prperties are given by the user (ID is given by us) 
		public string Code { get; set; }
		public string Name { get; set; }
		public double Area { get; set; }
		public double Lat { get; set; }
		public double Long { get; set; }
		public long Population { get; set; }
	}
}
