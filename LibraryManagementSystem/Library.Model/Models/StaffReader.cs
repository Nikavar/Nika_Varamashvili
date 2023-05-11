using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class StaffReader
    {
        public int StaffReaderID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool Gender { get; set; }
        public string? PersonalPhoto { get; set; }
        public int PositionId { get; set; }
        public Position Positions { get; set; }
        public int ReaderStatusID { get; set; }
        public ReaderStatus ReaderStatus { get; set; }
        public int OperationHistoryID { get; set; }
        public OperationHistory History { get; set; }
        public virtual List<User> Users { get; set; }

    }
}
