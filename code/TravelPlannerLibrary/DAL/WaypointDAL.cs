using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    /// <summary>
    ///     The waypoint data access layer
    /// </summary>
    public class WaypointDal
    {
        #region Data members

        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaypointDal" /> class.
        /// </summary>
        public WaypointDal()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaypointDal" /> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public WaypointDal(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the waypoints with the specified trip identifier.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <returns>
        ///     A collection of all waypoints for the specified trip identifier
        /// </returns>
        public List<Waypoint> GetWaypoints(int tripId)
        {
            return db.Waypoints.Where(t => t.TripId == tripId).OrderBy(t => t.StartDateTime).ToList();
        }

        /// <summary>
        ///     Creates a new waypoint with the specified values.
        /// </summary>
        /// @precondition - !string.IsNullOrEmpty(location);
        /// startTime.CompareTo(endTime) &lt; 0;
        /// endTime.CompareTo(LoggedUser.selectedTrip.EndDate) &lt; 0;
        /// @postcondition - if input valid new waypoint is created, else none
        /// <param name="location">The location.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="tripId">The trip identifier.</param>
        /// <param name="description"> The trips description </param>
        /// <returns>
        ///     The newly created waypoint
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Must enter a location!</exception>
        /// <exception cref="System.ArgumentException">
        ///     Start date must be before end date
        ///     or
        ///     Start date must be on or after trip start date
        ///     or
        ///     End date must be on or before trip end date
        /// </exception>
        public Waypoint CreateNewWaypoint(string location, DateTime startTime, DateTime endTime, int tripId, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Must enter a description!");
            }
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("Must enter a location!");
            }

            if (startTime.CompareTo(endTime) >= 0)
            {
                throw new ArgumentException("Start date must be before end date");
            }

            if (LoggedUser.SelectedTrip.StartDate.CompareTo(startTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }

            if (endTime.CompareTo(LoggedUser.SelectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
            }

            var waypoint = new Waypoint {
                Location = location,
                StartDateTime = startTime,
                EndDateTime = endTime,
                TripId = tripId,
                Description = description,
                Id = this.FindNextId()
            };

            db.Waypoints.Add(waypoint);
            db.SaveChanges();
            return waypoint;
        }

        /// <summary>
        ///     Removes the specified waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>
        ///     1 if removed, 0 if fail
        /// </returns>
        public int RemoveWaypoint(Waypoint waypoint)
        {
            db.Waypoints.Remove(waypoint);
            return db.SaveChanges();
        }

        public int FindNextId()
        {
            if (!db.Waypoints.Any())
            {
                return 0;
            }

            var waypointId = db.Waypoints.Max(wp => wp.Id);
            waypointId++;
            return waypointId;
        }

        /// <summary>
        ///     Gets the waypoint by specified id.
        /// </summary>
        /// <param name="waypointId">The waypoint identifier.</param>
        /// <returns>
        /// The specifeid waypoint if found, or null otherwise
        /// </returns>
        public Waypoint GetWaypoint(int waypointId)
        {
            return db.Waypoints.FirstOrDefault(w => w.Id == waypointId);
        }

        /// <summary>
        ///     Gets the waypoints that overlap wit the specified date range.
        /// </summary>
        /// @precondition - newStartTime != null
        /// newEndTIme != null, newEndTime &gt; newStartTime
        /// <param name="newStartTime">The new start time to check against.</param>
        /// <param name="newEndTime">The new end time to check against.</param>
        /// <returns>
        ///     Collection of all waypoints that have overlapping time ranges with the specified date range
        /// </returns>
        public List<Waypoint> GetOverlappingWaypoints(DateTime newStartTime, DateTime newEndTime)
        {
            var tripWaypoints = this.GetWaypoints(LoggedUser.SelectedTrip.Id);

            return tripWaypoints.Where(current => TimeChecker.TimesOverlapping(newStartTime, newEndTime, current.StartDateTime, current.EndDateTime)).ToList();
        }

        /// <summary>
        ///     Gets the associated trip from one of its waypoint ids.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     trip the associated trip
        /// </returns>
        public Trip GetTripFromWaypoint(int id)
        {
            Trip trip = null;
            if (id >= 0)
            {
               trip = db.Trips.Where(x => x.Id == id).FirstOrDefault();
            }
            return trip;
        }

        #endregion
    }
}