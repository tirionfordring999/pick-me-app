using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickMeApp.Models
{
    public partial class Feedbacks
    {
        public int FeedbackId { get; set; }
        public int? UserFrom { get; set; }
        public int? UserTo { get; set; }
        public int? Rating { get; set; }
        public string Feedback { get; set; }

        public Users UserFromNavigation { get; set; }
        public Users UserToNavigation { get; set; }
    }
}
