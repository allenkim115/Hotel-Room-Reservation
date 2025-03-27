using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace NustarResort.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
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
        public IActionResult register()
        {
            return View();
        }
        [Authorize] // Add this attribute (requires using Microsoft.AspNetCore.Authorization)
        public IActionResult Profile()
        {
            // Get the currently logged-in user
            var username = User.Identity?.Name;
            
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }
            
            // You'll need to inject ApplicationDbContext to access user data
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            
            return View(user);
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Index", "Home");
        }

    }
}

