using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.HelperMethods;
using Library.Web.Models;
using Library.Web.Models.Book;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Drawing.Text;
using Windows.ApplicationModel.VoiceCommands;

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
        private readonly IBookAuthorService _bookAuthorService;
        private readonly IBookPublisherService _bookPublisherService;
        private readonly IBookStorageService _bookStorageService;
		private readonly IBookCategoryService _bookCategoryService;
		private readonly ILogService _logService;

		public BookController
        (
            ILogService logService,
            IStorageService storageService, 
            ICategoryService categoryService, 
            ILanguageService languageService, 
            IPublisherService publisherService, 
            IAuthorService authorService, 
            IBookService bookService,
			IBookAuthorService bookAuthorService,
			IBookPublisherService bookPublisherService,
			IBookStorageService bookStorageService,
			IBookCategoryService bookCategoryService
		)
        {
            _logService = logService;
            _storageService = storageService;
            _categoryService = categoryService;
            _languageService = languageService;
            _publisherService = publisherService;
            _authorService = authorService;
            _bookService = bookService;
            _bookAuthorService = bookAuthorService;
            _bookPublisherService = bookPublisherService;
            _bookStorageService = bookStorageService;
            _bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();               
            var authors = await _authorService.GetAllAuthorsAsync();
            var bookAuthors = await _bookAuthorService.GetAllBookAuthorsAsync();

            var result = (from xx in books
                         join yy in bookAuthors
                         on xx.Id equals yy.BookId
                         join zz in authors
                         on yy.AuthorId equals zz.Id
                         //where yy.BookId == 7
                         select new
                         {   bookId = xx.Id,
                             authors = yy.author
                         }).ToList();


            CreateBookViewModel model = new CreateBookViewModel();

            foreach (var book in books.ToList())
            {
                var res = result.ToList().Where(x=>x.bookId == Convert.ToInt32(book.Id));
                //model.Authors.AddRange(res);

                SelectListItem items = new SelectListItem() { Text = res.ToString() };
				ViewBag.Authors = items;
			}

            return View();
        }

        [HttpPost]
        public IActionResult Index(Book book)
        {
            IEnumerable<Book> fillterBook = null;
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

        private async Task<IActionResult> FillDropDownLists(CreateBookViewModel model)
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var publishers = await _publisherService.GetAllPublishersAsync();
            var languages = await _languageService.GetAllLanguagesAsync();
            var genres = await _categoryService.GetAllCategoriesAsync();
            var shelves = await _storageService.GetAllStoragesAsync();

            var _authors = authors.ToList();
            var _publishers = publishers.ToList();
            var _languages = languages.ToList();
            var _genres = genres.ToList();
            var _shelves = shelves.ToList();

            // title
            ViewBag.Title = model.Title; 
            
            // description
            ViewBag.Description = model.Description;             

            // authors
            List<SelectListItem> autorsLst = new List<SelectListItem>();

            foreach (var author in _authors)
            {
                autorsLst.Add(new SelectListItem() { Value = author.Id.ToString(), Text = String.Concat(author.FirstName, " ", author.LastName) });
            }

            ViewBag.Authors = autorsLst;

            // publishers
            List<SelectListItem> publishersLst = new List<SelectListItem>();

            foreach (var publisher in _publishers)
            {
                publishersLst.Add(new SelectListItem() { Value = publisher.ID.ToString(), Text = publisher.PublisherName });
            }

            ViewBag.Publishers = publishersLst;

            // languages
            List<SelectListItem> languagesLst = new List<SelectListItem>();

            foreach (var language in _languages)
            {
                languagesLst.Add(new SelectListItem() { Value = language.Id.ToString(), Text = language.LanguageName });
            }

            ViewBag.Languages = languagesLst;

            // genres
            List<SelectListItem> genresLst = new List<SelectListItem>();

            foreach (var genre in _genres)
            {
                genresLst.Add(new SelectListItem() { Value = genre.Id.ToString(), Text = genre.CategoryName });
            }

            ViewBag.Genres = genresLst;

            // shelves
            List<SelectListItem> shelvesLst = new List<SelectListItem>();

            foreach (var shelf in _shelves)
            {
                shelvesLst.Add(new SelectListItem() { Value = shelf.Id.ToString(), Text = String.Concat("shelf:", shelf.ShelfNumber.ToString(), "-", "closet:", shelf.ClosetNumber.ToString()) });
            }

            ViewBag.Shelves = shelvesLst;

            return View();
        }

        public async Task<IActionResult> Add()
        {
            CreateBookViewModel model = new CreateBookViewModel();
            await FillDropDownLists(model);
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBookViewModel model)
        {
            await FillDropDownLists(model);
            
            if (ModelState.IsValid)
            {

                Book newBook = new Book() { Title = model.Title, Description = model.Description };
                await LogHelper.AddEntityWithLog(newBook,_bookService.AddBookAsync,_logService);      

                if(model.Authors != null)
                {
                    foreach (var authorId in model.Authors)
                    {                    
                        BookAuthor bookAuthor = new BookAuthor();
                        bookAuthor.BookId = newBook.Id;
                        bookAuthor.AuthorId = Convert.ToInt32(authorId);
                        await LogHelper.AddEntityWithLog(bookAuthor,_bookAuthorService.AddBookAuthorAsync,_logService);
                    }
                }

                if(model.Publishers != null)
                {
				    foreach (var publisherId in model.Publishers)
				    {
					    BookPublisher bookPublisher = new BookPublisher();
					    bookPublisher.BookId = newBook.Id;
					    bookPublisher.PublisherId = Convert.ToInt32(publisherId);
					    await LogHelper.AddEntityWithLog(bookPublisher, _bookPublisherService.AddBookPublisherAsync, _logService);
				    }
                }

                if(model.Shelves != null)
                {
					foreach (var shelfId in model.Shelves)
					{
						BookStorage bookStorage = new BookStorage();
						bookStorage.BookId = newBook.Id;
						bookStorage.StorageId = Convert.ToInt32(shelfId);
						await LogHelper.AddEntityWithLog(bookStorage, _bookStorageService.AddBookStorageAsync, _logService);
					}
				}

				if (model.Genres != null)
				{
					foreach (var genreId in model.Genres)
					{
						BookCategory bookGenre = new BookCategory();
						bookGenre.BookId = newBook.Id;
						bookGenre.CategoryId = Convert.ToInt32(genreId);
						await LogHelper.AddEntityWithLog(bookGenre, _bookCategoryService.AddBookCategoryAsync, _logService);
					}
				}

				return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet("{name}")]
        public IActionResult UpdateBook(string name)
        {
            return View();
        }

    }

}


