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
    }
}

