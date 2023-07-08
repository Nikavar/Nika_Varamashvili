using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
    public class EmailTemplateViewModel
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
