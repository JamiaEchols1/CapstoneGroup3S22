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
    ///     Edit tests for the lodgings controller
    /// </summary>
    [TestClass]
    public class EditTests
    {
        #region Methods

        /// <summary>
        ///     Tests the GET edit with no error messages.
        /// </summary>
        [TestMethod]
        public void TestGETEditWithNoErrorMessage()
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            var result = controller.Edit(0, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the get edit with error message.
        /// </summary>
        [TestMethod]
        public void TestGETEditWithErrorMessage()
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
            const string errorMessage = "Serious overlap issues";
            var result = controller.Edit(0, errorMessage);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the post edit lodging overlapping issue.
        /// </summary>
        [TestMethod]
        public void TestPOSTEditLodgingOverlappingIssue()
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var conflictingLodging = new Lodging {
                Location = "test lodging",
                StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                EndTime = DateTime.Now.AddDays(1).AddMinutes(14),
                TripId = 0,
                Id = 0
            };

            var addedLodging = new AddedLodging {
                Location = conflictingLodging.Location,
                StartTime = conflictingLodging.StartTime,
                EndTime = conflictingLodging.EndTime,
                TripId = conflictingLodging.TripId,
                Description = "description",
                Id = 0
            };

            var result = controller.Edit(addedLodging);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the post edit lodging without conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTEditLodgingWithoutConflicts()
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
                StartTime = DateTime.Now.AddMinutes(1),
                EndTime = DateTime.Now.AddMinutes(2),
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
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Tests the post edit lodging invalid start end date times.
        /// </summary>
        [TestMethod]
        public void TestPOSTEditLodgingInvalidStartEndDateTimes()
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
                StartTime = DateTime.Now.AddMinutes(14),
                EndTime = DateTime.Now.AddMinutes(10),
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
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            controller.ModelState.AddModelError("Mega", "Error");
            result = controller.Edit(addedLodging);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the update null lodging.
        /// </summary>
        [TestMethod]
        public void TestUpdateNullLodging()
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
                StartTime = DateTime.Now.AddMinutes(14),
                EndTime = DateTime.Now.AddMinutes(10),
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

            var nullCheck = lodgingsService.WebUpdateLodging(null);
            Assert.AreEqual(nullCheck, null);
        }

        #endregion
    }
}