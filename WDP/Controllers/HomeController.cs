using WDP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WDP.Controllers
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
            ViewBag.IsHome = true;
            ViewBag.Description = "Hypermodern's asp.net core 5, bootstrap 5, Nlog, Entity Framework Core, Identity, sqlite, and SCSS quickstart template.";
            ViewBag.Keywords = "Hypermodern, quickstart, asp.net core 5, bootstrap 5, Nlog, Entity Framework Core, Identity, SCSS, sqlite";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
