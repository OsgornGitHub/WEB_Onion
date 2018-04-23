using Microsoft.EntityFrameworkCore;
using WEB.Data;

namespace WEB.Repo
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Similar> Similars { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ArtistMap(modelBuilder.Entity<Artist>());
            new AlbumMap(modelBuilder.Entity<Album>());
            new TrackMap(modelBuilder.Entity<Track>());
            new SimilarMap(modelBuilder.Entity<Similar>());
        }
    }
}
