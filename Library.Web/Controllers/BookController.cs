using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Library.Web.Models.BookTest;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<BookTest> books = _bookRepository.GetAllBook();
            return View(books);
        }

        [HttpPost]
        public IActionResult Index(BookTest book)
        {
            var allBook = _bookRepository.GetAllBook();

            IEnumerable<BookTest> fillterBook =
                allBook.Where(b =>
                   b.Name == book.Name ||
                   b.Description == book.Description ||
                   b.Shelf == book.Shelf ||
                   b.Publicher == book.Publicher ||
                   b.Author == book.Author ||
                   b.Genre == book.Genre
                );

            if (fillterBook == null)
                return RedirectToAction("Index");


            return View(fillterBook.ToList());
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            _bookRepository.DeleteBook(name);
            return RedirectToAction("Index");
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(BookTest book)
        {
            if (ModelState.IsValid)
            {
                var _book = _bookRepository.SetBook(book);
                TempData["Add"] = "Add";

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet("{name}")]
        public IActionResult UpdateBook(string name)
        {
            return View();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateBook(string id, BookTest book)
        {

            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(id, book);

                return RedirectToAction("Index");
            }

            return View();

        }
    }

}


