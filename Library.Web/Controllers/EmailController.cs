using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Library.Web.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using Windows.Graphics.Printing.PrintSupport;

namespace Library.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly ILogService logService;       

        public EmailController(IEmailService emailService, ILogService logService)
        {
            this.emailService = emailService;
            this.logService = logService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await emailService.GetAllEmailsAsync();
            return View(result);
        }

        public IActionResult Add() => View();

        // POST/language/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(EmailTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Email>();
                await HelperMethods.AddEntityWithLog(entity, emailService.AddEmailAsync, logService);
                TempData["Success"] = Warnings.SuccessfullyAddedGeneric<Email>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET /email/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await emailService.GetEmailByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /email/edit/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmailTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Adapt<Email>();
                await HelperMethods.UpdateEntityWithLog(entity, emailService.UpdateEmailAsync, logService);
                TempData["Success"] = Warnings.SuccessfullyAddedGeneric<Email>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //GET /email/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await emailService.GetEmailByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = entity.Adapt<EmailTemplateViewModel>();
            return View(model);
        }


        // POST /email/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(LanguageViewModel model)
        {
            var entity = await emailService.GetEmailByIdAsync(model.Id);
            await emailService.DeleteEmailAsync(entity);
            await logService.DeleteManyLogsAsync(x => x.EntityID == entity.Id);

            TempData["Success"] = Warnings.SuccessfullyDeletedGeneric<Email>();

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

            var result = await emailService.GetEmailByIdAsync(id);
            ViewData["detailId"] = result.Id;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
