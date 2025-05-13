using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;  // Add this line
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return View(model);
            }

            var newUser = new User
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                Password = HashPassword(model.Password),
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Role = "Guest",
                EmailNotifications = true,
                SmsNotifications = true,
                PreferredLanguage = "English",
                ProfileImageUrl = "/images/default-profile.png"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return View(model);
            }

            try
            {
                var hashedPassword = HashPassword(model.Password);
                _logger.LogInformation($"Attempting login for username: {model.Username}");
                _logger.LogInformation($"Hashed password: {hashedPassword}");
                
                // Check if user exists
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (user == null)
                {
                    _logger.LogWarning($"User not found: {model.Username}");
                    ViewData["ErrorMessage"] = "Invalid Username or Password";
                    return View(model);
                }

                _logger.LogInformation($"Found user with role: {user.Role}");
                _logger.LogInformation($"Stored password hash: {user.Password}");
                _logger.LogInformation($"Input password hash: {hashedPassword}");
                
                if (user.Password != hashedPassword)
                {
                    _logger.LogWarning($"Password mismatch for user: {model.Username}");
                    ViewData["ErrorMessage"] = "Invalid Username or Password";
                    return View(model);
                }

                // Create claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.PK_USER_ID.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                _logger.LogInformation($"Successfully logged in user: {user.Username} with role: {user.Role}");

                // Redirect based on role
                if (user.Role == "Guest")
                    return RedirectToAction("Indexlogin", "Home");
                else if (user.Role == "Admin")
                    return RedirectToAction("AdminDash", "Admin");
                else if (user.Role == "Staff")
                    return RedirectToAction("Dashboard", "Staff");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login process");
                ViewData["ErrorMessage"] = "An error occurred during login. Please try again.";
                return View(model);
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
