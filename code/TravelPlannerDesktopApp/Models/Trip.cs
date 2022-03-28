using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerDesktopApp.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Lodging> Lodgings { get; set; }
        public virtual ICollection<Transportation> Transportations { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Waypoint> Waypoints { get; set; }

        public override string ToString()
        {
            return this.Name + ", Start Date:" + this.StartDate.ToString("MM/dd/yyyy h:mm tt") + ", End Date: " + this.EndDate.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
