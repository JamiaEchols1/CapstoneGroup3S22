using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.TripDALTests
{
    /// <summary>
    ///     Tests create trip
    /// </summary>
    [TestClass]
    public class CreateTripTests
    {
        #region Methods

        /// <summary>
        ///     Tests the name of the add trip null.
        /// </summary>
        [TestMethod]
        public void TestAddTripNullName()
        {
            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 1
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
            var trip = new Trip
                { Name = null, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(-1), UserId = 0 };

            Assert.ThrowsException<ArgumentNullException>(() => service.CreateTrip(trip));
        }

        /// <summary>
        ///     Tests the empty name of the add trip.
        /// </summary>
        [TestMethod]
        public void TestAddTripEmptyName()
        {
            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 1
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
            var trip = new Trip
                { Name = "", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10), UserId = 0 };

            Assert.ThrowsException<ArgumentNullException>(() => service.CreateTrip(trip));
        }

        /// <summary>
        ///     Tests the add trip start date after end date.
        /// </summary>
        [TestMethod]
        public void TestAddTripStartDateAfterEndDate()
        {
            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 1
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

            var trip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now.AddDays(7), EndDate = DateTime.Now.AddDays(6), UserId = 0 };

            Assert.ThrowsException<ArgumentException>(() => service.CreateTrip(trip));
        }

        /// <summary>
        ///     Tests the add trip start date after today.
        /// </summary>
        [TestMethod]
        public void TestAddTripStartDateAfterToday()
        {
            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 1
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

            var trip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1), UserId = 0 };

            Assert.ThrowsException<ArgumentException>(() => service.CreateTrip(trip));
        }

        /// <summary>
        ///     Tests the add vaild trip.
        /// </summary>
        [TestMethod]
        public void TestAddVaildTrip()
        {
            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);
            var wasCalled = false;
            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var service = new TripDal(mockContext.Object);

            var trip = new Trip
                { Name = "Trip4", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10), UserId = 0 };

            service.CreateTrip(trip);
            Assert.IsTrue(wasCalled);
        }

        #endregion
    }
}