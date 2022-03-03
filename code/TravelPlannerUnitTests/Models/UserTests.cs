using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.Models
{
    /// <summary>
    ///     Tests the user class
    /// </summary>
    [TestClass]
    public class UserTests
    {
        #region Methods

        /// <summary>
        ///     Tests the get username.
        /// </summary>
        [TestMethod]
        public void TestGetUsername()
        {
            var user = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual("username", user.Username);
        }

        /// <summary>
        ///     Tests the get password.
        /// </summary>
        [TestMethod]
        public void TestGetPassword()
        {
            var user = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual("password", user.Password);
        }

        /// <summary>
        ///     Tests the get identifier.
        /// </summary>
        [TestMethod]
        public void TestGetId()
        {
            var user = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual(1, user.Id);
        }

        /// <summary>
        ///     Tests the get trips.
        /// </summary>
        [TestMethod]
        public void TestGetTrips()
        {
            var user = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            Assert.AreEqual(0, user.Trips.Count);
        }

        #endregion
    }
}