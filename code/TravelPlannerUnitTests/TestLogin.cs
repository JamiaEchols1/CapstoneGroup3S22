using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests
{
    [TestClass]
    public class TestLogin
    {
        [TestMethod]
        public void TestGetAllUsers()
        {
            var data = new List<User>
            {
                new User { Username = "user1", Password = "pw" },
                new User { Username = "user2", Password = "pw" },
                new User { Username = "user3", Password = "pw" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDAL(mockContext.Object);
            var users = service.GetAllUsers();

            Assert.AreEqual(3, users.Count);
            Assert.AreEqual("user1", users[0].Username);
            Assert.AreEqual("user2", users[1].Username);
            Assert.AreEqual("user3", users[2].Username);
        }

        [TestMethod]
        public void TestValidUserLogin()
        {
            string expectedUsername = "user1";
            string encryptedPassword = "JGSOzecXROhJSz7Ru0Z4Qg==";
            string decryptedPassword = "password";
            var data = new List<User>
            {
                new User { Username = "user1", Password = encryptedPassword },
                new User { Username = "user2", Password = encryptedPassword },
                new User { Username = "user3", Password = encryptedPassword },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDAL(mockContext.Object);
            var loggedUser = service.Login(expectedUsername, decryptedPassword);

            Assert.AreEqual(expectedUsername, loggedUser.Username);
            Assert.AreEqual(encryptedPassword, loggedUser.Password);
        }

        [TestMethod]
        public void TestInvalidUserLogin()
        {
            string expectedUsername = "user4";
            string expectedPassword = "pw4";
            var data = new List<User>
            {
                new User { Username = "user1", Password = "pw1" },
                new User { Username = "user2", Password = "pw2" },
                new User { Username = "user3", Password = "pw3" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDAL(mockContext.Object);
            var loggedUser = service.Login(expectedUsername, expectedPassword);

            Assert.AreEqual(null, loggedUser);
        }
    }
}
