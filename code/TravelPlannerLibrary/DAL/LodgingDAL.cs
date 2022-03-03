using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    public class LodgingDAL
    {
        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public LodgingDAL() { }

        public LodgingDAL(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        public List<Lodging> GetLodgings(int tripId) => db.Lodgings.Where(t => t.TripId == tripId).ToList();

        public Lodging CreateNewLodging(string location, DateTime startTime, DateTime endTime, int tripId)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("Must enter a location!");
            }
            if (startTime == null)
            {
                throw new ArgumentNullException("Must enter a start time");
            }
            if (endTime == null)
            {
                throw new ArgumentNullException("Must enter an end time");
            }
            if (LoggedUser.selectedTrip.StartDate.CompareTo(startTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }
            if (startTime.CompareTo(LoggedUser.selectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("Start date must be before trip end date");
            }
            if (LoggedUser.selectedTrip.StartDate.CompareTo(endTime) >= 0)
            {
                throw new ArgumentException("End date must be after trip start date");
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
            lodging.Id = db.Lodgings.Count();

            db.Lodgings.Add(lodging);
            db.SaveChanges();
            return lodging;
        }
        public int RemoveLodging(Lodging lodging)
        {
            db.Lodgings.Remove(lodging);
            return db.SaveChanges();
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
