using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Publisher
    {
        [Key]
        public int ID { get; set; }
        public string? PublisherName { get; set; } 
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? LogID { get; set; }

        // relations
        public LogInfo? Log { get; set; }
        public ICollection<BookPublisher>? BookPublishers { get; set; }
    }
}
