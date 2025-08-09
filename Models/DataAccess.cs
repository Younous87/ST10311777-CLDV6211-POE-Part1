using System.Data.SqlClient;

namespace FirstWebApp.Models
{
    public static class DataAccess
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
