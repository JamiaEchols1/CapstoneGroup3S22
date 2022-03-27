using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;
using WebApplication4.Common;

namespace WebApplication4.Models
{
    /// <summary>
    /// The added lodging model
    /// </summary>
    public class AddedLodging
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
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
        [TripStartDateRequirement(ErrorMessage = "The Start Date is not in range of the trip date")]
        [Display(Name = "Start Date", AutoGenerateFilter = false)]
        public System.DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        [Required]
        [TripEndDateRequirement(ErrorMessage = "The End Date is not in range of the trip date")]
        [Display(Name = "End Date", AutoGenerateFilter = false)]
        public System.DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the trip identifier.
        /// </summary>
        /// <value>
        /// The trip identifier.
        /// </value>
        public int TripId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}