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
        public IActionResult AdminDash()
        {
            return View();
        }
        public IActionResult Indexlogin()
        {
            return View();
        }
        public IActionResult BookNowlogin()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Index", "Home"); // Ensure correct redirection
        }

    }
}

