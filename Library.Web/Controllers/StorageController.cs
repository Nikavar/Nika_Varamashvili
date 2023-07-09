using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageService storageService;
        private readonly ILogService logService;

        public StorageController(IStorageService storageService, ILogService logService)
        {
            this.storageService = storageService;
            this.logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var storages = await storageService.GetAllStoragesAsync();
            var model = storages.Adapt<IEnumerable<StorageViewModel>>();
            return View(model);
        }

        // GET/publisher/index
        public IActionResult Add() => View();

        // POST/storage/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(StorageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Storage>();
                await Helper.AddEntityWithLog(entity, storageService.AddStorageAsync, logService);

                TempData["Success"] = Helper.SuccessfullyAdded<Storage>();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET /storage/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var entity = await storageService.GetStorageByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST /storage/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StorageViewModel model)
        {

            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Storage>();
                await Helper.UpdateEntityWithLog(entity, storageService.UpdateStorageAsync, logService);
                TempData["Success"] = Helper.SuccessfullyUpdated<Storage>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //GET /storage/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await storageService.GetStorageByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }
            var model = entity.Adapt<StorageViewModel>();
            return View(model);
        }

        // POST /storage/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(StorageViewModel item)
        {
            var entity = item.Adapt<Storage>();
            await storageService.DeleteStorageAsync(entity);

            var log = await logService.GetLogByEntityId(entity.Id);
            await logService.DeleteLogAsync(log);

            TempData["Success"] = Helper.SuccessfullyDeleted<Storage>();

            return RedirectToAction("Index");
        }

        // GET /storage/details/3
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var result = await storageService.GetStorageByIdAsync(id);
            ViewData["detailId"] = result.Id;

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }
    }
}
