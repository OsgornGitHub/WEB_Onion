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
        [Fact]
        public void GetListSimilar()
        {
            // Arrange
            string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=oniondb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            ApplicationContext db = new ApplicationContext(options);
            UnitOfWork unitOfWork = new UnitOfWork(db);
            SimilarService _simialrService = new SimilarService(unitOfWork);
            // Act
            List<Similar> similar = _simialrService.GetListSimilar("Drake");
            // Assert
            Assert.Equal(12, similar?.Count);
        }

    }
}
