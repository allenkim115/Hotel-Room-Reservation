using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ProfileViewModel
    {
        public int PK_USER_ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        
        // Additional profile properties
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int TotalStays { get; set; }
        public int UpcomingStays { get; set; }
        public int PastStays { get; set; }

        public static ProfileViewModel FromUser(User user)
        {
            return new ProfileViewModel
            {
                PK_USER_ID = user.PK_USER_ID,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                // Initialize additional properties with default values
                TotalStays = 0,
                UpcomingStays = 0,
                PastStays = 0
            };
        }
    }
} 