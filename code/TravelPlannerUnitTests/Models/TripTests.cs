using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    /// <summary>
    ///     Tests the trip class
    /// </summary>
    [TestClass]
    public class TripTests
    {
        #region Methods

        /// <summary>
        ///     Tests the get identifier.
        /// </summary>
        [TestMethod]
        public void TestGetId()
        {
            var trip = new Trip
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(1, trip.Id);
        }

        /// <summary>
        ///     Tests the get start date.
        /// </summary>
        [TestMethod]
        public void TestGetStartDate()
        {
            var trip = new Trip
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(DateTime.Today, trip.StartDate);
        }

        /// <summary>
        ///     Tests the get end date.
        /// </summary>
        [TestMethod]
        public void TestGetEndDate()
        {
            var trip = new Trip
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(DateTime.Today.AddDays(7), trip.EndDate);
        }

        /// <summary>
        ///     Tests the name of the get.
        /// </summary>
        [TestMethod]
        public void TestGetName()
        {
            var trip = new Trip
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual("trip", trip.Name);
        }

        /// <summary>
        ///     Tests the get user identifier.
        /// </summary>
        [TestMethod]
        public void TestGetUserId()
        {
            var trip = new Trip
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            Assert.AreEqual(1, trip.UserId);
        }

        /// <summary>
        ///     Tests to string.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            var trip = new Trip
                { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Name = "trip", UserId = 1 };
            var result = trip.Name + ", Start Date:" + trip.StartDate.ToString("MM/dd/yyyy h:mm tt") + ", End Date: " +
                         trip.EndDate.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(result, trip.ToString());
        }

        #endregion
    }
}