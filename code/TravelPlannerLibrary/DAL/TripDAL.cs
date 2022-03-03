using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;

namespace TravelPlannerLibrary.DAL
{
    /// <summary>
    ///     The trip data access layer
    /// </summary>
    public class TripDal
    {
        #region Data members

        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TripDal" /> class.
        /// </summary>
        public TripDal()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TripDal" /> class.
        /// </summary>
        /// <param name="object">The database entity object.</param>
        public TripDal(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates a new trip with the specified values.
        /// </summary>
        /// @precondition - name != null
        /// startDate != null, startDate &gt;= DateTime.Now;
        /// endDate != null, endDate &gt;= startDate
        /// <param name="name">The name.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///     1 if creation success, 0 otherwise
        /// </returns>
        /// <exception cref="System.ArgumentNullException">name - Trip must have a name</exception>
        /// <exception cref="System.ArgumentException">
        ///     startDate date must not be before today
        ///     or
        ///     endDate date must be after start date
        /// </exception>
        public int CreateNewTrip(string name, DateTime startDate, DateTime endDate, int userId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Trip must have a name");
            }

            if (startDate < DateTime.Now)
            {
                throw new ArgumentException("startDate date must not be before today");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("endDate date must be after start date");
            }

            var trip = new Trip {
                Id = this.FindNextId(),
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                UserId = userId
            };
            db.Trips.Add(trip);
            return db.SaveChanges();
        }

        /// <summary>
        ///     Gets the trips for the specified userId.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///     Collection of all trips with the specified user identifier
        /// </returns>
        public List<Trip> GetTrips(int userId)
        {
            return db.Trips.Where(t => t.UserId == userId).OrderBy(t => t.StartDate).ToList();
        }

        /// <summary>
        ///     Creates the trip based on a trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <exception cref="System.ArgumentNullException">Trip must have a name</exception>
        /// <exception cref="System.ArgumentException">
        ///     startDate date must not be before today
        ///     or
        ///     endDate date must be after start date
        /// </exception>
        public void CreateTrip(Trip trip)
        {
            if (string.IsNullOrEmpty(trip.Name))
            {
                const string parameterName = "trip.Name";
                throw new ArgumentNullException(parameterName, "Trip must have a name");
            }

            if (trip.StartDate < DateTime.Now)
            {
                throw new ArgumentException("startDate date must not be before today");
            }

            if (trip.EndDate < trip.StartDate)
            {
                throw new ArgumentException("endDate date must be after start date");
            }

            db.Trips.Add(trip);
            db.SaveChanges();
        }

        /// <summary>
        ///     Finds the next available identifier.
        /// </summary>
        /// <returns>
        ///     The next available identifier for a trip
        /// </returns>
        public int FindNextId()
        {
            if (!db.Trips.Any())
            {
                return 0;
            }

            var tripId = db.Trips.Max(wp => wp.Id);
            tripId++;
            return tripId;
        }

        /// <summary>
        ///     Removes the trip with the specifeid identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// @preconditon - none
        /// @postconditon - the trip with the id and all its waypoints, lodging and transport are removed if it exists, else none
        public void RemoveTrip(int id)
        {
            var trip = db.Trips.Find(id);
            if (trip != null)
            {
                db.Trips.Remove(trip);
            }

            db.SaveChanges();
        }

        #endregion
    }
}