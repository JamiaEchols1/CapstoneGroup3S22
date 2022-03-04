using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    /// <summary>
    ///     The added waypoint class
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
        public DateTime StartDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the end date time.
        /// </summary>
        /// <value>
        ///     The end date time.
        /// </value>
        [Required]
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
        [Required]
        public string Description { get; set; }

        #endregion
    }
}