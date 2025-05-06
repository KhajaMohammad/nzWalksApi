using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nzWalksApi.Data;
using nzWalksApi.Models.Domain;
using nzWalksApi.Models.DTO;
using nzWalksApi.Repositories;
using nzWalksApi.CustomActionFilters;

namespace nzWalksApi.Controllers
{
    // https:localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly NzWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(
            NzWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper
        )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        // GET all regions
        // GET https:localhost:1234/api/regions
        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionDto = mapper.Map<List<RegionDto>>(regions);

            //var regionDto = new List<RegionDto>();

            //foreach (var region in regions)
            //{

            //    regionDto.Add(new RegionDto
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl

            //    });

            //}
            return Ok(regionDto);
        }

        // GET Region BY ID
        // GET http:localhost:1234/api/regions/{id}

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            // var res = dbContext.regions.Find(ID);
            // get data drom db
            // map domain models to dto
            // return dto
            var res = await regionRepository.GetByIdAsync(Id);

            if (res == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(res);

            return Ok(regionDto);
        }

        // post to create Region
        // POST http://localhost:124/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createRegion([FromBody] AddRegionDto receivedRegionDto)
        {
            
                var regionDomainModel = mapper.Map<Region>(receivedRegionDto);

                // use domain model to create region
                regionDomainModel = await regionRepository.createRegionAsync(regionDomainModel);

                // Map Domain model to create Region

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
            
        }

        // PUT update https://localhost:1234:/api/regions/{id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> update(
            [FromRoute] Guid Id,
            [FromBody] UpdateRegionDto receivedRegionDto
        )
        {
            // check if region exists

            var newregion = mapper.Map<Region>(receivedRegionDto);

            var regionDomainModel = await regionRepository.updateAsync(Id, newregion);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map dto to domain model

            //convert domain model to dto

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        //delete region
        //delete http://localhost:/api/regions/{id}

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> delete([FromRoute] Guid Id)
        {
            var regionDomainModel = await regionRepository.Delete(Id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(regionDomainModel);
        }
    } // controller method
}
