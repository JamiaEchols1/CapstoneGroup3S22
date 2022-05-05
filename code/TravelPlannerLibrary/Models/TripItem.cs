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
        ///     The start date
        /// </summary>
        public DateTime StartDate;

        /// <summary>
        /// The end time
        /// </summary>
        public DateTime EndDate;

        #endregion
    }
}