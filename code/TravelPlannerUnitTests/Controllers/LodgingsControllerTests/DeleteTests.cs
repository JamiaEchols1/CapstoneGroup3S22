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

namespace TravelPlannerUnitTests.Controllers.LodgingsControllerTests
{
    /// <summary>
    ///     Delete tests for the lodgings controller
    /// </summary>
    [TestClass]
    public class DeleteTests
    {
        /// <summary>
        ///     Tests the GET: delete with null identifier.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithNullId()
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
            var result = controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the GET: delete with an identifier that returns a null lodging.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithIdentifierThatReturnsNull()
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
            var result = controller.Delete(-1);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the GET: delete with a valid lodging identifier.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithValidIdentifier()
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
            var result = controller.Delete(0);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the delete confirmed.
        /// </summary>
        [TestMethod]
        public void TestDeleteConfirmed()
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
            var result = controller.DeleteConfirmed(0);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }



    }
}
