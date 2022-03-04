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
    public class CreateNewLodgingTests
    {
        [TestMethod]
        public void TestEmptyLocation()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDAL(mockContext.Object);

           
            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CreateNewLodging("", DateTime.Now, DateTime.Now.AddHours(1), 0));
        }

        [TestMethod]
        public void TestNullLocation()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDAL(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CreateNewLodging(null, DateTime.Now, DateTime.Now.AddHours(1), 0));
        }

        [TestMethod]
        public void TestStartDateAfterEndDate()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            LoggedUser.selectedTrip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDAL(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateNewLodging("location", DateTime.Now.AddMinutes(30), DateTime.Now, 0));
            LoggedUser.selectedWaypoint = null;
            LoggedUser.selectedTrip = null;
        }

        [TestMethod]
        public void TestEndDateAfterTripEndDate()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            LoggedUser.selectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDAL(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateNewLodging("location", DateTime.Now.AddMinutes(30), DateTime.Now.AddDays(15), 0));
            LoggedUser.selectedWaypoint = null;
            LoggedUser.selectedTrip = null;
        }

        [TestMethod]
        public void TestStartDateBeforeTripStartDate()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            LoggedUser.selectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDAL(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateNewLodging("location", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(5), 0));
            LoggedUser.selectedWaypoint = null;
            LoggedUser.selectedTrip = null;
        }

        [TestMethod]
        public void TestAddValidLodging()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            LoggedUser.selectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            Boolean wasCalled = false;
            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var service = new LodgingDAL(mockContext.Object);

            service.CreateNewLodging("location", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), 0);

            Assert.IsTrue(wasCalled); 
            LoggedUser.selectedWaypoint = null;
            LoggedUser.selectedTrip = null;
        }
    }
}
