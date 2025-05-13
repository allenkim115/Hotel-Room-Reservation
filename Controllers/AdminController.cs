using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
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
            return View();
        }

        public IActionResult RoomInventory()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }

        [HttpPost]
        public IActionResult AddRoom(Room room)
        {
            try
            {
                room.Status = RoomStatus.Available;
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteRoom(int id)
        {
            try
            {
                var room = _context.Rooms.Find(id);
                if (room == null)
                {
                    return Json(new { success = false, message = "Room not found" });
                }

                _context.Rooms.Remove(room);
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
    }
} 