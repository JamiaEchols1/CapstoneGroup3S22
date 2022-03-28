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
    ///     Tests create new trip
    /// </summary>
    [TestClass]
    public class TestTrip
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

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

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CreateNewTrip(null, DateTime.Now, DateTime.Now.AddDays(7), 1, "Desc"));
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

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CreateNewTrip("", DateTime.Now, DateTime.Now.AddDays(7), 1, "Desc"));
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

            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateNewTrip("test", DateTime.Now.AddDays(7), DateTime.Now, 1, "Desc"));
        }

        /// <summary>
        ///     Tests the add trip start date before today.
        /// </summary>
        [TestMethod]
        public void TestAddTripStartDateBeforeToday()
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

            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateNewTrip("Trip4", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(7), 1, "Desc"));
        }

        /// <summary>
        ///     Tests the add valid trip.
        /// </summary>
        [TestMethod]
        public void TestAddValidTrip()
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

            service.CreateNewTrip("Trip4", DateTime.Now, DateTime.Now.AddDays(7), 1, "Desc");

            Assert.IsTrue(wasCalled);
        }

        #endregion

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}