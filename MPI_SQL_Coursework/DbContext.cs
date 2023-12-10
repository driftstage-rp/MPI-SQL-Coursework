using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;

namespace MPI_SQL_Coursework
{
    internal static class DbContext
    {
        private static readonly string connString = "Server=localhost;Database=TSQL2012;Trusted_Connection=True;";
        private static readonly IDbConnection _dbConnection = new SqlConnection(connString);

        public static void DelNum(string query)
        {
            _dbConnection.Execute(query);
            _dbConnection.Close();
        }
    }
}
