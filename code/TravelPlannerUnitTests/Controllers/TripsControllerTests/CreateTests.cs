using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers.TripsControllerTests
{
    /// <summary>
    ///     The create tests for the web trips controller
    /// </summary>
    [TestClass]
    public class CreateTests
    {
        /// <summary>
        ///     Tests the GET: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestGETCreateTrip()
        {
            LoggedUser.User = new User { Id = 0 };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            controller.SetErrorMessage("Invalid date");
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the POST: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTripValidStartDate()
        {
            LoggedUser.User = new User { Id = 0 };

            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            var result = controller.Create(addedTrip);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the POST: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTripInvalidStartDate()
        {
            LoggedUser.User = new User { Id = 0 };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            var result = controller.Create(addedTrip);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the POST: Create of the trip controller.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTripInvalidModelState()
        {
            LoggedUser.User = new User { Id = 0 };

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);
            var trip = new Trip
            { Id = 1, StartDate = startDate, EndDate = endDate, Name = "trip", UserId = 1 };
            var addedTrip = new AddedTrip
            {
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Name = trip.Name,
                UserId = trip.UserId,
                Id = trip.Id
            };

            var data = new List<Trip>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Trip>>();
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Trips).Returns(mockSet.Object);

            var service = new TripDal(mockContext.Object);

            var controller = new TripsController(service, null, null);
            controller.ModelState.AddModelError("Mega", "Error");
            var result = controller.Create(addedTrip);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
