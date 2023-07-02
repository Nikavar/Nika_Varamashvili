using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookStorage
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StorageId { get; set; }
        public int BookQuantityOnShelf { get; set; }

        // relations
        //public Book? Book { get; set; }
        //public Storage? Storage { get; set; }

    }
}
