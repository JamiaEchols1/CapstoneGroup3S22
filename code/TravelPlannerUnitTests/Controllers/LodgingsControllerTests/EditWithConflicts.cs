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

namespace TravelPlannerUnitTests.Controllers.LodgingsControllerTests
{
    /// <summary>
    ///  Edit with conflicts test coverage
    /// </summary>
    [TestClass]
    public class EditWithConflicts
    {
        #region Methods

        /// <summary>
        ///     Tests the GET edit with no error messages.
        /// </summary>
        [TestMethod]
        public void TestGETEditWithConflictsNoErrorMessage()
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
                    Location = "test lodging 1", StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(14), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetLodgings = new Mock<DbSet<Lodging>>();
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingsData.Provider);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingsData.Expression);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingsData.ElementType);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator())
                           .Returns(lodgingsData.GetEnumerator());

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
                StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                EndTime = DateTime.Now.AddDays(1).AddMinutes(14),
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

            var result = controller.Edit(addedLodging);
            result = controller.EditWithConflicts(0, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            result = controller.EditWithConflicts(null, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the post edit with conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTEditWithConflicts()
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
                    Location = "test lodging 1", StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(14), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetLodgings = new Mock<DbSet<Lodging>>();
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(lodgingsData.Provider);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(lodgingsData.Expression);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(lodgingsData.ElementType);
            mockSetLodgings.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator())
                           .Returns(lodgingsData.GetEnumerator());

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
                StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                EndTime = DateTime.Now.AddDays(1).AddMinutes(14),
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

            var result = controller.Edit(addedLodging);
            result = controller.EditWithConflicts(addedLodging);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));


            controller.ModelState.AddModelError("Mega", "Error");
            result = controller.EditWithConflicts(addedLodging);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        #endregion
    }
}