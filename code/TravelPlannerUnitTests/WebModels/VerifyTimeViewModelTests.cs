using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    ///     Tests the web application trip details view model
    /// </summary>
    [TestClass]
    public class VerifyTimeViewModelTests
    {
        #region Methods

        /// <summary>
        ///     Tests the trip details view model.
        /// </summary>
        [TestMethod]
        public void TestTripDetailsViewModel()
        {
            TimeSpan estimatedTime = new TimeSpan(1,2,3,4);
            TimeSpan actualTime = new TimeSpan(2, 3, 4, 5);

            var newWaypoint = new Waypoint
            {
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(7),
                EndDateTime = DateTime.Now.AddDays(8).AddHours(18),
                TripId = 0,
                Description = "No"
            };
            var addedWaypoint = new AddedWaypoint
            {
                Id = newWaypoint.Id,
                StartDateTime = newWaypoint.StartDateTime,
                EndDateTime = newWaypoint.EndDateTime,
                Location = newWaypoint.Location,
                TripId = newWaypoint.Id,
                Description = newWaypoint.Description
            };

            TripItem tripItem = new Waypoint() 
            {
                Location = "UWG Bookstore Carrollton GA 30117",
                StartDateTime = DateTime.Now.AddDays(7),
                EndDateTime = DateTime.Now.AddDays(8).AddHours(18),
                TripId = 0,
                Description = "No"
            };

            bool otherIsBefore = false;

            VerifyTimeViewModel vm = new VerifyTimeViewModel(estimatedTime, actualTime, addedWaypoint, tripItem, otherIsBefore);

            string timespanstring = vm.ToReadableString(estimatedTime);

            Assert.AreEqual(addedWaypoint, vm.waypoint);
            Assert.AreEqual(tripItem, vm.otherLocation);
            Assert.AreEqual(estimatedTime, vm.estimatedTime);
            Assert.AreEqual(actualTime, vm.actualTime);
            Assert.AreEqual(otherIsBefore, vm.otherIsBefore);
        }

        #endregion
    }
}