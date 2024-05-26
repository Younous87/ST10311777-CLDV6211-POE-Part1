using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
	public class LoginController : Controller
    {

        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }

        [HttpPost]
        public ActionResult Login(string email, string name)
        {
            var loginModel = new LoginModel();
            int userId = loginModel.SelectUser(email, name);
            if (userId != -1)
            {

	            // Store userID in session
	            HttpContext.Session.SetInt32("userID", userId);

				return RedirectToAction("Cart", "Home", new { userId = userId });

            }
            else
            {
                return RedirectToAction("SignUp", "User");
            }
        }


    }
}
