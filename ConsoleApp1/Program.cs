using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static object lockobj = new object();
        private static object lockMon = new object();
        static int n = 0;
        static int n1 = 0;
        static int n2 = 0;

        static void Main(string[] args)
        {

            //programmazione parallela

            #region async await

            var res1 = DoCurlAsync();

            //net 4.5
            //await salva lo stato del contesto del caller e quando il metodo await si conclude
            //il contesto ritorna allo stato del chimante che puo eseguire il codice successivo
            //questo nel caso di ui permette di modiicare la ui che pè proprieta nella app gui del thread
            //della ui e viene eseguito nel ui syncronization context (dispatchercontext nella ui)
            //DAL C#7 TUTTO PUO ESSERE AWAITABLE ED ESEGUITO AINCRONO E NON  SOLO ISTANZA DI TASK
            //QUALSIAIS OGGETTO CHE ESPONE METODO GETAWAITER INFATTI LA CLASSE TASK ESPONE METODO GETAWAITER
            //await non puo essere usato su unsafe, lock, main
            MetodoAsyn();
            Console.WriteLine("codice prima fine await)");

            Task<string> res = GetAsync();

            List<string> files = new List<string>() { "pippo.txt", "pippo1.txt" };
            files.ForEach(async file => await ReadFileAsync(file));

            //all block
            //.Wait() 
            //.Result 
            //.GetAwaiter()
            //.GetResult()

            #endregion

            #region task

            //manca continuewhenall
            //continuewheany
            //task figli innestati
            Task tx = Task.Run(() =>
            {
                Thread.Sleep(1000);
            });
            Task ty = Task.Run(() =>
            {
                Thread.Sleep(1000);
            });


            Task taskall = Task.WhenAll((new Task[] { tx, ty }));
            //Task taskall = Task.WhenAny((new Task[] { t1, t2 }));
            taskall.Wait();

            Task<String> webtask = Task.Run(() =>
            {
                string url = "http://www.google.it";
                WebClient wc = new WebClient();
                return wc.DownloadString(url);
            });

            Task<String> headhtml = webtask.ContinueWith<string>(x =>
           {
               var res2 = x.Result;
                //fai qualcosa con res
                return res2;
           });

            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task cancellabletask = null;
            try
            {
                cancellabletask = Task.Run(() =>
               {
                   token.ThrowIfCancellationRequested();
                   for (int i = 0; i < 2; i++)
                   {
                       Thread.Sleep(100);
                       if (token.IsCancellationRequested)
                       {
                           //eseguo altre cose
                           token.ThrowIfCancellationRequested();
                       }
                   }
               }, token);

                tokenSource.CancelAfter(1000);
                cancellabletask.Wait();
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException is OperationCanceledException)
                {
                    Console.WriteLine("OperationCanceledException£");

                }
                if (cancellabletask != null && (cancellabletask.IsCanceled && cancellabletask.Status == TaskStatus.Canceled))
                {
                    Console.WriteLine(aggregateException.ToString());
                }
            }


            Task<String> task = Task.Run(() =>
            {
                string url = "http://www.google.it";
                WebClient wc = new WebClient();
                return wc.DownloadString(url);
            });
            var htmlUrl = task.Result;//blocco

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                int number = i;
                tasks.Add(
                   Task.Run(() =>
                    {
                        Console.WriteLine(number);
                        Thread.Sleep(1000);
                    }));
            }

            //Task.WaitAll(tasks.ToArray());
            Task.WaitAny(tasks.ToArray());
            Console.WriteLine("all task complete");

            Task longtask = Task.Run(() =>
             {
                 Thread.Sleep(1000);
             });
            longtask.Wait(1000);


            Task verylong = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
            }, TaskCreationOptions.LongRunning);
            #endregion

            #region thread
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(LongOperation);
            }

            //manca il mutex
            Thread threada = new Thread(IncrMon);
            Thread threadb = new Thread(IncrMon);
            threada.Start();
            threadb.Start();
            threada.Join();
            threadb.Join();
            Console.WriteLine("n={0}", n2);

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

            #endregion

            Console.ReadLine();

        }

        private static async void CallDoCurlAsync()
        {
            var res = await DoCurlAsync();//asyn 
            //var resr = DoCurlAsync().Result; //sync
        }

        public static async Task<string> DoCurlAsync()
        {
            using (var httpClient = new HttpClient())
            using (var httpResonse = await httpClient.GetAsync("https://www.google.com"))
            {
                return await httpResonse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        ///  leggo file asincrono
        /// </summary>
        public static async Task ReadFileAsync(string path)
        {
            await Task.Delay(100);
        }
        public static async void MetodoAsyn()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });
            Console.WriteLine("codice dopo fine await)");
        }

        public static async Task<string> GetAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            return "ciao";
        }

        private static void LongOperation(object state)
        {
            Thread.Sleep(1000);
            Console.WriteLine(state);
        }

        static void Incr()
        {
            // Add one then subtract two.
            Interlocked.Increment(ref Program.n1);
            Thread.Sleep(1);

        }

        static void IncrMon()
        {
            bool locktake = false;
            Monitor.TryEnter(lockMon, 1000, ref locktake);
            if (locktake)
            {
                Thread.Sleep(2000);//se scommento non prendo ock perchè supera il secondo di attesa
                try
                {
                    n2++;
                }
                finally
                {
                    Monitor.Exit(lockMon);
                }
            }
            else
            {
                Console.WriteLine("lock non acquisito");
            }
        }
    }
}
