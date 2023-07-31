using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class LanguageViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? LanguageName { get; set; }
        public string? LanguageCode { get; set; }
    }
}
