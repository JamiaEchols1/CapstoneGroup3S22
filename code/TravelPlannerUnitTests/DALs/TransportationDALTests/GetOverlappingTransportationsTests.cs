﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.TransportationDALTests
{
    [TestClass]
    public class GetOverlappingTransportationsTests
    {
        [TestMethod]
        public void TestGetOverlappingTransportations()
        {
            LoggedUser.selectedTrip = new Trip
                { Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0 };


            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            var data = new List<Transportation>
            {
                new Transportation { Description = "test transportation", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, DepartingWaypointId = 1, ArrivingWaypointId= 2, Id = 0},
                new Transportation { Description = "test transportation 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 0, DepartingWaypointId = 2, ArrivingWaypointId= 3, Id = 1},
                new Transportation { Description = "test transportation 2", StartTime = DateTime.Now.AddMinutes(15), EndTime = DateTime.Now.AddMinutes(25), TripId = 0, DepartingWaypointId = 3, ArrivingWaypointId= 1, Id = 2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

            var service = new TransportationDAL(mockContext.Object);

            Assert.AreEqual(2, service.GetOverlappingTransportation(DateTime.Now, DateTime.Now.AddMinutes(30)).Count());
            LoggedUser.selectedWaypoint = null;
            LoggedUser.selectedTrip = null;
        }
    }
}