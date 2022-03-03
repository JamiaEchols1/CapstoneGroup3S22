using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    [TestClass]
    public class LodgingTests
    {
        [TestMethod]
        public void TestGetLocation()
        {
            Lodging lodging = new Lodging() { Location = "Location", StartTime = DateTime.Now.AddMinutes(45), EndTime = DateTime.Now.AddMinutes(50), TripId = 2 };
            Assert.AreEqual("Location", lodging.Location);
        }

        [TestMethod]
        public void TestGetStartTime()
        {
            DateTime time = DateTime.Now.AddMinutes(45);
            Lodging lodging = new Lodging() { Location = "Location", StartTime = time, EndTime = DateTime.Now.AddMinutes(50), TripId = 2 };
            Assert.AreEqual(time, lodging.StartTime);
        }

        [TestMethod]
        public void TestGetEndTime()
        {
            DateTime time = DateTime.Now.AddMinutes(45);
            Lodging lodging = new Lodging() { Location = "Location", StartTime = DateTime.Now, EndTime = time, TripId = 2 };
            Assert.AreEqual(time, lodging.EndTime);
        }

        [TestMethod]
        public void TestGetTripId()
        {
            Lodging lodging = new Lodging() { Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), TripId = 2 };
            Assert.AreEqual(2, lodging.TripId);
        }

        [TestMethod]
        public void TestToString()
        {
            Lodging lodging = new Lodging() { Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), TripId = 2 };
            string result = lodging.Location + ", Start Date: " + lodging.StartTime.ToString("MM/dd/yyyy h:mm tt") +
                            ", End Time: " + lodging.EndTime.ToString("MM/dd/yyyy h:mm tt");
            Assert.AreEqual(result, lodging.ToString());
        }
    }
}
