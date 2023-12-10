using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPI_SQL_Coursework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Delete(args);
        }

        static void Delete(string[] args)
        {
            MPI.Environment.Run(ref args, comm =>
            {
                if (comm.Rank == 0)
                {
                    var timer = new System.Diagnostics.Stopwatch();
                    timer.Start();
                    string query = "";
                    int total = 0;

                    if (comm.Size == 1)
                    {
                        string querytemplate = Queries.Queries.Delete.Distinct().FirstOrDefault();
                        for (int i = 0; i < Queries.Queries.Delete.Count; i++)
                        {
                            query = string.Concat(query, string.Concat(querytemplate, Convert.ToString(i + 1) + "; "));
                            total = i+1;
                            
                        }
                        DbContext.DelNum(query);
                    }
                    else
                    {
                        for (int i = 1; i < comm.Size; i++)
                        {
                            var temp = comm.Receive<int>(i, 0);
                            total += temp;
                        }
                    }
                    timer.Stop();
                    Console.WriteLine($"time - {timer.ElapsedMilliseconds} count - {total}");
                }
                else
                {
                    var total = Queries.Queries.Delete.Count / (comm.Size - 1); 
                    var skip = total * (comm.Rank - 1);
                    var query = "";
                    string querytemplate = Queries.Queries.Delete.Distinct().FirstOrDefault();
                    for (int i = skip; i < skip+total; i++)
                    {
                        query = string.Concat(query, string.Concat(querytemplate, Convert.ToString(i + 1) + "; "));
                    }
                    DbContext.DelNum(query);
                    comm.Send(total, 0, 0);
                }
            });
        }
    }
}

