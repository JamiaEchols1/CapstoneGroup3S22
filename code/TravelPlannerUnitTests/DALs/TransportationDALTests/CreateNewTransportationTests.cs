using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.TransportationDALTests
{
    /// <summary>
    ///     Test the transaction DAL
    /// </summary>
    [TestClass]
    public class TestTransportation
    {
        #region Data members

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        ///     Tests the null description.
        /// </summary>
        [TestMethod]
        public void TestNullDescription()
        {
            var waypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDal(mockContext.Object);
            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CreateANewTransportation(1, 2, 1, DateTime.Now, DateTime.Now.AddMinutes(7), null));
        }

        /// <summary>
        ///     Tests the empty description.
        /// </summary>
        [TestMethod]
        public void TestEmptyDescription()
        {
            var waypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDal(mockContext.Object);
            Assert.ThrowsException<ArgumentNullException>(() =>
                service.CreateANewTransportation(1, 2, 1, DateTime.Now, DateTime.Now.AddMinutes(7), ""));
        }

        /// <summary>
        ///     Tests the same departure and arrival waypoint.
        /// </summary>
        [TestMethod]
        public void TestSameDepartureAndArrivalWaypoint()
        {
            var waypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDal(mockContext.Object);
            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateANewTransportation(1, 1, 1, DateTime.Now, DateTime.Now.AddMinutes(7), "Description"));
        }

        /// <summary>
        ///     Tests the start time after end time.
        /// </summary>
        [TestMethod]
        public void TestStartTimeAfterEndTime()
        {
            var waypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(5), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDal(mockContext.Object);
            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateANewTransportation(1, 0, 2, DateTime.Now, DateTime.Now.AddMinutes(-7), "Description"));
        }

        /// <summary>
        ///     Tests the start time before waypoint end time.
        /// </summary>
        [TestMethod]
        public void TestStartTimeBeforeWaypointEndTime()
        {
            LoggedUser.SelectedWaypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(4),
                EndDateTime = DateTime.Now.AddMinutes(34), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDal(mockContext.Object);
            Assert.ThrowsException<ArgumentException>(() =>
                service.CreateANewTransportation(0, 1, 1, DateTime.Now.AddMinutes(33), DateTime.Now.AddMinutes(40),
                    "Description"));
            LoggedUser.SelectedWaypoint = null;
        }

        /// <summary>
        ///     Tests the valid transportation.
        /// </summary>
        [TestMethod]
        public void TestValidTransportation()
        {
            LoggedUser.SelectedWaypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            var wasCalled = false;

            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var service = new TransportationDal(mockContext.Object);

            service.CreateANewTransportation(0, 1, 1, DateTime.Now.AddMinutes(40), DateTime.Now.AddMinutes(45),
                "Description");

            Assert.IsTrue(wasCalled);
            LoggedUser.SelectedWaypoint = null;
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