using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    public class TransportationDAL
    {
        private static TravelPlannerDatabaseEntities _db = new TravelPlannerDatabaseEntities();

        public TransportationDAL() { }

        public TransportationDAL(TravelPlannerDatabaseEntities @object)
        {
            _db = @object;
        }

        public List<Transportation> GetTransportation(int departingWaypointId, int arrivalWaypointId)
        {
             return _db.Transportations.Where(t => t.DepartingWaypointId == departingWaypointId && t.ArrivingWaypointId == arrivalWaypointId).ToList();
        }

        public List<Transportation> GetTransportations(int tripId)
        {
            return _db.Transportations.Where(t => t.TripId == tripId).ToList();
        }

        public int CreateANewTransportation(int departingWaypointId, int arrivalWaypointId, int tripId, DateTime startTime, DateTime endTime, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                string parameterName = "description";
                throw new ArgumentNullException(parameterName, "Description cannot be null");
            }
            if (startTime.CompareTo(endTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }

            if (arrivalWaypointId == departingWaypointId)
            {
                throw new ArgumentException("Departure waypoint cannot be the same as arrival waypoint");
            }
            if (LoggedUser.selectedWaypoint.EndDateTime.CompareTo(startTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after waypoint end date");
            }
            Transportation transportation = new Transportation();
            transportation.Id = _db.Transportations.Count();
            transportation.TripId = tripId;
            transportation.StartTime = startTime;
            transportation.EndTime = endTime;
            transportation.Description = description;
            transportation.ArrivingWaypointId = arrivalWaypointId;
            transportation.DepartingWaypointId = departingWaypointId;
            _db.Transportations.Add(transportation);
            return _db.SaveChanges();
        }

        public int CreateANewTransportation(Transportation transportation)
        {
            if (string.IsNullOrEmpty(transportation.Description))
            {
                string parameterName = "Description";
                throw new ArgumentNullException(parameterName, "Description cannot be null");
            }
            if (transportation.StartTime.CompareTo(transportation.EndTime) > 0)
            {
                throw new ArgumentException("End date must be on or after selected start date");
            }

            if (transportation.ArrivingWaypointId == transportation.DepartingWaypointId)
            {
                throw new ArgumentException("Departure waypoint cannot be the same as arrival waypoint");
            }
            if (LoggedUser.selectedWaypoint.EndDateTime.CompareTo(transportation.StartTime) > 0)
            {
                throw new ArgumentException("Start date must be on or after waypoint end date");
            }
            _db.Transportations.Add(transportation);
            return _db.SaveChanges();
        }

        public int DeleteTransportation(Transportation transportation)
        {
            _db.Transportations.Remove(transportation);
            return _db.SaveChanges();
        }

        public List<Transportation> GetOverlappingTransportation(DateTime newStartTime, DateTime newEndTime)
        {
            List<Transportation> tripTransportation = this.GetTransportations(LoggedUser.selectedTrip.Id);
            List<Transportation> overlappingTransportation = new List<Transportation>();

            foreach (Transportation current in tripTransportation)
            {
                if (TimeChecker.timesOverlapping(newStartTime, newEndTime, current.StartTime, current.EndTime))
                {
                    overlappingTransportation.Add(current);
                }
            }

            return overlappingTransportation;
        }
    }
}
