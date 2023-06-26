using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly ILogService _logService;

        public LanguageController(ILanguageService languageService, ILogService logService)
        {
            _languageService = languageService;
            _logService = logService;            
        }
        public async Task<IActionResult> Index()
        {
            var result = await _languageService.GetAllLanguagesAsync();
            return View(result);
        }

        // GET/language/index
        public IActionResult Add() => View();


        // POST/language/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(LanguageViewModel item)
        {
            if (ModelState.IsValid)
            {
                var loggedLanguage = await _logService.AddLogAsync(new LogInfo { TableName = "Language", DateCreated = DateTime.Now });
                item.LogID = loggedLanguage.LogID;

                await _languageService.AddLanguageAsync(item.Adapt<Language>());

                TempData["Success"] = "The Language has been added!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET /language/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _languageService.GetLanguageByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /language/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LanguageViewModel item)
        {
            if (ModelState.IsValid)
            {
                var logToUpdate = await _logService.GetLogByIdAsync(item.LogID);
                logToUpdate.DateUpdated = DateTime.Now;

                await _logService.UpdateLogAsync(logToUpdate);

                await _languageService.UpdateLanguageAsync(item.Adapt<Language>());
                TempData["Success"] = "The Language has Updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET /language/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _languageService.GetLanguageByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item.Adapt<LanguageViewModel>());
        }


        // POST /language/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(LanguageViewModel item)
        {
            var log = await _languageService.GetLanguageByIdAsync(item.Id);

            var logToDelete = await _logService.GetLogByIdAsync(log.LogID);
            logToDelete.DateDeleted = DateTime.Now;
            await _logService.UpdateLogAsync(logToDelete);

            await _languageService.DeleteLanguageAsync(log);
            TempData["Success"] = "The Language has been deleted!";

            return RedirectToAction("Index");
        }

        // GET /language/details/3
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var result = await _languageService.GetLanguageByIdAsync(id);
            ViewData["detailId"] = result.Id;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
