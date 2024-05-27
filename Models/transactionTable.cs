using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace FirstWebApp.Models
{
	// Define the transactionTable class
	public class transactionTable
	{
		// Define a static connection string for the SQL database
		internal static string con_string = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog=CLOUD-db;Persist Security Info=False;User ID=admin-youyou;Password=C'esttropcool87;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

		// Define properties for the transactionTable class
		public int UserID { get; set; }
		public int ProductID { get; set; }
		public DateTime TransactionTime { get; set; }

		// Static method to get transactions for a specific user by userID
		public static List<transactionTable> GetTransactionsForUser(int? userID)
		{
			// Initialize a list to store transactions
			List<transactionTable> transactions = new List<transactionTable>();

			// Create a new instance of SqlConnection using the connection string
			using (SqlConnection con = new SqlConnection(con_string))
			{
				// Define the SQL query to select transactions for the specific user
				string sql = "SELECT * FROM transactionTable WHERE userID = @userID";
				SqlCommand cmd = new SqlCommand(sql, con);

				// Add the userID parameter to the SqlCommand
				cmd.Parameters.AddWithValue("@userID", userID);

				// Open the SqlConnection
				con.Open();

				// Execute the SqlCommand and read the results
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					// Create a new transactionTable object for each row
					transactionTable transaction = new transactionTable();
					transaction.UserID = Convert.ToInt32(rdr["userID"]);
					transaction.ProductID = Convert.ToInt32(rdr["productID"]);
					transaction.TransactionTime = Convert.ToDateTime(rdr["TransactionTime"]);

					// Add the transaction to the list
					transactions.Add(transaction);
				}
			}

			// Return the list of transactions
			return transactions;
		}
	}

}
