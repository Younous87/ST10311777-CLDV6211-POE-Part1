using System.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace FirstWebApp.Models
{
    public class productsTable
    {
	    internal static string con_string = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog = CLOUD-db; Persist Security Info=False;User ID = admin-youyou; Password=C'esttropcool87; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

        public static SqlConnection con = new SqlConnection(con_string);

        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Category { get; set; }

        public string Availability { get; set; }

        public string ImageURL { get; set; }

		public int IsActive { get; set; }


		public int insert_product(productsTable p)
		{
			try
			{
				string sql = "INSERT INTO productsTable (prodName, prodPrice, prodCategory, prodAvailability, prodImage, IsActive) VALUES (@Name, @Price, @Category, @Availability, @ImageURL, @IsActive)";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@Name", p.Name);
				cmd.Parameters.AddWithValue("@Price", p.Price);
				cmd.Parameters.AddWithValue("@Category", p.Category);
				cmd.Parameters.AddWithValue("@Availability", p.Availability);
				cmd.Parameters.AddWithValue("@ImageURL", p.ImageURL);
				cmd.Parameters.AddWithValue("@IsActive", p.IsActive);
				con.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected;
			}
			catch (Exception ex)
			{
				// Log the exception or handle it appropriately
				// For now, rethrow the exception
				throw ex;
			}
			finally
			{
				if (con != null)
				{
					con.Close();
				}
			}
		}

		public static List<productsTable> GetAllProducts()
        {
            List<productsTable> products = new List<productsTable>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM productsTable";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    productsTable product = new productsTable();
                    product.ProductID = Convert.ToInt32(rdr["prodId"]);
                    product.Name = rdr["prodName"].ToString();
                    product.Price = rdr["prodPrice"].ToString();
                    product.Category = rdr["prodCategory"].ToString();
                    product.Availability = rdr["prodAvailability"].ToString();
                    product.ImageURL = rdr["prodImage"].ToString();
					product.IsActive = Convert.ToInt32(rdr["IsActive"]);

                    products.Add(product);
                }
            }

            return products;
        }

        public int delete_product(int productID)
        {
	        try
	        {
		        string sql = "DELETE FROM productsTable WHERE prodId = @ProductID";
		        SqlCommand cmd = new SqlCommand(sql, con);
		        cmd.Parameters.AddWithValue("@ProductID", productID);
		        con.Open();
		        int rowsAffected = cmd.ExecuteNonQuery();
		        return rowsAffected;
	        }
	        catch (Exception ex)
	        {
		        throw ex;
	        }
	        finally
	        {
		        if (con != null)
		        {
			        con.Close();
		        }
	        }
        }

        public static void UpdateProduct(productsTable product)
        {
	        using (SqlConnection con = new SqlConnection(con_string))
	        {
		        string sql = "UPDATE productsTable SET IsActive = @IsActive WHERE prodId = @ProductID";
		        SqlCommand cmd = new SqlCommand(sql, con);
		        cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
		        cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

		        con.Open();
		        cmd.ExecuteNonQuery();
	        }
        }

        public int update_availability(int productID, string availability)
        {
	        try
	        {
		        string sql = "UPDATE productsTable SET prodAvailability = @Availability WHERE prodId = @ProductID";
		        SqlCommand cmd = new SqlCommand(sql, con);
		        cmd.Parameters.AddWithValue("@Availability", availability);
		        cmd.Parameters.AddWithValue("@ProductID", productID);
		        con.Open();
		        int rowsAffected = cmd.ExecuteNonQuery();
		        return rowsAffected;
	        }
	        catch (Exception ex)
	        {
		        throw ex;
	        }
	        finally
	        {
		        if (con != null)
		        {
			        con.Close();
		        }
	        }
        }

        public int update_price(int productID, string price)
        {
	        try
	        {
		        string sql = "UPDATE productsTable SET prodPrice = @Price WHERE prodId = @ProductID";
		        SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@Price", price);
				cmd.Parameters.AddWithValue("@ProductID", productID);
		        con.Open();
		        int rowsAffected = cmd.ExecuteNonQuery();
		        return rowsAffected;
	        }
	        catch (Exception ex)
	        {
		        throw ex;
	        }
	        finally
	        {
		        if (con != null)
		        {
			        con.Close();
		        }
	        }
        }
	}

   
}
