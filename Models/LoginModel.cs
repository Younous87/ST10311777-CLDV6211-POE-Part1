using System.Data.SqlClient;

namespace FirstWebApp.Models
{
	// Define the LoginModel class
	public class LoginModel
	{
		// Define a static connection string for the SQL database
		public static string con_string = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog=CLOUD-db;Persist Security Info=False;User ID=admin-youyou;Password=C'esttropcool87;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

		// Method to select a user from the database by email and name
		public int SelectUser(string email, string name)
		{
			// Initialize userId to -1, indicating user not found
			int userId = -1;

			// Create a new instance of SqlConnection using the connection string
			using (SqlConnection con = new SqlConnection(con_string))
			{
				// Define the SQL query to select userId from the UserTable
				string sql = "SELECT userId FROM UserTable WHERE userEmail = @Email AND userName = @Name";

				// Create a new instance of SqlCommand with the SQL query and SqlConnection
				SqlCommand cmd = new SqlCommand(sql, con);

				// Add parameters to the SqlCommand for email and name
				cmd.Parameters.AddWithValue("@Email", email);
				cmd.Parameters.AddWithValue("@Name", name);

				try
				{
					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand and retrieve the userId
					object result = cmd.ExecuteScalar();

					// Check if a result was returned and is not null or DBNull
					if (result != null && result != DBNull.Value)
					{
						// Convert the result to an integer and assign it to userId
						userId = Convert.ToInt32(result);
					}
				}
				catch (Exception ex)
				{
					// Throw the exception if an error occurs
					throw ex;
				}
			}

			// Return the userId (will be -1 if user was not found)
			return userId;
		}
	}

}
