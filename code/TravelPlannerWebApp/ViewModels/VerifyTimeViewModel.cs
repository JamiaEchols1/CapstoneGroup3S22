using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;
using WebApplication4.Models;

namespace WebApplication4.ViewModels
{
    public class VerifyTimeViewModel
    {
        public TimeSpan estimatedTime { get; set; }
        public TimeSpan actualTime { get; set; }
        public AddedWaypoint waypoint { get; set; }
        public TripItem otherLocation { get; set; }

        public bool otherIsBefore { get; set; }

        public VerifyTimeViewModel(TimeSpan estimatedTime, TimeSpan actualTime, AddedWaypoint waypoint,
            TripItem otherLocation, bool otherIsBefore)
        {
            this.estimatedTime = estimatedTime;
            this.actualTime = actualTime;
            this.waypoint = waypoint;
            this.otherLocation = otherLocation;
           
            this.otherIsBefore = otherIsBefore;
        }

        public VerifyTimeViewModel()
        {

        }

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