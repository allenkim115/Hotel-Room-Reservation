using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(ApplicationDbContext context) : base(context)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] ProfileUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Json(new { success = false, message = "Validation failed: " + errors });
            }

            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Json(new { success = false, message = "User not authenticated." });

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return Json(new { success = false, message = "User not found." });

            try
            {
                // Log the incoming model data
                System.Diagnostics.Debug.WriteLine($"Updating profile for user {username}");
                System.Diagnostics.Debug.WriteLine($"Model data: {JsonSerializer.Serialize(model)}");

                // Update user properties
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.PreferredLanguage = model.PreferredLanguage;
                user.EmailNotifications = model.EmailNotifications;
                user.SmsNotifications = model.SmsNotifications;

                _context.SaveChanges();

                // Log the updated user data
                System.Diagnostics.Debug.WriteLine($"Updated user data: {JsonSerializer.Serialize(user)}");

                // Return updated user data
                return Json(new { 
                    success = true,
                    user = new {
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        email = user.Email,
                        phoneNumber = user.PhoneNumber,
                        address = user.Address,
                        preferredLanguage = user.PreferredLanguage,
                        emailNotifications = user.EmailNotifications,
                        smsNotifications = user.SmsNotifications
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating profile: {ex}");
                return Json(new { success = false, message = "Error updating profile: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdatePassword([FromForm] PasswordUpdateModel model)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Json(new { success = false, message = "User not authenticated." });

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return Json(new { success = false, message = "User not found." });

            // Verify current password
            if (user.Password != model.CurrentPassword) // Note: In production, use proper password hashing!
                return Json(new { success = false, message = "Current password is incorrect." });

            // Verify new passwords match
            if (model.NewPassword != model.ConfirmPassword)
                return Json(new { success = false, message = "New passwords do not match." });

            // Update password
            user.Password = model.NewPassword; // Note: In production, use proper password hashing!
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile profileImage)
        {
            if (profileImage == null || profileImage.Length == 0)
                return Json(new { success = false, message = "No image file provided." });

            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Json(new { success = false, message = "User not authenticated." });

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return Json(new { success = false, message = "User not found." });

            // Generate unique filename
            var fileName = $"{username}_{DateTime.UtcNow.Ticks}{Path.GetExtension(profileImage.FileName)}";
            var filePath = Path.Combine("wwwroot", "uploads", "profile-images", fileName);

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                profileImage.CopyTo(stream);
            }

            // Update user's profile image URL
            user.ProfileImageUrl = $"/uploads/profile-images/{fileName}";
            _context.SaveChanges();

            return Json(new { success = true, imageUrl = user.ProfileImageUrl });
        }
    }

    public class ProfileUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PreferredLanguage { get; set; }
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
    }

    public class PasswordUpdateModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
} 