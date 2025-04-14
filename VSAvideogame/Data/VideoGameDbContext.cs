using Microsoft.EntityFrameworkCore;
using VSAvideogame.Entities;

namespace VSAvideogame.Data
{
    public class VideoGameDbContext : DbContext
    {
        public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options)
        {
        }
        
        public DbSet<VideoGame> VideoGames { get; set; }
    }
}
