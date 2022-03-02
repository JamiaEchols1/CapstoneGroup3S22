﻿using System;
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

        public List<Waypoint> GetWaypoints(int tripId) => db.Waypoints.Where(t => t.TripId == tripId).OrderBy(t => t.DateTime).ToList();

        public Waypoint CreateNewWaypoint(string location, DateTime time, int tripId)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("Must enter a location!");
            }
            if (time == null)
            {
                throw new ArgumentNullException("Must enter a time");
            }
            if (LoggedUser.selectedTrip.StartDate.CompareTo(time) >= 0)
            {
                throw new ArgumentException("Date must be on or after trip start date");
            }
            if (time.CompareTo(LoggedUser.selectedTrip.EndDate) >= 0)
            {
                throw new ArgumentException("Date must be on or before trip end date");
            }

            Waypoint waypoint = new Waypoint();
            waypoint.Location = location;
            waypoint.DateTime = time;
            waypoint.TripId = tripId;
            waypoint.Id = FindNextID();
           
            db.Waypoints.Add(waypoint);
            db.SaveChanges();
            return waypoint;
        }

        public static int RemoveWaypoint(Waypoint waypoint)
        {
            db.Waypoints.Remove(waypoint);
            return db.SaveChanges();
        }

        public static Waypoint FindWaypointByID(int id)
        {
            Waypoint waypoint = db.Waypoints.Find(id);
            return waypoint;
        }

        public int FindNextID()
        {
            if (db.Waypoints.Count() == 0)
            {
                return 0;
            }
            var waypointId = db.Waypoints.OrderByDescending(t => t.Id).FirstOrDefault().Id;
            waypointId++;
            return waypointId;
        }
    }
}
