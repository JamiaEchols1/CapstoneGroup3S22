using System;

namespace TravelPlannerLibrary.Util
{
    /// <summary>
    ///     The time checker class
    /// </summary>
    public static class TimeChecker
    {
        #region Methods

        /// <summary>
        ///     Checks if the two time ranges overlap
        /// </summary>
        /// <param name="span1Start">The start of the first time range</param>
        /// <param name="span1End">The end of the first time range.</param>
        /// <param name="span2Start">The start of the second time range.</param>
        /// <param name="span2End">The end of the second time range.</param>
        /// <returns>
        ///     True if the time ranges overlap, false otherwise
        /// </returns>
        public static bool TimesOverlapping(DateTime span1Start, DateTime span1End, DateTime span2Start,
            DateTime span2End)
        {
            return DateTime.Compare(span1Start, span2End) < 0 && DateTime.Compare(span2Start, span1End) < 0;
        }

        #endregion
    }
}