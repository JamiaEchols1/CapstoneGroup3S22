using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.WaypointDALTests
{
    /// <summary>
    ///     Tests remove waypoints
    /// </summary>
    [TestClass]
    public class RemoveWaypointsTests
    {
        #region Methods

        /// <summary>
        ///     Tests the remove waypoint.
        /// </summary>
        [TestMethod]
        public void TestRemoveWaypoint()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var waypoint = new Waypoint {
                Location = "Nowhere",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddMinutes(120),
                TripId = 0,
                Id = 0
            };
            var waypointData = new List<Waypoint> {
                waypoint,
                new Waypoint {
                    Location = "Out here", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 2
                },
                new Waypoint {
                    Location = "Somewhere", StartDateTime = DateTime.Now.AddDays(1),
                    EndDateTime = DateTime.Now.AddDays(1).AddMinutes(120), TripId = 0, Id = 1
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var mockSetWaypoint = new Mock<DbSet<Waypoint>>();
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Provider).Returns(waypointData.Provider);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.Expression).Returns(waypointData.Expression);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.ElementType).Returns(waypointData.ElementType);
            mockSetWaypoint.As<IQueryable<Waypoint>>().Setup(m => m.GetEnumerator())
                           .Returns(waypointData.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Waypoints).Returns(mockSetWaypoint.Object);
            var wasCalled = false;
            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var waypointService = new WaypointDal(mockContext.Object);
            waypointService.RemoveWaypoint(waypoint);

            Assert.IsTrue(wasCalled);
        }

        #endregion
    }
}