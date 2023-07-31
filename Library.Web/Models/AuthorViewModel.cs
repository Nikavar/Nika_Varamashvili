using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class AuthorViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DoB { get; set; }
    }
}
