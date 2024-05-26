using System.Data.SqlClient;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace FirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger; 
            _httpContextAccessor = httpContextAccessor; // Initialize IHttpContextAccessor
        }

        public static string con_String = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog = CLOUD - db; Persist Security Info=False;User ID = admin - youyou; Password=C'esttropcool87; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

        public static SqlConnection con = new SqlConnection(con_String);

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Contact()
        {
			return View();
		}

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Cart()
        {
	        // Retrieve userID from session
	        int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("userID");

	        if (userId == null)
	        {
		        // User is not logged in, display a message
		        ViewData["Message"] = "Please login to view your cart.";
		        return View("Login");
	        }
	        else
	        {
		        // Retrieve all products from the database
		        List<productsTable> products = productsTable.GetAllProducts();
                List<transactionTable> transactions = transactionTable.GetTransactionsForUser(userId);


                // Pass products and userID to the view
                ViewData["Products"] = products;
		        ViewData["userID"] = userId;
                ViewData["Transactions"] = transactions;

                return View();
	        }
        }



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
