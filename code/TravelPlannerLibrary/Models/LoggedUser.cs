namespace TravelPlannerLibrary.Models
{
    /// <summary>
    ///     The logged user class
    /// </summary>
    public class LoggedUser
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        public static User User { get; set; }

        /// <summary>
        ///     Gets or sets the selected trip.
        /// </summary>
        /// <value>
        ///     The selected trip.
        /// </value>
        public static Trip SelectedTrip { get; set; }

        /// <summary>
        ///     Gets or sets the selected waypoint.
        /// </summary>
        /// <value>
        ///     The selected waypoint.
        /// </value>
        public static Waypoint SelectedWaypoint { get; set; }

        /// <summary>
        ///     Gets or sets the selected lodging.
        /// </summary>
        /// <value>
        ///     The selected lodging.
        /// </value>
        public static Lodging SelectedLodging { get; set; }

        /// <summary>
        ///     Gets or sets the selected transportation.
        /// </summary>
        /// <value>
        ///     The selected transportation.
        /// </value>
        public static Transportation SelectedTransportation { get; set; }

        #endregion
    }
}