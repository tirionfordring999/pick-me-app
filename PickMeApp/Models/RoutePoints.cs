using System;
using System.Collections.Generic;

namespace PickMeApp.Models
{
    public partial class RoutePoints
    {
        public RoutePoints()
        {
            RouteRequestEndPointNavigation = new HashSet<RouteRequest>();
            RouteRequestStartPointNavigation = new HashSet<RouteRequest>();
        }

        public int RoutePointId { get; set; }
        public int? RouteId { get; set; }
        public int? PointId { get; set; }
        public int? PointOrder { get; set; }
        public int? Price { get; set; }

        public Points Point { get; set; }
        public Routes Route { get; set; }
        public ICollection<RouteRequest> RouteRequestEndPointNavigation { get; set; }
        public ICollection<RouteRequest> RouteRequestStartPointNavigation { get; set; }
    }
}
