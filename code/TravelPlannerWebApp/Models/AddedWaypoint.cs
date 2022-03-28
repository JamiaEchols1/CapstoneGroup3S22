using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Common;

namespace WebApplication4.Models
{
    /// <summary>
    ///     The added waypoint model
    /// </summary>
    public class AddedWaypoint
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the location.
        /// </summary>
        /// <value>
        ///     The location.
        /// </value>
        [Required]
        public string Location { get; set; }

        /// <summary>
        ///     Gets or sets the start date time.
        /// </summary>
        /// <value>
        ///     The start date time.
        /// </value>
        [Required]
        [TripStartDateRequirement(ErrorMessage = "The Start Date is not in range of the trip date")]
        [Display(Name = "Start Date", AutoGenerateFilter = false)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the end date time.
        /// </summary>
        /// <value>
        ///     The end date time.
        /// </value>
        [Required]
        [TripEndDateRequirement(ErrorMessage = "The End Date is not in range of the trip date")]
        [Display(Name = "End Date", AutoGenerateFilter = false)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the trip identifier.
        /// </summary>
        /// <value>
        ///     The trip identifier.
        /// </value>
        public int TripId { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        #endregion
    }
}