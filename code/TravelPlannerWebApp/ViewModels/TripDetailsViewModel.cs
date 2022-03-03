using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;

namespace WebApplication4.ViewModels
{
    public class TripDetailsViewModel
    {
        public Trip Trip { get; set; }
        public IEnumerable<Waypoint> Waypoints { get; set; }

    }
}