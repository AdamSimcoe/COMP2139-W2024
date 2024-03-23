using COMP2139_Labs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace COMP2139_Labs.Controllers
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

        public IActionResult About()
        {
            return View();
        }
      
        [HttpGet]
        public IActionResult GeneralSearch(string searchType, string searchString)
        {
            if (searchType == "Projects")
            {
                return RedirectToAction("Search", "Projects", new { area = "ProjectManagement", searchString });
            }
            else if (searchType == "Tasks")
            {
                var url = Url.Action("Search", "Tasks", new { area = "ProjectManagement" }) + $"?searchString={searchString}";

                return Redirect(url);
            }

            return RedirectToAction("Index", "Home");
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
