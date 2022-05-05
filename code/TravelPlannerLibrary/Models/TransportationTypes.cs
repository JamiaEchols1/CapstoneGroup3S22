using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerLibrary.Models 
{ 
    public enum Types { DRIVING = 0, TRANSIT =1, WALKING=2, CYCLING=3}
    public class TransportationTypes
    {
        public static ICollection<Types> GetTypes() { 
            return new[] { Types.DRIVING, Types.TRANSIT, Types.WALKING, Types.CYCLING };
        }
    }
}
