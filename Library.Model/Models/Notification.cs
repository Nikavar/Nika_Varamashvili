using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int SubscriberId { get; set; }
        public int StaffId { get; set; }
        public string? NotificationText { get; set; }
        public DateTime SendTime { get; set; }

        // relations
        public Subscriber? Subscriber { get; set; }
        public StaffReader? StaffReader { get; set; }

    }
}
