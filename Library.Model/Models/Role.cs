﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string? RoleName { get; set; }
        public int LogID { get; set; }
        public ICollection<RoleUser>? RoleUsers { get; set; }
        public LogInfo? Log { get; set; }
    }
}
