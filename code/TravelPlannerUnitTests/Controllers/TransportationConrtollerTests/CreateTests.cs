using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TravelPlannerLibrary;
using TravelPlannerLibrary.DAL;
using TravelPlannerLibrary.Models;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace TravelPlannerUnitTests.Controllers.TransportationConrtollerTests
{
    [TestClass]
    public class CreateTests
    {
        /// <summary>
        /// Tests the get create transport including null trip identifier.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTransportWithConflicts()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

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

            var result = controller.Create(addedTransportation);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        /// <summary>
        /// Tests the post create transport without conflicts.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTransportWithoutConflicts()
        {
            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(2);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

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

            var result = controller.Create(addedTransportation);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        /// <summary>
        /// Tests the state of the post create transport with invalid model.
        /// </summary>
        [TestMethod]
        public void TestPOSTCreateTransportWithInvalidModelState()
        {
            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(2);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Transportation>>();
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transportation>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TravelPlannerDatabaseEntities>();
            mockContext.Setup(c => c.Transportations).Returns(mockSet.Object);

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
            controller.ModelState.AddModelError("Mega", "Error");
            var result = controller.Create(addedTransportation);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Tests the get create transport.
        /// </summary>
        [TestMethod]
        public void TestGETCreateTransport()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();
            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(2);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 2
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

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

            var ErrorMessage = "Serious overlap issues";
            var result = controller.Create(0, ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        /// <summary>
        /// Tests the get create transport with null identifier.
        /// </summary>
        [TestMethod]
        public void TestGETCreateTransportWithNullId()
        {
            var tripData = new List<Trip> {
                new Trip {
                    Name = "Trip1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(14), UserId = 0, Id = 0
                }
            }.AsQueryable();
            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(2);
            var data = new List<Transportation> {
                new Transportation {
                    Description = "test transportation", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 0
                },
                new Transportation {
                    Description = "test transportation 1", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 1
                },
                new Transportation {
                    Description = "test transportation 2", StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMinutes(14), TripId = 0,
                    Id = 2
                }
            }.AsQueryable();

            var mockSetTrip = new Mock<DbSet<Trip>>();
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(tripData.Provider);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(tripData.Expression);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(tripData.ElementType);
            mockSetTrip.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(tripData.GetEnumerator());

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

            var ErrorMessage = "Serious overlap issues";
            var result = controller.Create(null, ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
