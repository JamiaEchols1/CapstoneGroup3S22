using System;

namespace TravelPlannerDesktopApp.Models
{
    /// <summary>
    ///     Waypoint class
    /// </summary>
    public class Waypoint
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
        public string Location { get; set; }

        /// <summary>
        ///     Gets or sets the start date time.
        /// </summary>
        /// <value>
        ///     The start date time.
        /// </value>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the end date time.
        /// </summary>
        /// <value>
        ///     The end date time.
        /// </value>
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

        #region Methods

        /// <summary>
        ///     Converts to string.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Waypoint: " + this.Location + ", Start: " + this.StartDateTime.ToString("MM/dd/yyyy h:mm tt") +
                   ", End: " + this.EndDateTime.ToString("MM/dd/yyyy h:mm tt");
        }

        #endregion
    }
}