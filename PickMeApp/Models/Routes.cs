using System;
using System.Collections.Generic;

namespace PickMeApp.Models
{
    public partial class Routes
    {
        public Routes()
        {
            RoutePoints = new HashSet<RoutePoints>();
            RouteRequest = new HashSet<RouteRequest>();
        }

        public int RouteId { get; set; }
        public DateTime? DateOfRoute { get; set; }
        public string PreferencesAtRide { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? DriverId { get; set; }
        public int? CarId { get; set; }

        public Cars Car { get; set; }
        public Users Driver { get; set; }
        public ICollection<RoutePoints> RoutePoints { get; set; }
        public ICollection<RouteRequest> RouteRequest { get; set; }
    }
}
