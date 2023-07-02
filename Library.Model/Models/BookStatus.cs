using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StatusId { get; set; }
        public string? UniqueNumber { get; set; }
        public DateTime PublishDate { get; set; }

        // relations
        //public Book? Book { get; set; }
        //public StatusOfBook? StatusOfBook { get; set; }

    }
}
