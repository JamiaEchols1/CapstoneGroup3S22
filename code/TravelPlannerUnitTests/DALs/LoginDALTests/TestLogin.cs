using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;

namespace TravelPlannerUnitTests.DALs.LoginDALTests
{
    /// <summary>
    /// Tests login
    /// </summary>
    [TestClass]
    public class TestLogin
    {
        #region Methods

        /// <summary>
        /// Tests the get all users.
        /// </summary>
        [TestMethod]
        public void TestGetAllUsers()
        {
            var data = new List<User> {
                new User { Username = "user1", Password = "pw" },
                new User { Username = "user2", Password = "pw" },
                new User { Username = "user3", Password = "pw" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);
            var users = service.GetAllUsers();

            Assert.AreEqual(3, users.Count);
            Assert.AreEqual("user1", users[0].Username);
            Assert.AreEqual("user2", users[1].Username);
            Assert.AreEqual("user3", users[2].Username);
        }

        /// <summary>
        /// Tests the valid user login.
        /// </summary>
        [TestMethod]
        public void TestValidUserLogin()
        {
            const string expectedUsername = "user1";
            const string encryptedPassword = "JGSOzecXROhJSz7Ru0Z4Qg==";
            const string decryptedPassword = "password";
            var data = new List<User> {
                new User { Username = "user1", Password = encryptedPassword },
                new User { Username = "user2", Password = encryptedPassword },
                new User { Username = "user3", Password = encryptedPassword }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);
            var loggedUser = service.CheckLoginCredentials(expectedUsername, decryptedPassword);

            Assert.AreEqual("user1", loggedUser.Username);
        }

        /// <summary>
        /// Tests the invalid user login.
        /// </summary>
        [TestMethod]
        public void TestInvalidUserLogin()
        {
            const string expectedUsername = "user4";
            const string expectedPassword = "pw4";
            var data = new List<User> {
                new User { Username = "user1", Password = "pw1" },
                new User { Username = "user2", Password = "pw2" },
                new User { Username = "user3", Password = "pw3" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);
            var loggedUser = service.CheckLoginCredentials(expectedUsername, expectedPassword);

            Assert.AreEqual(null, loggedUser);
        }

        /// <summary>
        /// Tests the null username.
        /// </summary>
        [TestMethod]
        public void TestNullUsername()
        {
            const string expectedPassword = "pw4";
            var data = new List<User> {
                new User { Username = "user1", Password = "pw1" },
                new User { Username = "user2", Password = "pw2" },
                new User { Username = "user3", Password = "pw3" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() => service.CheckLoginCredentials(null, expectedPassword));
        }

        /// <summary>
        /// Tests the empty username.
        /// </summary>
        [TestMethod]
        public void TestEmptyUsername()
        {
            const string expectedPassword = "pw4";
            var data = new List<User> {
                new User { Username = "user1", Password = "pw1" },
                new User { Username = "user2", Password = "pw2" },
                new User { Username = "user3", Password = "pw3" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() => service.CheckLoginCredentials("", expectedPassword));
        }

        /// <summary>
        /// Tests the null password.
        /// </summary>
        [TestMethod]
        public void TestNullPassword()
        {
            const string expectedUsername = "user4";

            var data = new List<User> {
                new User { Username = "user1", Password = "pw1" },
                new User { Username = "user2", Password = "pw2" },
                new User { Username = "user3", Password = "pw3" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() => service.CheckLoginCredentials(expectedUsername, null));
        }

        /// <summary>
        /// Tests the empty password.
        /// </summary>
        [TestMethod]
        public void TestEmptyPassword()
        {
            const string expectedUsername = "user4";

            var data = new List<User> {
                new User { Username = "user1", Password = "pw1" },
                new User { Username = "user2", Password = "pw2" },
                new User { Username = "user3", Password = "pw3" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new LoginDal(mockContext.Object);

            Assert.ThrowsException<ArgumentNullException>(() => service.CheckLoginCredentials(expectedUsername, ""));
        }

        #endregion
    }
}