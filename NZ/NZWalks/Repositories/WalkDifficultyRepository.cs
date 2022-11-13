using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class WalkDifficultyRepository:IWalkDifficultyRepository
    {
        private readonly ApplicationDbContext _db;
        public WalkDifficultyRepository(ApplicationDbContext _db)
        {
            this._db = _db;  
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _db.WalkDifficulties.AddAsync(walkDifficulty);
            await _db.SaveChangesAsync();
            return walkDifficulty;
        }

        public async  Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var exixtingWalkDifficulty = await _db.WalkDifficulties.FindAsync(id);
            _db.WalkDifficulties.Remove(exixtingWalkDifficulty);
            await _db.SaveChangesAsync();
            return exixtingWalkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _db.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await _db.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _db.WalkDifficulties.FindAsync(id);
            if (existingWalkDifficulty == null)
                return null;
            existingWalkDifficulty.Code = walkDifficulty.Code;
            await _db.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
