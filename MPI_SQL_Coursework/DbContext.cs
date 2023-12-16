using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace MPI_SQL_Coursework
{
    internal static class DbContext
    {
        private static readonly string connString = "Server=localhost\\SQLEXPRESS;Database=TSQL2012;Trusted_Connection=True;";
        private static readonly IDbConnection _dbConnection = new SqlConnection(connString);

        public static void DelNum(string query)
        {
            _dbConnection.Execute(query);
            _dbConnection.Close();
        }
    }
}
