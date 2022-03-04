using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    /// <summary>
    ///     The lodging tests
    /// </summary>
    [TestClass]
    public class LodgingTests
    {
        #region Methods

        /// <summary>
        ///     Tests the get location.
        /// </summary>
        [TestMethod]
        public void TestGetLocation()
        {
            var lodging = new Lodging {
                Location = "Location", StartTime = DateTime.Now.AddMinutes(45), EndTime = DateTime.Now.AddMinutes(50),
                TripId = 2
            };
            Assert.AreEqual("Location", lodging.Location);
        }

        /// <summary>
        ///     Tests the get start time.
        /// </summary>
        [TestMethod]
        public void TestGetStartTime()
        {
            var time = DateTime.Now.AddMinutes(45);
            var lodging = new Lodging
                { Location = "Location", StartTime = time, EndTime = DateTime.Now.AddMinutes(50), TripId = 2 };
            Assert.AreEqual(time, lodging.StartTime);
        }

        /// <summary>
        ///     Tests the get end time.
        /// </summary>
        [TestMethod]
        public void TestGetEndTime()
        {
            var time = DateTime.Now.AddMinutes(45);
            var lodging = new Lodging { Location = "Location", StartTime = DateTime.Now, EndTime = time, TripId = 2 };
            Assert.AreEqual(time, lodging.EndTime);
        }

        /// <summary>
        ///     Tests the get trip identifier.
        /// </summary>
        [TestMethod]
        public void TestGetTripId()
        {
            var lodging = new Lodging
                { Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), TripId = 2 };
            Assert.AreEqual(2, lodging.TripId);
        }

        /// <summary>
        ///     Tests to string.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            var lodging = new Lodging
                { Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), TripId = 2 };
            var result = lodging.Location + ", Start Date: " + lodging.StartTime.ToString("MM/dd/yyyy h:mm tt") +
                         ", End Time: " + lodging.EndTime.ToString("MM/dd/yyyy h:mm tt");
            Assert.AreEqual(result, lodging.ToString());
        }

        #endregion
    }
}