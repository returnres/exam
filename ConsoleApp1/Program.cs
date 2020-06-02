using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static object lockobj = new object();
        static int n = 0;
        static int n1 = 0;
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Incr);
            Thread thread2 = new Thread(Incr);
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("n={0}", n1);

            Thread t = new Thread(() =>
            {
                Console.WriteLine(Thread.CurrentThread.Name);
                    Thread.Sleep(1000);
            });

            Thread t1 = new Thread(() =>
            {
                Console.WriteLine(Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    lock (lockobj)
                    {
                        n = n + 1;
                        Thread.Sleep(1);
                        Console.WriteLine("n={0}", n);
                    }
                   }
            });
            Thread t2 = new Thread(() =>
            {
                Console.WriteLine(Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    lock (lockobj)
                    {
                        n = n + 1;
                        Thread.Sleep(1);
                        Console.WriteLine("n={0}", n);
                    }
                }
            });

            t.IsBackground = true;//termina appena finisce la app
            t.Priority = ThreadPriority.AboveNormal;//determinata quantita di cpu destinata al thread ma non è deterministico
            t.Start();
            t1.Start();
            t2.Start();
            t.Join();//il codice si blocca e attende che t finisce
            Console.ReadLine();

        }

        static void Incr()
        {
            // Add one then subtract two.
            Interlocked.Increment(ref Program.n1);
            Thread.Sleep(1);

        }
    }
}
