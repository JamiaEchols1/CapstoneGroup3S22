using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.LodgingDALTests
{
    /// <summary>
    /// Tests get lodgings
    /// </summary>
    [TestClass]
    public class GetLodgingsTests
    {
        #region Methods

        /// <summary>
        /// Tests the get lodgings.
        /// </summary>
        [TestMethod]
        public void TestGetLodgings()
        {
            LoggedUser.SelectedWaypoint = new Waypoint {
                Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33),
                EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0
            };
            var lodging = new Lodging {
                Location = "Lodging", StartTime = DateTime.Now.AddMinutes(45), EndTime = DateTime.Now.AddMinutes(50),
                TripId = 2
            };

            var data = new List<Lodging> {
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
                },
                lodging
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lodging>>();
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lodging>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Lodgings).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(mockContext.Object.SaveChanges());

            var service = new LodgingDal(mockContext.Object);

            Assert.AreEqual(2, service.GetLodgings(2).Count);
            LoggedUser.SelectedWaypoint = null;
        }

        #endregion
    }
}