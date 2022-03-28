using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    /// <summary>
    ///     Waypoint tests
    /// </summary>
    [TestClass]
    public class WaypointTests
    {
        #region Methods

        /// <summary>
        ///     Tests the create waypoint.
        /// </summary>
        [TestMethod]
        public void TestCreateWaypoint()
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
            var desktopWaypoint = new TravelPlannerDesktopApp.Models.Waypoint {
                Id = waypoint.Id,
                StartDateTime = waypoint.StartDateTime,
                EndDateTime = waypoint.EndDateTime,
                Location = waypoint.Location,
                TripId = waypoint.Id,
                Description = waypoint.Description
            };
            var resultToString = "Waypoint: " + desktopWaypoint.Location + ", Start: " +
                                 desktopWaypoint.StartDateTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " +
                                 desktopWaypoint.EndDateTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(startDate, desktopWaypoint.StartDateTime);
            Assert.AreEqual(endDate, desktopWaypoint.EndDateTime);
            Assert.AreEqual(1, desktopWaypoint.Id);
            Assert.AreEqual(1, desktopWaypoint.TripId);
            Assert.AreEqual("waypoint", desktopWaypoint.Location);
            Assert.AreEqual("description", desktopWaypoint.Description);
            Assert.AreEqual(resultToString, desktopWaypoint.ToString());
        }

        #endregion
    }
}