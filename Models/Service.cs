using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string OperatingHours { get; set; }
        public string Location { get; set; }
        [Required]
        public decimal PricePerPerson { get; set; }
        public int? MaxGuests { get; set; }
    }
} 