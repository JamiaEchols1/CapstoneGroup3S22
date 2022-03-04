using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    [TestClass]
    public class WaypointTests
    {
        [TestMethod]
        public void TestGetId()
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = 1, StartDateTime = DateTime.Today, EndDateTime = DateTime.Today.AddDays(1), Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(1, waypoint.Id);
        }

        [TestMethod]
        public void TestGetStartDate()
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(DateTime.Today, waypoint.StartDateTime);
        }

        [TestMethod]
        public void TestGetEndDate()
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(DateTime.Today.AddDays(1), waypoint.EndDateTime);
        }

        [TestMethod]
        public void TestGetLocation()
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual("waypoint", waypoint.Location);
        }

        [TestMethod]
        public void TestGetTripId()
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            Assert.AreEqual(1, waypoint.TripId);
        }

        [TestMethod]
        public void TestToString()
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            string result = "Waypoint: " + waypoint.Location + ", Start: " + waypoint.StartDateTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " + waypoint.EndDateTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(result, waypoint.ToString());
        }
    }
}