using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;

namespace WebApplication4.Models
{
    /// <summary>
    /// The added waypoint model
    /// </summary>
    public class AddedWaypoint
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [Required]
        public string Location { get; set; }
        /// <summary>
        /// Gets or sets the start date time.
        /// </summary>
        /// <value>
        /// The start date time.
        /// </value>
        [Required]
        [Display(Name = "Start Date", AutoGenerateFilter = false)]
        public System.DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "End Date", AutoGenerateFilter = false)]
        public System.DateTime EndDateTime { get; set; }
        /// <summary>
        /// Gets or sets the trip identifier.
        /// </summary>
        /// <value>
        /// The trip identifier.
        /// </value>
        public int TripId { get; set; }

        /// <summary>
        /// The conflicting waypoints
        /// </summary>
        public static List<Waypoint> ConflictingWaypoints = new List<Waypoint>();

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public  string Description { get; set; }
    }
}