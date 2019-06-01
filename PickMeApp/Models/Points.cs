using System;
using System.Collections.Generic;

namespace PickMeApp.Models
{
    public partial class Points
    {
        public Points()
        {
            RoutePoints = new HashSet<RoutePoints>();
        }

        public int PointId { get; set; }
        public string PointName { get; set; }

        public ICollection<RoutePoints> RoutePoints { get; set; }
    }
}
