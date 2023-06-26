using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Subscriber
    {
        [Key]
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public int LogID { get; set; }

        // relations
        public StaffReader? Reader { get; set; }
        public Book? Book { get; set; }
        public SubscriberStatus? Status { get; set; }
        public LogInfo? Log { get; set; }
    }
}
