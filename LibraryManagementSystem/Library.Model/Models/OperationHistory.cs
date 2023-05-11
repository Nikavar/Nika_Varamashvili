using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class OperationHistory
    {
        public int HistoryID { get; set; }
        public string? TableName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public OperationHistory()
        {
            DateCreated = DateTime.Now;
        }

        public virtual List<User> Users { get; set; }
        public virtual List<StaffReader> StaffReaders { get; set; }
        public virtual List<Position> Positions { get; set; }
        public virtual List<ReaderStatus> ReaderStatuses { get; set; }
    }
}
