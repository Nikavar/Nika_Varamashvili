﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Category

    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public int LogID { get; set; }

        // relations
        public LogInfo Log { get; set; }
    }
}
