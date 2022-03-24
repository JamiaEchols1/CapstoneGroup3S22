using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.WebModels
{
    [TestClass]
    public class AddedLodgingTests
    {
        [TestMethod]
        public void TestWebLodgingCreatedFromLibraryLodging()
        {
            var startTime = DateTime.Now.AddMinutes(45);
            var endTime = DateTime.Now.AddMinutes(50);
            var lodging = new Lodging
            {
                Location = "Location",
                StartTime = startTime,
                EndTime = endTime,
                TripId = 2
            };
            var addedLodging = new AddedLodging
            {
                Location = lodging.Location,
                StartTime = lodging.StartTime,
                EndTime = lodging.EndTime,
                TripId = lodging.TripId,
                Description = "description",
                Id = 0
             
            };
            Assert.AreEqual("Location", addedLodging.Location);
            Assert.AreEqual(startTime, addedLodging.StartTime);
            Assert.AreEqual(endTime, addedLodging.EndTime);
            Assert.AreEqual(2, addedLodging.TripId);
            Assert.AreEqual(0, addedLodging.Id);
            Assert.AreEqual("description", addedLodging.Description);
        }
    }
}
