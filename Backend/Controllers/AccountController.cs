using Azure.Identity;
using BlogApplication.DataAccess;
using BlogApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
       
     

        public AccountController(UserService userService)
        {
            _userService = userService;
          
        }


        public IActionResult Login()
        {
            var model = new LoginViewModel(); // Create a new instance of LoginViewModel
            return View(model); // Pass the model to the view
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByEmailAndPassword(model.Email, model.Password);

                if (user != null)
                {
                    if (user.Role == UserRole.Admin)
                    {
                        // Redirect admin users to the admin dashboard
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        // Redirect regular users to the regular dashboard
                        return RedirectToAction("Dashboard", "Blog");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Your email or password is incorrect. Please try again.");
                }
            }

            return View(model);
        }


        public IActionResult Register()
        {
            var user = new User(); // Create a new instance of User
            return View(user);
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.InsertUser(user);
                TempData["SuccessMessage"] = "User registered successfully!";
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        // This action is called when the user logs in successfully
        public IActionResult Dashboard()
        {
            // Get the user's email from the ClaimsPrincipal
            string userEmail = User.Identity.Name;
          

            // Pass the email to the dashboard view
            return View("Dashboard", userEmail);
        }

        public IActionResult Profile()
        {
            // Retrieve user's profile information from the database using the user ID
            int userId = 1; // Replace with actual user ID logic
            UserProfileModel userProfile = _userService.GetUserProfileById(userId);

            if (userProfile == null)
            {
                // Handle case where user profile is not found
                return NotFound(); // Return a 404 Not Found error
            }

            // Pass the UserProfileModel instance to the view
            return View(userProfile);
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page
            return RedirectToAction("Index","Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the user's email from the ClaimsPrincipal
                string userEmail = User.Identity.Name;

                // Retrieve the user from the database based on the email
                var user = _userService.GetUserByEmail(userEmail);

                if (user != null)
                {
                    // Verify if the current password matches the user's stored password
                    if (user.Password == model.CurrentPassword)
                    {
                        // Update the user's password with the new password
                        user.Password = model.NewPassword;

                        // Call the method to update the user's password in the database using UserService
                        _userService.UpdateUserPassword(user);

                        // Redirect to the dashboard or any other desired page
                        return RedirectToAction("Dashboard", "Account");
                    }
                    else
                    {
                        // Current password provided is incorrect, add an error message
                        ModelState.AddModelError(string.Empty, "Your current password is incorrect. Please try again.");
                    }
                }
                else
                {
                    // User not found, handle the case
                }
            }

            // If model state is not valid or password change fails, return the view with errors
            return View(model);
        }
    }



    public class RegisterViewModel
    {
        // Add properties relevant to registration if needed
    }
}
