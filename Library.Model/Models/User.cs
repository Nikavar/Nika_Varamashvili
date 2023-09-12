﻿using Microsoft.AspNet.Identity.EntityFramework;
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
        public int id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
		public string? ImageLink { get; set; }
		public int? StaffReaderID { get; set; }
        public bool? IsConfirmed { get; set; } = false;
        // relations
        public StaffReader? StaffReader { get; set; }
        public ICollection<RoleUser>? UserRoles{ get; set; }    
    }
}
