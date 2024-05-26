using System.Data.SqlClient;

namespace FirstWebApp.Models
{
    public class ProductDisplayModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public bool ProductAvailability { get; set; }

        public ProductDisplayModel() { }

        //Parameterized Constructor: This constructor takes five parameters (id, name, price, category, availability) and initializes the corresponding properties of ProductDisplayModel with the provided values.
        public ProductDisplayModel(int id, string name, decimal price, string category, bool availability)
        {
            ProductID = id;
            ProductName = name;
            ProductPrice = price;
            ProductCategory = category;
            ProductAvailability = availability;
        }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            //string con_string = "Integrated Security=SSPI;Persist Security Info=False;User ID=\"\";Initial Catalog=test;Data Source=labVMH8OX\\SQLEXPRESS";
         string con_string = "Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog = CLOUD-db; Persist Security Info=False;User ID = admin-youyou; Password=C'esttropcool87; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT prodId, prodName, prodPrice, prodCategory, prodAvailability FROM productsTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel();
                    product.ProductID = Convert.ToInt32(reader["prodId"]);
                    product.ProductName = Convert.ToString(reader["prodName"]);
                    product.ProductPrice = Convert.ToDecimal(reader["prodPrice"]);
                    product.ProductCategory = Convert.ToString(reader["prodCategory"]);
                    product.ProductAvailability = Convert.ToBoolean(reader["prodAvailability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}
