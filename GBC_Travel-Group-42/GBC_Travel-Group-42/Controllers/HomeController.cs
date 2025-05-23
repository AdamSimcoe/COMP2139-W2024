using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GBC_Travel_Group_42.Controllers
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
			return View();
		}

		public IActionResult Profile() 
		{
			return View();
		}

		public IActionResult NotFound(int statusCode)
		{
			if (statusCode == 404)
			{
				return View("NotFound");
			}

			return View("Error");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		[Route("Home/Error")]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		// TEST 500 ERROR CALL Home/TriggerError
		public IActionResult TriggerError()
		{
			throw new InvalidOperationException();
		}

	}
}
