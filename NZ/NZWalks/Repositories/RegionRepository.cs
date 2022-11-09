using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _db;
        public RegionRepository(ApplicationDbContext _db)
        {
            this._db = _db;
        }

        public ApplicationDbContext Db { get; }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id= Guid.NewGuid();
           await  _db.AddAsync(region);
           await  _db.SaveChangesAsync();
            return region; 
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region= await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
                return null;
            _db.Regions.Remove(region);
            await _db.SaveChangesAsync();
            return region; 

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var  existingRegion = await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
                return null;
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat=region.Lat;
            existingRegion.Long=region.Long;
            existingRegion.Population = region.Population;
            await _db.SaveChangesAsync();
            return existingRegion;
        }
    }
}
