using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class UserController : Controller
    {
        public UserTable usertbl = new UserTable();

        [HttpPost]
        public ActionResult About(UserTable user)
        {
            var result = usertbl.insert_User(user);
            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public ActionResult About()
        {
            return View(usertbl);
        }
    }
}
