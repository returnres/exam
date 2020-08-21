using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppYeld
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * You use a yield return statement to return each element one at a time.
               The sequence returned from an iterator method can be consumed 
               by using a foreach statement or LINQ query. Each iteration of the 
               foreach loop calls the iterator method. When a yield return statement is 
               reached in the iterator method, expression is returned, and the current location
               in code is retained. Execution is restarted from that location the next time that 
               the iterator function is called.
               You can use a yield break statement to end the iteration.
             * 
             * 
             */
            // Display powers of 2 up to the exponent of 8:
            Console.Write("yeld");

            foreach (int i in Power(2, 8))
            {
                Console.Write("{0} ", i);
                // Output: 2 4 8 16 32 64 128 256
            }

            Console.Write("no yeld");

            var res = Power1(2, 8);
            foreach (int i in res)
            {
                Console.Write("{0} ", i);
                // Output: 2 4 8 16 32 64 128 256
            }
        }

        public static IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }

        public static IEnumerable<int> Power1(int number, int exponent)
        {
            int result = 1;
            List<int> res = new List<int>();
            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                res.Add(result);
            }
            return res;
        }

    }
}
