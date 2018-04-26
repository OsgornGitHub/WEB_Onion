using Microsoft.EntityFrameworkCore;
using OAA.Data;
using OAA.Repo;
using OAA.Service.Service;

namespace OAA.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            TrackService _trackService = new TrackService(unitOfWork);
            AlbumService _albumService = new AlbumService(unitOfWork);
            SearchTrack search = new SearchTrack(_albumService, _trackService);
            search.Search();
        }
    }
}
