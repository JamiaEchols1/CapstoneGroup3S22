using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    [TestClass]
    public class TransportationTests
    {
        [TestMethod]
        public void TestCreateTransportation()
        {
            var startTime = DateTime.Today;
            var endTime = DateTime.Today.AddHours(1);
            var transportation = new Transportation
            {
                Description = "transportation",
                StartTime = startTime,
                EndTime = endTime ,
                Id = 1,
                TripId = 1
            };
            var desktopTransportation = new TravelPlannerDesktopApp.Models.Transportation
            {
                Description = transportation.Description,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                Id = transportation.Id,
                TripId = transportation.TripId,
            };
            string resultToString = "Transportation: " + "Start: " + desktopTransportation.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " + desktopTransportation.EndTime.ToString("MM/dd/yyyy h:mm tt");
            Assert.AreEqual("transportation",desktopTransportation.Description);
            Assert.AreEqual(startTime, desktopTransportation.StartTime);
            Assert.AreEqual(endTime, desktopTransportation.EndTime);
            Assert.AreEqual(1, desktopTransportation.Id);
            Assert.AreEqual(1, desktopTransportation.TripId);
            Assert.AreEqual(resultToString, desktopTransportation.ToString());
        }
    }
}
