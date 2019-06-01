using System;
using System.Collections.Generic;

namespace PickMeApp.Models
{
    public partial class RouteRequest
    {
        public int RequestId { get; set; }
        public int? RouteId { get; set; }
        public int? UserId { get; set; }
        public int? NumberOfSeats { get; set; }
        public string Notes { get; set; }
        public int? Status { get; set; }
        public int? StartPoint { get; set; }
        public int? EndPoint { get; set; }

        public RoutePoints EndPointNavigation { get; set; }
        public Routes Route { get; set; }
        public RoutePoints StartPointNavigation { get; set; }
        public Users User { get; set; }
    }
}
