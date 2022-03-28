using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerDesktopApp.Models
{
    public class Transportation
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return "Transportation: " + "Start: " + this.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End: " + this.EndTime.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
