using Library.Model.Models;
using Library.Service;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly ILogService _logService;

        public AuthorController(IAuthorService authorService, ILogService logService)
        {
            _authorService = authorService;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAuthorsAsync();
            return View(result);
        }

        // GET/author/index
        public IActionResult Add() => View();


        // POST/author/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AuthorViewModel item)
        {
            if (ModelState.IsValid)
            {
                var loggedAuthor = await _logService.AddLogAsync(new LogInfo { TableName = "Authors", DateCreated = DateTime.Now });
                item.LogID = loggedAuthor.LogID;

                await _authorService.AddAuthorAsync(item.Adapt<Category>());

                TempData["Success"] = "The Author has been added!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET /author/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _authorService.GetAuthorByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /author/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AuthorViewModel item)
        {
            if (ModelState.IsValid)
            {
                var logToUpdate = await _logService.GetLogByIdAsync(item.LogID);
                logToUpdate.DateUpdated = DateTime.Now;

                await _logService.UpdateLogAsync(logToUpdate);

                await _authorService.UpdateAuthorAsync(item.Adapt<Category>());
                TempData["Success"] = "The Author has Updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET /author/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _authorService.GetAuthorByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item.Adapt<AuthorViewModel>());
        }


        // POST /author/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(AuthorViewModel item)
        {
            var log = await _authorService.GetAuthorByIdAsync(item.Id);

            var logToDelete = await _logService.GetLogByIdAsync(log.LogID);
            logToDelete.DateDeleted = DateTime.Now;
            await _logService.UpdateLogAsync(logToDelete);

            await _authorService.DeleteAuthorAsync(log);
            TempData["Success"] = "The Author has been deleted!";

            return RedirectToAction("Index");
        }

        // GET /author/details/3
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var result = await _authorService.GetAuthorByIdAsync(id);
            ViewData["detailId"] = result.Id;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
