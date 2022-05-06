using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    /// <summary>
    ///     The lodging data access layer
    /// </summary>
    public class LodgingDal
    {
        #region Data members

        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LodgingDal" /> class.
        /// </summary>
        public LodgingDal()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LodgingDal" /> class.
        /// </summary>
        /// <param name="object">The database entity object.</param>
        public LodgingDal(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the lodgings.
        /// </summary>
        /// <param name="tripId">The trip identifier.</param>
        /// <returns>
        ///     The list of all lodgings for the specified trip id
        /// </returns>
        public List<Lodging> GetLodgings(int tripId)
        {
            return db.Lodgings.Where(t => t.TripId == tripId).ToList();
        }

        /// <summary>
        ///     Creates a new lodging.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="tripId">The trip identifier.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        ///     The newly created lodging
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Must enter a location!</exception>
        /// <exception cref="System.ArgumentException">
        ///     Start date must be on or after trip start date
        ///     or
        ///     End date must be on or before trip end date
        ///     or
        ///     End date must be on or after selected start date
        /// </exception>
        public Lodging CreateNewLodging(string location, DateTime startTime, DateTime endTime, int tripId,
            string description)
        {
            if (string.IsNullOrEmpty(location))
            {
                const string parameterName = "location";
                throw new ArgumentNullException(parameterName, "Must enter a location!");
            }

            if (LoggedUser.SelectedTrip.StartDate.CompareTo(startTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }

            if (endTime.CompareTo(LoggedUser.SelectedTrip.EndDate) > 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
            }

            if (startTime.CompareTo(endTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }

            var lodging = new Lodging {
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                TripId = tripId,
                Id = this.FindNextId(),
                Description = description
            };

            db.Lodgings.Add(lodging);
            db.SaveChanges();
            return lodging;
        }

        /// <summary>
        /// Edits the lodging.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Must enter a location!</exception>
        /// <exception cref="System.ArgumentException">
        /// Start date must be on or after trip start date
        /// or
        /// End date must be on or before trip end date
        /// or
        /// End date must be on or after selected start date
        /// </exception>
        public Lodging EditLodging(string location, DateTime startTime, DateTime endTime, string description)
        {
            if (string.IsNullOrEmpty(location))
            {
                const string parameterName = "location";
                throw new ArgumentNullException(parameterName, "Must enter a location!");
            }

            if (LoggedUser.SelectedTrip.StartDate.CompareTo(startTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }

            if (endTime.CompareTo(LoggedUser.SelectedTrip.EndDate) > 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
            }

            if (startTime.CompareTo(endTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }
            int id = LoggedUser.SelectedLodging.Id;
            var lodging = new Lodging
            {
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                TripId = LoggedUser.SelectedTrip.Id,
                Id = id,
                Description = description
            };
            db.Lodgings.Remove(db.Lodgings.Find(id));
            db.Lodgings.Add(lodging);
            db.SaveChanges();
            return lodging;
        }


        /// <summary>
        ///     Removes the specified lodging.
        /// </summary>
        /// <param name="lodging">The lodging to be removed.</param>
        /// <returns>
        ///     The number of state entries written to the database
        /// </returns>
        public int RemoveLodging(Lodging lodging)
        {
            db.Lodgings.Remove(lodging);
            return db.SaveChanges();
        }

        /// <summary>
        ///     Gets all of the overlapping lodgings.
        /// </summary>
        /// @precondition - newStartTime != null
        /// newEndTime != null
        /// @postcondition - none
        /// <param name="newStartTime">The start time to check against.</param>
        /// <param name="newEndTime">The end time to check against.</param>
        /// <returns>
        ///     A list with all lodgings that overlap the input time span
        /// </returns>
        public List<Lodging> GetOverlappingLodging(DateTime newStartTime, DateTime newEndTime)
        {
            var tripLodgings = this.GetLodgings(LoggedUser.SelectedTrip.Id);

            return tripLodgings.Where(current =>
                TimeChecker.TimesOverlapping(newStartTime, newEndTime, current.StartTime, current.EndTime)).ToList();
        }

        /// <summary>
        ///     Gets the overlapping lodgings for updated lodging.
        /// </summary>
        /// <param name="newStartTime">The new start time.</param>
        /// <param name="newEndTime">The new end time.</param>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     List of overlapping lodgings excluding the lodging parameter
        /// </returns>
        public List<Lodging> GetOverlappingLodgingsForUpdatedLodging(DateTime newStartTime, DateTime newEndTime, Lodging lodging)
        {
            var tripLodgings = this.GetLodgings(LoggedUser.SelectedTrip.Id);

            return tripLodgings.Where(current =>
                                    TimeChecker.TimesOverlapping(newStartTime, newEndTime, current.StartTime,
                                        current.EndTime)).Where(current => current.Id != lodging.Id)
                                .ToList();
        }

        /// <summary>
        ///     Updates the lodging.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     the updated lodging item if exists, null otherwise
        /// </returns>
        public Lodging WebUpdateLodging(Lodging lodging)
        {
            if (lodging != null)
            {
                var editedLodging = db.Lodgings.First(a => a.Id == lodging.Id);
                editedLodging.Location = lodging.Location;
                editedLodging.StartTime = lodging.StartTime;
                editedLodging.EndTime = lodging.EndTime;
                editedLodging.Description = lodging.Description;
                db.SaveChanges();
                return editedLodging;
            }
            return null;
        }

        /// <summary>
        ///     Gets the lodging by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     lodging the lodging point
        /// </returns>
        public Lodging GetLodgingById(int id)
        {
            if (id < 0)
            {
                return null;
            }
            return db.Lodgings.First(x => x.Id == id);
        }

        /// <summary>
        ///     Finds the next available identifier.
        /// </summary>
        /// <returns>
        ///     The next available identifier for a lodging point
        /// </returns>
        public int FindNextId()
        {
            if (!db.Lodgings.Any())
            {
                return 0;
            }

            var lodgingId = db.Lodgings.Max(wp => wp.Id);
            lodgingId++;
            return lodgingId;
        }

        #endregion
    }
}