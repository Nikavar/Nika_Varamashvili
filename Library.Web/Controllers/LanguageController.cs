using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Library.Web.Models.BookTest.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Language = Library.Model.Models.Language;

namespace Library.Web.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService languageService;
        private readonly ILogService logService;

        public LanguageController(ILanguageService languageService, ILogService logService)
        {
            this.languageService = languageService;
            this.logService = logService;            
        }
        public async Task<ActionResult> Index()
        {
            var result = await languageService.GetAllLanguagesAsync();
            return View(result);
        }

        // GET/language/index
        public ActionResult Add() => View();


        // POST/language/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(LanguageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Language>();
                await HelperMethods.AddEntityWithLog(entity, languageService.AddLanguageAsync, logService);
                TempData["Success"] = Warnings.SuccessfullyAddedGeneric<Language>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET /language/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await languageService.GetLanguageByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /language/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LanguageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Language>();
                await HelperMethods.UpdateEntityWithLog(entity, languageService.UpdateLanguageAsync, logService);
                TempData["Success"] = Warnings.SuccessfullyUpdatedGeneric<Language>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //GET /language/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await languageService.GetLanguageByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = entity.Adapt<LanguageViewModel>();
            return View(model);
        }


        // POST /language/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(LanguageViewModel model)
        {
            var entity = await languageService.GetLanguageByIdAsync(model.Id);
            await languageService.DeleteLanguageAsync(entity);
            await logService.DeleteManyLogsAsync(x => x.EntityID == entity.Id);

            TempData["Success"] = Warnings.SuccessfullyDeletedGeneric<Language>();

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

            var result = await languageService.GetLanguageByIdAsync(id);
            ViewData["detailId"] = result.Id;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
