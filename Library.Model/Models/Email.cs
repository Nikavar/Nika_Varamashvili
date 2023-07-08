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
        public string TemplateName { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
