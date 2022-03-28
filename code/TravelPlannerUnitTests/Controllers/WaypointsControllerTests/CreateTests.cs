using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers.WaypointsControllerTests
{
    /// <summary>
    ///     The web travel planner waypoints controller tests for creating a waypoint
    /// </summary>
    [TestClass]
    public class CreateTests
    {
        /// <summary>
        ///     Tests the GET: create waypoint with null trip identifier.
        /// </summary>
        [TestMethod]
        public void TestGETCreateWaypointIncludingNullTripID()
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
            var ErrorMessage = "Serious overlap issues";
            var result = controller.Create(null, ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the POST: create waypoint.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointOverlappingIssue()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypointData = new List<Waypoint> {               
                new Waypoint {
                    Location = "Out here", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 2
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

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var conflictingWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var addedWaypoint = new AddedWaypoint
            {
                Id = conflictingWaypoint.Id,
                StartDateTime = conflictingWaypoint.StartDateTime,
                EndDateTime = conflictingWaypoint.EndDateTime,
                Location = conflictingWaypoint.Location,
                TripId = conflictingWaypoint.Id,
                Description = conflictingWaypoint.Description
            };
            var result = controller.Create(addedWaypoint);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the POST: create valid waypoint without conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypoint()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var waypointData = new List<Waypoint> {
                waypoint,
                new Waypoint {
                    Location = "Out here", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 2
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

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);

            var newWaypoint = new Waypoint
            {
                Location = "there",
                StartDateTime = DateTime.Now.AddDays(5),
                EndDateTime = DateTime.Now.AddDays(10),
                TripId = 0,
                Description = "No"
            };
            var addedWaypoint = new AddedWaypoint
            {
                Id = newWaypoint.Id,
                StartDateTime = newWaypoint.StartDateTime,
                EndDateTime = newWaypoint.EndDateTime,
                Location = newWaypoint.Location,
                TripId = newWaypoint.Id,
                Description = newWaypoint.Description
            };
            var result = controller.Create(addedWaypoint);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            controller.ModelState.AddModelError("Mega", "Error");
        }

        /// <summary>
        ///     Tests the POST: create valid waypoint with invalid model state.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointWithInvalidModelState()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var waypointData = new List<Waypoint> {
                waypoint,
                new Waypoint {
                    Location = "Out here", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 2
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

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);

            var newWaypoint = new Waypoint
            {
                Location = "there",
                StartDateTime = DateTime.Now.AddDays(5),
                EndDateTime = DateTime.Now.AddDays(10),
                TripId = 0,
                Description = "No"
            };
            var addedWaypoint = new AddedWaypoint
            {
                Id = newWaypoint.Id,
                StartDateTime = newWaypoint.StartDateTime,
                EndDateTime = newWaypoint.EndDateTime,
                Location = newWaypoint.Location,
                TripId = newWaypoint.Id,
                Description = newWaypoint.Description
            };
            controller.ModelState.AddModelError("Mega", "Error");
            var result = controller.Create(addedWaypoint);              
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
