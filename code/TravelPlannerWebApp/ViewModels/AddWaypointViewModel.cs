using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;

namespace WebApplication4.ViewModels
{
    public class AddWaypointViewModel
    {
        public Waypoint waypoint { get; set; }

        public DateTime selectedTripStartDate = LoggedUser.selectedTrip.StartDate;

        public DateTime selectedTripEndDate = LoggedUser.selectedTrip.EndDate;
    }
}