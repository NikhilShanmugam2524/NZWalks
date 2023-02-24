using AutoMapper;

namespace NZWalks.API.Profiles
{
	public class RegionsProfiles : Profile
	{
		public RegionsProfiles()
		{
			CreateMap<Models.Domain.Region, Models.DTO.Region>()  //For mapping if both properties are exactly same
				.ReverseMap();   //Reverse mapping of the same is also done

			//CreateMap<Models.Domain.Region, Models.DTO.Region>()
			//.ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId);     //Used for copying by property by property if the property names are different
		}

	}
}
