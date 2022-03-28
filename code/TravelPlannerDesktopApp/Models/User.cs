using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerDesktopApp.Models
{
    public class User
    {
        public User()
        {
            this.Trips = new HashSet<Trip>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
