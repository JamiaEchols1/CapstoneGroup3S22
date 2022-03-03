using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    [TestClass]
    public class TransportationTests
    {
        [TestMethod]
        public void TestGetArrivalWaypointId()
        {
            Transportation transportation = new Transportation()
            {
                ArrivingWaypointId = 1, DepartingWaypointId = 2, Description = "transportation",
                StartTime = DateTime.Today, EndTime = DateTime.Today.AddHours(1), Id = 1, TripId = 1
            };
            Assert.AreEqual(1, transportation.ArrivingWaypointId);
        }

        [TestMethod]
        public void TestGetDepartureWaypointId()
        {
            Transportation transportation = new Transportation()
            {
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
        [TestMethod]
        public void TestGetDescription()
        {
            Transportation transportation = new Transportation()
            {
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

        [TestMethod]
        public void TestGetStartTime()
        {
            Transportation transportation = new Transportation()
            {
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
        [TestMethod]
        public void TestEndTime()
        {
            Transportation transportation = new Transportation()
            {
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
        [TestMethod]
        public void TestGetId()
        {
            Transportation transportation = new Transportation()
            {
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
        [TestMethod]
        public void TestGetTripId()
        {
            Transportation transportation = new Transportation()
            {
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

        [TestMethod]
        public void TestToString()
        {
            Transportation transportation = new Transportation()
            {
                ArrivingWaypointId = 1,
                DepartingWaypointId = 2,
                Description = "transportation",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                Id = 1,
                TripId = 1
            };
            string result = "Transportation: " + "Start: " + transportation.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " + transportation.EndTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(result, transportation.ToString());
        }
    }
}
