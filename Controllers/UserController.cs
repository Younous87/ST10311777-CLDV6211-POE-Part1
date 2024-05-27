using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
	// Define the UserController class which inherits from the Controller class
	public class UserController : Controller
	{
		// Instantiate a UserTable object
		public UserTable usertbl = new UserTable();

		// Action method to handle POST requests for the About page
		[HttpPost]
		public ActionResult About(UserTable user)
		{
			// Insert the user into the UserTable
			var result = usertbl.insert_User(user);
			// Redirect to the HomePage action in the Home controller
			return RedirectToAction("HomePage", "Home");
		}

		// Action method to handle GET requests for the About page
		[HttpGet]
		public ActionResult About()
		{
			// Return the view, passing the UserTable object
			return View(usertbl);
		}

		// Action method to handle POST requests for the SignUp page
		[HttpPost]
		public ActionResult SignUp(UserTable user)
		{
			// Insert the user into the UserTable
			var result = usertbl.insert_User(user);
			// Redirect to the HomePage action in the Home controller
			return RedirectToAction("HomePage", "Home");
		}

		// Action method to handle GET requests for the SignUp page
		[HttpGet]
		public ActionResult SignUp()
		{
			// Return the view, passing the UserTable object
			return View(usertbl);
		}
	}

}
