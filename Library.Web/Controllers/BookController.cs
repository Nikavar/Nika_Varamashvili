using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Library.Web.Models.Book;
using Library.Web.Models.BookTest;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IStorageService _storageService;
        private readonly ICategoryService _categoryService;
        private readonly ILanguageService _languageService;
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BookController
        (
            IStorageService storageService, 
            ICategoryService categoryService, 
            ILanguageService languageService, 
            IPublisherService publisherService, 
            IAuthorService authorService, 
            IBookService bookService
        )
        {
            _storageService = storageService;
            _categoryService = categoryService;
            _languageService = languageService;
            _publisherService = publisherService;
            _authorService = authorService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpPost]
        public IActionResult Index(BookTest book)
        {
            //var allBook = _bookRepository.GetAllBook();

            IEnumerable<BookTest> fillterBook = null;
                //allBook.Where(b =>
                //   b.Name == book.Name ||
                //   b.Description == book.Description ||
                //   b.Shelf == book.Shelf ||
                //   b.Publicher == book.Publicher ||
                //   b.Author == book.Author ||
                //   b.Genre == book.Genre
                //);

            //if (fillterBook == null)
            //    return RedirectToAction("Index");


            return View(fillterBook.ToList());
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            //_bookRepository.DeleteBook(name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Add()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var publishers = await _publisherService.GetAllPublishersAsync();
            var languages = await _languageService.GetAllLanguagesAsync();
            var genres = await _categoryService.GetAllCategoriesAsync();
            var shelves = await _storageService.GetAllStoragesAsync();

            CreateBookViewModel model = new CreateBookViewModel();
            model.Authors = authors.ToList();
            model.Publishers = publishers.ToList();
            model.Languages = languages.ToList();
            model.Genres = genres.ToList();
            model.Shelves = shelves.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(BookTest book)
        {
            if (ModelState.IsValid)
            {
                //var _book = _bookRepository.SetBook(book);
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
                //_bookRepository.UpdateBook(id, book);

                return RedirectToAction("Index");
            }

            return View();

        }
    }

}


