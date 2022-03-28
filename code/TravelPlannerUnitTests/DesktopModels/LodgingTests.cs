using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerDesktopApp.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    [TestClass]
    public class LodgingTests
    {
        [TestMethod]
        public void TestCreateLodging()
        {
            var startTime = DateTime.Now.AddMinutes(45);
            var endTime = DateTime.Now.AddMinutes(50);
            var lodging = new TravelPlannerLibrary.Models.Lodging()
            {
                Location = "Location",
                StartTime = startTime,
                EndTime = endTime,
                TripId = 2
            };
            var desktopLodging = new Lodging
            {
                Location = lodging.Location,
                StartTime = lodging.StartTime,
                EndTime = lodging.EndTime,
                TripId = lodging.TripId,
                Description = "description",
                Id = 0

            };
            string resultToString =  desktopLodging.Location + ", Start Date: " + desktopLodging.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End Time: " + desktopLodging.EndTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual("Location", desktopLodging.Location);
            Assert.AreEqual(startTime, desktopLodging.StartTime);
            Assert.AreEqual(endTime, desktopLodging.EndTime);
            Assert.AreEqual(2, desktopLodging.TripId);
            Assert.AreEqual(0, desktopLodging.Id);
            Assert.AreEqual("description", desktopLodging.Description);
            Assert.AreEqual(resultToString, desktopLodging.ToString());
        }
    }
}
