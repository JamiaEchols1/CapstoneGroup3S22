using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerDesktopApp.Models
{
    public class Lodging
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public string Location { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Location + ", Start Date: " + this.StartTime.ToString("MM/dd/yyyy h:mm tt") + ", End Time: " + this.EndTime.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
