using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    /// <summary>
    ///     Trip tests
    /// </summary>
    [TestClass]
    public class TripTests
    {
        #region Methods

        /// <summary>
        ///     Tests the create trip.
        /// </summary>
        [TestMethod]
        public void TestCreateTrip()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
                { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var desktopTrip = new TravelPlannerDesktopApp.Models.Trip {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id,
                Transportations = null,
                Waypoints = null,
                Lodgings = null
            };
            var resultToString = desktopTrip.Name + ", Start Date:" +
                                 desktopTrip.StartDate.ToString("MM/dd/yyyy h:mm tt") + ", End Date: " +
                                 desktopTrip.EndDate.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(startDate, desktopTrip.StartDate);
            Assert.AreEqual(endDate, desktopTrip.EndDate);
            Assert.AreEqual("trip", desktopTrip.Name);
            Assert.AreEqual(1, desktopTrip.UserId);
            Assert.AreEqual(1, desktopTrip.Id);
            Assert.IsNull(desktopTrip.Lodgings);
            Assert.IsNull(desktopTrip.Transportations);
            Assert.IsNull(desktopTrip.Waypoints);
            Assert.AreEqual(resultToString, desktopTrip.ToString());
        }

        #endregion
    }
}