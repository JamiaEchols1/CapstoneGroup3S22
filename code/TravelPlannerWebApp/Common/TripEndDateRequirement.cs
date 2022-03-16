using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelPlannerLibrary.Models;

namespace WebApplication4.Common
{
    public class TripEndDateRequirement : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime.CompareTo(LoggedUser.SelectedTrip.EndDate) < 0;
        }
    }
}