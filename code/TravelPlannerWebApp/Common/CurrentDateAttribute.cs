using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Common
{
    /// <summary>
    ///     Validates that a selected trips start/end date are past the current date
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class CurrentDateAttribute : ValidationAttribute
    {
        #region Methods

        /// <summary>
        ///     Returns true if the trips start/end date are past the current date.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///     <see langword="true" /> if the specified value is valid; otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsValid(object value)
        {
            var dateTime = Convert.ToDateTime(value);
            return dateTime.CompareTo(DateTime.Now) > 0;
        }

        #endregion
    }
}