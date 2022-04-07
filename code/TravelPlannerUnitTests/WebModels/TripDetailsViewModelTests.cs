using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;
using WebApplication4.ViewModels;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    ///     Tests the web application trip details view model
    /// </summary>
    [TestClass]
    public class TripDetailsViewModelTests
    {
        #region Methods

        /// <summary>
        ///     Tests the trip details view model.
        /// </summary>
        [TestMethod]
        public void TestTripDetailsViewModel()
        {
            var trip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };
            var waypoints = new List<Waypoint> {
                new Waypoint {
                    Location = "Nowhere", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddMinutes(120),
                    TripId = 0, Id = 0
                },
                new Waypoint {
                    Location = "Somewhere", StartDateTime = DateTime.Now.AddDays(1),
                    EndDateTime = DateTime.Now.AddDays(1).AddMinutes(120), TripId = 0, Id = 1
                }
            };
            var lodgings = new List<Lodging> {
                new Lodging {
                    Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0
                },
                new Lodging {
                    Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1
                },
                new Lodging {
                    Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2
                }
            };

            var viewmodel = new TripDetailsViewModel {
                Trip = trip,
                Lodgings = lodgings,
                Waypoints = waypoints,
                WaypointsAndTransportation = new List<TripItem>()
            };
            var sample_waypoints = viewmodel.Waypoints;
            var sample_transports = viewmodel.Transportations;
            var waypointsandtransports = viewmodel.WaypointsAndTransportation;
            Assert.AreEqual(waypoints, viewmodel.Waypoints);
            Assert.AreEqual(lodgings, viewmodel.Lodgings);
            Assert.AreEqual(trip, viewmodel.Trip);
        }

        #endregion
    }
}