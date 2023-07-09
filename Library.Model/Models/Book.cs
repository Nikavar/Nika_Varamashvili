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
        public string? BookTitle { get; set; }
        public DateTime WrittenDate { get; set; }
        public int BookStatusId { get; set; }
        public int BookStorageId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        // relations
        public Category? Category { get; set; }
        public BookStatus? BookStatus { get; set; }
        public BookStorage? BookStorage { get; set; }
        public ICollection<Subscriber>? Subscribers { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
        public ICollection<BookStatus>? BookStatuses { get; set; }
        public ICollection<BookStorage>? BookStorages { get; set; }
        public ICollection<BookPublisher>? BookPublishers { get; set; }
    }
}
