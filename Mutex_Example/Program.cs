using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex_Example
{
    class Mutex_Example
    {
        private static Mutex mutex = new Mutex();
        public void Example()
        {
            //Create mumber of thread to explain muiltiple thread example  
            for (int i = 0; i < 4; i++)
            {
                Thread t = new Thread(MutexDemo);
                t.Name = string.Format("Thread {0} :", i + 1);
                t.Start();
            }
        }



        static void Main(string[] args)
        {
            Mutex_Example p = new Mutex_Example();
            p.Example();
            Console.ReadKey();
        }



        //Method to implement syncronization using Mutex  
        static void MutexDemo()
        {
            try
            {
                //Blocks the current thread until the current WaitHandle receives a signal.   
                mutex.WaitOne();   // Wait until it is safe to enter.  
                Console.WriteLine("{0} has entered in the Domain", Thread.CurrentThread.Name);
                Thread.Sleep(1000);    // Wait until it is safe to enter.  
                Console.WriteLine("{0} is leaving the Domain\r\n", Thread.CurrentThread.Name);
            }
            finally
            {
                //ReleaseMutex unblock other threads that are trying to gain ownership of the mutex.  
                mutex.ReleaseMutex();
            }
        }
    }
}
