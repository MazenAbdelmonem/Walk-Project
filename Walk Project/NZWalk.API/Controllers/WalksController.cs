using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.DTO;
using AutoMapper;
using NZWalk.API.Repositories;
using NZWalk.API.Mappings;
using NZWalk.API.Models.Domin;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http.HttpResults;
using NZWalk.API.CustomActionFilters;

namespace NZWalk.API.Controllers
{
    // api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper maper;

        public WalksController(IWalkRepository walkRepository, IMapper maper) 
        {
            this.walkRepository = walkRepository;
            this.maper = maper;
        }
        // Creat Walk
        // post: api/Walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Creat([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            
                var WalkDominModel = maper.Map<Walk>(addWalkRequestDto);
                WalkDominModel = await walkRepository.CreatWalkAsync(WalkDominModel);

                var walkDto = maper.Map<WalkDto>(WalkDominModel);
                return Created(nameof(WalkDominModel), walkDto);
            
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 1000)
        {
            var WalkDominModel = await walkRepository.GelAlleWalkAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, PageSize);

            return Ok(maper.Map<List<WalkDto>>(WalkDominModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var WalkDominModel = await walkRepository.GetWalkByIdAsync(id);
            if(WalkDominModel == null)
            {
                return NotFound();
            }
            return Ok(maper.Map<WalkDto>(WalkDominModel));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            
                var WalkDominModel = maper.Map<Walk>(updateWalkRequestDto);
                WalkDominModel = await walkRepository.UpdateWalkAsync(id, WalkDominModel);
                if (WalkDominModel == null)
                {
                    return NotFound();
                }
                return Ok(maper.Map<WalkDto>(WalkDominModel));
            
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
                var ModelDominModel = await walkRepository.DeleteWalkAsync(id);
                if (ModelDominModel == null)
                {
                    return NotFound();
                }

                return Ok(maper.Map<WalkDto>(ModelDominModel));
        }

    }
}
