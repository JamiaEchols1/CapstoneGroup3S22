using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using System.Web.Services.Description;

namespace TravelPlannerUnitTests.TransportationDALTests
{
    [TestClass]
    public class CreateNewTransportationOverrideTests
    {

        [TestMethod]
        public void TestNullDescription()
        {
            Waypoint waypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40), EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0 };
            var data = new List<Transportation>
            {
                new Transportation { Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId= 2, Id = 0},
                new Transportation { Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId= 3, Id = 1},
                new Transportation { Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId= 1, Id = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDAL(mockContext.Object);

            Transportation transportation = new Transportation { Waypoint = waypoint, Description = null, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(-7), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 0 };

            Assert.ThrowsException<ArgumentNullException>(() => service.CreateANewTransportation(transportation));
        }

        [TestMethod]
        public void TestEmptyDescription()
        {
            Waypoint waypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40), EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0 };
            var data = new List<Transportation>
            {
                new Transportation { Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId= 2, Id = 0},
                new Transportation { Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId= 3, Id = 1},
                new Transportation { Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId= 1, Id = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDAL(mockContext.Object);

            Transportation transportation = new Transportation { Waypoint = waypoint, Description = "", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(-7), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 0 };

            Assert.ThrowsException<ArgumentNullException>(() => service.CreateANewTransportation(transportation));
        }

        [TestMethod]
        public void TestSameDepartureAndArrivalWaypoint()
        {
            Waypoint waypoint = new Waypoint
            {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0
            };
            var data = new List<Transportation>
            {
                new Transportation
                {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation
                {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation
                {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            var service = new TransportationDAL(mockContext.Object);

            Transportation transportation = new Transportation
            {
                Waypoint = waypoint, Description = "Description", StartTime = DateTime.Now.AddMinutes(5),
                EndTime = DateTime.Now.AddMinutes(15), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 1
            };

            Assert.ThrowsException<ArgumentException>(() => service.CreateANewTransportation(transportation));
        }

        [TestMethod]
        public void TestStartTimeAfterEndTime()
        {
            Waypoint waypoint = new Waypoint
            {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(5), TripId = 0, Id = 0
            };
            var data = new List<Transportation>
            {
                new Transportation
                {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation
                {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation
                {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            var service = new TransportationDAL(mockContext.Object);

            Transportation transportation = new Transportation
            {
                Waypoint = waypoint, Description = "Description", StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(-7), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 0
            };

            Assert.ThrowsException<ArgumentException>(() => service.CreateANewTransportation(transportation));
        }

        [TestMethod]
        public void TestStartTimeBeforeWaypointEndTime()
        {
            LoggedUser.selectedWaypoint = new Waypoint
            {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(4),
                EndDateTime = DateTime.Now.AddMinutes(34), TripId = 0, Id = 0
            };
            var data = new List<Transportation>
            {
                new Transportation
                {
                    Description = "test transportation", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation
                {
                    Description = "test transportation 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation
                {
                    Description = "test transportation 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDAL(mockContext.Object);
            Transportation transportation = new Transportation
            {
                Description = "Description", StartTime = DateTime.Now.AddMinutes(33),
                EndTime = DateTime.Now.AddMinutes(40), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 0
            };

            Assert.ThrowsException<ArgumentException>(() => service.CreateANewTransportation(transportation));
            LoggedUser.selectedWaypoint = null;
        }

        [TestMethod]
        public void TestValidTransportation()
        {
            LoggedUser.selectedWaypoint = new Waypoint
            {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0
            };
            var data = new List<Transportation>
            {
                new Transportation
                {
                    Description = "test transportation", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation
                {
                    Description = "test transportation 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation
                {
                    Description = "test transportation 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            Boolean wasCalled = false;
            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var service = new TransportationDAL(mockContext.Object);
            Transportation transportation = new Transportation
            {
                Description = "Description", StartTime = DateTime.Now.AddMinutes(45),
                EndTime = DateTime.Now.AddMinutes(50), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 0
            };

            service.CreateANewTransportation(transportation);

            Assert.IsTrue(wasCalled);
            LoggedUser.selectedWaypoint = null;
        }
    }
}
