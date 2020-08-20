using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLinq
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] data1 = { 1, 2, 3, 4 };
            IEnumerable<int> res = from d in data1
                                   select d;
            Console.WriteLine(string.Join(",", res));



            //Cross - Join
            int[] data2 = { 1, 2, 3, 4 };
            int[] data3 = { 1, 2, 3, 4 };

            var joinres = from c in data2
                          join o in data3 on c equals o
                          where c == 2
                          select new
                          {
                              c,
                              o
                          };

            //var rtt =  data2
            //     .Where(c => c == 2)
            //     .Join(data3,
            //         c => new { c },
            //         o => new { o },
            //         (c, o) => new { c, o  });


            var qCross1 = from em in data2
                          from sh in data3
                          select new { em, sh };

            var qCross2 = data2.Join(
                data3,
                em => true,
                sh => true,
                (em, sh) => new { em, sh }
            );

            var qCross3 = data2.Join(
                data3,
                em => new { Dummy = 1 },
                sh => new { Dummy = 1 },
                (em, sh) => new { em, sh }
            );


            List<Order> orders = new List<Order>();
            orders.Add(new Order()
            {
                OrderLines = new List<OrderLine>()
                {
                   new OrderLine()
                   {
                       Amount = 1,
                       Product = new Product()
                       {
                           Des = "pippo",
                           Price = 10

                       }
                   }
                }
            });
            orders.Add(new Order()
            {

                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Amount = 2,
                        Product = new Product()
                        {
                            Des = "pluto",
                            Price = 20

                        }
                    }
                }
            });

            var tt = from c in orders
                     group c by c.name;

            //grouping and projectons
            var res1 = from o in orders
                       from l in o.OrderLines
                       group l by l.Product
                into p
                       select new
                       {
                           Product = p.Key,
                           Amount = p.Sum(x => x.Amount)
                       };


            var regroupres = from ordine in orders
                             group ordine by ordine.name into grouped
                             where grouped.Count() > 2
                             select new
                             {
                                 Country = grouped.Key,
                                 Conto = grouped.Count()
                             };


            int[] datax = { 1, 2, 3, 4 };
            int[] datay = { 1, 6, 3, 5 };

            IEnumerable<int> rr = from x in datax
                                  join s2 in datay
                                      on x equals s2
                                  select x;

            Console.WriteLine(string.Join(",", rr));// 1, 3
        }
    }

    public class Order
    {
        public string name { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine
    {
        public int Amount { get; set; }
        public Product Product { get; set; }
    }

    public class Student
    {
        public string StudentName;
        public int StudentNumber;
    }

    public class Book
    {
        public string BookName;
        public int BookNumber;
    }
}
