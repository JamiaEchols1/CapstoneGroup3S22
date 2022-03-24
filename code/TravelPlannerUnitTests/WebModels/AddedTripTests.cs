using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    ///     Added trip tests from the travel planner web models
    /// </summary>
    [TestClass]
    public class AddedTripTests
    {
        /// <summary>
        ///     Tests the web trip created from library trip.
        /// </summary>
        [TestMethod]
        public void TestWebTripCreatedFromLibraryTrip()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };
            Assert.AreEqual(startDate, addedTrip.StartDate);
            Assert.AreEqual(endDate, addedTrip.EndDate);
            Assert.AreEqual("trip", addedTrip.Name);
            Assert.AreEqual(1, addedTrip.UserId);
            Assert.AreEqual(1, addedTrip.Id);
        }
    }
}
