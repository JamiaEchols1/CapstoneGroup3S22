using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.DALs.LodgingDALTests
{
    /// <summary>
    ///     Test create new lodging
    /// </summary>
    [TestClass]
    public class EditLodgingTests
    {
        #region Methods

        /// <summary>
        /// Tests the edit lodging.
        /// </summary>
        [TestMethod]
        public void TestEditLodging()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var editedLodging = new Lodging
            {
                Location = "test lodging",
                StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                EndTime = DateTime.Now.AddDays(1).AddMinutes(14),
                TripId = 0,
                Id = 0
            };

            var lodgingsData = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 2
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(14), TripId = 0, Id = 1
                },
                editedLodging
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

            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            LoggedUser.SelectedLodging = editedLodging;

            lodgingsService.EditLodging(editedLodging.Location, editedLodging.StartTime, editedLodging.EndTime, editedLodging.Description);
            var lodging = lodgingsService.GetLodgingById(editedLodging.Id);

            Assert.AreEqual(editedLodging.Location, lodging.Location);
            Assert.AreEqual(editedLodging.StartTime, lodging.StartTime);
            Assert.AreEqual(editedLodging.EndTime, lodging.EndTime);
            Assert.AreEqual(editedLodging.Description, lodging.Description);            
        }

        /// <summary>
        /// Tests the edit lodging.
        /// </summary>
        [TestMethod]
        public void TestEditLodgingNullValuesAndIncorrectDateTimes()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now.AddHours(3), EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var editedLodging = new Lodging
            {
                Location = "test lodging",
                StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                EndTime = DateTime.Now.AddDays(1).AddMinutes(14),
                TripId = 0,
                Id = 0
            };

            var lodgingsData = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0, Id = 2
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddDays(1).AddMinutes(10),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(14), TripId = 0, Id = 1
                },
                editedLodging
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

            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            LoggedUser.SelectedLodging = editedLodging;

            Assert.ThrowsException<ArgumentNullException>(() =>
               lodgingsService.EditLodging(null, DateTime.Now.AddHours(1), DateTime.Now.AddHours(2), null));
            Assert.ThrowsException<ArgumentException>(() =>
            lodgingsService.EditLodging(editedLodging.Location, DateTime.Now.AddHours(1), DateTime.Now.AddHours(2), null));
            Assert.ThrowsException<ArgumentException>(() =>
            lodgingsService.EditLodging(editedLodging.Location, DateTime.Now.AddHours(4), DateTime.Now.AddHours(2), null));
            Assert.ThrowsException<ArgumentException>(() =>
            lodgingsService.EditLodging(editedLodging.Location, DateTime.Now.AddHours(6), DateTime.Now.AddHours(5), null));

        }

        /// <summary>
        ///     Tests the empty location.
        /// </summary>
        [TestMethod]
        public void TestEmptyLocation()
        {
            LoggedUser.SelectedWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40),
                TripId = 0,
                Id = 0
            };

            var data = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDal(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.EditLodging("", DateTime.Now, DateTime.Now.AddHours(1), "desc"));
        }

        /// <summary>
        ///     Tests the null location.
        /// </summary>
        [TestMethod]
        public void TestNullLocation()
        {
            LoggedUser.SelectedWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40),
                TripId = 0,
                Id = 0
            };

            var data = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDal(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.EditLodging(null, DateTime.Now, DateTime.Now.AddHours(1), "desc"));
        }

        /// <summary>
        ///     Tests the start date after end date.
        /// </summary>
        [TestMethod]
        public void TestStartDateAfterEndDate()
        {
            LoggedUser.SelectedWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40),
                TripId = 0,
                Id = 0
            };
            LoggedUser.SelectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDal(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() =>
                service.EditLodging("location", DateTime.Now.AddMinutes(30), DateTime.Now, "desc"));
            LoggedUser.SelectedWaypoint = null;
            LoggedUser.SelectedTrip = null;
        }

        /// <summary>
        ///     Tests the end date after trip end date.
        /// </summary>
        [TestMethod]
        public void TestEndDateAfterTripEndDate()
        {
            LoggedUser.SelectedWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40),
                TripId = 0,
                Id = 0
            };
            LoggedUser.SelectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDal(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() =>
                service.EditLodging("location", DateTime.Now.AddMinutes(30), DateTime.Now.AddDays(15), "desc"));
            LoggedUser.SelectedWaypoint = null;
            LoggedUser.SelectedTrip = null;
        }

        /// <summary>
        ///     Tests the start date before trip start date.
        /// </summary>
        [TestMethod]
        public void TestStartDateBeforeTripStartDate()
        {
            LoggedUser.SelectedWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40),
                TripId = 0,
                Id = 0
            };
            LoggedUser.SelectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            var data = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDal(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() =>
                service.EditLodging("location", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(5), "desc"));
            LoggedUser.SelectedWaypoint = null;
            LoggedUser.SelectedTrip = null;
        }

        /// <summary>
        ///     Tests the add valid lodging.
        /// </summary>
        [TestMethod]
        public void TestEditValidLodging()
        {
            LoggedUser.SelectedWaypoint = new Waypoint
            {
                Location = "Nowhere",
                StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40),
                TripId = 0,
                Id = 0
            };
            LoggedUser.SelectedTrip = new Trip
            { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };

            Lodging testLodging = new Lodging
            {
                Location = "test lodging",
                StartTime = DateTime.Now.AddMinutes(10),
                EndTime = DateTime.Now.AddMinutes(14),
                TripId = 1,
                Id = 0
            };
            var data = new List<Lodging> {
                testLodging,
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            }.AsQueryable();

            LoggedUser.SelectedLodging = testLodging;
            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);

            var service = new LodgingDal(mockContext.Object);

            LoggedUser.SelectedLodging = service.EditLodging("location", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), "desc");

            Assert.AreEqual("location", LoggedUser.SelectedLodging.Location);
            LoggedUser.SelectedWaypoint = null;
            LoggedUser.SelectedTrip = null;
            LoggedUser.SelectedLodging = null;
        }

        #endregion
    }
}