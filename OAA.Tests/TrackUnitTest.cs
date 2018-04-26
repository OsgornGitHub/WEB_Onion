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
        [Fact]
        public void GetTopTracks()
        {
            // Arrange
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            TrackService _trackService = new TrackService(unitOfWork);
            // Act
            List<Track> tracks = _trackService.GetTopTracks("RadioHead");
            // Assert
            Assert.Equal(24, tracks?.Count);
        }
    }
}
