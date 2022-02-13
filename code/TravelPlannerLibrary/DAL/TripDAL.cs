﻿using System;
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

        public static int CreateNewTrip(string name, DateTime startDate, DateTime endDate, int userId)
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
            if (startDate.CompareTo(endDate) > 0)
            {
                throw new ArgumentException("Start date must be before end date");
            }
            Trip trip = new Trip();
            trip.Id = db.Trips.Count();
            trip.Name = name;
            trip.StartDate = startDate; 
            trip.EndDate = endDate;
            trip.UserId = userId;
            db.Trips.Add(trip);
            return db.SaveChanges();
        }

        public static List<Trip> GetTrips(int userId) => db.Trips.Where(t => t.UserId == userId).ToList();


    }
}