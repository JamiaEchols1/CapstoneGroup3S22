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
    ///     Tests delete transportation
    /// </summary>
    [TestClass]
    public class DeleteTransportationTests
    {
        #region Methods

        /// <summary>
        ///     Tests the delete transportation.
        /// </summary>
        [TestMethod]
        public void TestDeleteTransportation()
        {
            LoggedUser.SelectedWaypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0
            };
            var transportation = new Transportation {
                Description = "Description", StartTime = DateTime.Now.AddMinutes(45),
                EndTime = DateTime.Now.AddMinutes(50), TripId = 2, ArrivingWaypointId = 1, DepartingWaypointId = 0
            };

            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId = 2,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 2, DepartingWaypointId = 2, ArrivingWaypointId = 3,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now.AddMinutes(10),
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 3, DepartingWaypointId = 3, ArrivingWaypointId = 1,
                    Id = 2
                },
                transportation
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var wasCalled = false;
            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var service = new TransportationDal(mockContext.Object);

            service.DeleteTransportation(transportation);

            Assert.IsTrue(wasCalled);
        }

        #endregion
    }
}