using System;
using System.Collections.Generic;

namespace PickMeApp.Models
{
    public partial class Users
    {
        public Users()
        {
            Cars = new HashSet<Cars>();
            Feedbacks = new HashSet<Feedbacks>();
            RouteRequest = new HashSet<RouteRequest>();
            Routes = new HashSet<Routes>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Pass { get; set; }
        public string Login { get; set; }

        public ICollection<Cars> Cars { get; set; }
        public ICollection<Feedbacks> Feedbacks { get; set; }
        public ICollection<RouteRequest> RouteRequest { get; set; }
        public ICollection<Routes> Routes { get; set; }
    }
}
