using Autofac.Core;
using DocumentFormat.OpenXml.Office.CustomUI;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
	public class PositionController : Controller
	{
		private readonly IPositionService _positionService;
		private readonly ILogService _logService;

		public PositionController(IPositionService positionService, ILogService logService)
		{
			_positionService = positionService;
			_logService = logService;
		}

		public async Task<IActionResult> Index()
		{
			var result = await _positionService.GetAllPositionsAsync();
			return View(result);
		}

		// GET/position/index
		public IActionResult Add() => View();


		// POST/position/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(PositionViewModel item)
		{
			if (ModelState.IsValid)
			{
				var loggedPosition = await _logService.AddLogAsync(new LogInfo { TableName = "Position", DateCreated = DateTime.Now });
				item.LogID = loggedPosition.LogID;

				await _positionService.AddPositionAsync(item.Adapt<Position>());

				TempData["Success"] = "The Position has been added!";

				return RedirectToAction("Index");
			}

			return View(item);
		}

		// GET /position/edit/3
		public async Task<ActionResult> Edit(int id)
		{
			var item = await _positionService.GetPositionByIdAsync(id);

			if (item == null)
			{
				return NotFound();
			}

			return View(item);
		}

		// POST /position/edit/4
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(PositionViewModel item)
		{
			if (ModelState.IsValid)
			{
				var logToUpdate = await _logService.GetLogByIdAsync(item.LogID);
				logToUpdate.DateUpdated = DateTime.Now;

				await _logService.UpdateLogAsync(logToUpdate);

				await _positionService.UpdatePositionAsync(item.Adapt<Position>());
				TempData["Success"] = "The Position has Updated!";

				return RedirectToAction("Index");
			}

			return View(item);
		}

		//GET /position/edit/3
		public async Task<ActionResult> Delete(int id)
		{
			var item = await _positionService.GetPositionByIdAsync(id);

			if (item == null)
			{
				return NotFound();
			}

			return View(item.Adapt<PositionViewModel>());
		}


		// POST /position/delete/4
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(PositionViewModel item)
		{
			var log = await _positionService.GetPositionByIdAsync(item.ID);

			var logToDelete = await _logService.GetLogByIdAsync(log.LogID);
			logToDelete.DateDeleted = DateTime.Now;
			await _logService.UpdateLogAsync(logToDelete);

			await _positionService.DeletePositionAsync(log);
			TempData["Success"] = "The Position has been deleted!";

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

			var result = await _positionService.GetPositionByIdAsync(id);
			ViewData["detailId"] = result.ID;

			if (result == null)
			{
				return NotFound();
			}
			return View(result);
		}

	}
}
