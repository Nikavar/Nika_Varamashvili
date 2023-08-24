using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageLink { get; set; }

        // relations
        public ICollection<BookAuthor>? BookAuthors { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
        public ICollection<BookStorage>? BookStorages { get; set; }
        public ICollection<BookPublisher>? BookPublishers { get; set; }
    }
}
