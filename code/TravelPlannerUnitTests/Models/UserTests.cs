using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void TestGetUsername()
        {
            User user = new User() { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual("username", user.Username);
        }

        [TestMethod]
        public void TestGetPassword()
        {
            User user = new User() { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual("password", user.Password);
        }

        [TestMethod]
        public void TestGetId()
        {
            User user = new User() { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual(1, user.Id);
        }

        [TestMethod]
        public void TestGetTrips()
        {
            User user = new User() { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual(0, user.Trips.Count);
        }

    }
}
