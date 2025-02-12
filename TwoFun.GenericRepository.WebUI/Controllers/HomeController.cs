using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TwoFun.GenericRepository.WebUI.Models;

namespace TwoFun.GenericRepository.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //branch try
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
