using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers.TransportationConrtollerTests
{
    /// <summary>
    ///     Create tests class for transport
    /// </summary>
    [TestClass]
    public class EditTests
    {
        #region Methods

        /// <summary>
        ///     Tests the GET edit transportation with no error message.
        /// </summary>
        [TestMethod]
        public void TestGETEditTransportation()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var service = new TransportationDal(mockContext.Object);

            var controller = new TransportationController(service);
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var transportation = new Transportation {
                Description = "transportation",
                StartTime = startDate,
                EndTime = endDate,
                Id = 1,
                TripId = 1
            };

            var addedTransportation = new AddedTransportation {
                Id = 1,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = 1,
                Description = transportation.Description
            };

            var result = controller.Edit(0, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the get edit transportation with error message.
        /// </summary>
        [TestMethod]
        public void TestGETEditTransportationWithErrorMessage()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var service = new TransportationDal(mockContext.Object);

            var controller = new TransportationController(service);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var transportation = new Transportation
            {
                Description = "transportation",
                StartTime = startDate,
                EndTime = endDate,
                Id = 1,
                TripId = 1
            };

            var addedTransportation = new AddedTransportation
            {
                Id = 1,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = 1,
                Description = transportation.Description
            };
            const string errorMessage = "Serious overlap issues";
            var result = controller.Edit(0, errorMessage);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the POST edit transportation without conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTEditTransportationWithoutConflicts()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var service = new TransportationDal(mockContext.Object);

            var controller = new TransportationController(service);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var transportation = new Transportation
            {
                Description = "transportation",
                StartTime = startDate.AddDays(3),
                EndTime = endDate.AddDays(3),
                Id = 0,
                TripId = 0,
                Origin = "UWG Bookstore Carrollton GA 30117",
                Destination = "Quilt Museum Carrollton GA 30117"
            };

            var addedTransportation = new AddedTransportation
            {
                Id = transportation.Id,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = 0,
                Description = transportation.Description,
                Origin = transportation.Origin,
                Destination = transportation.Destination
            };

            var result = controller.Edit(addedTransportation);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        ///     Tests the post edit transportation with conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTEditTransportationWithConflicts()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                },
               new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddHours(2), TripId = 0,
                    Id = 1, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var service = new TransportationDal(mockContext.Object);

            var controller = new TransportationController(service);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var transportation = new Transportation
            {
                Description = "transportation",
                StartTime = startDate.AddDays(1),
                EndTime = endDate.AddDays(1).AddHours(2),
                Id = 0,
                TripId = 0,
                Origin = "UWG Bookstore Carrollton GA 30117",
                Destination = "Quilt Museum Carrollton GA 30117"
            };

            var addedTransportation = new AddedTransportation
            {
                Id = transportation.Id,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = 0,
                Description = transportation.Description,
                Origin = transportation.Origin,
                Destination = transportation.Destination
            };

            var result = controller.Edit(addedTransportation);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            controller.ModelState.AddModelError("Mega", "Error");
            result = controller.Edit(addedTransportation);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the update null transportation.
        /// </summary>
        [TestMethod]
        public void TestUpdateNullTransportation()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                },
               new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddHours(2), TripId = 0,
                    Id = 1, Origin = "UWG Bookstore Carrollton GA 30117", Destination = "Quilt Museum Carrollton GA 30117"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);
            mockContext.Setup(c => c.Trips).Returns(mockSetTrip.Object);

            var service = new TransportationDal(mockContext.Object);

            var controller = new TransportationController(service);
            LoggedUser.SelectedTrip = new Trip
            {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var transportation = new Transportation
            {
                Description = "transportation",
                StartTime = startDate.AddDays(1),
                EndTime = endDate.AddDays(1).AddHours(2),
                Id = 0,
                TripId = 0,
                Origin = "UWG Bookstore Carrollton GA 30117",
                Destination = "Quilt Museum Carrollton GA 30117"
            };

            var addedTransportation = new AddedTransportation
            {
                Id = transportation.Id,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = 0,
                Description = transportation.Description,
                Origin = transportation.Origin,
                Destination = transportation.Destination
            };

            var nullCheck = service.WebUpdateTransportation(null);
            Assert.AreEqual(nullCheck, null);
        }

        #endregion
    }
}