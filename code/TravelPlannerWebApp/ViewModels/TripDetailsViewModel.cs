using System.Collections.Generic;
using TravelPlannerLibrary.Models;

namespace WebApplication4.ViewModels
{
    /// <summary>
    ///     The trip details view model
    /// </summary>
    public class TripDetailsViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the trip.
        /// </summary>
        /// <value>
        ///     The trip.
        /// </value>
        public Trip Trip { get; set; }

        /// <summary>
        ///     Gets or sets the waypoints.
        /// </summary>
        /// <value>
        ///     The waypoints.
        /// </value>
        public IEnumerable<Waypoint> Waypoints { get; set; }

        /// <summary>
        ///     Gets or sets the lodgings.
        /// </summary>
        /// <value>
        ///     The lodgings.
        /// </value>
        public IEnumerable<Lodging> Lodgings { get; set; }

        /// <summary>
        ///     Gets or sets the transportations.
        /// </summary>
        /// <value>
        ///     The transportations.
        /// </value>
        public IEnumerable<Transportation> Transportations { get; set; }

        /// <summary>
        ///     Gets or sets the waypoints and transportation.
        /// </summary>
        /// <value>
        ///     The waypoints and transportation.
        /// </value>
        public IEnumerable<TripItem> WaypointsAndTransportation { get; set; }

        #endregion
    }
}