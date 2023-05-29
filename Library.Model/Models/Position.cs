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
        public int PositionID { get; set; }
        public string? PositionName { get; set; }
        public double Salary { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public int LogID { get; set; }
        public LogInfo? Logs { get; set; }
        public ICollection<StaffReader>? StaffReaders { get; set; }
    }
}
