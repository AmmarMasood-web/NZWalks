using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<DifficultyModel> Difficulties { get; set; }
        public DbSet<RegionsModel> Regions { get; set; }
        public DbSet<WalkModel> Walks { get; set; }
    }
}
