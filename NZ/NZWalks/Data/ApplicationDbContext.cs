using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options):base(Options)
        {

        }
        public DbSet<Region> Regions { set; get; }
        public DbSet<Walk> Walks { set; get; }
        public DbSet<WalkDifficulty> WalkDifficulties { set; get; }
    }
}
