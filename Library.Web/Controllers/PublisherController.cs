using Library.Model.Models;
using Library.Service;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class PublisherController : Controller
    {       
        private readonly IPublisherService _publisherService;
        private readonly ILogService _logService;

        public PublisherController(IPublisherService publisherService, ILogService logService)
        {
            _publisherService = publisherService;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            return View(publishers);
        }

        // GET/publisher/index
        public IActionResult Add() => View();


        // POST/publisher/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(PublisherViewModel item)
        {
            if (ModelState.IsValid)
            {
                var loggedPublisher = await _logService.AddLogAsync(new LogInfo { TableName = "Publishers", DateCreated = DateTime.Now });
                item.LogID = loggedPublisher.LogID;

                await _publisherService.AddPublisherAsync(item.Adapt<Publisher>());

                TempData["Success"] = "The Publisher has been added!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET /publisher/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _publisherService.GetPublisherByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /publisher/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PublisherViewModel item)
        {
            if (ModelState.IsValid)
            {
                var logToUpdate = await _logService.GetLogByIdAsync(item.LogID);
                logToUpdate.DateUpdated = DateTime.Now;

                await _logService.UpdateLogAsync(logToUpdate);

                await _publisherService.UpdatePublisherAsync(item.Adapt<Publisher>());
                TempData["Success"] = "The Publisher has Updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET /publisher/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _publisherService.GetPublisherByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item.Adapt<PublisherViewModel>());
        }


        // POST /publisher/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PublisherViewModel item)
        {
            var logToDelete = await _logService.GetLogByIdAsync(item.ID);
            logToDelete.DateDeleted = DateTime.Now;
            await _logService.UpdateLogAsync(logToDelete);

            await _publisherService.DeletePublisherAsync(item.Adapt<Publisher>());
            TempData["Success"] = "The Publisher has been deleted!";

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

            var result = await _publisherService.GetPublisherByIdAsync(id);
            ViewData["detailId"] = result.ID;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
