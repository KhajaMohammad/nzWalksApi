using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nzWalksApi.CustomActionFilters;
using nzWalksApi.Models.Domain;
using nzWalksApi.Models.DTO;
using nzWalksApi.Repositories;

namespace nzWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Create Walk
        // POST: http/api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto receivedWalkDto)
        {
            // MAP dto to main model AddWalkRequestDto --> Walk
            var walkDomainModel = mapper.Map<Walk>(receivedWalkDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // map domain moel to dto

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // GET https:localhost:/api/walks/filterOn=Name$filterQuery=Track$sortBy=Name$isAscending=true$pageNumber=1$pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync(
            [FromQuery] string? filterOn = null,
            [FromQuery] string? filterQuery = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool? isAscending = true,
            [FromQuery] int pageNuber = 1,
            [FromQuery] int pageSize = 5
        )
        {
            var allWalks = await walkRepository.GetAllAsync(
                filterOn,
                filterQuery,
                sortBy,
                isAscending ?? true,
                pageNuber,
                pageSize
            );

            return Ok(mapper.Map<List<WalkDto>>(allWalks));
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid Id)
        {
            var allWalk = await walkRepository.GetWalkByIdAsync(Id);

            return Ok(mapper.Map<WalkDto>(allWalk));
        }

        [HttpPut]
        [Route("{Id}:Guid")]
        public async Task<IActionResult> UpdateWalk(
            [FromBody] UpdateWalkRequestDto updateWalkDto,
            [FromRoute] Guid Id
        )
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkDto);

            walkDomainModel = await walkRepository.UpdateWalk(walkDomainModel, Id);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteWalkByIdAsync(Guid Id)
        {
            var deletedDomainModel = await walkRepository.DeleteWalkByIdAsync(Id);

            return Ok(deletedDomainModel);
        }
    }
}
