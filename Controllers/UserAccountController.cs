using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hotel_Room_Reservation.Models;

namespace Hotel_Room_Reservation.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        public IActionResult Index()
        {
            // This would typically fetch users from a database
            var users = new List<UserAccount>
            {
                new UserAccount { Id = 1, Username = "admin", Role = "Admin", Email = "admin@hotel.com" },
                new UserAccount { Id = 2, Username = "staff1", Role = "Staff", Email = "staff1@hotel.com" }
            };
            return View(users);
        }

        // GET: UserAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAccount/Create
        [HttpPost]
        public IActionResult Create(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                // Add user to database
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UserAccount/Edit/5
        public IActionResult Edit(int id)
        {
            // Fetch user from database
            var user = new UserAccount { Id = id, Username = "example", Role = "Staff" };
            return View(user);
        }

        // POST: UserAccount/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, UserAccount user)
        {
            if (ModelState.IsValid)
            {
                // Update user in database
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UserAccount/Delete/5
        public IActionResult Delete(int id)
        {
            // Fetch user from database
            var user = new UserAccount { Id = id, Username = "example", Role = "Staff" };
            return View(user);
        }

        // POST: UserAccount/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete user from database
            return RedirectToAction(nameof(Index));
        }
    }
} 