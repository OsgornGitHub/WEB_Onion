using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OAA.Data;
using OAA.Repo;
using OAA.Service.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OAA.Tests
{
    public class TrackUnitTest
    {
        private static string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        private static DbContextOptions<OAA.Data.ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;
        private static ApplicationContext db = new ApplicationContext(options);
        private UnitOfWork unitOfWork = new UnitOfWork(db);

        [Fact]
        public void GetTopTracks()
        {
            // Arrange
            TrackService _trackService = new TrackService(unitOfWork);
            // Act
            List<Track> tracks = _trackService.GetTopTracks("RadioHead");
            // Assert
            Assert.Equal(24, tracks?.Count);
        }
    }
}
