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
        [Fact]
        public void GetTopAlbum()
        {
            // Arrange
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
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
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
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
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
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
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
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
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            AlbumService _albumService = new AlbumService(unitOfWork);
            // Act
            Album album = _albumService.GetAlbum("Drake", "Take Care");
            // Assert
            Assert.Equal(17, album?.Tracks.Count);
        }
    }
}
