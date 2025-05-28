using AutoMapper; // For mapping between Domain models and DTOs.
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nzWalksApi.CustomActionFilters; // Custom attribute to validate the model.
using nzWalksApi.Models.Domain; // Domain models representing the business logic.
using nzWalksApi.Models.DTO; // Data Transfer Objects (DTOs) used to return data to the client.
using nzWalksApi.Repositories; // Repositories to interact with the database.

namespace nzWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper; // AutoMapper instance to convert between domain models and DTOs.
        private readonly IWalkRepository walkRepository; // WalkRepository instance to interact with the database.

        // Constructor that takes dependencies (AutoMapper and WalkRepository) via Dependency Injection.
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // POST: /api/walks
        // This endpoint is used to create a new walk. It accepts data from the client and stores it in the database.
        [HttpPost]
        [ValidateModel] // Custom action filter to validate the model before proceeding.
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto receivedWalkDto)
        {
            // Map the incoming DTO (AddWalkRequestDto) to the domain model (Walk).
            var walkDomainModel = mapper.Map<Walk>(receivedWalkDto);

            // Call the repository to create the new walk in the database asynchronously.
            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);
            
            // Map the created domain model to a DTO (WalkDto) and return it with a 200 OK response.
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // GET: /api/walks
        // This endpoint is used to retrieve all walks, with options for filtering, sorting, and paging.
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync(
            [FromQuery] string? filterOn = null, // Optional parameter to specify which field to filter on.
            [FromQuery] string? filterQuery = null, // The query value to filter by.
            [FromQuery] string? sortBy = null, // The field to sort by.
            [FromQuery] bool? isAscending = true, // Whether the sort order should be ascending or descending.
            [FromQuery] int pageNuber = 1, // The page number for pagination.
            [FromQuery] int pageSize = 5 // The page size for pagination.
        )
        {
            // Fetch walks using the repository, applying filtering, sorting, and pagination logic.
            var allWalks = await walkRepository.GetAllAsync(
                filterOn,
                filterQuery,
                sortBy,
                isAscending ?? true, // Default to ascending if null.
                pageNuber,
                pageSize
            );
            
            // Map the list of domain models to a list of DTOs and return it in the response.
            return Ok(mapper.Map<List<WalkDto>>(allWalks));
        }

        // GET: /api/walks/{Id}
        // This endpoint retrieves a single walk by its ID.
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid Id)
        {
            // Fetch the walk by its ID using the repository.
            var walk = await walkRepository.GetWalkByIdAsync(Id);

            // If the walk is not found, return a 404 Not Found response.
            if (walk == null)
            {
                return NotFound();
            }

            // Map the retrieved domain model to a DTO and return it.
            return Ok(mapper.Map<WalkDto>(walk));
        }

        // PUT: /api/walks/{Id}
        // This endpoint is used to update an existing walk. It requires the ID of the walk to update.
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateWalk(
            [FromBody] UpdateWalkRequestDto updateWalkDto, // The updated data for the walk.
            [FromRoute] Guid Id // The ID of the walk to update.
        )
        {
            // Map the incoming update DTO to the domain model (Walk).
            var walkDomainModel = mapper.Map<Walk>(updateWalkDto);

            // Call the repository to update the walk in the database.
            walkDomainModel = await walkRepository.UpdateWalk(walkDomainModel, Id);

            // If the walk is not found, return a 404 Not Found response.
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map the updated domain model to a DTO and return it in the response.
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // DELETE: /api/walks/{Id}
        // This endpoint is used to delete a walk by its ID.
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteWalkByIdAsync(Guid Id)
        {
            // Call the repository to delete the walk by its ID.
            var deletedDomainModel = await walkRepository.DeleteWalkByIdAsync(Id);

            // If the walk is not found, return a 404 Not Found response.
            if (deletedDomainModel == null)
            {
                return NotFound();
            }

            // Return the deleted walk details in the response.
            return Ok(deletedDomainModel);
        }
    }
}
