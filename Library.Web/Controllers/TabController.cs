using Library.Model.Models;
using Library.Service;
using Library.Web.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Library.Web.Controllers
{
    public class TabController : Controller
    {
        private readonly ITabService _tabService;

        public TabController(ITabService tabService)
        {
            _tabService = tabService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTabMenu()
        {
            var catAll = await _tabService.GetAllTabsAsync();

            var model = catAll
                .Where(category => category.ParentId == 0)
                .Select(x => new TabViewModel
                {
                    Id = x.Id,
                    TabName = x.TabName,
                    ParentId = x.ParentId,
                    Child = x.Child.Adapt<List<TabViewModel>>()
                }).ToList();

            return PartialView("~/Views/Shared/Partial/GetTabMenu.cshtml", model);
        }
    }
}
