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
    public class SimilarUnitTest
    {

        private static string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        private static DbContextOptions<OAA.Data.ApplicationContext> options = optionsBuilder.UseSqlServer(connectionString).Options;
        private static ApplicationContext db = new ApplicationContext(options);
        private UnitOfWork unitOfWork = new UnitOfWork(db);

        [Fact]
        public void GetListSimilar()
        {
            // Arrange
            SimilarService _simialrService = new SimilarService(unitOfWork);
            // Act
            List<Similar> similar = _simialrService.GetListSimilar("Drake");
            // Assert
            Assert.Equal(12, similar?.Count);
        }

    }
}
