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
        private static readonly TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public static List<Waypoint> GetWaypoints(int tripId) => db.Waypoints.Where(t => t.TripId == tripId).ToList();

        public static Waypoint CreateNewWaypoint(string location, TimeSpan time, int tripId)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("Must enter a location!");
            }
            if (time == null)
            {
                throw new ArgumentNullException("Must enter a time");
            }

            Waypoint waypoint = new Waypoint();
            waypoint.Location = location;
            waypoint.Time = time;
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
    }
}
