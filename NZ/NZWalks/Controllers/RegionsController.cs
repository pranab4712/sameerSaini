using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            //var regions = new List<Region>()
            //{
            //    new Region()
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Wellington",
            //        Code="WLG",
            //        Area=1254,
            //        Lat=10,
            //        Long=20,
            //        Population=1478
            //    },
            //     new Region()
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Auckland",
            //        Code="WLG",
            //        Area=1254,
            //        Lat=10,
            //        Long=20,
            //        Population=1478
            //    }

            //};
            //return Ok(regions);
            var regions = await regionRepository.GetAllAsync();
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,

            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region=await regionRepository.GetAsync(id);
            if(region == null)
                return NotFound();
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            var region=new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };
            var reply=await regionRepository.AddAsync(region);
            var regionDTO = new Models.DTO.Region()
            {
                Id=reply.Id,
                Code=reply.Code,
                Name=reply.Name,
                Area=reply.Area,
                Lat=reply.Lat,
                Long=reply.Long,
                Population=reply.Population,

            };
            return CreatedAtAction(nameof(GetRegionAsync),new {id=regionDTO.Id},regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region=await regionRepository.DeleteAsync(id);
            if(region==null)
                return NotFound();
            var regionDTO = new Models.DTO.Region()
            {
                Id= region.Id,
                Code = region.Code,
                Name=region.Name,
                Area=region.Area,
                Lat=region.Lat,
                Long=region.Long,
                Population=region.Population,

            };
            return Ok(regionDTO);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRegionAsync(Guid id,Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,
            };
            var reply=await regionRepository.UpdateAsync(id,region);
            var regionDTO = new Models.DTO.Region()
            {
                Id = reply.Id,
                Code = reply.Code,
                Name = reply.Name,
                Area = reply.Area,
                Lat = reply.Lat,
                Long = reply.Long,
                Population = reply.Population,

            };
            return Ok(regionDTO);

        }
    }
}
