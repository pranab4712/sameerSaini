using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;
using NZWalks.Repositories;
using System.Net.NetworkInformation;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        public WalksController(IWalkRepository _walkRepository, IMapper _mapper)
        {
            this._walkRepository = _walkRepository;
            this._mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walksDomain = await _walkRepository.GetAllAsync();
            var walksDTO = _mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walksDomain = await _walkRepository.GetAsync(id);
            var walksDTO = _mapper.Map<Models.DTO.Walk>(walksDomain);
            return Ok(walksDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walkDomain = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId= addWalkRequest.WalkDifficultyId,
            };
            var reply = await _walkRepository.AddAsync(walkDomain);
            var walkDTO = new Models.DTO.Walk()
            {
                Id= reply.Id,
                Name = reply.Name,
                Length= reply.Length,
                RegionId= reply.RegionId,
                WalkDifficultyId=reply.WalkDifficultyId,
            };
            return CreatedAtAction(nameof(GetWalkAsync), new {id=walkDTO.Id},walkDTO);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute]Guid id,[FromBody]Models.DTO. UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,

            };
            var reply = await _walkRepository.UpdateAsync(id,walkDomain);
            if(reply == null)
                return NotFound();
            var walkDTO = new Models.DTO.Walk()
            {
                Id = reply.Id,
                Name = reply.Name,
                Length = reply.Length,
                RegionId = reply.RegionId,
                WalkDifficultyId = reply.WalkDifficultyId,

            };
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkDomain=await _walkRepository.DeleteAsync(id);
            if (walkDomain == null)
                return NotFound();
            var walkDTO=_mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
