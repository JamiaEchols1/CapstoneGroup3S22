using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.ViewModels
{
    /// <summary>
    ///     The verify time view model for the AddedWaypoints
    /// </summary>
    public class VerifyTimeViewModel
    {
        /// <summary>
        ///     Gets or sets the estimated time.
        /// </summary>
        /// <value>
        /// The estimated time.
        /// </value>
        public TimeSpan estimatedTime { get; set; }

        /// <summary>
        ///     Gets or sets the actual time.
        /// </summary>
        /// <value>
        /// The actual time.
        /// </value>
        public TimeSpan actualTime { get; set; }

        /// <summary>
        ///     Gets or sets the waypoint.
        /// </summary>
        /// <value>
        /// The waypoint.
        /// </value>
        public AddedWaypoint waypoint { get; set; }

        /// <summary>
        ///     Gets or sets the other location.
        /// </summary>
        /// <value>
        /// The other location.
        /// </value>
        public TripItem otherLocation { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [other is before].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [other is before]; otherwise, <c>false</c>.
        /// </value>
        public bool otherIsBefore { get; set; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="VerifyTimeViewModel"/> class.
        /// </summary>
        /// <param name="estimatedTime">The estimated time.</param>
        /// <param name="actualTime">The actual time.</param>
        /// <param name="waypoint">The waypoint.</param>
        /// <param name="otherLocation">The other location.</param>
        /// <param name="otherIsBefore">if set to <c>true</c> [other is before].</param>
        public VerifyTimeViewModel(TimeSpan estimatedTime, TimeSpan actualTime, AddedWaypoint waypoint,
            TripItem otherLocation, bool otherIsBefore)
        {
            this.estimatedTime = estimatedTime;
            this.actualTime = actualTime;
            this.waypoint = waypoint;
            this.otherLocation = otherLocation;
           
            this.otherIsBefore = otherIsBefore;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VerifyTimeViewModel"/> class.
        /// </summary>
        public VerifyTimeViewModel()
        {

        }

        /// <summary>
        ///     Converts to readablestring.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <returns></returns>
        public string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

    }
}