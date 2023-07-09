using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Account
{
    public class EmailTemplateViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TemplateType { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
    }
}
