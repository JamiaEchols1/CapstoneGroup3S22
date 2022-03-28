using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void TestCreateUser()
        {
            var user = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            var desktopUser = new TravelPlannerDesktopApp.Models.User
            {
                Password = user.Password,
                Username = user.Username,
                Id = user.Id
            };
            Assert.AreEqual("username", desktopUser.Username);
            Assert.AreEqual("password", desktopUser.Password);
            Assert.AreEqual(1, desktopUser.Id);
            Assert.AreEqual(0, desktopUser.Trips.Count);
        }
    }
}
