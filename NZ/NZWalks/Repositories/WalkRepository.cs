using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _db;
        public WalkRepository(ApplicationDbContext _db)
        {
            this._db= _db;
        }

       

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var exixtingWalk = await _db.Walks.FindAsync(id);
            if (exixtingWalk == null)
                return null;
            _db.Walks.Remove(exixtingWalk); 
            await _db.SaveChangesAsync();
            return exixtingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _db.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await _db.Walks
                 .Include(x => x.Region)
                 .Include(x => x.WalkDifficulty)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var exixtingWalk = await _db.Walks.FindAsync(id);
            if (exixtingWalk == null)
                return null;
            exixtingWalk.Name = walk.Name;
            exixtingWalk.Length=walk.Length;
            exixtingWalk.RegionId=walk.RegionId;
            exixtingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            await _db.SaveChangesAsync();
            return exixtingWalk;

        }
    }
}
