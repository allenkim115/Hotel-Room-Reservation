using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;


namespace NustarResort.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookNow()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }
        public IActionResult registration()
        {
            return View();
        }
        public IActionResult profile()
        {
            return View();
        }
    }
}

