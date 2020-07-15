using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphore_Example
{
    class Semaphore_Example
    {
        //Object of Semaphore class with two parameter.  
        //first parameter explain number of process to initial start  
        //second parameter explain maximum number of process can start  
        //second parameter must be equal or greate then first paramete  

        static Semaphore obj = new Semaphore(3, 2);

        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(SempStart);
                t.Start(i);
            }
            Console.ReadKey();
        }
        static void SempStart(object id)
        {

            Console.WriteLine(id + " Wants to Get Enter for processing");
            try
            {
                //Blocks the current thread until the current WaitHandle receives a signal.   
                obj.WaitOne();
                Console.WriteLine(" Success: " + id + " is in!");
                Thread.Sleep(5000);
                Console.WriteLine(id + "<<-- is exit because it completed there operation");
            }
            finally
            {
                //Release() method to releage semaphore  
                obj.Release();
            }
        }
    }
}
