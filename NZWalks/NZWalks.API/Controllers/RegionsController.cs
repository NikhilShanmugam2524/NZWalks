using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RegionsController : Controller
	{
		private readonly IRegionRepository _regionRepository;
		private readonly IMapper _mapper;
		public RegionsController(IRegionRepository regionRepository, IMapper mapper)
		{
			this._regionRepository = regionRepository;
			this._mapper = mapper;
		}

		public IMapper Mapper { get; }

		[HttpGet]
		public async Task<IActionResult> GetAllRegions()
		{
			var regions = await _regionRepository.GetAllAsync();
			//When a request is given it takes some time for controller to fetch the data from DB,
			//since the method is async, the website doesnot go unresponsive


			//return DTO regions using AutoMapper
			var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions);
			return Ok(regionsDTO);

		}
	}
}
