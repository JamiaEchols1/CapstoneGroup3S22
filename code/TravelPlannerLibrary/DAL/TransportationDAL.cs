﻿using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    /// <summary>
    ///     The transportation data access layer
    /// </summary>
    public class TransportationDal
    {
        #region Data members

        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportationDal" /> class.
        /// </summary>
        public TransportationDal()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportationDal" /> class.
        /// </summary>
        /// <param name="object">The database entity object.</param>
        public TransportationDal(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets all the transportations for the specified trip id.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <returns>
        ///     A collection of all transportations with the specified trip id
        /// </returns>
        public List<Transportation> GetTransportationsByTrip(int tripId)
        {
            return db.Transportations.Where(t => t.TripId == tripId).ToList();
        }

        /// <summary>
        ///     Creates a new transportation.
        /// </summary>
        /// @precondition - !string.IsNullOrEmpty(description);
        /// startTime.CompareTo(endTime) &lt;= 0;
        /// arrivalWaypointId != departingWaypointId;
        /// selectedWaypoint.EndDateTime.CompareTo(startTime) &lt;= 0;
        /// @postcondition - transportation is added to the db with the specified values
        /// <param name="tripId">The trip identifier.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        ///     The number of entries written to the database, 0 if transport not created, 1 if creation was successful
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Description cannot be null</exception>
        /// <exception cref="System.ArgumentException">
        ///     End date must be on or after selected start date
        ///     or
        ///     Departure waypoint cannot be the same as arrival waypoint
        ///     or
        ///     Start date must be on or after waypoint end date
        /// </exception>
        public Transportation CreateANewTransportation(int tripId,
            DateTime startTime, DateTime endTime, string description, string type)
        {
            if (string.IsNullOrEmpty(description))
            {
                const string parameterName = "description";
                throw new ArgumentNullException(parameterName, "Description cannot be null");
            }

            if (startTime.CompareTo(endTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }

            if (LoggedUser.SelectedTrip.StartDate.CompareTo(startTime) >= 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }

            if (endTime.CompareTo(LoggedUser.SelectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
            }

            var transportation = new Transportation {
                Id = db.Transportations.Count(),
                TripId = tripId,
                StartTime = startTime,
                EndTime = endTime,
                Description = description,
                Type = type
            };
            db.Transportations.Add(transportation);
            db.SaveChanges();
            return transportation;
        }

        /// <summary>
        ///     Creates a new transportation based off of the input transportation.
        /// </summary>
        /// @precondition - !string.IsNullOrEmpty(transportation.Description);
        /// transportation.StartTime.CompareTo(transportation.EndTime) &lt;= 0;
        /// transportation.ArrivingWaypointId != transportation.DepartingWaypointId;
        /// LoggedUser.selectedWaypoint.EndDateTime.CompareTo(transportation.StartTime) &lt;= 0
        /// <param name="transportation">The transportation.</param>
        /// <returns>
        ///     1 if creation success, 0 if failed
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Description cannot be null</exception>
        /// <exception cref="System.ArgumentException">
        ///     End date must be on or after selected start date
        ///     or
        ///     Departure waypoint cannot be the same as arrival waypoint
        ///     or
        ///     Start date must be on or after waypoint end date
        /// </exception>
        public int CreateANewTransportation(Transportation transportation)
        {
            if (string.IsNullOrEmpty(transportation.Description))
            {
                const string parameterName = "Description";
                throw new ArgumentNullException(parameterName, "Description cannot be null");
            }

            if (transportation.StartTime.CompareTo(transportation.EndTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }

            if (LoggedUser.SelectedTrip.EndDate.CompareTo(transportation.StartTime) < 0)
            {
                throw new ArgumentException("Start date must be before trip end date");
            }

            db.Transportations.Add(transportation);
            return db.SaveChanges();
        }

        /// <summary>
        ///     Deletes the specified transportation.
        /// </summary>
        /// @precondition - none
        /// @postcondition - Transportation is removed from db
        /// <param name="transportation">The transportation.</param>
        /// <returns>
        ///     1 if successful, 0 if failed
        /// </returns>
        public int DeleteTransportation(Transportation transportation)
        {
            db.Transportations.Remove(transportation);
            return db.SaveChanges();
        }

        /// <summary>
        ///     Gets the overlapping transportation.
        /// </summary>
        /// @precondition - newStartTime != null
        /// newEndTime != null
        /// @postcondition - none
        /// <param name="newStartTime">The new start time.</param>
        /// <param name="newEndTime">The new end time.</param>
        /// <returns>
        ///     A collection of all transportation that overlaps the specified time range
        /// </returns>
        public List<Transportation> GetOverlappingTransportation(DateTime newStartTime, DateTime newEndTime)
        {
            var tripTransportation = this.GetTransportationsByTrip(LoggedUser.SelectedTrip.Id);

            return tripTransportation.Where(current => TimeChecker.TimesOverlapping(newStartTime, newEndTime, current.StartTime, current.EndTime)).ToList();
        }


        /// <summary>
        /// Gets the transportation by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The transportation with the specified id
        /// </returns>
        public Transportation GetTransportationById(int id)
        {
            return db.Transportations.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///     Gets the associated trip from one of its waypoint ids.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     trip the associated trip
        /// </returns>
        public Trip GetTripFromTransportation(int id)
        {
            Trip trip = null;
            if (id >= 0)
            {
                trip = db.Trips.FirstOrDefault(x => x.Id == id);
            }
            return trip;
        }

        #endregion
    }
}