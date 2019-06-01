using System;
using System.Collections.Generic;

namespace PickMeApp.Models
{
    public partial class Cars
    {
        public Cars()
        {
            Routes = new HashSet<Routes>();
        }

        public int CarId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string SeatsAvaliable { get; set; }
        public bool? BabySeat { get; set; }
        public string LicensePlate { get; set; }
        public int? UserId { get; set; }

        public Users User { get; set; }
        public ICollection<Routes> Routes { get; set; }
    }
}
