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
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers
{
    /// <summary>
    ///     Tests for the travel planner web trip controller
    /// </summary>
    [TestClass]
    public class TripControllerTests
    {
        /// <summary>
        ///     Tests the GET: index of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestTripIndex()
        {
            LoggedUser.User = new User { Id = 0 };

            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 0
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the GET: details view for a trip.
        /// </summary>
        [TestMethod]
        public void TestTripDetails()
        {
            LoggedUser.User = new User { Id = 0 };

            var tripData = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 }
            }.AsQueryable();

            var tripMockSet = new Mock<DbSet<Trip>>();
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(tripMockSet.Object);

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

            var waypointMockSet = new Mock<DbSet<Waypoint>>();
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            mockContext.Setup(c => c.Waypoints).Returns(waypointMockSet.Object);

            var lodging = new Lodging
            {
                Location = "Lodging",
                StartTime = DateTime.Now.AddMinutes(45),
                EndTime = DateTime.Now.AddMinutes(50),
                TripId = 2
            };

            var lodgingData = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 2
                },
                lodging
            }.AsQueryable();

            var lodgingMockSet = new Mock<DbSet<Lodging>>();
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingData.Provider);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingData.Expression);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingData.ElementType);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(lodgingData.GetEnumerator());

            mockContext.Setup(c => c.Lodgings).Returns(lodgingMockSet.Object);

            var tripService = new TripDal(mockContext.Object);
            var waypointService = new WaypointDal(mockContext.Object);
            var lodgingService = new LodgingDal(mockContext.Object);
            LoggedUser.SelectedTrip = new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var controller = new TripsController(tripService, waypointService, lodgingService);
            var result = controller.Details(0);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the GET: details view for a trip when the Id is not found.
        /// </summary>
        [TestMethod]
        public void TestNullTripDetails()
        {
            LoggedUser.User = new User { Id = 0 };

            var tripData = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 }
            }.AsQueryable();

            var tripMockSet = new Mock<DbSet<Trip>>();
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(tripMockSet.Object);

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

            var waypointMockSet = new Mock<DbSet<Waypoint>>();
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            mockContext.Setup(c => c.Waypoints).Returns(waypointMockSet.Object);

            var lodging = new Lodging
            {
                Location = "Lodging",
                StartTime = DateTime.Now.AddMinutes(45),
                EndTime = DateTime.Now.AddMinutes(50),
                TripId = 2
            };

            var lodgingData = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 2
                },
                lodging
            }.AsQueryable();

            var lodgingMockSet = new Mock<DbSet<Lodging>>();
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingData.Provider);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingData.Expression);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingData.ElementType);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(lodgingData.GetEnumerator());

            mockContext.Setup(c => c.Lodgings).Returns(lodgingMockSet.Object);

            var tripService = new TripDal(mockContext.Object);
            var waypointService = new WaypointDal(mockContext.Object);
            var lodgingService = new LodgingDal(mockContext.Object);
            LoggedUser.SelectedTrip = new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var controller = new TripsController(tripService, waypointService, lodgingService);
            var result = controller.Details(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the GET: details view for a trip when the Id is not found.
        /// </summary>
        [TestMethod]
        public void TestNotFoundTripDetails()
        {
            LoggedUser.User = new User { Id = 0 };

            var tripData = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 }
            }.AsQueryable();

            var tripMockSet = new Mock<DbSet<Trip>>();
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            tripMockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(tripMockSet.Object);

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

            var waypointMockSet = new Mock<DbSet<Waypoint>>();
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            waypointMockSet.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            mockContext.Setup(c => c.Waypoints).Returns(waypointMockSet.Object);

            var lodging = new Lodging
            {
                Location = "Lodging",
                StartTime = DateTime.Now.AddMinutes(45),
                EndTime = DateTime.Now.AddMinutes(50),
                TripId = 2
            };

            var lodgingData = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 2
                },
                lodging
            }.AsQueryable();

            var lodgingMockSet = new Mock<DbSet<Lodging>>();
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingData.Provider);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingData.Expression);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingData.ElementType);
            lodgingMockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(lodgingData.GetEnumerator());

            mockContext.Setup(c => c.Lodgings).Returns(lodgingMockSet.Object);

            var tripService = new TripDal(mockContext.Object);
            var waypointService = new WaypointDal(mockContext.Object);
            var lodgingService = new LodgingDal(mockContext.Object);
            LoggedUser.SelectedTrip = new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var controller = new TripsController(tripService, waypointService, lodgingService);
            var result = controller.Details(-1);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            controller = new TripsController();
        }

        /// <summary>
        ///     Tests the GET: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestGETCreateTrip()
        {
            LoggedUser.User = new User { Id = 0 };

            var data = new List<Trip> {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            controller.SetErrorMessage("Invalid date");
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the POST: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTripValidStartDate()
        {
            LoggedUser.User = new User { Id = 0 };

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            var result = controller.Create(addedTrip);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the POST: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTripInvalidStartDate()
        {
            LoggedUser.User = new User { Id = 0 };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            var result = controller.Create(addedTrip);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the POST: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTripInvalidModelState()
        {
            LoggedUser.User = new User { Id = 0 };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            controller.ModelState.AddModelError("Mega", "Error");
            var result = controller.Create(addedTrip);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
