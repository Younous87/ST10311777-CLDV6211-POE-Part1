using System.Data.SqlClient;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public static string con_String = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog = CLOUD - db; Persist Security Info=False;User ID = admin - youyou; Password=C'esttropcool87; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

        public static SqlConnection con = new SqlConnection(con_String);
       
        public IActionResult HomePage()
        {
            return View();
        }


	

		public IActionResult MyWork()
        {
            return View();

        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
			return View();
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
