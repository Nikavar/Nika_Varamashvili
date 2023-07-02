using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class SubscriberStatus
    {
        [Key]
        public int Id { get; set; }
        public string? StatusName { get; set; }

        // relations
        public ICollection<Subscriber>? Subscribers { get; set; }
    }
}
