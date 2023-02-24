using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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


		[HttpGet]
		//[Authorize(Roles = "reader")]
		public async Task<IActionResult> GetAllRegionsAsync()
		{
			var regions = await _regionRepository.GetAllAsync();
			//When a request is given it takes some time for controller to fetch the data from DB,
			//since the method is async, the website doesnot go unresponsive

			// return DTO regions
			//var regionsDTO = new List<Models.DTO.Region>();
			//regions.ToList().ForEach(region =>
			//{
			//    var regionDTO = new Models.DTO.Region()
			//    {
			//        Id = region.Id,
			//        Code = region.Code,
			//        Name = region.Name,
			//        Area = region.Area,
			//        Lat = region.Lat,
			//        Long = region.Long,
			//        Population = region.Population,
			//    };

			//    regionsDTO.Add(regionDTO);
			//});

			//return DTO regions using AutoMapper
			var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions);
			return Ok(regionsDTO);
		}


		[HttpGet]
		[Route("{id:guid}")]
		[ActionName("GetRegionAsync")]
		//[Authorize(Roles = "reader")]
		public async Task<IActionResult> GetRegionAsync(Guid id)
		{
			var region = await _regionRepository.GetAsync(id);

			if (region == null)
			{
				return NotFound();
			}

			var regionDTO = _mapper.Map<Models.DTO.Region>(region);
			return Ok(regionDTO);
		}

		[HttpPost]
		//[Authorize(Roles = "writer")]
		public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
		{
			// Validate The Request
			//if (!ValidateAddRegionAsync(addRegionRequest))
			//{
			//    return BadRequest(ModelState);
			//}

			// Request(DTO) to Domain model
			var region = new Models.Domain.Region()
			{
				Code = addRegionRequest.Code,
				Area = addRegionRequest.Area,
				Lat = addRegionRequest.Lat,
				Long = addRegionRequest.Long,
				Name = addRegionRequest.Name,
				Population = addRegionRequest.Population
			};

			// Pass details to Repository
			region = await _regionRepository.AddAsync(region);

			// Convert back to DTO
			var regionDTO = new Models.DTO.Region
			{
				Id = region.Id,
				Code = region.Code,
				Area = region.Area,
				Lat = region.Lat,
				Long = region.Long,
				Name = region.Name,
				Population = region.Population
			};

			return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);  //Displaying the region value in screen after adding the new record in the DB
			//CreatedAtAction => Gives a response Status Code 201 (Save/Add a new record in the DB successfully)
			//Parameter1 - GetRegionAsync => Action name is required by the CreatedAtAction() (Name of the action to generate the URL)
			//Parameter2 -  => Route value (Id) => Route id to find the record
			//Parameter1 - Entire Object is required  => Entire the object to display it in the SwaggerUI

		}


		[HttpDelete]
		[Route("{id:guid}")]
		//[Authorize(Roles = "writer")]
		public async Task<IActionResult> DeleteRegionAsync(Guid id)
		{
			// Get region from database
			var region = await _regionRepository.DeleteAsync(id);

			// If null NotFound
			if (region == null)
			{
				return NotFound();
			}

			// Convert response back to DTO
			var regionDTO = new Models.DTO.Region
			{
				Id = region.Id,
				Code = region.Code,
				Area = region.Area,
				Lat = region.Lat,
				Long = region.Long,
				Name = region.Name,
				Population = region.Population
			};

			// return Ok response
			return Ok(regionDTO);
		}


		[HttpPut]
		[Route("{id:guid}")]
		//[Authorize(Roles = "writer")]
		public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,
			[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
		{
			// Validate the incoming request
			//if (!ValidateUpdateRegionAsync(updateRegionRequest))
			//{
			//    return BadRequest(ModelState);
			//}

			// Convert updateRequestDTO to Domain model
			var region = new Models.Domain.Region()
			{
				Code = updateRegionRequest.Code,
				Area = updateRegionRequest.Area,
				Lat = updateRegionRequest.Lat,
				Long = updateRegionRequest.Long,
				Name = updateRegionRequest.Name,
				Population = updateRegionRequest.Population
			};


			// Update Region using repository
			region = await _regionRepository.UpdateAsync(id, region);


			// If Null then NotFound
			if (region == null)
			{
				return NotFound();
			}

			// Convert Domain back to DTO
			var regionDTO = new Models.DTO.Region
			{
				Id = region.Id,
				Code = region.Code,
				Area = region.Area,
				Lat = region.Lat,
				Long = region.Long,
				Name = region.Name,
				Population = region.Population
			};

			// Return Ok response
			return Ok(regionDTO);
		}


		/*#region Private methods

		private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
		{
			if (addRegionRequest == null)
			{
				ModelState.AddModelError(nameof(addRegionRequest),
					$"Add Region Data is required.");
				return false;
			}

			if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
			{
				ModelState.AddModelError(nameof(addRegionRequest.Code),
					$"{nameof(addRegionRequest.Code)} cannot be null or empty or white space.");
			}

			if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
			{
				ModelState.AddModelError(nameof(addRegionRequest.Name),
					$"{nameof(addRegionRequest.Name)} cannot be null or empty or white space.");
			}

			if (addRegionRequest.Area <= 0)
			{
				ModelState.AddModelError(nameof(addRegionRequest.Area),
					$"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero.");
			}

			if (addRegionRequest.Population < 0)
			{
				ModelState.AddModelError(nameof(addRegionRequest.Population),
					$"{nameof(addRegionRequest.Population)} cannot be less than zero.");
			}

			if (ModelState.ErrorCount > 0)
			{
				return false;
			}

			return true;
		}

		private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
		{
			if (updateRegionRequest == null)
			{
				ModelState.AddModelError(nameof(updateRegionRequest),
					$"Add Region Data is required.");
				return false;
			}

			if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
			{
				ModelState.AddModelError(nameof(updateRegionRequest.Code),
					$"{nameof(updateRegionRequest.Code)} cannot be null or empty or white space.");
			}

			if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
			{
				ModelState.AddModelError(nameof(updateRegionRequest.Name),
					$"{nameof(updateRegionRequest.Name)} cannot be null or empty or white space.");
			}

			if (updateRegionRequest.Area <= 0)
			{
				ModelState.AddModelError(nameof(updateRegionRequest.Area),
					$"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero.");
			}

			if (updateRegionRequest.Population < 0)
			{
				ModelState.AddModelError(nameof(updateRegionRequest.Population),
					$"{nameof(updateRegionRequest.Population)} cannot be less than zero.");
			}

			if (ModelState.ErrorCount > 0)
			{
				return false;
			}

			return true;
		}*/
	}
}
