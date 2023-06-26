using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DoB { get; set; }
        public int LogID { get; set; }

        // relations
        public LogInfo? Log { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }

    }
}
