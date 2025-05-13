using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }

        [Required]
        [Display(Name = "Floor")]
        public int Floor { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Price Per Night")]
        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public RoomStatus Status { get; set; }

        [Display(Name = "Features")]
        public string Features { get; set; }

        [Display(Name = "Last Maintenance")]
        public DateTime? LastMaintenance { get; set; }

        [Display(Name = "Next Maintenance")]
        public DateTime? NextMaintenance { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; } = true;
    }

    public enum RoomStatus
    {
        Available,
        Occupied,
        Maintenance,
        Cleaning,
        Reserved
    }
} 