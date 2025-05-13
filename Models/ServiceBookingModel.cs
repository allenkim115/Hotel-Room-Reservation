using System;

namespace WebApplication1.Models
{
    public class ServiceBookingModel
    {
        public int ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; }
    }
} 