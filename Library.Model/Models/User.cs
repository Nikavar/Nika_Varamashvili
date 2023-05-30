using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? LogID { get; set; }
        public int? StaffReaderID { get; set; }
        public StaffReader? StaffReader { get; set; }
        public LogInfo? Logs { get; set; }
        public ICollection<RoleUser>? RoleUsers { get; set; }    
    }
}
