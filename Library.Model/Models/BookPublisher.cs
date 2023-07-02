﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookPublisher
    {
        [Key]
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public int BookId { get; set; }

        // relations
        public Publisher? Publisher { get; set; }
        public Book? Book { get; set; }
    }
}
