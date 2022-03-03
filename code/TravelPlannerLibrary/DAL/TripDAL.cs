using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;

namespace TravelPlannerLibrary.DAL
{
    public class TripDAL
    {
        private static TravelPlannerDatabaseEntities _db = new TravelPlannerDatabaseEntities();

        public TripDAL() { }
        public TripDAL(TravelPlannerDatabaseEntities @object)
        {
            _db = @object;
        }

        public int CreateNewTrip(string name, DateTime startDate, DateTime endDate, int userId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Trip must have a name");
            }

            if (startDate < DateTime.Now)
            {
                throw new ArgumentException("startDate date must not be before today");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("endDate date must be after start date");
            }
            Trip trip = new Trip();
            trip.Id = _db.Trips.Count();
            trip.Name = name;
            trip.StartDate = startDate; 
            trip.EndDate = endDate;
            trip.UserId = userId;
            _db.Trips.Add(trip);
            return _db.SaveChanges();
        }

        public void CreateTrip(Trip trip)
        {
            if (string.IsNullOrEmpty(trip.Name))
            {
                string parameterName = "trip.Name";
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
            _db.Trips.Add(trip);
            _db.SaveChanges();
        }

        public List<Trip> GetTrips(int userId) => _db.Trips.Where(t => t.UserId == userId).ToList();


    }
}
