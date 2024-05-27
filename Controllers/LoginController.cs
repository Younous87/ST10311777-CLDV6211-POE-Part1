using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
	// Define the LoginController class which inherits from the Controller class
	public class LoginController : Controller
	{
		// Declare a private readonly field for the LoginModel
		private readonly LoginModel login;

		// Constructor for LoginController, initializes the login model
		public LoginController()
		{
			login = new LoginModel(); // Instantiate the LoginModel
		}

		// Define an action method for handling POST requests to the Login endpoint
		[HttpPost]
		public ActionResult Login(string email, string name)
		{
			// Create a new instance of LoginModel
			var loginModel = new LoginModel();
			// Call SelectUser method to get the userId based on email and name
			int userId = loginModel.SelectUser(email, name);

			// Check if a valid userId is returned
			if (userId != -1)
			{
				// Store userId in the session
				HttpContext.Session.SetInt32("userID", userId);

				// Redirect to the Cart action in the Home controller, passing the userId
				return RedirectToAction("Cart", "Home", new { userId = userId });
			}
			else
			{
				// If userId is -1, redirect to the SignUp action in the User controller
				return RedirectToAction("SignUp", "User");
			}
		}
	}

}
