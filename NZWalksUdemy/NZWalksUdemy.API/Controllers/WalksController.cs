using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksUdemy.API.CustomActionFilters;
using NZWalksUdemy.API.Data;
using NZWalksUdemy.API.Models.Domain;
using NZWalksUdemy.API.Models.DTO;
using NZWalksUdemy.API.Repositories;

namespace NZWalksUdemy.API.Controllers
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

        [HttpPost]
        [ValidateModelAttributes]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {

            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
            await walkRepository.CreateAsync(walkDomainModel);

            // Map to DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));

        }

        // Filtering: GET api/walks?filterOn=Name&filterType=Track&sortBy=Name&isAsc=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortby, [FromQuery] bool? isAsc,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomainModels = await walkRepository.GetAllAsync(filterOn, filterQuery, sortby, isAsc ?? true, pageNumber, pageSize);
            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModels));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIDAsync(id);
            if (walkDomainModel == null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);
            if (walkDomainModel == null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttributes]
        public async Task<IActionResult> Update(Guid id, [FromRoute] UpdateWalkRequestDTO updateWalkRequestDTO)
        {

            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null) return NotFound();

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));

        }
    }
}
