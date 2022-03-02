using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;

namespace TravelPlannerLibrary.DAL
{
    public class WaypointDAL
    {
        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public WaypointDAL() { }

        public WaypointDAL(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        public List<Waypoint> GetWaypoints(int tripId) => db.Waypoints.Where(t => t.TripId == tripId).ToList();

        public Waypoint CreateNewWaypoint(string location, DateTime startTime, DateTime endTime, int tripId)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("Must enter a location!");
            }
            if (startTime == null)
            {
                throw new ArgumentNullException("Must enter a time");
            }
            if (endTime == null)
            {
                throw new ArgumentNullException("Must enter a time");
            }
            if (LoggedUser.selectedTrip.StartDate.CompareTo(startTime) >= 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }
            if (LoggedUser.selectedTrip.StartDate.CompareTo(endTime) >= 0)
            {
                throw new ArgumentException("End date must be on or after trip start date");
            }

            if (endTime.CompareTo(LoggedUser.selectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
            }
            if (startTime.CompareTo(LoggedUser.selectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("Start date must be on or before trip end date");
            }
            if (startTime.CompareTo(endTime) >= 0)
            {
                throw new ArgumentException("Start date must be before end date");
            }

            Waypoint waypoint = new Waypoint();
            waypoint.Location = location;
            waypoint.StartDateTime = startTime;
            waypoint.EndDateTime = endTime;
            waypoint.TripId = tripId;
            waypoint.Id = db.Waypoints.Count();
           
            db.Waypoints.Add(waypoint);
            db.SaveChanges();
            return waypoint;
        }

        public static int RemoveWaypoint(Waypoint waypoint)
        {
            db.Waypoints.Remove(waypoint);
            return db.SaveChanges();
        }

        public Waypoint GetWaypoint(int waypointId)
        {
            return db.Waypoints.Where(w => w.Id == waypointId).FirstOrDefault();
        }
    }
}
