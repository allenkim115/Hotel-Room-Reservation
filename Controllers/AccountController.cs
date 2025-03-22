using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            // Static username and password validation for User and Admin
            string userUsername = "user";
            string userPassword = "user123";
            string adminUsername = "admin";
            string adminPassword = "admin123";

            if (model.Username == userUsername && model.Password == userPassword)
            {
                // Redirect to user dashboard or home page
                return RedirectToAction("Indexlogin", "Home");
            }
            else if (model.Username == adminUsername && model.Password == adminPassword)
            {
                // Redirect to admin dashboard or admin page
                return RedirectToAction("AdminDash", "Home");
            }
            else
            {
                // Add an error message if the credentials are incorrect
                ViewData["ErrorMessage"] = "Incorrect username or password.";
                return View(model); // Return the same view to display the error
            }
        }
    }
}
