using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    /// <summary>
    ///     Tests transportation
    /// </summary>
    [TestClass]
    public class TransportationTests
    {
        #region Methods

        /// <summary>
        ///     Tests the get arrival waypoint identifier.
        /// </summary>
        [TestMethod]
        public void TestGetArrivalWaypointId()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1, DepartingWaypointId = 2, Description = "transportation",
                StartTime = DateTime.Today, EndTime = DateTime.Today.AddHours(1), Id = 1, TripId = 1
            };
            Assert.AreEqual(1, transportation.ArrivingWaypointId);
        }

        /// <summary>
        ///     Tests the get departure waypoint identifier.
        /// </summary>
        [TestMethod]
        public void TestGetDepartureWaypointId()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            Assert.AreEqual(2, transportation.DepartingWaypointId);
        }

        /// <summary>
        ///     Tests the get description.
        /// </summary>
        [TestMethod]
        public void TestGetDescription()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            Assert.AreEqual("transportation", transportation.Description);
        }

        /// <summary>
        ///     Tests the get start time.
        /// </summary>
        [TestMethod]
        public void TestGetStartTime()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            Assert.AreEqual(DateTime.Today, transportation.StartTime);
        }

        /// <summary>
        ///     Tests the end time.
        /// </summary>
        [TestMethod]
        public void TestEndTime()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            Assert.AreEqual(DateTime.Today.AddHours(1), transportation.EndTime);
        }

        /// <summary>
        ///     Tests the get identifier.
        /// </summary>
        [TestMethod]
        public void TestGetId()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            Assert.AreEqual(1, transportation.Id);
        }

        /// <summary>
        ///     Tests the get trip identifier.
        /// </summary>
        [TestMethod]
        public void TestGetTripId()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            Assert.AreEqual(1, transportation.TripId);
        }

        /// <summary>
        ///     Tests to string.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            var transportation = new Transportation {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            var result = "Transportation: " + "Start: " + transportation.StartTime.ToString("MM/dd/yyyy h:mm tt") +
                         ", End: " + transportation.EndTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(result, transportation.ToString());
        }

        #endregion
    }
}