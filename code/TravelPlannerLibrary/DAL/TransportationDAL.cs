using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;
using TravelPlannerLibrary.Util;

namespace TravelPlannerLibrary.DAL
{
    public class TransportationDAL
    {
        private static TravelPlannerDatabaseEntities db = new TravelPlannerDatabaseEntities();

        public TransportationDAL() { }
        public TransportationDAL(TravelPlannerDatabaseEntities @object)
        {
            db = @object;
        }

        public List<Transportation> GetTransportation(int departingWaypointId, int arrivalWaypointId)
        {
             return db.Transportations.Where(t => t.DepartingWaypointId == departingWaypointId && t.ArrivingWaypointId == arrivalWaypointId).ToList();
        }

        public List<Transportation> GetTransportations(int tripId)
        {
            return db.Transportations.Where(t => t.TripId == tripId).ToList();
        }

        public int CreateANewTransportation(int departingWaypointId, int arrivalWaypointId, int tripId, DateTime startTime, DateTime endTime, string Description)
        {
            if (Description == null)
            {
                throw new ArgumentNullException("Description cannot be null");
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
            Transportation transportation = new Transportation();
            transportation.Id = db.Transportations.Count();
            transportation.TripId = tripId;
            transportation.StartTime = startTime;
            transportation.EndTime = endTime;
            transportation.Description = Description;
            transportation.ArrivingWaypointId = arrivalWaypointId;
            transportation.DepartingWaypointId = departingWaypointId;
            db.Transportations.Add(transportation);
            return db.SaveChanges();
        }

        public int CreateANewTransportation(Transportation transportation)
        {

            db.Transportations.Add(transportation);
            return db.SaveChanges();
        }

        public int DeleteTransportation(Transportation transportation)
        {
            db.Transportations.Remove(transportation);
            return db.SaveChanges();
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
