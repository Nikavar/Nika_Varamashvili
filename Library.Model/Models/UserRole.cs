using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int LogID { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
        public LogInfo? Logs { get; set; }
    }
}
