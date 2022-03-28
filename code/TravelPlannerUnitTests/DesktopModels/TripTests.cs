using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    [TestClass]
    public class TripTests
    {
        [TestMethod]
        public void TestCreateTrip()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
                { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var desktopTrip = new TravelPlannerDesktopApp.Models.Trip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };
            string resultToString =  desktopTrip.Name + ", Start Date:" + desktopTrip.StartDate.ToString("MM/dd/yyyy h:mm tt") + ", End Date: " + desktopTrip.EndDate.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual(startDate, desktopTrip.StartDate);
            Assert.AreEqual(endDate, desktopTrip.EndDate);
            Assert.AreEqual("trip", desktopTrip.Name);
            Assert.AreEqual(1, desktopTrip.UserId);
            Assert.AreEqual(1, desktopTrip.Id);
            Assert.AreEqual(resultToString, desktopTrip.ToString());
        }
    }
}
