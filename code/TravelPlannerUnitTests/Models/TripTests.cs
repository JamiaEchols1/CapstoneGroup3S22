using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    [TestClass]
    public class TripTests
    {
        [TestMethod]
        public void TestGetId()
        {
            Trip trip = new Trip()
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(1, trip.Id);
        }

        [TestMethod]
        public void TestGetStartDate()
        {
            Trip trip = new Trip()
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(DateTime.Today, trip.StartDate);
        }

        [TestMethod]
        public void TestGetEndDate()
        {
            Trip trip = new Trip()
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(DateTime.Today.AddDays(7), trip.EndDate);
        }

        [TestMethod]
        public void TestGetName()
        {
            Trip trip = new Trip()
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual("trip", trip.Name);
        }

        [TestMethod]
        public void TestGetUserId()
        {
            Trip trip = new Trip()
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(1, trip.UserId);
        }

        [TestMethod]
        public void TestToString()
        {
            Trip trip = new Trip()
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
           string result = trip.Name + ", Start Date:" + trip.StartDate.ToString("MM/dd/yyyy h:mm tt") + ", End Date: " + trip.EndDate.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(result, trip.ToString());
        }
    }
}
