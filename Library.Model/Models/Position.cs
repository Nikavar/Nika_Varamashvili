using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Position
    {
        [Key]
        public int ID { get; set; }
        public string? PositionName { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? StartWorkingDate { get; set; }

        // relations
        public ICollection<StaffReader>? StaffReaders { get; set; }
    }
}
