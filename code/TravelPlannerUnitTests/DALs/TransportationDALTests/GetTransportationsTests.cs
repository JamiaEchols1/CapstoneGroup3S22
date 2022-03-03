using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.TransportationDALTests
{
    /// <summary>
    ///     Tests get transportations
    /// </summary>
    [TestClass]
    public class GetTransportationsTests
    {
        #region Methods

        /// <summary>
        ///     Tests the get transportations.
        /// </summary>
        [TestMethod]
        public void TestGetTransportations()
        {
            var waypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(-40),
                EndDateTime = DateTime.Now.AddMinutes(-5), TripId = 0, Id = 0
            };
            var data = new List<Transportation> {
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Waypoint = waypoint, Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDal(mockContext.Object);
            var transportations = service.GetTransportationsByTrip(1);

            Assert.AreEqual(2, transportations.Count);
            Assert.AreEqual(0, transportations[0].Id);
            Assert.AreEqual(1, transportations[1].Id);
        }

        #endregion
    }
}