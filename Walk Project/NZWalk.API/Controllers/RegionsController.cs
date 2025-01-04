using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.CustomActionFilters;
using NZWalk.API.Data;
using NZWalk.API.Models.Domin;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Azure.Core.Serialization;
using System.Text.Json;

namespace NZWalk.API.Controllers
{



    // https://localhost:portnumber/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext dbcontext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public RegionsController(NZWalkDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbcontext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        // Get All Regions
        // Get: https://localhost:portnumber/api/Regions
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            // Get Regions Domin model from DataBase
            var regionsDomin = await regionRepository.GetAllAsync();

            // Map/ Convert Regions Domin Modl to Region DTO
            return Ok(mapper.Map<List<RegionDto>>(regionsDomin));
        }
        // Get Single Region
        // Get: https://localhost:portnumber/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) 
        {
            //var region = dbcontext.Regions.Find(id);

            // Get Region Domin model from DataBase
            var regionDomin = await regionRepository.GetByIdAsync(id);
            if (regionDomin == null)
            {
                return NotFound();
            }
            // Map/ Convert Region Domin Modl to Region DTO
            
            return Ok(mapper.Map<RegionDto>(regionDomin));
        }
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Creat([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            
            var RegionDominModel = mapper.Map<Region>(addRegionRequestDto);
            //Use Domin Model to Creat Region
            RegionDominModel = await regionRepository.CreatAsync(RegionDominModel);

            // Map Domin Model back to DTO
            var RegionDTO = mapper.Map<RegionDto>(RegionDominModel);
            return CreatedAtAction(nameof(GetById), new { id = RegionDTO.Id }, RegionDTO);
            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
                var RegionDominModel = mapper.Map<Region>(updateRegionRequestDto);
                RegionDominModel = await regionRepository.UpdateAsync(id, RegionDominModel);
                if (RegionDominModel == null)
                {
                    return NotFound();
                }


                // Map Domin Model back to DTO
                var RegionDto = mapper.Map<RegionDto>(RegionDominModel);

                return Ok(RegionDto);
            
            
        }
        // Delete Region
        // Delete: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        { 
            var RegionDominModel = await regionRepository.DeleteAsync(id);
            if(RegionDominModel == null) { return NotFound(); }
            
            // Map Domin Model back to DTO
            
            return Ok(mapper.Map<RegionDto>(RegionDominModel));
        }
    }
}

