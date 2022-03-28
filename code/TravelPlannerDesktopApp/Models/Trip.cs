using System;
using System.Collections.Generic;

namespace TravelPlannerDesktopApp.Models
{
    /// <summary>
    ///     Trip class
    /// </summary>
    public class Trip
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
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the start date.
        /// </summary>
        /// <value>
        ///     The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Gets or sets the end date.
        /// </summary>
        /// <value>
        ///     The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the lodgings.
        /// </summary>
        /// <value>
        ///     The lodgings.
        /// </value>
        public virtual ICollection<Lodging> Lodgings { get; set; }

        /// <summary>
        ///     Gets or sets the transportations.
        /// </summary>
        /// <value>
        ///     The transportations.
        /// </value>
        public virtual ICollection<Transportation> Transportations { get; set; }

        /// <summary>
        ///     Gets or sets the waypoints.
        /// </summary>
        /// <value>
        ///     The waypoints.
        /// </value>
        public virtual ICollection<Waypoint> Waypoints { get; set; }

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
            return this.Name + ", Start Date:" + this.StartDate.ToString("MM/dd/yyyy h:mm tt") + ", End Date: " +
                   this.EndDate.ToString("MM/dd/yyyy h:mm tt");
        }

        #endregion
    }
}