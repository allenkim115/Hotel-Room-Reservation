using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View(GetBaseViewModel());
        }

        public IActionResult BookNow()
        {
            return View(GetBaseViewModel());
        }

        public IActionResult Login()
        {
            return View(GetBaseViewModel());
        }

        public IActionResult Register()
        {
            return View(GetBaseViewModel());
        }

        [Authorize]
        public IActionResult Profile()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult Services()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult Reservations()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult AdminDash()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult Indexlogin()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult BookNowlogin()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult RoomTypes()
        {
            return AuthorizedView();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

