﻿using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly ILogService logService;

        public AuthorController(IAuthorService authorService, ILogService logService)
        {
            this.authorService = authorService;
            this.logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await this.authorService.GetAllAuthorsAsync();
            return View(result);
        }

        // GET/author/index
        public IActionResult Add() => View();


        // POST/author/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {

                var authorEntity = model.Adapt<Author>();
                await Helper.AddEntityWithLog(authorEntity, authorService.AddAuthorAsync, logService);
                TempData["Success"] = Helper.SuccessfullyAdded<Author>();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET /author/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await authorService.GetAuthorByIdAsync(id);

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
                var authorEntity = item.Adapt<Author>();
                await Helper.UpdateEntityWithLog(authorEntity, authorService.UpdateAuthorAsync, logService);

                TempData["Success"] = Helper.SuccessfullyUpdated<Author>();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET /author/edit/3
        public async Task<ActionResult> Delete(int id)
        {
            var item = await authorService.GetAuthorByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item.Adapt<AuthorViewModel>());
        }


        // POST /author/delete/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(AuthorViewModel model)
        {
            var entity = model.Adapt<Author>();
            await authorService.DeleteAuthorAsync(entity);
            await logService.DeleteManyLogsAsync(x => x.EntityID == entity.Id);
            TempData["Success"] = Helper.SuccessfullyDeleted<Author>();

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

            var result = await authorService.GetAuthorByIdAsync(id);
            ViewData["detailId"] = result.Id;

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
