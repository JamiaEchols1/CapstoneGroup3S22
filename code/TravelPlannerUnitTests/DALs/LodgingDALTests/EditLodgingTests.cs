using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.LodgingDALTests
{
    [TestClass]
    public class EditLodgingTests
    {

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
                service.EditLodging("", DateTime.Now, DateTime.Now.AddHours(1),  "desc"));
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
                service.EditLodging(null, DateTime.Now, DateTime.Now.AddHours(1),  "desc"));
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
                service.EditLodging("location", DateTime.Now.AddMinutes(30), DateTime.Now,  "desc"));
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

    }
}
