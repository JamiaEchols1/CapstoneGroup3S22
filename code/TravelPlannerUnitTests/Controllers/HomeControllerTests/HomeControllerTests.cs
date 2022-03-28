using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers
{
    /// <summary>
    ///     Tests for the home controller in the travel planner web application
    /// </summary>
    [TestClass]
    public class HomeControllerTests
    {
        /// <summary>
        ///     Tests a successful login page transition.
        /// </summary>
        [TestMethod]
        public void TestSuccessfulLogin()
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
            var controller = new HomeController(service);
            var userCredentials = new UserCredentials()
            {
                Password = decryptedPassword,
                Username = expectedUsername
            };

            var result = controller.AuthenticateLogin(userCredentials);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests a successful login page transition.
        /// </summary>
        [TestMethod]
        public void TestUnSuccessfulLogin()
        {
            const string unexpectedUsername = "user1";
            const string encryptedPassword = "JGSOzecXROhJSz7Ru0Z4Qg==";
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
            var controller = new HomeController(service);
            var userCredentials = new UserCredentials()
            {
                Password = encryptedPassword,
                Username = unexpectedUsername
            };

            var result = controller.AuthenticateLogin(userCredentials);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests an invalid model state for home controller.
        /// </summary>
        [TestMethod]
        public void TestInvalidModelState()
        {
            var userCredentials = new UserCredentials()
            {
                Password = "",
                Username = ""
            };
            var context = new ValidationContext(userCredentials, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(UserCredentials), typeof(UserCredentials)), typeof(UserCredentials));

            var isModelStateValid = Validator.TryValidateObject(userCredentials, context, results, true);
            Assert.AreEqual(false, isModelStateValid);
        }

        /// <summary>
        ///     Tests an invalid model state for home controller.
        /// </summary>
        [TestMethod]
        public void TestInvalidModelStateWithErrors()
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
            var controller = new HomeController(service);
            var userCredentials = new UserCredentials()
            {
                Password = decryptedPassword,
                Username = expectedUsername
            };
            controller.ModelState.AddModelError("Mega", "Error");
            var result = controller.AuthenticateLogin(userCredentials);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests an invalid model state for home controller.
        /// </summary>
        [TestMethod]
        public void TestReturnDefaultLoginView()
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
            var controller = new HomeController(service);
            var userCredentials = new UserCredentials()
            {
                Password = decryptedPassword,
                Username = expectedUsername
            };
            var result = controller.Login();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            controller = new HomeController();
        }
    }
}
