using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.TransportationDALTests
{
    /// <summary>
    /// Summary description for TestGetTransportation
    /// </summary>
    [TestClass]
    public class GetTransportationTests
    {
        public GetTransportationTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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

        [TestMethod]
        public void TestGetTransportation()
        {
            Waypoint waypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-4), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            var data = new List<Transportation>
            {
                new Transportation { Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId= 2, Id = 0},
                new Transportation { Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId= 3, Id = 1},
                new Transportation { Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId= 1, Id = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDAL(mockContext.Object);
            var transportations = service.GetTransportation(1, 2);

            Assert.AreEqual(1, transportations.Count);
            Assert.AreEqual(0, transportations[0].Id);
            Assert.AreEqual(1, transportations[0].DepartingWaypointId);
            Assert.AreEqual(2, transportations[0].ArrivingWaypointId);
            Assert.AreEqual("test transportation", transportations[0].Description);
        }
    }
}
