using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class SystemUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
    }
}
