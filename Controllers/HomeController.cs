using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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

        public IActionResult Services()
        {
            var model = GetBaseViewModel();
            model.Services = _context.Services
                .Select(s => new ServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    OperatingHours = s.OperatingHours,
                    Location = s.Location,
                    PricePerPerson = s.PricePerPerson,
                    MaxGuests = s.MaxGuests
                }).ToList();
            return View(model);
        }

        [Authorize]
        public IActionResult Reservations()
        {
            var model = GetBaseViewModel();
            
            // Get current user's ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                // If user ID claim is missing, try to get user from username
                var username = User.Identity?.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    var user = _context.Users.FirstOrDefault(u => u.Username == username);
                    if (user != null)
                    {
                        // Add the missing claim
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.PK_USER_ID.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        User.AddIdentity(identity);
                        userIdClaim = claims.First();
                    }
                }
            }

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                var reservations = _context.Reservations.Where(r => r.UserId == userId).ToList();
                model.ActiveReservations = reservations
                    .Where(r => r.Status != "Completed")
                    .Select(r => new Reservation
                    {
                        Id = r.Id,
                        RoomType = r.RoomType,
                        Status = r.Status,
                        CheckInDate = r.CheckInDate,
                        CheckOutDate = r.CheckOutDate,
                        NumberOfGuests = r.NumberOfGuests,
                        TotalAmount = r.TotalAmount,
                        SpecialRequests = r.SpecialRequests
                    }).ToList();
                model.PastReservations = reservations
                    .Where(r => r.Status == "Completed")
                    .Select(r => new Reservation
                    {
                        Id = r.Id,
                        RoomType = r.RoomType,
                        Status = r.Status,
                        CheckInDate = r.CheckInDate,
                        CheckOutDate = r.CheckOutDate,
                        NumberOfGuests = r.NumberOfGuests,
                        TotalAmount = r.TotalAmount,
                        SpecialRequests = r.SpecialRequests
                    }).ToList();
            }
            else
            {
                // If we couldn't get the user ID, return empty lists
                model.ActiveReservations = new List<Reservation>();
                model.PastReservations = new List<Reservation>();
            }
            
            return View(model);
        }
        [Authorize]
        public IActionResult Indexlogin()
        {
            var model = GetBaseViewModel();
            
            // Populate services (only map properties that exist)
            model.Services = _context.Services
                .Select(s => new ServiceModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    OperatingHours = s.OperatingHours,
                    Location = s.Location,
                    PricePerPerson = s.PricePerPerson,
                    MaxGuests = s.MaxGuests
                }).ToList();

            // Get current user's ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                // If user ID claim is missing, try to get user from username
                var username = User.Identity?.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    var user = _context.Users.FirstOrDefault(u => u.Username == username);
                    if (user != null)
                    {
                        // Add the missing claim
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.PK_USER_ID.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        User.AddIdentity(identity);
                        userIdClaim = claims.First();
                    }
                }
            }

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                // Populate reservations for current user only
                var reservations = _context.Reservations.Where(r => r.UserId == userId).ToList();
                model.ActiveReservations = reservations
                    .Where(r => r.Status != "Completed")
                    .Select(r => new Reservation
                    {
                        Id = r.Id,
                        RoomType = r.RoomType,
                        Status = r.Status,
                        CheckInDate = r.CheckInDate,
                        CheckOutDate = r.CheckOutDate,
                        NumberOfGuests = r.NumberOfGuests,
                        TotalAmount = r.TotalAmount,
                        SpecialRequests = r.SpecialRequests
                    }).ToList();
            }
            else
            {
                // If we couldn't get the user ID, return an empty list
                model.ActiveReservations = new List<Reservation>();
            }

            return View(model);
        }

        [Authorize]
        public IActionResult BookNowlogin()
        {
            return AuthorizedView();
        }

        [Authorize]
        public IActionResult RoomTypes()
        {
            var model = GetBaseViewModel();
            model.RoomTypes = _context.RoomTypes
                .Select(rt => new RoomTypeModel
                {
                    Id = rt.Id,
                    Name = rt.Name,
                    ImageUrl = rt.ImageUrl,
                    PricePerNight = rt.PricePerNight,
                    BedType = rt.BedType,
                    MaxOccupancy = rt.MaxOccupancy,
                    Size = rt.Size,
                    Description = rt.Description,
                    Amenities = new List<AmenityModel>() // Map if you add amenities to your DB
                }).ToList();
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

