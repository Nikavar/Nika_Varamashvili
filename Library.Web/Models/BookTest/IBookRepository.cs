using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Web.Models.BookTest
{
    public interface IBookRepository
    {
        public IEnumerable<BookTest> GetAllBook();
        public BookTest DeleteBook(string Name);
        public BookTest SetBook(BookTest book);
        public BookTest UpdateBook(string name, BookTest book);

    }
}
