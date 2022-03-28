using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    ///     Testing added transportation model
    /// </summary>
    [TestClass]
    public class AddedTransportationTests
    {
        #region Methods

        /// <summary>
        ///     Tests the web transportation created from library transportation.
        /// </summary>
        [TestMethod]
        public void TestWebTransportationCreatedFromLibraryTransportation()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var transportation = new Transportation {
                Description = "transportation",
                StartTime = startDate,
                EndTime = endDate,
                Id = 1,
                TripId = 1
            };
            var addedTransportation = new AddedTransportation {
                Id = 1,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = 1,
                Description = transportation.Description
            };
            Assert.AreEqual(startDate, addedTransportation.StartTime);
            Assert.AreEqual(endDate, addedTransportation.EndTime);
            Assert.AreEqual(1, addedTransportation.Id);
            Assert.AreEqual(1, addedTransportation.TripId);
            Assert.AreEqual("transportation", addedTransportation.Description);
        }

        #endregion
    }
}