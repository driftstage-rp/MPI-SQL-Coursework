using System.Collections.Generic;
using System.Linq;

namespace MPI_SQL_Coursework.Queries
{
    internal class Queries
    {
        public static List<string> Delete = Enumerable.Repeat("delete from dbo.Nums where n = ", 800).ToList();
    }
}

