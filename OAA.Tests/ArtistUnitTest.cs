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
        private static string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        private static DbContextOptions<OAA.Data.ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;
        private static ApplicationContext db = new ApplicationContext(options);
        private UnitOfWork unitOfWork = new UnitOfWork(db);

        [Fact]
        public void GetNextPage()
        {
            // Arrange
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
            ArtistService _artistService = new ArtistService(unitOfWork);
            // Act
            int count = _artistService.GetCountPageTopArtist(1, 24);
            // Assert
            Assert.InRange(count, 400, 450);
        }
    }
}
