using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlannerLibrary.Models 
{ 
    public enum Types { Driving = 0, Transit =1, Walking=2, Cycling=3}
    public class TransportationTypes
    {
        public static ICollection<Types> GetTypes() { 
            return new[] { Types.Driving, Types.Transit, Types.Walking, Types.Cycling };
        }
    }
}
