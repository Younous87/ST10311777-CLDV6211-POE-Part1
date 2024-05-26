using System.Data.SqlClient;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace FirstWebApp.Models
{
    public class UserTable
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }

        public int insert_User(UserTable m)
        {
            try

            
            {
                SqlConnection con = new SqlConnection("Server=tcp:cloudev-sql-server.database.windows.net,1433;Initial Catalog = CLOUD-db; Persist Security Info=False;User ID = admin-youyou; Password=C'esttropcool87; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");
                string sql = "INSERT INTO UserTable(userName,userSurname, userEmail) VALUES(@Name,@Surname, @Email)";
                SqlCommand cmd = new SqlCommand(sql,con );
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Surname", m.Surname);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
