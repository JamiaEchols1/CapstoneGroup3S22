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

namespace TravelPlannerUnitTests.Controllers.TripsControllerTests
{
    /// <summary>
    ///     The details tests for the web trips controller
    /// </summary>
    [TestClass]
    public class DetailsTests
    {
        #region Methods

        /// <summary>
        ///     Tests the GET: details view for a trip.
        /// </summary>
        [TestMethod]
        public void TestTripDetails()
        {
            LoggedUser.User = new User { Id = 0 };

            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
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

            var lodging = new Lodging {
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var tripService = new TripDal(mockContext.Object);
            var waypointService = new WaypointDal(mockContext.Object);
            var lodgingService = new LodgingDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);
            LoggedUser.SelectedTrip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var controller = new TripsController(tripService, waypointService, lodgingService, transportationService);
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
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
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

            var lodging = new Lodging {
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var tripService = new TripDal(mockContext.Object);
            var waypointService = new WaypointDal(mockContext.Object);
            var lodgingService = new LodgingDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);
            LoggedUser.SelectedTrip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var controller = new TripsController(tripService, waypointService, lodgingService, transportationService);
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
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
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

            var lodging = new Lodging {
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

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var tripService = new TripDal(mockContext.Object);
            var waypointService = new WaypointDal(mockContext.Object);
            var lodgingService = new LodgingDal(mockContext.Object);
            var transportationService = new TransportationDal(mockContext.Object);
            LoggedUser.SelectedTrip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var controller = new TripsController(tripService, waypointService, lodgingService, transportationService);
            var result = controller.Details(-1);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            controller = new TripsController();
        }
        #endregion
    }
}