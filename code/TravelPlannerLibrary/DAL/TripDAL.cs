using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;

namespace TravelPlannerLibrary.DAL
{
    public class TripDAL
    {
        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public TripDAL() { }
        public TripDAL(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        public int CreateNewTrip(string name, DateTime startDate, DateTime endDate, int userId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Must enter a name!");
            }
            if (startDate == null)
            {
                throw new ArgumentNullException("Must enter a start date");
            }
            if (endDate == null)
            {
                throw new ArgumentNullException("Must enter an end date");
            }
            if (DateTime.Today.CompareTo(startDate) >= 0)
            {
                throw new ArgumentException("Start date must be on or after current date");
            }
            if (startDate.CompareTo(endDate) > 0)
            {
                throw new ArgumentException("Start date must be before end date");
            }
            Trip trip = new Trip();
            trip.Id = FindNextID();
            trip.Name = name;
            trip.StartDate = startDate; 
            trip.EndDate = endDate;
            trip.UserId = userId;
            db.Trips.Add(trip);
            return db.SaveChanges();
        }

        public List<Trip> GetTrips(int userId) => db.Trips.Where(t => t.UserId == userId).OrderBy(t => t.StartDate).ToList();

        public void RemoveTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
        }

        public int FindNextID()
        {
            if (db.Trips.Count() == 0)
            {
                return 0;
            }
            var tripId = db.Trips.Max(wp => wp.Id); ;
            tripId++;
            return tripId;
        }


    }
}
