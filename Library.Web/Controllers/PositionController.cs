using Autofac.Core;
using Library.Model.Models;
using Library.Service;
using Library.Web.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public IActionResult Index()                
        {
            var result = _positionService.GetAllPositionsAsync();
            //var positions= result.Adapt<List<Position>>();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Position model)
        {
            if (ModelState.IsValid)
            {
                await _positionService.AddPositionAsync(model);
                return RedirectToAction("Index", "Position");
            }
            return View(model);
        }

    }
}
