using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        internal DbSet<Session> Sessions { get; set; }
        internal DbSet<Game> AliveGames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>();
            modelBuilder.Entity<Game>().ToTable("AliveGames");
        }
    }
}
