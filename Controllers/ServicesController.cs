using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Authorize]  // Add authorization requirement
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Book([FromForm] ServiceBookingModel model)
        {
            // Get the current user's ID
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == 0)
            {
                return Json(new { success = false, message = "User not authenticated." });
            }

            // Find the service
            var service = _context.Services.FirstOrDefault(s => s.Id == model.ServiceId);
            if (service == null)
                return Json(new { success = false, message = "Invalid service." });

            // Save as a reservation with a special status
            var reservation = new Reservation
            {
                RoomTypeId = service.Id, // Use ServiceId for RoomTypeId
                RoomType = service.Name, // Use Service Name for RoomType
                Status = "Pending",
                CheckInDate = model.BookingDate.Kind == System.DateTimeKind.Utc ? model.BookingDate : System.DateTime.SpecifyKind(model.BookingDate, System.DateTimeKind.Utc),
                CheckOutDate = model.BookingDate.Kind == System.DateTimeKind.Utc ? model.BookingDate : System.DateTime.SpecifyKind(model.BookingDate, System.DateTimeKind.Utc),
                NumberOfGuests = model.NumberOfGuests,
                TotalAmount = service.PricePerPerson * model.NumberOfGuests,
                SpecialRequests = model.SpecialRequests,
                UserId = userId  // Associate with current user
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
} 