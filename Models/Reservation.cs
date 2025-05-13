using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalAmount { get; set; }
        public string SpecialRequests { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
} 