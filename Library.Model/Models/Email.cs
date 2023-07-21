using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }       
        
        [Required]
        public string? TemplateType { get; set; }

        [Required]
        public string? From { get; set; }
        [Required]
        public string? To { get; set; }
        public string? Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Body { get; set; }

    }
}
