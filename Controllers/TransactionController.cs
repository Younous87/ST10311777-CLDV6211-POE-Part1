using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace FirstWebApp.Controllers
{
	// Define the TransactionController class which inherits from the Controller class
	public class TransactionController : Controller
	{
		// Define a static connection string for the SQL database
		private static string con_string =
			"Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog=CLOUD-db;Persist Security Info=False;User ID=admin-youyou;Password=C'esttropcool87;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

		// Action method to handle POST requests for placing an order
		[HttpPost]
		public ActionResult PlaceOrder(int userId, int productID)
		{
			// Create a new instance of SqlConnection using the connection string
			using (SqlConnection con = new SqlConnection(con_string))
			{
				// Define the SQL query to insert a new record into the transactionTable
				string sql =
					"INSERT INTO transactionTable (userID, productID, TransactionTime) VALUES (@UserID, @ProductID, @TransactionTime)";

				// Create a new instance of SqlCommand with the SQL query and SqlConnection
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					// Add parameters to the SqlCommand for userID, productID, and TransactionTime
					cmd.Parameters.AddWithValue("@UserID", userId);
					cmd.Parameters.AddWithValue("@ProductID", productID);
					cmd.Parameters.AddWithValue("@TransactionTime", DateTime.Now);

					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand to insert the record into the transactionTable
					int rowsAffected = cmd.ExecuteNonQuery();

					// Close the SqlConnection
					con.Close();

					// Check if the insert operation was successful
					if (rowsAffected > 0)
					{
						// Redirect the user to the cart page after placing the order
						return RedirectToAction("Cart", "Home");
					}
					else
					{
						// If the insert operation failed, redirect to the cart page with an error indication
						return RedirectToAction("Cart", "Home");
					}
				}
			}
		}
	}

}


