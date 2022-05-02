using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelPlannerLibrary.Models;
using WebApplication4.Common;

namespace WebApplication4.Models
{
    /// <summary>
    ///     The added transportation model
    /// </summary>
    public class AddedTransportation
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
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [Required]
        public string Type { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

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
        ///     Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; set; }

        /// <summary>
        ///     Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public string Destination { get; set; }

        /// <summary>
        ///     Gets or sets the travel time.
        /// </summary>
        /// <value>
        /// The travel time.
        /// </value>
        [Display(Name = "Estimated Travel Time", AutoGenerateFilter = false)]
        public string TravelTime { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>
        /// The types.
        /// </value>
        public List<string> Types { get; set; }

        #endregion

        #region Conversions
        /// <summary>
        ///     Converts the transportation to added transportation.
        /// </summary>
        /// <param name="transportation">The transportation.</param>
        /// <returns>
        ///     the converted transportation item 
        /// </returns>
        public static AddedTransportation ConvertTransportationToAddedTransportation(Transportation transportation)
        {
            AddedTransportation addedTransportation = new AddedTransportation()
            {
                Id = transportation.Id,
                Origin = transportation.Origin,
                Destination = transportation.Destination,
                StartTime = transportation.StartTime,
                EndTime = transportation.EndTime,
                TripId = transportation.TripId,
                Description = transportation.Description,
                Type = transportation.Type

            };
            return addedTransportation;
        }

        /// <summary>
        ///     Converts the added transportation to transportation.
        /// </summary>
        /// <param name="addedTransportation">The added transportation.</param>
        /// <returns>
        ///     The converted transportation
        /// </returns>
        public static Transportation ConvertAddedTransportationToTransportation(AddedTransportation addedTransportation)
        {
            Transportation transportation = new Transportation()
            {
                Id = addedTransportation.Id,
                Origin = addedTransportation.Origin,
                Destination = addedTransportation.Destination,
                StartTime = addedTransportation.StartTime,
                EndTime = addedTransportation.EndTime,
                TripId = addedTransportation.TripId,
                Description = addedTransportation.Description,
                Type = addedTransportation.Type

            };
            return transportation;
        }
        #endregion
    }
}