using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace FirstWebApp.Models
{
	public class transactionTable
	{

		internal static string con_string = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog = CLOUD-db; Persist Security Info=False;User ID = admin-youyou; Password=C'esttropcool87; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

		public int UserID { get; set; }
		public int ProductID { get; set; }
		public DateTime TransactionTime { get; set; }


		public static List<transactionTable> GetTransactionsForUser(int? userID)
		{
			List<transactionTable> transactions = new List<transactionTable>();

			using (SqlConnection con = new SqlConnection(con_string))
			{
				string sql = "SELECT * FROM transactionTable WHERE userID = @userID";
				SqlCommand cmd = new SqlCommand(sql, con);

				cmd.Parameters.AddWithValue("@userID", userID);

				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					transactionTable transaction = new transactionTable();
					transaction.UserID = Convert.ToInt32(rdr["userID"]);
					transaction.ProductID = Convert.ToInt32(rdr["productID"]);
					transaction.TransactionTime = Convert.ToDateTime(rdr["TransactionTime"]);

					transactions.Add(transaction);
				}
			}

			return transactions;
		}
	}
}
