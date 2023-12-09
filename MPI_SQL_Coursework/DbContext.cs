using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;
using MPI_SQL_Coursework.DTOs;

namespace MPI_SQL_Coursework
{
    internal static class DbContext
    {
        private static readonly string connString = "Server=localhost\\SQLEXPRESS;Database=TSQL2012;Trusted_Connection=True;";
        private static readonly IDbConnection _dbConnection = new SqlConnection(connString);

        public static List<Product> GetProducts(string query)
        {
            var res = _dbConnection.Query<Product>(query).ToList();
            return res;
        }

        public static void AddProducts(string query)
        {
            _dbConnection.Execute($"insert into Production.Products\r\nvalues {query}");
        }
    }
}
