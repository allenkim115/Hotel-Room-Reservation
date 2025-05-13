using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // Get current controller and action
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // Get current user if authenticated
            var username = User.Identity?.Name;
            var user = !string.IsNullOrEmpty(username) 
                ? _context.Users.FirstOrDefault(u => u.Username == username)
                : null;

            // Create base view model
            var baseViewModel = BaseViewModel.FromUser(user);
            baseViewModel.CurrentController = controller;
            baseViewModel.CurrentAction = action;

            // Make the base view model available to all views
            ViewData["BaseViewModel"] = baseViewModel;
        }

        protected BaseViewModel GetBaseViewModel()
        {
            return ViewData["BaseViewModel"] as BaseViewModel ?? new BaseViewModel();
        }

        protected IActionResult AuthorizedView(object model = null)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var baseViewModel = BaseViewModel.FromUser(user);
            
            if (model != null)
            {
                // If the model is already a BaseViewModel, use it
                if (model is BaseViewModel baseModel)
                {
                    baseModel.Profile = baseViewModel.Profile;
                    return View(baseModel);
                }
                
                // Otherwise, create a new view model with the model's properties
                var viewModel = new BaseViewModel
                {
                    Profile = baseViewModel.Profile
                };
                
                return View(viewModel);
            }

            return View(baseViewModel);
        }
    }
} 