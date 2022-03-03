using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

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

            if (startTime.CompareTo(endTime) >= 0)
            {
                throw new ArgumentException("Start date must be before end date");
            }

            if (LoggedUser.selectedTrip.StartDate.CompareTo(startTime) >= 0)
            {
                throw new ArgumentException("Start date must be on or after trip start date");
            }

            if (endTime.CompareTo(LoggedUser.selectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("End date must be on or before trip end date");
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

        public int RemoveWaypoint(Waypoint waypoint)
        {
            db.Waypoints.Remove(waypoint);
            return db.SaveChanges();
        }

        public Waypoint GetWaypoint(int waypointId)
        {
            return db.Waypoints.Where(w => w.Id == waypointId).FirstOrDefault();
        }

        public List<Waypoint> GetOverlappingWaypoints(DateTime newStartTime, DateTime newEndTime)
        {
            List<Waypoint> tripWaypoints = this.GetWaypoints(LoggedUser.selectedTrip.Id);
            List<Waypoint> overlappingWaypoints = new List<Waypoint>();

            foreach (Waypoint current in tripWaypoints)
            {
                if (TimeChecker.timesOverlapping(newStartTime, newEndTime, current.StartDateTime, current.EndDateTime))
                {
                    overlappingWaypoints.Add(current);
                }
            }
            return overlappingWaypoints;

        }
    }
}
