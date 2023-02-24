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

		public async Task<Region> AddAsync(Region region)
		{
			region.Id = Guid.NewGuid();    //It cant be obtained from the user, thus it is assigned here
			await _nZWalksDbContext.AddAsync(region);   //Added to the DB
			await _nZWalksDbContext.SaveChangesAsync();  //Saving the changes done in the DB
			return region;  //Returning the region added
		}

		public async Task<Region> DeleteAsync(Guid id)
		{
			var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);  //searches and finds via ID

			if (region == null)
			{
				return null;
			}

			// Delete the region
			_nZWalksDbContext.Regions.Remove(region);
			await _nZWalksDbContext.SaveChangesAsync();
			return region;
		}

		public async Task<IEnumerable<Region>> GetAllAsync()
		{
			return await _nZWalksDbContext.Regions.ToListAsync();
			//Convert into List as Async way (ToListAsync) => Thus, use Task in the method name and await in return,
			//since await is used in return, thus use async in the prperty implementation 
		}

		public async Task<Region> GetAsync(Guid id)
		{
			return await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Region> UpdateAsync(Guid id, Region region)
		{
			var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRegion == null)
			{
				return null;
			}

			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;
			existingRegion.Area = region.Area;
			existingRegion.Lat = region.Lat;
			existingRegion.Long = region.Long;
			existingRegion.Population = region.Population;

			await _nZWalksDbContext.SaveChangesAsync();

			return existingRegion;
		}
	}
}
