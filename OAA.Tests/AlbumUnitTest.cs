using Microsoft.EntityFrameworkCore;
using OAA.Data;
using OAA.Repo;
using OAA.Service.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OAA.Tests
{
    public class AlbumUnitTest
    {
        private static string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        private static  DbContextOptions<OAA.Data.ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;
        private static ApplicationContext db = new ApplicationContext(options);
        private UnitOfWork unitOfWork = new UnitOfWork(db);

        [Fact]
        public void GetTopAlbum()
        {
            // Arrange
            AlbumService _albumService = new AlbumService(unitOfWork);
            // Act
            var listAlbum = _albumService.GetTopAlbum("Drake", 1, 24);
            // Assert
            Assert.Equal(24, listAlbum?.Count);
        }
        [Fact]
        public void IsValidAlbumFalseEmtyString()
        {
            // Arrange
            AlbumService _albumService = new AlbumService(unitOfWork);            
            // Act
            bool result = _albumService.IsValidAlbum("" , "test");
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidAlbumFalseNull()
        {
            // Arrange
            AlbumService _albumService = new AlbumService(unitOfWork);
            // Act
            bool result = _albumService.IsValidAlbum("null", "test");
            // Assert
            Assert.False(result);
        }
        [Fact]
        public void IsValidAlbumTrue()
        {
            // Arrange
            UnitOfWork unitOfWork = new UnitOfWork(db);
            AlbumService _albumService = new AlbumService(unitOfWork);
            // Act
            bool result = _albumService.IsValidAlbum("test", "test");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAlbum()
        {
            // Arrange
            AlbumService _albumService = new AlbumService(unitOfWork);
            // Act
            Album album = _albumService.GetAlbum("Drake", "Take Care");
            // Assert
            Assert.Equal(17, album?.Tracks.Count);
        }
    }
}
