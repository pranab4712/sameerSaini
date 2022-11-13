using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository,IMapper _mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDiFiiculty=await walkDifficultyRepository.GetAllAsync();
            return Ok(walkDiFiiculty);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDiFiiculty=await walkDifficultyRepository.GetAsync(id);
            if(walkDiFiiculty==null)
                return NotFound();
            var walkDiFiicultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDiFiiculty);
            return Ok(walkDiFiicultyDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code,
            };
            var reply=await walkDifficultyRepository.AddAsync(walkDifficultyDomain);
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id= reply.Id,
                Code=reply.Code,

            };

            return CreatedAtAction(nameof(GetWalkDifficultyById),new {Id=walkDifficultyDTO.Id},walkDifficultyDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code= updateWalkDifficultyRequest.Code,
            };
            var reply=await walkDifficultyRepository.UpdateAsync(id,walkDifficultyDomain);
            var replyDTO = new Models.DTO.WalkDifficulty()
            {
                Code= reply.Code,
            };
            return Ok(replyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain=await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
                return NotFound();
            var replyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(replyDTO);
        }

    }
}
