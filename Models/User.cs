using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("USER")]
    public class User
    {
        [Key]
        [Column("PK_USER_ID")]
        public int PK_USER_ID { get; set; }

        [Required]
        [Column("FIRSTNAME")]
        public string FirstName { get; set; }

        [Column("MIDDLENAME")]
        public string MiddleName { get; set; }

        [Required]
        [Column("LASTNAME")]
        public string LastName { get; set; }

        [Required]
        [Column("USERNAME")]
        public string Username { get; set; }

        [Required]
        [Column("PASSWORD")]
        public string Password { get; set; }  // 🔴 Store as a hashed password!

        [Required]
        [EmailAddress]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required]
        [Column("ROLE")]
        public string Role { get; set; }

        //public string PhoneNumber { get; set; }

       // public string Address { get; set; }

       // public int TotalStays { get; set; }

        //public int UpcomingStays { get; set; }

       // public int PastStays { get; set; }
    }
}
