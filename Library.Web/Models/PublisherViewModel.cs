using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class PublisherViewModel
    {
        [Key]
        public int ID { get; set; }
         
        [Required(ErrorMessage = "Please enter publisher name!")]
        public string? PublisherName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int LogID { get; set; }
    }
}
