using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;

namespace WebApplication4.Common
{
    /// <summary>
    ///     Validator for user's selected trip end date
    ///     Used for validating points of interest end date when compared to
    ///     the selected trip
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class TripEndDateRequirement : ValidationAttribute
    {
        /// <summary>
        /// Returns true if point of interests end date is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified value is valid; otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime.CompareTo(LoggedUser.SelectedTrip.EndDate) < 0;
        }
    }
}