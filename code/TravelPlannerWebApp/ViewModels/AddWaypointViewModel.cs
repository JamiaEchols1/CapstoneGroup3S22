using System;
using TravelPlannerLibrary.Models;

namespace WebApplication4.ViewModels
{
    /// <summary>
    ///     The add waypooint view model
    /// </summary>
    public class AddWaypointViewModel
    {
        #region Data members

        /// <summary>
        ///     The selected trip start date
        /// </summary>
        public DateTime SelectedTripStartDate = LoggedUser.SelectedTrip.StartDate;

        /// <summary>
        ///     The selected trip end date
        /// </summary>
        public DateTime SelectedTripEndDate = LoggedUser.SelectedTrip.EndDate;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the waypoint.
        /// </summary>
        /// <value>
        ///     The waypoint.
        /// </value>
        public Waypoint Waypoint { get; set; }

        #endregion
    }
}