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

        [Authorize]
        public IActionResult Profile()
        {
            var username = User.Identity?.Name;
            
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }
            
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var profileViewModel = ProfileViewModel.FromUser(user);
            
            // Add mock data for the view
            profileViewModel.TotalStays = 12;
            profileViewModel.UpcomingStays = 5;
            profileViewModel.PastStays = 7;
            
            return View(profileViewModel);
        }

        [Authorize]
        public IActionResult Services()
        {
            return View();
        }

        [Authorize]
        public IActionResult Reservations()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminDash()
        {
            return View();
        }

        [Authorize]
        public IActionResult Indexlogin()
        {
            return View();
        }

        [Authorize]
        public IActionResult BookNowlogin()
        {
            return View();
        }

        [Authorize]
        public IActionResult RoomTypes()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

