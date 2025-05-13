using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        public AdminController(ApplicationDbContext context) : base(context)
        {
        }

        public IActionResult AdminDash()
        {
            return AuthorizedView();
        }

        public IActionResult UserAccountManagement()
        {
            var model = GetBaseViewModel();
            model.Users = _context.Users.ToList();
            return AuthorizedView(model);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                user.Password = HashPassword(user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok();
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { errors });
        }

        [HttpPost]
        public IActionResult EditUser([FromBody] WebApplication1.Models.UserEditViewModel user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.Find(user.PK_USER_ID);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update user properties
                existingUser.FirstName = user.FirstName;
                existingUser.MiddleName = user.MiddleName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Role = user.Role;

                // Only update password if a new one is provided
                if (!string.IsNullOrEmpty(user.Password))
                {
                    existingUser.Password = HashPassword(user.Password);
                }

                _context.SaveChanges();
                return Ok();
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { errors });
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(UserAccountManagement));
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public IActionResult RoomInventory()
        {
            var model = GetBaseViewModel();
            model.Rooms = _context.Rooms.ToList();
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
                    Amenities = new List<AmenityModel>()
                }).ToList();
            return AuthorizedView(model);
        }

        [HttpGet]
        public IActionResult GetRoomType(int id)
        {
            try
            {
                var roomType = _context.RoomTypes.Find(id);
                if (roomType == null)
                {
                    return Json(new { success = false, message = "Room type not found" });
                }

                return Json(new { 
                    success = true, 
                    roomType = new {
                        id = roomType.Id,
                        name = roomType.Name,
                        description = roomType.Description,
                        pricePerNight = roomType.PricePerNight,
                        bedType = roomType.BedType,
                        maxOccupancy = roomType.MaxOccupancy,
                        size = roomType.Size
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateRoomType(RoomType roomType)
        {
            try
            {
                var existingRoomType = _context.RoomTypes.Find(roomType.Id);
                if (existingRoomType == null)
                {
                    return Json(new { success = false, message = "Room type not found" });
                }

                // Update properties
                existingRoomType.Name = roomType.Name;
                existingRoomType.Description = roomType.Description;
                existingRoomType.PricePerNight = roomType.PricePerNight;
                existingRoomType.BedType = roomType.BedType;
                existingRoomType.MaxOccupancy = roomType.MaxOccupancy;
                existingRoomType.Size = roomType.Size;

                _context.RoomTypes.Update(existingRoomType);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteRoomType(int id)
        {
            try
            {
                var roomType = _context.RoomTypes.Find(id);
                if (roomType == null)
                {
                    return Json(new { success = false, message = "Room type not found" });
                }

                // Delete all rooms of this type first
                var rooms = _context.Rooms.Where(r => r.RoomType == roomType.Name);
                _context.Rooms.RemoveRange(rooms);

                // Then delete the room type
                _context.RoomTypes.Remove(roomType);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddRoomType(RoomType roomType)
        {
            try
            {
                // Check if room type with same name already exists
                if (_context.RoomTypes.Any(rt => rt.Name == roomType.Name))
                {
                    return Json(new { success = false, message = "A room type with this name already exists" });
                }

                _context.RoomTypes.Add(roomType);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult EditRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public IActionResult EditRoom(Room room)
        {
            try
            {
                _context.Rooms.Update(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(RoomInventory));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(room);
            }
        }

        public IActionResult CheckInOut()
        {
            return View();
        }

        public IActionResult PaymentProcessing()
        {
            return View();
        }

        public IActionResult GuestInformation()
        {
            return View();
        }

        public IActionResult Housekeeping()
        {
            return View();
        }

        public IActionResult RoomMaintenance()
        {
            return View();
        }

        public IActionResult BillingInvoice()
        {
            return View();
        }

        public IActionResult ReportingAnalytics()
        {
            return View();
        }

        public IActionResult NotificationSystem()
        {
            return View();
        }

        public IActionResult AccessControl()
        {
            return View();
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
            return AuthorizedView("AdminServices", model);
        }

        [HttpGet]
        public IActionResult GetService(int id)
        {
            try
            {
                var service = _context.Services.Find(id);
                if (service == null)
                {
                    return Json(new { success = false, message = "Service not found" });
                }

                return Json(new { 
                    success = true, 
                    service = new {
                        id = service.Id,
                        name = service.Name,
                        category = service.Category,
                        description = service.Description,
                        operatingHours = service.OperatingHours,
                        location = service.Location,
                        pricePerPerson = service.PricePerPerson,
                        maxGuests = service.MaxGuests,
                        imageUrl = service.ImageUrl
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateService(Service service)
        {
            try
            {
                var existingService = _context.Services.Find(service.Id);
                if (existingService == null)
                {
                    return Json(new { success = false, message = "Service not found" });
                }

                // Update properties
                existingService.Name = service.Name;
                existingService.Category = service.Category;
                existingService.Description = service.Description;
                existingService.OperatingHours = service.OperatingHours;
                existingService.Location = service.Location;
                existingService.PricePerPerson = service.PricePerPerson;
                existingService.MaxGuests = service.MaxGuests;
                existingService.ImageUrl = service.ImageUrl;

                _context.Services.Update(existingService);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteService(int id)
        {
            try
            {
                var service = _context.Services.Find(id);
                if (service == null)
                {
                    return Json(new { success = false, message = "Service not found" });
                }

                _context.Services.Remove(service);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddService(Service service)
        {
            try
            {
                // Check if service with same name already exists
                if (_context.Services.Any(s => s.Name == service.Name))
                {
                    return Json(new { success = false, message = "A service with this name already exists" });
                }

                _context.Services.Add(service);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Json(new
            {
                pk_USER_ID = user.PK_USER_ID,
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                phoneNumber = user.PhoneNumber,
                role = user.Role
            });
        }

        public async Task<IActionResult> Reservations()
        {
            var model = GetBaseViewModel();
            model.ActiveReservations = await _context.Reservations
                .Include(r => r.User)
                .OrderByDescending(r => r.CheckInDate)
                .ToListAsync();

            return View("AdminReservations", model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found" });
            }

            reservation.Status = "Approved";
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RejectReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found" });
            }

            reservation.Status = "Rejected";
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> CompleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found" });
            }

            reservation.Status = "Completed";
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public async Task<IActionResult> ReservationDetails(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var model = GetBaseViewModel();
            model.SelectedReservation = reservation;
            return View(model);
        }
    }
} 