using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<Models.Domain.WalkDifficulty>> GetAllAsync();
        Task<Models.Domain.WalkDifficulty> GetAsync(Guid id);
        Task<Models.Domain.WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);
        Task<Models.Domain.WalkDifficulty> UpdateAsync(Guid id,WalkDifficulty walkDifficulty);
        Task<Models.Domain.WalkDifficulty> DeleteAsync(Guid id);
    }
}
