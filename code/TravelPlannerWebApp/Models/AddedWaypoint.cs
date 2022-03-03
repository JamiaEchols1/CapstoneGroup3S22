using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;

namespace WebApplication4.Models
{
    public class AddedWaypoint
    {
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Start Date", AutoGenerateFilter = false)]
        public System.DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "End Date", AutoGenerateFilter = false)]
        public System.DateTime EndDateTime { get; set; }
        public int TripId { get; set; }

        public static List<Waypoint> ConflictingWaypoints = new List<Waypoint>();
    }
}