using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;

namespace WebApplication1.Services
{
    public class AdminInitializationService
    {
        private readonly ApplicationDbContext _context;

        public AdminInitializationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAdminAccount()
        {
            // Check if admin account exists
            var adminExists = await _context.Users
                .AnyAsync(u => u.Role.ToLower() == "admin");

            if (!adminExists)
            {
                var adminUser = new User
                {
                    FirstName = "System",
                    MiddleName = "Admin",
                    LastName = "Administrator",
                    Email = "admin@hotelreservation.com",
                    Username = "admin",
                    Password = "admin123",
                    Role = "Admin"
                };

                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();

                Console.WriteLine("Admin account created successfully!");
            }
            else
            {
                Console.WriteLine("Admin account already exists.");
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
} 