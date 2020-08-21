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
            /*
             * Quantifiers =>	All, Any, Contains
             * Aggregation=>	Aggregate, Average, Count, LongCount, Max, Min, Sum
             * Conversion=>	AsEnumerable, AsQueryable, Cast, ToArray, ToDictionary, ToList
             * Elements=>	ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault
                 Set	Distinct, Except, Intersect, Union
             * Grouping	=>GroupBy, ToLookup
             * Join	=>GroupJoin, Join
             * sorting	=>OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse
             * Projection	=>Select, SelectMany
             * Partitioning=>	Skip, SkipWhile, Take, TakeWhile
             * Filtering=>	Where, OfType
             * Concatenation	=>Concat
             * Equality	=>SequenceEqual
             * Generation=>	DefaultEmpty, Empty, Range, Repeat
             */

            int[] data1 = { 1, 2, 3, 4 };
            IEnumerable<int> res = from d in data1
                                   select d;
            Console.WriteLine(string.Join(",", res));

            //Cross - Join
            int[] data2 = { 1, 2, 3, 4 };
            int[] data3 = { 1, 2, 3, 4 };

            //{ c = 2, o = 2 }
            var joinres = from c in data2
                          join o in data3 on c equals o
                          where c == 2
                          select new
                          {
                              c,
                              o
                          };
            //prodotto cartesiano
            var qCross1 = from em in data2
                          from sh in data3
                          select new { em, sh };

            //prodotto cartesiano
            var qCross2 = data2.Join(
                data3,
                em => true,
                sh => true,
                (em, sh) => new { em, sh }
            );

            //prodotto cartesiano
            var qCross3 = data2.Join(
                data3,
                em => new { Dummy = 1 },
                sh => new { Dummy = 1 },
                (em, sh) => new { em, sh }
            );


            /*ordini[]
             *   ordine
             *       tipopagamento
             *       nome
             *       linee[]
             *           linea
             *               amount
             *               prodotto
             *                      des
             *                      price
             * 
             * nome ordine1
             * tipopag bo
             * 2 giacca 10
             * 1 pigiama 20
             * 
             * nome ordine2
             * tipopag bo
             * 1 giacca 30
             * 
             * nome ordine2
             * tipopag bo
             * 1 scarpa 10
             * 1 cappello 10
             */
            #region ordini
            List<Order> orders = new List<Order>();
            orders.Add(new Order()
            {
                name = "ordine1",
                tipopagamento = "bo",
                OrderLines = new List<OrderLine>()
                {
                   new OrderLine()
                   {
                       Amount = 2,
                       Product = new Product()
                       {
                           Des = "giacca",
                           Price = 10

                       }
                   },
                    new OrderLine()
                    {
                        Amount = 1,
                        Product = new Product()
                        {
                            Des = "pigiama",
                            Price = 10

                        }
                    }
                }
            });
            orders.Add(new Order()
            {
                tipopagamento = "bo",
                name = "ordine2",
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Amount = 1,
                        Product = new Product()
                        {
                            Des = "giacca",
                            Price = 30

                        }
                    }
                }
            });
            orders.Add(new Order()
            {
                tipopagamento = "carta",
                name = "ordine3",
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Amount = 1,
                        Product = new Product()
                        {
                            Des = "scarpa",
                            Price = 20

                        }
                    },
                    new OrderLine()
                    {
                        Amount = 1,
                        Product = new Product()
                        {
                            Des = "cappello",
                            Price = 20

                        }
                    }
                }
            });

            #endregion

            var tt = from c in orders
                     group c by c.tipopagamento;

            //grouping and projectons
            var res1 = from o in orders
                       from l in o.OrderLines
                       group l by l.Product.Price
                into p
                       select new
                       {
                           Product = p.Key,
                           Amount = p.Sum(x => x.Amount)
                       };


            var regroupres = from ordine in orders
                             group ordine by ordine.tipopagamento into grouped
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
        public string tipopagamento  { get; set; }
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

    public class Product
    {
        public string Des { get; set; }
        public decimal Price { get; set; }
    }
}
