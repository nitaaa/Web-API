using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksUdemy.API.CustomActionFilters;
using NZWalksUdemy.API.Data;
using NZWalksUdemy.API.Models.Domain;
using NZWalksUdemy.API.Models.DTO;
using NZWalksUdemy.API.Repositories;
using System.Text.Json;

namespace NZWalksUdemy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()// IActionResult GetAll()
        {
            logger.LogInformation("GetAll Regions Action Method Invoked"); // Warning, Error
            //hardcoded
            var regions2 = new List<Region>
            {
                new Region { Id = Guid.NewGuid(), Name = "Auckland", Code = "AKL", RegionImageUrl="https://unsplash.com/photos/overlooking-island-L-mvjXO1WAM"},
                new Region { Id = Guid.NewGuid(), Name = "Wellington", Code = "WLG", RegionImageUrl="https://unsplash.com/photos/a-path-through-a-forest-with-lots-of-trees-n0WEwn6gtqc"}
            };
            // Get data from Database (Domain Models)
            var regions = await regionRepository.GetAllAsync(); //await dbContext.Regions.ToListAsync();

            // Map Domain Models to DTO (Data Transfer Object)
            //var regionsDTO = new List<RegionDTO>();
            //foreach (var region in regions)
            //{
            //    regionsDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

            logger.LogInformation($"Finished GetAllRegions requestion with data {JsonSerializer.Serialize(regions)}");
            // Return DTOs to Client
            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetByIDAsync([FromRoute] Guid id)
        {
            // Get Region Domain Model from DB
            /* var region = await dbContext.Regions.FindAsync(id); //PK only

             var region2 = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id); //using LINQ
            */
            var region = await regionRepository.GetByIDAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            // Map DM to DTO
            //RegionDTO regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            // Return DTO to Client
            return Ok(mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        [ValidateModelAttributes]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //if(ModelState.IsValid)
            //{
            // Map DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDTO.Code,
            //    Name = addRegionRequestDTO.Name,
            //    RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            //};

            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            // Use DM to ceate Region in DB Context
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map DM to DTO
            //RegionDTO regionDTO = new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetByIDAsync), new { id = regionDTO.Id }, regionDTO);
            //} 
            //else {
            //    return BadRequest(ModelState);
            //}            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAttributes]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //if (ModelState.IsValid) {
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDTO.Code,
            //    Name = updateRegionRequestDTO.Name,
            //    RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null) return NotFound();

            //var regiondm = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //if (regiondm == null) return NotFound();

            // Map DTO to Domain Model
            //regiondm.Code = updateRegionRequestDTO.Code;
            //regiondm.Name = updateRegionRequestDTO.Name;
            //regiondm.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            // Convert DM to DTO
            //var regionDTO = new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
            //}
            //else {
            //    return BadRequest(ModelState);
            //}

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regiondm = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //if (regiondm == null) return NotFound();

            //dbContext.Regions.Remove(regiondm);
            //await dbContext.SaveChangesAsync();
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null) return NotFound();

            //var regionDTO = new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }
    }
}