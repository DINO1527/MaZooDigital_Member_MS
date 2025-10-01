using System.Configuration;
using System.Data.SqlClient;

namespace MadZooDigital.Data
{
    public static class DbHelper
    {
        // Change server name if different
        private const string Connection = @"Server=DINOTARGARYAN;Database=MadZoo_Digital;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(Connection);
        }
    }
}
