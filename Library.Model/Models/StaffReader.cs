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
        public DateTime? DOB { get; set; }
        public string? PersonalNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool Gender { get; set; }
        public string? PersonalPhoto { get; set; }
        public int PositionId { get; set; }
        public int ReaderStatusID { get; set; }
        public int LogID { get; set; }
        public Position Position { get; set; }
        public ReaderStatus ReaderStatus { get; set; }
        public LogInfo Logs { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Card> Cards { get; set; }

    }
}
