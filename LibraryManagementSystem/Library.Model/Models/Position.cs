using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        public string? PositionName { get; set; }
        public double Salary { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public int OperationHistoryID { get; set; }
        public OperationHistory History { get; set; }
        public virtual List<StaffReader> StaffReaders { get; set; }
    }
}
