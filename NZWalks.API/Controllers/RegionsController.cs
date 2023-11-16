using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public readonly NZWalksDBContext _dbContext;
        public RegionsController(NZWalksDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllRegion()
        {
            //Get Data From Database - Domain Models
            var regionsDomainModel = _dbContext.Regions.ToList();

            //Map Domain Models to DTOs
            var regionsDto = new List<RegionsDTO>();
            foreach (var regionDomain in regionsDomainModel)
            {
                regionsDto.Add(new RegionsDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageURL = regionDomain.RegionImageURL
                });
            }
            //Return DTOs
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            //Find uses only Primary Key but doesnt use other Parameters in the conditions whereas, the FirstOrDefault Linq method does.
            var regionDomainModel = _dbContext.Regions.Find(id);

            //var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel != null)
            {
                var regionDto = new RegionsDTO
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageURL = regionDomainModel.RegionImageURL
                };
                return Ok(regionDto);
            }
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult CreateRegion([FromBody] RegionRequestDTO regionRequest)
        {
            //Map or Convert DTO into DomainModel
            var regionDomainModel = new RegionsModel
            {
                Code = regionRequest.Code,
                Name = regionRequest.Name,
                RegionImageURL = regionRequest.RegionImageURL
            };
            //Use Domain Model to create Region
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            //Map DomainModel into DTO
            var regionDTO = new RegionsDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }
    }
}
