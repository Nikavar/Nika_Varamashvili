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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Windows.UI.Xaml.Controls;


namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _webhost;
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
		List<AllBooksViewModel> allBooks = new List<AllBooksViewModel>();


		public BookController
        (
            IWebHostEnvironment webhost,
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
            _webhost = webhost;
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

        private async Task<List<Author>> GetAuthors(int id)
        {
			var books = await _bookService.GetAllBooksAsync();
			var authors = await _authorService.GetAllAuthorsAsync();
			var bookAuthors = await _bookAuthorService.GetAllBookAuthorsAsync();

			List<Author> _authors = (from b in books
						 join ba in bookAuthors
						 on b.Id equals ba.BookId
						 join a in authors
						 on ba.AuthorId equals a.Id
						 where ba.BookId == id
						 select a).ToList();
            
            return _authors;
		}

		private async Task<List<Publisher>> GetPublishers(int id)
		{
			var books = await _bookService.GetAllBooksAsync();
			var bookPublishers = await _bookPublisherService.GetAllBookPublishersAsync();
			var publishers = await _publisherService.GetAllPublishersAsync();

			List<Publisher> _publishers = (from b in books
									 join bp in bookPublishers
									 on b.Id equals bp.BookId
									 join p in publishers
									 on bp.PublisherId equals p.ID
									 where bp.BookId == id
									 select p).ToList();

			return _publishers;
		}

		private async Task<List<Category>> GetGenres(int id)
		{
			var books = await _bookService.GetAllBooksAsync();
			var bookGenres = await _bookCategoryService.GetAllBookCategoriesAsync();
			var genres = await _categoryService.GetAllCategoriesAsync();

			List<Category> _genres = (from b in books
										   join bg in bookGenres
										   on b.Id equals bg.BookId
										   join g in genres
										   on bg.CategoryId equals g.Id
										   where bg.BookId == id
										   select g).ToList();

			return _genres;
		}

		private async Task<List<Storage>> GetShelves(int id)
		{
			var books = await _bookService.GetAllBooksAsync();
			var bookShelves = await _bookStorageService.GetAllBookStoragesAsync();
			var shelves = await _storageService.GetAllStoragesAsync();

			List<Storage> _shelves = (from b in books
									  join bs in bookShelves
									  on b.Id equals bs.BookId
									  join s in shelves
									  on bs.StorageId equals s.Id
									  where bs.BookId == id
									  select s).ToList();

			return _shelves;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();               

            //List<AllBooksViewModel> allBooks = new List<AllBooksViewModel>();

            foreach (var book in books)
            {
                AllBooksViewModel model = new AllBooksViewModel();

                model.Id = book.Id;
                model.Title = book.Title;
                model.Description = book.Description;
                model.Authors = await GetAuthors(book.Id);
				model.Publishers = await GetPublishers(book.Id);
                model.Genres = await GetGenres(book.Id);
                model.Shelves = await GetShelves(book.Id);				

				allBooks.Add(model);
            }

            return View(allBooks);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AllBooksViewModel books)
        {
            await Index();
            return View();
        }

		public async Task<ActionResult> Delete(int id)
		{
			var book = await _bookService.GetBookByIdAsync(id);

            return View(book);
		}

        [HttpPost]
        public async Task<ActionResult> Delete(AllBooksViewModel model)
        {
            var book = await _bookService.GetBookByIdAsync(model.Id);
			await _bookAuthorService.DeleteManyBookAuthorsAsync(x => x.BookId == book.Id);
			await _bookPublisherService.DeleteManyBookPublishersAsync(x => x.BookId == book.Id);
			await _bookCategoryService.DeleteManyBookCategoriesAsync(x => x.BookId == book.Id);
			await _bookStorageService.DeleteManyBookStoragesAsync(x => x.BookId == book.Id);

			var imgToDelete = Path.Combine(_webhost.WebRootPath, "CoverPhotos", book.ImageLink);
			var removeImg = new FileStream(imgToDelete, FileMode.Open);

			//using (removeImg)
			//{
			//	await imgFile.CopyToAsync(imgToDelete);
			//}

			await _bookService.DeleteBookAsync(book);
			await Index();

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
        public async Task<IActionResult> Add(CreateBookViewModel model, IFormFile imgFile)
        {
            await FillDropDownLists(model);

            if (ModelState.IsValid)
            {
                // save images to Folder "~/CoverPhotos"
                var saveimg = Path.Combine(_webhost.WebRootPath, "CoverPhotos", imgFile.FileName);
                string imgext = Path.GetExtension(imgFile.FileName);
                var uploadimg = new FileStream(saveimg, FileMode.Create);

                using (uploadimg)
                {
                    await imgFile.CopyToAsync(uploadimg);
                }

                Book newBook = new Book() { Title = model.Title, Description = model.Description, ImageLink = saveimg };
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

    }

}


