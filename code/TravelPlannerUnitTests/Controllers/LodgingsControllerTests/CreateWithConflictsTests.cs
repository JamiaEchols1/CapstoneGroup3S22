using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers.LodgingsControllerTests
{
    /// <summary>
    ///     Tests for the lodging controller : CreateWithConflicts GET and POST
    /// </summary>
    [TestClass]
    public class CreateWithConflictsTests
    {
        /// <summary>
        ///     Tests the GET: create lodging including null trip identifier.
        /// </summary>
        [TestMethod]
        public void TestGETCreateLodgingIncludingNullTripID()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var lodgingsData = new List<Lodging> {
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
                }
            }.AsQueryable();

            var mockSetLodgings = new Mock<DbSet<Lodging>>();
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingsData.Provider);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingsData.Expression);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingsData.ElementType);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(lodgingsData.GetEnumerator());

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSetLodgings.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var lodgingsService = new LodgingDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new LodgingsController(tripService, lodgingsService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            var result = controller.CreateWithConflicts(null, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the POST: CreateWithConflicts
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWithConflicts()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var lodgingsData = new List<Lodging> {
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
                }
            }.AsQueryable();

            var mockSetLodgings = new Mock<DbSet<Lodging>>();
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingsData.Provider);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingsData.Expression);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingsData.ElementType);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(lodgingsData.GetEnumerator());

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSetLodgings.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var lodgingsService = new LodgingDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new LodgingsController(tripService, lodgingsService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var conflictingLodging = new Lodging
            {
                Location = "test lodging",
                StartTime = DateTime.Now.AddMinutes(10),
                EndTime = DateTime.Now.AddMinutes(14),
                TripId = 0,
                Id = 0
            };

            var addedLodging = new AddedLodging
            {
                Location = conflictingLodging.Location,
                StartTime = conflictingLodging.StartTime,
                EndTime = conflictingLodging.EndTime,
                TripId = conflictingLodging.TripId,
                Description = "description",
                Id = 0
            };

            var result = controller.CreateWithConflicts(addedLodging);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the POST: create with an invalid model state.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateWithInvalidModelState()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var lodgingsData = new List<Lodging> {
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
                }
            }.AsQueryable();

            var mockSetLodgings = new Mock<DbSet<Lodging>>();
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingsData.Provider);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingsData.Expression);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingsData.ElementType);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(lodgingsData.GetEnumerator());

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSetLodgings.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var lodgingsService = new LodgingDal(mockContext.Object);
            var tripService = new TripDal(mockContext.Object);

            var controller = new LodgingsController(tripService, lodgingsService);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var conflictingLodging = new Lodging
            {
                Location = "test lodging",
                StartTime = DateTime.Now.AddMinutes(100),
                EndTime = DateTime.Now.AddMinutes(200),
                TripId = 0,
                Id = 0
            };

            var addedLodging = new AddedLodging
            {
                Location = conflictingLodging.Location,
                StartTime = conflictingLodging.StartTime,
                EndTime = conflictingLodging.EndTime,
                TripId = conflictingLodging.TripId,
                Description = "description",
                Id = 0

            };
            controller.ModelState.AddModelError("Mega", "Error");
            var result = controller.CreateWithConflicts(addedLodging);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
