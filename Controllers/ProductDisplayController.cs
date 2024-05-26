using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class ProductDisplayController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var products = ProductDisplayModel.SelectProducts();
            return View(products);
        }
    }
}
