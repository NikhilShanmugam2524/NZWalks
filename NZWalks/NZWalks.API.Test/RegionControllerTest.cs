using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Algorithm;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Test
{
	public class RegionControllerTest
	{
		[Fact]
		public async Task GetAllRegionsAsync_ShouldReturnAllRegions()
		{
			//Arrange =>Value Assignement and MoQ setup
			var mockRegionRepository = new Mock<IRegionRepository>();
			var mockMapper = new Mock<IMapper>();
			var regions = new List<Models.Domain.Region>() {
				new (){Area = 1 }
			};
			var regionsDTO = new List<Models.DTO.Region>() {
				new (){Area = 1 }
			};
			mockRegionRepository
				.Setup(repo => repo.GetAllAsync())
				.ReturnsAsync(regions);
			mockMapper
				.Setup(map => map.Map<List<Models.DTO.Region>>(regions))
				.Returns(regionsDTO);

			var regionController = new RegionsController(mockRegionRepository.Object, mockMapper.Object);

			//Act
			var result = await regionController.GetAllRegionsAsync() as OkObjectResult;

			//Assert
			Assert.Equal(200, result.StatusCode);
			Assert.Equal(regionsDTO, result.Value);
		}

		public static List<Object[]> GetIdData() 
		{
			var regions = new List<Object[]>()
			{
				new Object[]  { new Guid("79E9872D5A2F413EAC36511036CCD3D4"), true },
				new Object[]  { new Guid("79E9872D5A2F413EAC36511036CCD3D5"), false}
			};

			return regions;
		}

		[Theory]
		[MemberData(nameof(GetIdData))]
		public async Task GetRegionAsync_ShouldReturnRegion(Guid id, bool assert)
		{
			// Arrange =>Value Assignement and MoQ setup
			var mockRegionRepository = new Mock<IRegionRepository>();
			var mockMapper = new Mock<IMapper>();
			var region = new Models.Domain.Region { Id = new Guid("79E9872D5A2F413EAC36511036CCD3D4"), Code = "AUCK", Name = "Auckland", Area = 100000, Lat = -8905, Long = 90922, Population = 34000 };
			var regionDTO = new Models.DTO.Region { Id = new Guid("79E9872D5A2F413EAC36511036CCD3D4"), Code = "AUCK", Name = "Auckland", Area = 100000, Lat = -8905, Long = 90922, Population = 34000 };
			mockRegionRepository
				.Setup(repo => repo.GetAsync(new Guid("79E9872D5A2F413EAC36511036CCD3D4")))
				.ReturnsAsync(region);
			mockMapper
				.Setup(map => map.Map<Models.DTO.Region>(region))
				.Returns(regionDTO);

			var regionController = new RegionsController(mockRegionRepository.Object, mockMapper.Object);

			// Act
			

			// Assert
			if (assert)
			{
				var result = await regionController.GetRegionAsync(id) as OkObjectResult;
				Assert.Equal(200, result.StatusCode);
				Assert.Equal(regionDTO, result.Value);
			}
			else
			{
				var result = await regionController.GetRegionAsync(id) as NotFoundResult;
				Assert.Equal(404, result.StatusCode);
			}
		}
		/*
				[Theory]
				[InlineData("79E9872D5A2F413EAC36511036CCD3D4")]
				public async Task GetRegionAsync_ShouldReturnNull(Guid id)
				{
					// Arrange =>Value Assignement and MoQ setup
					var mockRegionRepository = new Mock<IRegionRepository>();
					var mockMapper = new Mock<IMapper>();
					var region = new Models.Domain.Region { };
					var regionDTO = new Models.DTO.Region { };
					mockRegionRepository
						.Setup(repo => repo.GetAsync(id))
						.ReturnsAsync(region);
					mockMapper
						.Setup(map => map.Map<Models.DTO.Region>(region))
						.Returns(regionDTO);

					var regionController = new RegionsController(mockRegionRepository.Object, mockMapper.Object);

					// Act
					var result = await regionController.GetRegionAsync(id) as OkObjectResult;

					// Assert
					Assert.Equal(404, result.StatusCode);
					Assert.Equal(regionDTO, result.Value);
				}
		*/

		public static List<Object[]> GetRegionData()
		{
			var regions = new List<Object[]>()
			{
				new Object[]  { new AddRegionRequest() { Code = "AUCK", Name = "Auckland", Area = 100000, Lat = -8905, Long = 90922, Population = 34000 }, true },
				new Object[]  { new AddRegionRequest() { Code = "Wellington", Name = "Well", Area = 10000, Lat = -8955, Long = 9922, Population = 3400 }, false}
			};

			return regions;
		}

		[Theory]
		[MemberData(nameof(GetRegionData))]
		public async Task AddRegionAsync_ShouldReturnRegion(Models.DTO.AddRegionRequest addRegionRequest, bool assert)
		{
			// Arrange =>Value Assignement and MoQ setup
			var mockRegionRepository = new Mock<IRegionRepository>();
			var mockMapper = new Mock<IMapper>();
			var region = new Models.Domain.Region { Id = new Guid("79E9872D5A2F413EAC36511036CCD3D4"), Code = "AUCK", Name = "Auckland", Area = 100000, Lat = -8905, Long = 90922, Population = 34000 };
			var regionDTO = new Models.DTO.Region { Id = new Guid("79E9872D5A2F413EAC36511036CCD3D4"), Code = "AUCK", Name = "Auckland", Area = 100000, Lat = -8905, Long = 90922, Population = 34000 };
			mockRegionRepository
				.Setup(repo => repo.GetAsync(new Guid("79E9872D5A2F413EAC36511036CCD3D4")))
				.ReturnsAsync(region);
			mockMapper
				.Setup(map => map.Map<Models.DTO.Region>(region))
				.Returns(regionDTO);

			var regionController = new RegionsController(mockRegionRepository.Object, mockMapper.Object);

			// Act


			// Assert
			if (assert)
			{
				var result = await regionController.GetRegionAsync(id) as OkObjectResult;
				Assert.Equal(200, result.StatusCode);
				Assert.Equal(regionDTO, result.Value);
			}
			else
			{
				var result = await regionController.GetRegionAsync(id) as NotFoundResult;
				Assert.Equal(404, result.StatusCode);
			}
		}



	}
}