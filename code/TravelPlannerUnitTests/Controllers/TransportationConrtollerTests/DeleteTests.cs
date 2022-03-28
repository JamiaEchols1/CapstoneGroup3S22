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

namespace TravelPlannerUnitTests.Controllers.TransportationConrtollerTests
{
    /// <summary>
    ///     Tests the transport delete methods
    /// </summary>
    [TestClass]
    public class DeleteTests
    {
        #region Methods

        /// <summary>
        ///     Tests the delete with nonexisting identifier.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithNonexistingId()
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var result = controller.Delete(-1);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the delete with null identifier.
        /// </summary>
        [TestMethod]
        public void TestDeleteWithNullId()
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var result = controller.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        /// <summary>
        ///     Tests the delete existing.
        /// </summary>
        [TestMethod]
        public void TestDeleteExisting()
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var result = controller.Delete(0);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        ///     Tests the delete confimed.
        /// </summary>
        [TestMethod]
        public void TestDeleteConfimed()
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
            LoggedUser.SelectedTrip = new Trip {
                Name = "Trip1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                UserId = 0,
                Id = 0
            };

            var result = controller.DeleteConfirmed(0);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        #endregion
    }
}