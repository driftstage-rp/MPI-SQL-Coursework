using System;
using System.Collections.Generic;
using System.Linq;
using MPI_SQL_Coursework.DTOs;

namespace MPI_SQL_Coursework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Delete(args);

            Select(args);
        }

        static void Delete(string[] args)
        {
            MPI.Environment.Run(ref args, comm =>
            {
                if (comm.Rank == 0)
                {
                    var timer = new System.Diagnostics.Stopwatch();
                    timer.Start();
                    List<Product> total = new List<Product>();

                    if (comm.Size == 1)
                    {
                        var query = string.Join(" union all ", Queries.Queries.Unions);
                        total = DbContext.GetProducts(query);
                    }
                    else
                    {
                        for (int i = 1; i < comm.Size; i++)
                        {
                            var temp = comm.Receive<List<Product>>(i, 0);
                            total = total.Concat(temp).ToList();
                        }
                    }

                    timer.Stop();
                    Console.WriteLine($"time - {timer.ElapsedMilliseconds} count - {total.Count}");
                }
                else
                {
                    var total = Queries.Queries.Unions.Count / (comm.Size - 1);
                    var skip = total * (comm.Rank - 1);
                    var res = DbContext.GetProducts(string.Join(" union all ", Queries.Queries.Unions.Skip(skip).Take(total)));
                    comm.Send(res, 0, 0);
                }
            });
        }

        static void Select(string[] args)
        {
            MPI.Environment.Run(ref args, comm =>
            {
                if (comm.Rank == 0)
                {
                    var timer = new System.Diagnostics.Stopwatch();
                    timer.Start();
                    List<Product> total = new List<Product>();

                    if (comm.Size == 1)
                    {
                        var query = string.Join(" union all ", Queries.Queries.Unions);
                        total = DbContext.GetProducts(query);
                    }
                    else
                    {
                        for (int i = 1; i < comm.Size; i++)
                        {
                            var temp = comm.Receive<List<Product>>(i, 0);
                            total = total.Concat(temp).ToList();
                        }
                    }

                    timer.Stop();
                    Console.WriteLine($"time - {timer.ElapsedMilliseconds} count - {total.Count}");
                }
                else
                {
                    var total = Queries.Queries.Unions.Count / (comm.Size - 1);
                    var skip = total * (comm.Rank - 1);
                    var res = DbContext.GetProducts(string.Join(" union all ", Queries.Queries.Unions.Skip(skip).Take(total)));
                    comm.Send(res, 0, 0);
                }
            });
        }
    }
}
