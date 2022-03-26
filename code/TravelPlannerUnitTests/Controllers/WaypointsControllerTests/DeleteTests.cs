using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;

namespace TravelPlannerUnitTests.Controllers.WaypointsControllerTests
{
    /// <summary>
    ///     The web waypoints controller delete controller tests
    /// </summary>
    [TestClass]
    public class DeleteTests
    {
        /// <summary>
        ///     Tests the delete with null identifier.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithNullID()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypointData = new List<Waypoint> {
                new Waypoint {
                    Location = "Nowhere", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 0
                },
                new Waypoint {
                    Location = "Somewhere", StartDateTime = DateTime.Now.AddDays(1),
                    EndDateTime = DateTime.Now.AddDays(1).AddMinutes(120), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockSetWaypoint = new Mock<DbSet<Waypoint>>();
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Waypoints).Returns(mockSetWaypoint.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new WaypointsController(tripService, waypointService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };            
            var result = controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the GET delete functionality using null waypoint.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithNullWaypoint()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypointData = new List<Waypoint> {
                new Waypoint {
                    Location = "Nowhere", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 0
                },
                new Waypoint {
                    Location = "Somewhere", StartDateTime = DateTime.Now.AddDays(1),
                    EndDateTime = DateTime.Now.AddDays(1).AddMinutes(120), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockSetWaypoint = new Mock<DbSet<Waypoint>>();
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Waypoints).Returns(mockSetWaypoint.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new WaypointsController(tripService, waypointService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            var result = controller.Delete(-1);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the GET delete functionality using valid waypoint reference.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithValidWaypoint()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypointData = new List<Waypoint> {
                new Waypoint {
                    Location = "Nowhere", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 0
                },
                new Waypoint {
                    Location = "Somewhere", StartDateTime = DateTime.Now.AddDays(1),
                    EndDateTime = DateTime.Now.AddDays(1).AddMinutes(120), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockSetWaypoint = new Mock<DbSet<Waypoint>>();
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Waypoints).Returns(mockSetWaypoint.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new WaypointsController(tripService, waypointService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            var result = controller.Delete(0);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the GET deleteConfirmed functionality using valid waypoint reference.
        /// </summary>
        [TestMethod]
        public void TestDeleteConfirmedWithValidWaypoint()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypointData = new List<Waypoint> {
                new Waypoint {
                    Location = "Nowhere", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 0
                },
                new Waypoint {
                    Location = "Somewhere", StartDateTime = DateTime.Now.AddDays(1),
                    EndDateTime = DateTime.Now.AddDays(1).AddMinutes(120), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockSetWaypoint = new Mock<DbSet<Waypoint>>();
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Waypoints).Returns(mockSetWaypoint.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new WaypointsController(tripService, waypointService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            var result = controller.DeleteConfirmed(0);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
