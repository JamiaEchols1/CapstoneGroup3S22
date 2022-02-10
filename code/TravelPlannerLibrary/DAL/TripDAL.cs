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
        private static readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public static void CreateNewTrip(string name, DateTime startDate, DateTime endDate, int userId)
        {
            Trip trip = new Trip();
            trip.Name = name;
            trip.StartDate = startDate; 
            trip.EndDate = endDate;
            trip.UserId = userId;
            db.Trips.Add(trip);
            db.SaveChanges();
        }

        public static List<Trip> GetTrips(int userId) => db.Trips.Where(t => t.UserId == userId).ToList();


    }
}
