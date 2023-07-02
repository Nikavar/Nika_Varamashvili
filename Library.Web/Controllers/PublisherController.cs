using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class PublisherController : Controller
    {       
        private readonly IPublisherService publisherService;
        private readonly ILogService logService;

        public PublisherController(IPublisherService publisherService, ILogService logService)
        {
            this.publisherService = publisherService;
            this.logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await publisherService.GetAllPublishersAsync();
            return View(publishers);
        }

        // GET/publisher/index
        public IActionResult Add() => View();


        // POST/publisher/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(PublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Publisher>();
                await Helper.AddEntityWithLog(entity, publisherService.AddPublisherAsync, logService);

                TempData["Success"] = Helper.SuccessfullyAdded<Publisher>();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET /publisher/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var entity = await publisherService.GetPublisherByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST /publisher/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Publisher>();
                await Helper.UpdateEntityWithLog(entity,publisherService.UpdatePublisherAsync, logService);
                TempData["Success"] = Helper.SuccessfullyUpdated<Publisher>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //GET /publisher/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await publisherService.GetPublisherByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }
            var model = entity.Adapt<PublisherViewModel>();
            return View(model);
        }


        // POST /publisher/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PublisherViewModel item)
        {
            var entity = item.Adapt<Publisher>();
            await publisherService.DeletePublisherAsync(entity);
            
            var log = await logService.GetLogByEntityId(entity.ID);
            await logService.DeleteLogAsync(log);

            TempData["Success"] = Helper.SuccessfullyDeleted<Publisher>();

            return RedirectToAction("Index");
        }

        // GET /publisher/details/3
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var result = await publisherService.GetPublisherByIdAsync(id);
            ViewData["detailId"] = result.ID;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
