using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelPlannerLibrary.Models;
using WebApplication4.ViewModels;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    /// Tests for the travel planner web application add waypoint viewmodel
    /// </summary>
    [TestClass]
    public class AddWaypointViewModelTests
    {
        /// <summary>
        /// Tests the add waypoint view model.
        /// </summary>
        [TestMethod]
        public void TestWaypointViewModel()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            LoggedUser.User = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            LoggedUser.SelectedTrip = new Trip
            { 
                Id = 1, 
                StartDate = startDate, 
                EndDate = endDate, 
                Name = "trip", UserId = 1 
            };
            var waypoint = new Waypoint
            {
                Id = 1,
                StartDateTime = DateTime.Today,
                EndDateTime = DateTime.Today.AddDays(1),
                Location = "waypoint",
                TripId = 1
            };
            var addwpViewModel = new AddWaypointViewModel();
            addwpViewModel.Waypoint = waypoint;
            Assert.AreEqual(startDate, addwpViewModel.SelectedTripStartDate);
            Assert.AreEqual(endDate, addwpViewModel.SelectedTripEndDate);
            Assert.AreEqual(waypoint, addwpViewModel.Waypoint);
        }
    }
}
