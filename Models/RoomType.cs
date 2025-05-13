using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int PricePerNight { get; set; }
        public string BedType { get; set; }
        public int MaxOccupancy { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        // Optionally: public ICollection<Amenity> Amenities { get; set; }
    }
} 