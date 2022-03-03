using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.TripDALTests
{
    [TestClass]
    public class GetTripsTests
    {
        [TestMethod]
        public void TestGetTrips()
        {
            var data = new List<Trip>
            {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0},
                new Trip { Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0},
                new Trip { Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 1},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDAL(mockContext.Object);
            var trips = service.GetTrips(0);

            Assert.AreEqual(2, trips.Count);
            Assert.AreEqual(0, trips[0].Id);
            Assert.AreEqual(0, trips[1].Id);
        }
    }
}
