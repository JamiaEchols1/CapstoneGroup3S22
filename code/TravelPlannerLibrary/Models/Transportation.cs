namespace TravelPlannerLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using TravelPlannerLibrary.DAL;

    // The transportation class
    //
    //@version Spring 2022
    //@author Jamia Echols
    public partial class Transportation
    {
        
        // The Id
        public int Id { get; set; }
        
        // The departing waypoint id
        public int DepartingWaypointId {
            get { return DepartingWaypointId; }
            set
            {
                if (value == ArrivingWaypointId)
                {
                    throw new ArgumentException("Departing waypoint must not equal departing waypoint");
                }
                DepartingWaypointId = value;
            }
        }

        // The arriving waypoint
        public int ArrivingWaypointId
        {
            get { return ArrivingWaypointId; }
            set
            {
                if (value == DepartingWaypointId) {
                    throw new ArgumentException("Arriving waypoint must not equal departing waypoint");
                }
                ArrivingWaypointId = value;
            }
        }

        // The trip Id
        public int TripId { get; set; }
        
        // The start time
        public System.DateTime StartTime
        {
            get { return StartTime; }
            set
            {
                if (value < Waypoint.DateTime)
                {
                    throw new ArgumentException("Start time must be after the waypoint time");
                }
                StartTime = value;
            }
        }

        // The end time
        public System.DateTime EndTime { 
            get { return EndTime; }
            set
            {
                if (value < StartTime)
                {
                    throw new ArgumentException("End time must be after the start time.");
                }
                EndTime = value;
            } 
        }

        //  The description
        public string Description { 
            get { return this.Description; }
            set
            {
                if (string.IsNullOrEmpty(value)) {
                    throw new ArgumentNullException("Transportation must have a description");
                }
                Description = value;
            }
        }
    
        //The Waypoint
        public virtual Waypoint Waypoint { get; set; }
    }
}
