using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DesktopModels
{
    /// <summary>
    ///     User tests
    /// </summary>
    [TestClass]
    public class UserTests
    {
        #region Methods

        /// <summary>
        ///     Tests the create user.
        /// </summary>
        [TestMethod]
        public void TestCreateUser()
        {
            var user = new User { Id = 1, Password = "password", Username = "username", Trips = new List<Trip>() };
            var desktopUser = new TravelPlannerDesktopApp.Models.User {
                Password = user.Password,
                Username = user.Username,
                Id = user.Id
            };
            Assert.AreEqual("username", desktopUser.Username);
            Assert.AreEqual("password", desktopUser.Password);
            Assert.AreEqual(1, desktopUser.Id);
            Assert.AreEqual(0, desktopUser.Trips.Count);
        }

        #endregion
    }
}