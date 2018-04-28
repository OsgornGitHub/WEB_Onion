using Microsoft.EntityFrameworkCore;
using OAA.Data;
using OAA.Repo;
using OAA.Service.Service;

namespace OAA.Cons
{
    class Program
    {
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        private static DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;
        private static ApplicationContext db = new ApplicationContext(options);
        private static UnitOfWork unitOfWork = new UnitOfWork(db);

        static void Main(string[] args)
        {
            TrackService _trackService = new TrackService(unitOfWork);
            AlbumService _albumService = new AlbumService(unitOfWork);
            ArtistService _artistService = new ArtistService(unitOfWork);
            SearchTrack search = new SearchTrack(_albumService, _trackService, _artistService);
            search.Search();
        }
    }
}
