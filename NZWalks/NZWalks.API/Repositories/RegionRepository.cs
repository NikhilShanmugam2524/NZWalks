using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class RegionRepository : IRegionRepository
	{
		private readonly NZWalksDbContext _nZWalksDbContext;
		public RegionRepository(NZWalksDbContext nZWalksDbContext)
		{
			this._nZWalksDbContext = nZWalksDbContext;
		}

		public async Task<IEnumerable<Region>> GetAllAsync()
		{
			return await _nZWalksDbContext.Regions.ToListAsync();
			//Convert into List as Async way (ToListAsync) => Thus, use Task in the method name and await in return,
			//since await is used in return, thus use async in the prperty implementation 
		}
	}
}
