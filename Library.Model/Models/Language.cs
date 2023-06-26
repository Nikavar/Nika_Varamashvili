using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string? LanguageName { get; set; }
        public string? LanguageCode { get; set; }
        public int LogID { get; set; }

        // relations
        public LogInfo? Log { get; set; }

    }
}
