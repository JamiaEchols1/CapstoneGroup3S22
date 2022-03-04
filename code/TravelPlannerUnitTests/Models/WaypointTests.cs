using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    /// <summary>
    ///     Tests the waypoint class
    /// </summary>
    [TestClass]
    public class WaypointTests
    {
        #region Methods

        /// <summary>
        ///     Tests the get identifier.
        /// </summary>
        [TestMethod]
        public void TestGetId()
        {
            var waypoint = new Waypoint {
                Id = 1, StartDateTime = DateTime.Today, EndDateTime = DateTime.Today.AddDays(1), Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(1, waypoint.Id);
        }

        /// <summary>
        ///     Tests the get start date.
        /// </summary>
        [TestMethod]
        public void TestGetStartDate()
        {
            var waypoint = new Waypoint {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(DateTime.Today, waypoint.StartDateTime);
        }

        /// <summary>
        ///     Tests the get end date.
        /// </summary>
        [TestMethod]
        public void TestGetEndDate()
        {
            var waypoint = new Waypoint {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(DateTime.Today.AddDays(1), waypoint.EndDateTime);
        }

        /// <summary>
        ///     Tests the get location.
        /// </summary>
        [TestMethod]
        public void TestGetLocation()
        {
            var waypoint = new Waypoint {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual("waypoint", waypoint.Location);
        }

        /// <summary>
        ///     Tests the get trip identifier.
        /// </summary>
        [TestMethod]
        public void TestGetTripId()
        {
            var waypoint = new Waypoint {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(1, waypoint.TripId);
        }

        /// <summary>
        ///     Tests to string.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            var waypoint = new Waypoint {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            var result = "Waypoint: " + waypoint.Location + ", Start: " +
                         waypoint.StartDateTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " +
                         waypoint.EndDateTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(result, waypoint.ToString());
        }

        #endregion
    }
}