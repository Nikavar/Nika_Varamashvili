using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class LogInfo
    {
        public int LogID { get; set; }
        public string? TableName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int UserID { get; set; }

        public LogInfo()
        {
            DateCreated = DateTime.Now;
        }

        public ICollection<User> Users { get; set; }
        public ICollection<StaffReader> StaffReaders { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<ReaderStatus> ReaderStatuses { get; set; }   
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
