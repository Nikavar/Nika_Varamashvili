using Library.Web.Models.BookTest.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.BookTest
{
    public class BookTest
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public Author? Author { get; set; }

        [Required]
        public Publisher? Publicher { get; set; }

        [Required]
        public Language? Language { get; set; }

        [Required]
        public Genre? Genre { get; set; }

        [Required]
        public Shelf? Shelf { get; set; }

    }
}
