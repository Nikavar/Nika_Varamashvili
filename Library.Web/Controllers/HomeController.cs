using DocumentFormat.OpenXml.InkML;
using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using http = Windows.Web.Http;

namespace Library.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if(Request.Cookies["Token"] != null)
				return View();

			return RedirectToAction("Login", "Account");
		}

 
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}