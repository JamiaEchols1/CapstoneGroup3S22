using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace TravelPlannerUnitTests.Controllers.WaypointsControllerTests
{
    /// <summary>
    ///     The web travel planner waypoints controller tests for creating a waypoint
    /// </summary>
    [TestClass]
    public class CreateTests
    {
        #region Methods

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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            const string errorMessage = "Serious overlap issues";
            var result = controller.Create(null, errorMessage);
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var conflictingWaypoint = new Waypoint {
                Location = "Nowhere",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var addedWaypoint = new AddedWaypoint {
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

            var waypoint = new Waypoint {
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);

            var newWaypoint = new Waypoint {
                Location = "there",
                StartDateTime = DateTime.Now.AddDays(5),
                EndDateTime = DateTime.Now.AddDays(10),
                TripId = 0,
                Description = "No"
            };
            var addedWaypoint = new AddedWaypoint {
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
        ///     Tests the POST: create valid waypoint without conflicts.
        /// </summary>
        [TestMethod]
        public void TestPostCreateWaypointWithoutExistingWaypoints()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypointData = new List<Waypoint> {            
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
        }

        /// <summary>
        ///     Tests the post create waypoint with transportation time conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointWithPreviousWaypointTimeConflicts()
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
                    Location = "Las Vegas Airport", StartDateTime = DateTime.Now.AddDays(6),
                    EndDateTime = DateTime.Now.AddDays(6).AddHours(18), TripId = 0, Id = 1
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(9),
                    Origin = "Las Vegas Airport", Destination = "UWG Bookstore Carrollton GA 30117", Type = "WALKING",
                    EndTime = DateTime.Now.AddDays(10), TripId = 0,
                    Id = 0
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);

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
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(7),
                EndDateTime = DateTime.Now.AddDays(8).AddHours(18),
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
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the post create waypoint with transportation time conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointWithNextTransportationTimeConflicts()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypoint = new Waypoint
            {
                Location = "Aldi Carrollton GA 30117",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var waypointData = new List<Waypoint> {
                waypoint,
                new Waypoint {
                    Location = "Aldi Carrollton GA 30117", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 2
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(9),
                    Origin = "Las Vegas Airport", Destination = "UWG Bookstore Carrollton GA 30117", Type = "WALKING",
                    EndTime = DateTime.Now.AddDays(10), TripId = 0,
                    Id = 0
                }      
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);

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
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(7),
                EndDateTime = DateTime.Now.AddDays(8).AddHours(18),
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
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the post create waypoint with transportation time conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointWithNextWaypointTimeConflicts()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypoint = new Waypoint
            {
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var waypointData = new List<Waypoint> {
                waypoint,
                new Waypoint {
                    Location = "Las Vegas Airport", StartDateTime = DateTime.Now.AddDays(6),
                    EndDateTime = DateTime.Now.AddDays(6).AddMinutes(120), TripId = 0, Id = 1
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(9),
                    Origin = "Las Vegas Airport", Destination = "UWG Bookstore Carrollton GA 30117", Type = "WALKING",
                    EndTime = DateTime.Now.AddDays(10), TripId = 0,
                    Id = 0
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);

            var controller = new WaypointsController(tripService, waypointService, transportationService);
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
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(5),
                EndDateTime = DateTime.Now.AddDays(5).AddHours(18),
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
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the post create waypoint with transportation time conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointWithPreviousTransportationTimeConflicts()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypoint = new Waypoint
            {
                Location = "Walmart Carrollton GA 30117",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var waypointData = new List<Waypoint> {
                waypoint,
                new Waypoint {
                    Location = "Las Vegas Airport", StartDateTime = DateTime.Now.AddDays(6),
                    EndDateTime = DateTime.Now.AddDays(6).AddMinutes(120), TripId = 0, Id = 1
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(6),
                    Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Las Vegas Airport", Type = "WALKING",
                    EndTime = DateTime.Now.AddDays(6).AddHours(4), TripId = 0,
                    Id = 0
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);

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
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(7),
                EndDateTime = DateTime.Now.AddDays(8).AddHours(18),
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
        }

        /// <summary>
        ///     Tests the POST: create waypoint with invalid start/end datetime.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWaypointWithInvalidStartEndDateTimes()
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
                EndDateTime = DateTime.Now.AddDays(5),
                StartDateTime = DateTime.Now.AddDays(10),
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

            var waypoint = new Waypoint {
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);

            var newWaypoint = new Waypoint {
                Location = "there",
                StartDateTime = DateTime.Now.AddDays(5),
                EndDateTime = DateTime.Now.AddDays(10),
                TripId = 0,
                Description = "No"
            };
            var addedWaypoint = new AddedWaypoint {
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


        /// <summary>
        ///     Tests the post create with time verification functionality.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWithTimeVerification()
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
                    Location = "Las Vegas Airport", StartDateTime = DateTime.Now.AddDays(6),
                    EndDateTime = DateTime.Now.AddDays(6).AddHours(18), TripId = 0, Id = 1
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(9),
                    Origin = "Las Vegas Airport", Destination = "UWG Bookstore Carrollton GA 30117", Type = "WALKING",
                    EndTime = DateTime.Now.AddDays(10), TripId = 0,
                    Id = 0
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var waypointService = new WaypointDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);

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
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(7),
                EndDateTime = DateTime.Now.AddDays(8).AddHours(18),
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
            var model = new VerifyTimeViewModel()
            {
                waypoint = addedWaypoint,
            };
            controller.Create(addedWaypoint);
            var result = controller.CreateWithTimeVerification(model);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        #endregion
    }
}