using System.Data.SqlClient;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace FirstWebApp.Models
{
	// Define the UserTable class
	public class UserTable
	{
		// Define properties for the UserTable class
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }

		// Method to insert a new user into the database
		public int insert_User(UserTable m)
		{
			try
			{
				// Define the connection string for the SQL database
				SqlConnection con = new SqlConnection("Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog=CLOUD-db;Persist Security Info=False;User ID=admin-youyou;Password=C'esttropcool87;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

				// Define the SQL query to insert a new user into the UserTable
				string sql = "INSERT INTO UserTable(userName, userSurname, userEmail) VALUES(@Name, @Surname, @Email)";

				// Create a new instance of SqlCommand with the SQL query and SqlConnection
				SqlCommand cmd = new SqlCommand(sql, con);

				// Add parameters to the SqlCommand for the user's name, surname, and email
				cmd.Parameters.AddWithValue("@Name", m.Name);
				cmd.Parameters.AddWithValue("@Surname", m.Surname);
				cmd.Parameters.AddWithValue("@Email", m.Email);

				// Open the SqlConnection
				con.Open();

				// Execute the SqlCommand to insert the user into the UserTable
				int i = cmd.ExecuteNonQuery();

				// Close the SqlConnection
				con.Close();

				// Return the number of rows affected (1 if the insert was successful, otherwise 0)
				return i;
			}
			catch (Exception e)
			{
				// Print the exception to the console
				Console.WriteLine(e);
				// Rethrow the exception to be handled by the calling code
				throw;
			}
		}
	}
    
}
