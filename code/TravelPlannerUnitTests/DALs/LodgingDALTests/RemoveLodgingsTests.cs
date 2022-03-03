﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.LodgingDALTests
{
    [TestClass]
    public class RemoveLodgingsTests
    {
        [TestMethod]
        public void TestDeleteLodging()
        {
            LoggedUser.selectedWaypoint = new Waypoint { Location = "Nowhere", StartDateTime = DateTime.Now.AddMinutes(33), EndDateTime = DateTime.Now.AddMinutes(40), TripId = 0, Id = 0 };
            Lodging lodging = new Lodging() { Location = "Lodging", StartTime = DateTime.Now.AddMinutes(45), EndTime = DateTime.Now.AddMinutes(50), TripId = 2};

            var data = new List<Lodging>
            {
                new Lodging { Location = "test lodging", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 1, Id = 0},
                new Lodging { Location = "test lodging 1", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 2, Id = 1},
                new Lodging { Location = "test lodging 2", StartTime = DateTime.Now.AddMinutes(10), EndTime = DateTime.Now.AddMinutes(14), TripId = 3, Id = 2},
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

            Boolean wasCalled = false;
            mockContext.Setup(m => m.SaveChanges()).Callback(() => wasCalled = true);

            var service = new LodgingDAL(mockContext.Object);

            service.RemoveLodging(lodging);

            Assert.IsTrue(wasCalled);
            LoggedUser.selectedWaypoint = null;
        }
    }
}