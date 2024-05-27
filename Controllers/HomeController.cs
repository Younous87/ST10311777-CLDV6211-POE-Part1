using System.Data.SqlClient;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace FirstWebApp.Controllers
{
	// Define the HomeController class which inherits from the Controller class
	public class HomeController : Controller
	{
		// Declare a private readonly field for logging
		private readonly ILogger<HomeController> _logger;
		// Declare a private readonly field for accessing HTTP context
		private readonly IHttpContextAccessor _httpContextAccessor;

		// Constructor for HomeController, initializes the logger and HTTP context accessor
		public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger; // Assign the logger
			_httpContextAccessor = httpContextAccessor; // Assign the HTTP context accessor
		}

		// Define a static connection string for the SQL database
		public static string con_String = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog=CLOUD-db;Persist Security Info=False;User ID=admin-youyou;Password=C'esttropcool87;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

		// Define a static SQL connection using the connection string
		public static SqlConnection con = new SqlConnection(con_String);

		// Action method to return the HomePage view
		public IActionResult HomePage()
		{
			return View();
		}

		// Action method to return the Contact view
		public IActionResult Contact()
		{
			return View();
		}

		// Action method to return the Login view
		public IActionResult Login()
		{
			return View();
		}

		// Action method to return the Cart view
		public IActionResult Cart()
		{
			// Retrieve userID from the session
			int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("userID");

			// Check if userID is null (user not logged in)
			if (userId == null)
			{
				// Set a message indicating the user needs to log in to view the cart
				ViewData["Message"] = "Please login to view your cart.";
				// Redirect to the Login view
				return View("Login");
			}
			else
			{
				// Retrieve all products from the database
				List<productsTable> products = productsTable.GetAllProducts();
				// Retrieve transactions for the logged-in user
				List<transactionTable> transactions = transactionTable.GetTransactionsForUser(userId);

				// Pass products, userID, and transactions to the view
				ViewData["Products"] = products;
				ViewData["userID"] = userId;
				ViewData["Transactions"] = transactions;

				// Return the Cart view
				return View();
			}
		}

		// Action method to handle errors, with no caching
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			// Return the Error view with a new ErrorViewModel containing the request ID
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}

}
