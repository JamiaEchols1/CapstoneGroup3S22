using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.LodgingDALTests
{
    [TestClass]
    public class GetOverlappingLodgingsTests
    {
        [TestMethod]
        public void TestGetOverlappingLodgings()
        {
            LoggedUser.selectedTrip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            var data = new List<Lodging>
            {
                new Lodging { Location = "Test Lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 0},
                new Lodging { Location = "Test Lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 1},
                new Lodging { Location = "Test Lodging 3", StartTime = DateTime.Now.AddMinutes(15), EndTime = DateTime.Now.AddMinutes(25), TripId = 1, Id = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);

            var service = new LodgingDAL(mockContext.Object);

            Assert.AreEqual(2, service.GetOverlappingLodging(DateTime.Now, DateTime.Now.AddMinutes(30)).Count());
            LoggedUser.selectedWaypoint = null;
            LoggedUser.selectedTrip = null;
        }
    }
}
