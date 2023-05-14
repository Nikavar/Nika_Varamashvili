using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int LogID { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public LogInfo Logs { get; set; }
    }
}
