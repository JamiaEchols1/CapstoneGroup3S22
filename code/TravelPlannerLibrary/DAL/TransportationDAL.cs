using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerLibrary.Models;

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
    }
}
