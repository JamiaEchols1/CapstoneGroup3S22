using System;
using System.ComponentModel.DataAnnotations;
using TravelPlannerLibrary.Models;
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
        #region Conversions

        /// <summary>
        ///     Converts the waypoint to added waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>
        ///     addedWaypoint the converted waypoint
        /// </returns>
        public static AddedWaypoint ConvertWaypointToAddedWaypoint(Waypoint waypoint)
        {
            AddedWaypoint addedWaypoint = new AddedWaypoint()
            {
                Id = waypoint.Id,
                Location = waypoint.Location,
                StartDateTime = waypoint.StartDateTime,
                EndDateTime = waypoint.EndDateTime,
                TripId = waypoint.TripId,
                Description = waypoint.Description

            };
        return addedWaypoint;
        }

        /// <summary>
        /// Converts the added waypoint to waypoint.
        /// </summary>
        /// <param name="addedWaypoint">The added waypoint.</param>
        /// <returns></returns>
        public static Waypoint ConvertAddedWaypointToWaypoint(AddedWaypoint addedWaypoint)
        {
            Waypoint waypoint = new Waypoint()
            {
                Id = addedWaypoint.Id,
                Location = addedWaypoint.Location,
                StartDateTime = addedWaypoint.StartDateTime,
                EndDateTime = addedWaypoint.EndDateTime,
                TripId = addedWaypoint.TripId,
                Description = addedWaypoint.Description

            };
            return waypoint;
        }

        #endregion
    }
}