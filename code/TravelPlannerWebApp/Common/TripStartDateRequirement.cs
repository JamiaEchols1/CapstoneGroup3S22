using System;
using System.ComponentModel.DataAnnotations;
using TravelPlannerLibrary.Models;

namespace WebApplication4.Common
{
    /// <summary>
    ///     Validator for user's selected trip start date
    ///     Used for validating points of interest start date when compared to
    ///     the selected trip
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class TripStartDateRequirement : ValidationAttribute
    {
        #region Methods

        /// <summary>
        ///     Returns true if start date when compared to the selected trip is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///     <see langword="true" /> if the specified value is valid; otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsValid(object value)
        {
            var dateTime = Convert.ToDateTime(value);
            return dateTime.CompareTo(LoggedUser.SelectedTrip.StartDate) > 0;
        }

        #endregion
    }
}