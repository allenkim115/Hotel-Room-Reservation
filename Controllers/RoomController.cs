using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public IActionResult Index()
        {
            // This would typically fetch rooms from a database
            var rooms = new List<Room>
            {
                new Room { 
                    Id = 1, 
                    RoomNumber = "101", 
                    RoomType = "Standard", 
                    Floor = 1, 
                    Capacity = 2, 
                    PricePerNight = 100.00m,
                    Status = RoomStatus.Available
                },
                new Room { 
                    Id = 2, 
                    RoomNumber = "201", 
                    RoomType = "Deluxe", 
                    Floor = 2, 
                    Capacity = 2, 
                    PricePerNight = 150.00m,
                    Status = RoomStatus.Occupied
                }
            };
            return View(rooms);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                // Add room to database
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Room/Edit/5
        public IActionResult Edit(int id)
        {
            // Fetch room from database
            var room = new Room { 
                Id = id, 
                RoomNumber = "101", 
                RoomType = "Standard", 
                Floor = 1, 
                Capacity = 2, 
                PricePerNight = 100.00m,
                Status = RoomStatus.Available
            };
            return View(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Room room)
        {
            if (ModelState.IsValid)
            {
                // Update room in database
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Room/Delete/5
        public IActionResult Delete(int id)
        {
            // Fetch room from database
            var room = new Room { 
                Id = id, 
                RoomNumber = "101", 
                RoomType = "Standard" 
            };
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete room from database
            return RedirectToAction(nameof(Index));
        }

        // GET: Room/Status/5
        public IActionResult Status(int id)
        {
            // Fetch room from database
            var room = new Room { 
                Id = id, 
                RoomNumber = "101", 
                RoomType = "Standard",
                Status = RoomStatus.Available
            };
            return View(room);
        }

        // POST: Room/Status/5
        [HttpPost]
        public IActionResult Status(int id, RoomStatus status)
        {
            // Update room status in database
            return RedirectToAction(nameof(Index));
        }
    }
} 