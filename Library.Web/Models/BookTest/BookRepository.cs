using System.Reflection.Metadata.Ecma335;
using Library.Model.Models;
using Library.Web.Constants;

namespace Library.Web.Models.BookTest
{
    public class BookRepository : IBookRepository
    {

        private List<BookTest> Books;
        public BookRepository()
        {
                Books = new List<BookTest>()
                {
                    new BookTest() { Name="Jon", Description="Best Book", Author = Enums.Author.Author_1, Publicher=Enums.Publisher.Publicher_1, Language=Enums.Language.English, Genre=Enums.Genre.Genre_1, Shelf=Enums.Shelf.Shelf_1 },

                    new BookTest() { Name="Anna", Description="Good Book", Author = Enums.Author.Author_3, Publicher=Enums.Publisher.Publicher_2, Language=Enums.Language.Latin, Genre=Enums.Genre.Genre_2, Shelf=Enums.Shelf.Shelf_2 }
                };
        }
        public IEnumerable<BookTest> GetAllBook()
        {

            return Books;
        }

        public BookTest Geet(BookTest book)
        {
            throw new NotImplementedException();
        }

        public BookTest DeleteBook(string Name)
        {
            var _book = Books.FirstOrDefault(x => x.Name == Name);

            if (_book == null)
                return null;

            Books.Remove(_book);

            return _book;

        }

        public BookTest SetBook(BookTest book)
        {
            Books.Add(book);

            return book;
        }

        public BookTest UpdateBook(string name, BookTest book)
        {
            var _book = Books.FirstOrDefault(d => d.Name == name);

            if (_book != null)
            {
                _book.Name = book.Name;
                _book.Description = book.Description;
                _book.Author = book.Author;
                _book.Shelf = book.Shelf;
                _book.Publicher = book.Publicher;
                _book.Language = book.Language;
                _book.Genre = book.Genre;
            }

            return _book;
        }
       
    }
}
