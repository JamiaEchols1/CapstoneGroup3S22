using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.WebModels
{
    /// <summary>
    /// Tests the user credentials model class in the travel planner web application
    /// </summary>
    [TestClass]
    public class UserCredentialsTests
    {
        /// <summary>
        ///     Tests the web user credentials created from library user.
        /// </summary>
        [TestMethod]
        public void TestWebUserCredentialsCreatedFromLibraryUser()
        {
            var id = 1;
            var password = "password";
            var username = "username";
            var user = new User { Id = id, Password = password, Username = username, Trips = new List<Trip>() };
            var userCredentials = new UserCredentials
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
            Assert.AreEqual(id, userCredentials.Id);
            Assert.AreEqual(username, userCredentials.Username);
            Assert.AreEqual(password, userCredentials.Password);
        }
    }
}
