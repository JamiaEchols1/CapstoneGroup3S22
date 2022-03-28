using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;

namespace TravelPlannerUnitTests.Controllers.TripsControllerTests
{
    /// <summary>
    ///     The index tests for the web trips controller
    /// </summary>
    [TestClass]
    public class IndexTests
    {
        #region Methods

        /// <summary>
        ///     Tests the GET: index of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestTripIndex()
        {
            LoggedUser.User = new User { Id = 0 };

            var data = new List<Trip> {
                new Trip { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0 },
                new Trip {
                    Name = "Trip2", StartDate = DateTime.Now.AddDays(34), EndDate = DateTime.Now.AddDays(38), UserId = 0
                },
                new Trip {
                    Name = "Trip3", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(30), UserId = 0
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null, null);
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        #endregion
    }
}