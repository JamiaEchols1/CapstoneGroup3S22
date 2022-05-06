using System;

namespace TravelPlannerLibrary.Models
{
    /// <summary>
    ///     The trip item model
    /// </summary>
    public abstract class TripItem
    {
        #region Data members

        /// <summary>
        /// The type
        /// </summary>
        public TripItemType ItemType { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public string ItemLocation { get; set; }

        /// <summary>
        ///     The start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date
        /// </summary>
        public DateTime EndDate { get; set; }

        #endregion
        /// <summary>
        /// Enum of possible trip item types
        /// </summary>
        public enum TripItemType
        {
            /// <summary>
            /// The transportation
            /// </summary>
            Transportation,
            /// <summary>
            /// The waypoint
            /// </summary>
            Waypoint
        }
    }
}