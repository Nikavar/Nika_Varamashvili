using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int StaffReaderID { get; set; }
        public StaffReader StaffReader { get; set; }
        public int OperationHistoryID { get; set; }
        public OperationHistory History { get; set; }
    }
}
