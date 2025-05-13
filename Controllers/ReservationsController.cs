using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Authorize]  // Add authorization requirement
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromForm] Reservation model)
        {
            // Get the current user's ID
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == 0)
            {
                return Json(new { success = false, message = "User not authenticated." });
            }

            // Find the room type for price and name
            var roomType = _context.RoomTypes.FirstOrDefault(r => r.Id == model.RoomTypeId);
            if (roomType == null)
            {
                return Json(new { success = false, message = "Invalid room type." });
            }

            // Calculate total price
            var nights = (model.CheckOutDate - model.CheckInDate).Days;
            if (nights < 1) nights = 1;
            var total = nights * roomType.PricePerNight;

            // Save reservation
            var reservation = new Reservation
            {
                RoomTypeId = roomType.Id,
                RoomType = roomType.Name,
                Status = "Pending",
                CheckInDate = model.CheckInDate.Kind == DateTimeKind.Utc ? model.CheckInDate : DateTime.SpecifyKind(model.CheckInDate, DateTimeKind.Utc),
                CheckOutDate = model.CheckOutDate.Kind == DateTimeKind.Utc ? model.CheckOutDate : DateTime.SpecifyKind(model.CheckOutDate, DateTimeKind.Utc),
                NumberOfGuests = model.NumberOfGuests,
                TotalAmount = total,
                SpecialRequests = model.SpecialRequests,
                UserId = userId  // Associate with current user
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            // Get the current user's ID
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == 0)
            {
                return Json(new { success = false, message = "User not authenticated." });
            }

            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found or unauthorized." });
            }
            reservation.Status = "Cancelled";
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Modify(int id, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests, string specialRequests)
        {
            // Get the current user's ID
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == 0)
            {
                return Json(new { success = false, message = "User not authenticated." });
            }

            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found or unauthorized." });
            }

            reservation.CheckInDate = checkInDate.Kind == DateTimeKind.Utc ? checkInDate : DateTime.SpecifyKind(checkInDate, DateTimeKind.Utc);
            reservation.CheckOutDate = checkOutDate.Kind == DateTimeKind.Utc ? checkOutDate : DateTime.SpecifyKind(checkOutDate, DateTimeKind.Utc);
            reservation.NumberOfGuests = numberOfGuests;
            reservation.SpecialRequests = specialRequests;

            // Optionally, recalculate TotalAmount for rooms/services
            // (You can add logic here if needed)

            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
} 