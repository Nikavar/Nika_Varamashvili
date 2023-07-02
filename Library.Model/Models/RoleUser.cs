using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class RoleUser
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        // relations
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
