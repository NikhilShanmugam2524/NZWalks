using AutoMapper;

namespace NZWalks.API.Profiles
{
	public class RegionsProfiles : Profile
	{
		public RegionsProfiles()
		{
			CreateMap<Models.Domain.Region, Models.DTO.Region>();  // For both properties are exactly are same

			//CreateMap<Models.Domain.Region, Models.DTO.Region>()
			//.ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId);     //Used for copying by property by property if the property names are different
		}

	}
}
