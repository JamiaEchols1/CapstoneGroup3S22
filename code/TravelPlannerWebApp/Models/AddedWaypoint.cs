using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class AddedWaypoint
    {
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public System.DateTime StartDateTime { get; set; }
        [Required]
        public System.DateTime EndDateTime { get; set; }
        public int TripId { get; set; }
    }
}