using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerDesktopApp.Models
{
    public class Waypoint
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public System.DateTime EndDateTime { get; set; }
        public int TripId { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return "Waypoint: " + this.Location + ", Start: " + this.StartDateTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " + this.EndDateTime.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
