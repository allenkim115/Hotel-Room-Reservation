using System;

namespace WebApplication1.Models
{
    public class BaseViewModel
    {
        public ProfileViewModel Profile { get; set; }
        public bool IsAuthenticated { get; set; }
        public string CurrentController { get; set; }
        public string CurrentAction { get; set; }

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
} 