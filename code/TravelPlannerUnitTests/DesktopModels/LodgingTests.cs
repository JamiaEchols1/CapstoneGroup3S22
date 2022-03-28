using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    /// <summary>
    ///     Lodging tests
    /// </summary>
    [TestClass]
    public class LodgingTests
    {
        #region Methods

        /// <summary>
        ///     Tests the create lodging.
        /// </summary>
        [TestMethod]
        public void TestCreateLodging()
        {
            var startTime = DateTime.Now.AddMinutes(45);
            var endTime = DateTime.Now.AddMinutes(50);
            var lodging = new Lodging {
                Location = "Location",
                StartTime = startTime,
                EndTime = endTime,
                TripId = 2
            };
            var desktopLodging = new TravelPlannerDesktopApp.Models.Lodging {
                Location = lodging.Location,
                StartTime = lodging.StartTime,
                EndTime = lodging.EndTime,
                TripId = lodging.TripId,
                Description = "description",
                Id = 0
            };
            var resultToString = desktopLodging.Location + ", Start Date: " +
                                 desktopLodging.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End Time: " +
                                 desktopLodging.EndTime.ToString("MM/dd/yyyy h:mm tt");

            Assert.AreEqual("Location", desktopLodging.Location);
            Assert.AreEqual(startTime, desktopLodging.StartTime);
            Assert.AreEqual(endTime, desktopLodging.EndTime);
            Assert.AreEqual(2, desktopLodging.TripId);
            Assert.AreEqual(0, desktopLodging.Id);
            Assert.AreEqual("description", desktopLodging.Description);
            Assert.AreEqual(resultToString, desktopLodging.ToString());
        }

        #endregion
    }
}