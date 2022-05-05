using System;
using System.IO.Packaging;

namespace TravelPlannerDesktopApp.Models
{
    /// <summary>
    ///     Transport class
    /// </summary>
    public class Transportation
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
        ///     Gets or sets the trip identifier.
        /// </summary>
        /// <value>
        ///     The trip identifier.
        /// </value>
        public int TripId { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        /// <value>
        ///     The start time.
        /// </value>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     Gets or sets the end time.
        /// </summary>
        /// <value>
        ///     The end time.
        /// </value>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

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
            return "Transportation: " + "Start: " + this.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " +
                   this.EndTime.ToString("MM/dd/yyyy h:mm tt") + " Origin:" + Origin + " Destination: " + Destination;
        }

        #endregion
    }
}