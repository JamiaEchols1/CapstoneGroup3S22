using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerLibrary.Models
{
    public class LoggedUser
    {
        public static User user { get; set; }
        public static Trip selectedTrip { get; set; }

        public static Waypoint selectedWaypoint { get; set; }

        public static Lodging selectedLodging { get; set; }

        public static Transportation SelectedTransportation { get; set; }
    }
}
