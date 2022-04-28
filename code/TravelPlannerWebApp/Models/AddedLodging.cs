using System;
using System.ComponentModel.DataAnnotations;
using TravelPlannerLibrary.Models;
using WebApplication4.Common;

namespace WebApplication4.Models
{
    /// <summary>
    ///     The added lodging model
    /// </summary>
    public class AddedLodging
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
        [Required]
        public string Location { get; set; }

        /// <summary>
        ///     Gets or sets the start date time.
        /// </summary>
        /// <value>
        ///     The start date time.
        /// </value>
        [Required]
        [TripStartDateRequirement(ErrorMessage = "The Start Date is not in range of the trip date")]
        [Display(Name = "Start Date", AutoGenerateFilter = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     Gets or sets the end time.
        /// </summary>
        /// <value>
        ///     The end time.
        /// </value>
        [Required]
        [TripEndDateRequirement(ErrorMessage = "The End Date is not in range of the trip date")]
        [Display(Name = "End Date", AutoGenerateFilter = false)]
        public DateTime EndTime { get; set; }

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

        #region Conversions

        /// <summary>
        ///     Converts the lodging to added lodging.
        /// </summary>
        /// <param name="lodging">The lodging.</param>
        /// <returns>
        ///     The converted lodging to added lodging object
        /// </returns>
        public static AddedLodging ConvertLodgingToAddedLodging(Lodging lodging)
        {
            AddedLodging addedLodging = new AddedLodging()
            {
                Id = lodging.Id,
                Location = lodging.Location,
                StartTime = lodging.StartTime,
                EndTime = lodging.EndTime,
                TripId = lodging.TripId,
                Description = lodging.Description

            };
            return addedLodging;
        }

        /// <summary>
        ///     Converts the added lodging to lodging.
        /// </summary>
        /// <param name="addedLodging">The added lodging.</param>
        /// <returns>
        ///     The converted addedlodging to lodging object
        /// </returns>
        public static Lodging ConvertAddedLodgingToLodging(AddedLodging addedLodging)
        {
            Lodging lodging = new Lodging()
            {
                Id = addedLodging.Id,
                Location = addedLodging.Location,
                StartTime = addedLodging.StartTime,
                EndTime = addedLodging.EndTime,
                TripId = addedLodging.TripId,
                Description = addedLodging.Description

            };
            return lodging;
        }

        #endregion
    }
}