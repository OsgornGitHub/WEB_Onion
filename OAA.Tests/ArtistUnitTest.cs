using Microsoft.EntityFrameworkCore;
using OAA.Data;
using OAA.Repo;
using OAA.Service.Service;
using System;
using Xunit;

namespace OAA.Tests
{
    public class ArtistUnitTest
    {
        [Fact]
        public void GetNextPage()
        {
            // Arrange
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            ArtistService _artistService = new ArtistService(unitOfWork);
            // Act
            var listArtist = _artistService.GetNextPage(1, 24);
            // Assert
            Assert.Equal(24, listArtist?.Count);
        }

        [Fact]
        public void GetArtist()
        {
            // Arrange
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            ArtistService _artistService = new ArtistService(unitOfWork);
            // Act
            Artist artist = _artistService.GetArtist("Drake");
            // Assert
            Assert.Equal("Drake", artist?.Name);
        }

        [Fact]
        public void GetCountPageTopArtist()
        {
            // Arrange
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            ArtistService _artistService = new ArtistService(unitOfWork);
            // Act
            int count = _artistService.GetCountPageTopArtist(1, 24);
            // Assert
            Assert.InRange(count, 400, 450);
        }
    }
}
