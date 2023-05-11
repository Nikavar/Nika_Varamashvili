using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class ReaderStatus
    {
        public int ReaderStatusID { get; set; }
        public string ReaderStat { get; set; }
        public int OperationHistoryID { get; set; }
        public OperationHistory History { get; set; }
        public virtual List<StaffReader> StaffReaders { get; set; }
    }
}
