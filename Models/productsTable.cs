using System.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace FirstWebApp.Models
{
	// Define the productsTable class
	public class productsTable
	{
		// Define properties for the productsTable class
		public int ProductID { get; set; }
		public string Name { get; set; }
		public string Price { get; set; }
		public string Category { get; set; }
		public string Availability { get; set; }
		public string ImageURL { get; set; }
		public int IsActive { get; set; }

		// Method to insert a product into the database
		public static int insert_product(productsTable p)
		{
			using (SqlConnection con = DataAccess.GetConnection())
			{
				// Define the SQL query to insert a new product
				string sql = "INSERT INTO productsTable (prodName, prodPrice, prodCategory, prodAvailability, prodImage, IsActive) VALUES (@Name, @Price, @Category, @Availability, @ImageURL, @IsActive)";
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					// Add parameters to the SqlCommand
					cmd.Parameters.AddWithValue("@Name", p.Name);
					cmd.Parameters.AddWithValue("@Price", p.Price);
					cmd.Parameters.AddWithValue("@Category", p.Category);
					cmd.Parameters.AddWithValue("@Availability", p.Availability);
					cmd.Parameters.AddWithValue("@ImageURL", p.ImageURL);
					cmd.Parameters.AddWithValue("@IsActive", p.IsActive);

					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand to insert the product
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected;
				}
			}
		}

		// Static method to get all products from the database
		public static List<productsTable> GetAllProducts()
		{
			// Initialize a list to store products
			List<productsTable> products = new List<productsTable>();

			// Create a new instance of SqlConnection using the connection string
			using (SqlConnection con = DataAccess.GetConnection())
			{
				// Define the SQL query to select all products
				string sql = "SELECT * FROM productsTable";
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand and read the results
					using (SqlDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							// Create a new productsTable object for each row
							productsTable product = new productsTable();
							product.ProductID = Convert.ToInt32(rdr["prodId"]);
							product.Name = rdr["prodName"].ToString();
							product.Price = rdr["prodPrice"].ToString();
							product.Category = rdr["prodCategory"].ToString();
							product.Availability = rdr["prodAvailability"].ToString();
							product.ImageURL = rdr["prodImage"].ToString();
							product.IsActive = Convert.ToInt32(rdr["IsActive"]);

							// Add the product to the list
							products.Add(product);
						}
					}
				}
			}

			// Return the list of products
			return products;
		}

		// Method to delete a product from the database by productID
		public static int delete_product(int productID)
		{
			using (SqlConnection con = DataAccess.GetConnection())
			{
				// Define the SQL query to delete a product
				string sql = "DELETE FROM productsTable WHERE prodId = @ProductID";
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					// Add parameters to the SqlCommand
					cmd.Parameters.AddWithValue("@ProductID", productID);

					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand to delete the product
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected;
				}
			}
		}

		// Static method to update a product's IsActive status in the database
		public static void UpdateProduct(productsTable product)
		{
			// Create a new instance of SqlConnection using the connection string
			using (SqlConnection con = DataAccess.GetConnection())
			{
				// Define the SQL query to update the product's IsActive status
				string sql = "UPDATE productsTable SET IsActive = @IsActive WHERE prodId = @ProductID";
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					// Add parameters to the SqlCommand
					cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
					cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand to update the product
					cmd.ExecuteNonQuery();
				}
			}
		}

		// Method to update a product's availability in the database
		public static int update_availability(int productID, string availability)
		{
			using (SqlConnection con = DataAccess.GetConnection())
			{
				// Define the SQL query to update the product's availability
				string sql = "UPDATE productsTable SET prodAvailability = @Availability WHERE prodId = @ProductID";
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					// Add parameters to the SqlCommand
					cmd.Parameters.AddWithValue("@Availability", availability);
					cmd.Parameters.AddWithValue("@ProductID", productID);

					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand to update the availability
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected;
				}
			}
		}

		// Method to update a product's price in the database
		public int update_price(int productID, string price)
		{
			using (SqlConnection con = DataAccess.GetConnection())
			{
				// Define the SQL query to update the product's price
				string sql = "UPDATE productsTable SET prodPrice = @Price WHERE prodId = @ProductID";
				using (SqlCommand cmd = new SqlCommand(sql, con))
				{

					// Add parameters to the SqlCommand
					cmd.Parameters.AddWithValue("@Price", price);
					cmd.Parameters.AddWithValue("@ProductID", productID);

					// Open the SqlConnection
					con.Open();

					// Execute the SqlCommand to update the price
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected;
				}
			}
		}
	}



}
