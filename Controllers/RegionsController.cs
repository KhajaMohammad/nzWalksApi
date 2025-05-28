using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nzWalksApi.CustomActionFilters;
using nzWalksApi.Data;
using nzWalksApi.Models.Domain;
using nzWalksApi.Models.DTO;
using nzWalksApi.Repositories;
using Serilog.Data;
using System.Text.Json;

namespace nzWalksApi.Controllers
{
    // Controller to manage Regions.
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMapper mapper; // Used to map between domain models and DTOs.
        private readonly NzWalksDbContext dbContext; // DbContext to interact with the database.
        private readonly IRegionRepository regionRepository; // Repository to interact with region data.
        private readonly ILogger<RegionsController> logger;

        // Constructor to inject dependencies (e.g., DbContext, Repository, Mapper).
        public RegionsController(
            NzWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger
        )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.logger = logger;
        }

        // GET: /api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")] // Only users with the "Reader" role can access this endpoint.
        public async Task<IActionResult> GetAll()
        {

            try
            {

                //logging
                logger.LogInformation(" GetAll Regions controller invoked");
               //throw new Exception("Custom Exception");
                // Fetch all regions from the repository.
                var regions = await regionRepository.GetAllAsync();
                var regionDto = mapper.Map<List<RegionDto>>(regions); // Map to DTOs.
                logger.LogInformation($"Finished fetching data of all regions :{JsonSerializer.Serialize(regions)}");
               
                return Ok(regionDto); // Return the list of region DTOs.
            }
            catch(Exception e )
            {

                logger.LogError(e.Message, e );
                throw;

            }
            }// end try

        // GET: /api/regions/{id}
        [HttpGet]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Reader")] // Only users with the "Reader" role can access this endpoint.
        public async Task<IActionResult> GetById(Guid Id)
        {
            // Get region by ID from the repository.
            var res = await regionRepository.GetByIdAsync(Id);

            // If region not found, return NotFound status.
            if (res == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(res); // Map to DTO.

            return Ok(regionDto); // Return the region DTO.
        }

        // POST: /api/regions
        [HttpPost]
        [ValidateModel] // Custom action filter to validate model before proceeding.
       [Authorize(Roles = "Writer")] // Only users with the "Writer" role can create regions.
        public async Task<IActionResult> createRegion([FromBody] AddRegionDto receivedRegionDto)
        {
            // Convert DTO to domain model using AutoMapper.
            var regionDomainModel = mapper.Map<Region>(receivedRegionDto);

            // Create the region in the repository.
            regionDomainModel = await regionRepository.createRegionAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel); // Map domain model to DTO.

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto); // Return a Created status with location.
        }

        // PUT: /api/regions/{id}
        [HttpPut]
        [Route("{Id:Guid}")]
       [Authorize(Roles = "Writer")] // Only users with the "Writer" role can update regions.
        public async Task<IActionResult> update(
            [FromRoute] Guid Id,
            [FromBody] UpdateRegionDto receivedRegionDto
        )
        {
            // Convert DTO to domain model.
            var newRegion = mapper.Map<Region>(receivedRegionDto);

            // Update the region in the repository.
            var regionDomainModel = await regionRepository.updateAsync(Id, newRegion);

            // If the region is not found, return NotFound status.
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel); // Map to DTO.

            return Ok(regionDto); // Return updated region DTO.
        }

        // DELETE: /api/regions/{id}
        [HttpDelete]
        [Route("{Id:Guid}")]
      [Authorize(Roles = "Writer,Reader")] // Both Writer and Reader roles can delete regions.
        public async Task<IActionResult> delete([FromRoute] Guid Id)
        {
            // Delete the region from the repository.
            var regionDomainModel = await regionRepository.Delete(Id);

            // If the region is not found, return NotFound status.
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(regionDomainModel); // Return the deleted region details.
        }
    }
}
