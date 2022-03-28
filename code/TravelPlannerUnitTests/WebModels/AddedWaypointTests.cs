using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    ///     The added waypoint test from the travel planner web application
    /// </summary>
    [TestClass]
    public class AddedWaypointTests
    {
        #region Methods

        /// <summary>
        ///     Tests the web waypoint created from library waypoint.
        /// </summary>
        [TestMethod]
        public void TestWebWaypointCreatedFromLibraryWaypoint()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var waypoint = new Waypoint {
                Id = 1,
                StartDateTime = startDate,
                EndDateTime = endDate,
                Location = "waypoint",
                TripId = 1,
                Description = "description"
            };
            var addedWaypoint = new AddedWaypoint {
                Id = waypoint.Id,
                StartDateTime = waypoint.StartDateTime,
                EndDateTime = waypoint.EndDateTime,
                Location = waypoint.Location,
                TripId = waypoint.Id,
                Description = waypoint.Description
            };
            Assert.AreEqual(startDate, addedWaypoint.StartDateTime);
            Assert.AreEqual(endDate, addedWaypoint.EndDateTime);
            Assert.AreEqual(1, addedWaypoint.Id);
            Assert.AreEqual(1, addedWaypoint.TripId);
            Assert.AreEqual("waypoint", addedWaypoint.Location);
            Assert.AreEqual("description", addedWaypoint.Description);
        }

        #endregion
    }
}