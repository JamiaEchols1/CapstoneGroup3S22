using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    public class LodgingDAL
    {
        private static TravelPlannerDatabaseEntities _db = new TravelPlannerDatabaseEntities();

        public LodgingDAL() { }

        public LodgingDAL(TravelPlannerDatabaseEntities @object)
        {
            _db = @object;
        }

        public List<Lodging> GetLodgings(int tripId) => _db.Lodgings.Where(t => t.TripId == tripId).ToList();

        public Lodging CreateNewLodging(string location, DateTime startTime, DateTime endTime, int tripId)
        {
            if (string.IsNullOrEmpty(location))
            {
                string parameterName = "location";
                throw new ArgumentNullException(parameterName, "Must enter a location!");
            }
            if (LoggedUser.selectedTrip.StartDate.CompareTo(startTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }
            if (endTime.CompareTo(LoggedUser.selectedTrip.EndDate) > 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
            }
            if (startTime.CompareTo(endTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }

            Lodging lodging = new Lodging();
            lodging.Location = location;
            lodging.StartTime = startTime;
            lodging.EndTime = endTime;
            lodging.TripId = tripId;
            lodging.Id = _db.Lodgings.Count();

            _db.Lodgings.Add(lodging);
            _db.SaveChanges();
            return lodging;
        }
        public int RemoveLodging(Lodging lodging)
        {
            _db.Lodgings.Remove(lodging);
            return _db.SaveChanges();
        }

        public List<Lodging> GetOverlappingLodging(DateTime newStartTime, DateTime newEndTime)
        {
            List<Lodging> tripLodgings = this.GetLodgings(LoggedUser.selectedTrip.Id);
            List<Lodging> overlappingLodgings = new List<Lodging>();

            foreach(Lodging current in tripLodgings)
            {
                if (TimeChecker.timesOverlapping(newStartTime, newEndTime, current.StartTime, current.EndTime))
                {
                    overlappingLodgings.Add(current);
                }
            }
            return overlappingLodgings;

        }
    }
}
