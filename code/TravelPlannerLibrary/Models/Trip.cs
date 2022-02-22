//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelPlannerLibrary.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trip()
        {
            this.Waypoints = new HashSet<Waypoint>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Waypoint> Waypoints { get; set; }

        public override string ToString()
        {
            return Name + " Start Date: " + StartDate.Date.ToString("yyyy-MM-dd") + " End Date: " + EndDate.Date.ToString("yyyy-MM-dd");
        }
    }
}
