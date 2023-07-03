using Autofac.Core;
using DocumentFormat.OpenXml.Office.CustomUI;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Constants;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
	public class PositionController : Controller
	{
		private readonly IPositionService positionService;
		private readonly ILogService logService;

		public PositionController(IPositionService positionService, ILogService logService)
		{
			this.positionService = positionService;
			this.logService = logService;
		}

		public async Task<IActionResult> Index()
		{
			var result = await positionService.GetAllPositionsAsync();
			return View(result);
		}

		// GET/position/index
		public IActionResult Add() => View();


		// POST/position/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(PositionViewModel model)
		{
			if (ModelState.IsValid)
			{
				var entity = model.Adapt<Position>();
				await Helper.AddEntityWithLog(entity,positionService.AddPositionAsync,logService);

   				TempData["Success"] = Helper.SuccessfullyAdded<Position>();

				return RedirectToAction("Index");
			}
			return View(model);
		}

		// GET /position/edit/3
		public async Task<ActionResult> Edit(int id)
		{
			var entity = await positionService.GetPositionByIdAsync(id);

			if (entity == null)
			{
				return NotFound();
			}

			return View(entity);
		}

		// POST /position/edit/4
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(PositionViewModel model)
		{
			if (ModelState.IsValid)
			{ 
				var entity = model.Adapt<Position>();
				await Helper.UpdateEntityWithLog(entity,positionService.UpdatePositionAsync,logService);

				TempData["Success"] = Helper.SuccessfullyUpdated<Position>();

				return RedirectToAction("Index");
			}

			return View(model);
		}

		//GET /position/edit/3
		public async Task<ActionResult> Delete(int id)
		{
			var entity = await positionService.GetPositionByIdAsync(id);

			if (entity == null)
			{
				return NotFound();
			}

			var model = entity.Adapt<PositionViewModel>();
			return View(model);
		}


		// POST /position/delete/4
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(PositionViewModel model)
		{
			var entity = await positionService.GetPositionByIdAsync(model.ID);
			await positionService.DeletePositionAsync(entity);
			await logService.DeleteManyLogsAsync(x => x.EntityID == entity.ID && x.TableName == entity.GetType().Name);

			TempData["Success"] = Helper.SuccessfullyDeleted<Position>();

			return RedirectToAction("Index");			
		}

		// GET /position/details/3
		public async Task<ActionResult> Details(int? id)
		{
			if (!id.HasValue)
			{
				return RedirectToAction(actionName: nameof(Index),
					controllerName: "Home");
			}

			var entity = await positionService.GetPositionByIdAsync(id);
			ViewData["detailId"] = entity.ID;

			if (entity == null)
			{
				return NotFound();
			}
			return View(entity);
		}

	}
}
