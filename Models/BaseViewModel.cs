using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class BaseViewModel
    {
        public ProfileViewModel Profile { get; set; }
        public bool IsAuthenticated { get; set; }
        public string CurrentController { get; set; }
        public string CurrentAction { get; set; }

        // Add these properties for dynamic views
        public List<ServiceModel> Services { get; set; } = new List<ServiceModel>();
        public List<RoomTypeModel> RoomTypes { get; set; } = new List<RoomTypeModel>();
        public List<Reservation> ActiveReservations { get; set; } = new List<Reservation>();
        public List<Reservation> PastReservations { get; set; } = new List<Reservation>();

        // Add missing property for views
        public List<string> SpecialOffers { get; set; } = new List<string>();

        public BaseViewModel()
        {
            Profile = new ProfileViewModel();
            IsAuthenticated = false;
        }

        public static BaseViewModel FromUser(User user)
        {
            if (user == null)
                return new BaseViewModel();

            return new BaseViewModel
            {
                IsAuthenticated = true,
                Profile = ProfileViewModel.FromUser(user)
            };
        }
    }

    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string OperatingHours { get; set; }
        public string Location { get; set; }
        public decimal PricePerPerson { get; set; }
        public int? MaxGuests { get; set; }

        // Add missing properties for views
        public bool IsActive { get; set; }
        public string IconUrl { get; set; }
        public string IconClass { get; set; }
        public bool HasOptions { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public string OptionLabel { get; set; }
        public bool Optional { get; set; }
    }

    public class RoomTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerNight { get; set; }
        public string BedType { get; set; }
        public int MaxOccupancy { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public List<AmenityModel> Amenities { get; set; } = new List<AmenityModel>();
    }

    public class AmenityModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
} 