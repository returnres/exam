﻿#define PIPPO

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace Freedom
{

    class Program
    {
        private static Mutex _Mutex = new Mutex(false, "DarksideErrorLog");

        // Use DllImport to import the Win32 MessageBox function.
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        static void Main(string[] args)
        {

            /*                      class
             * ArrayList
             * BitArray
             * Comparer confronyo ta stringhe casesensituve
             * Hshtable chiave valore
             * Queue
             * Sortedlist
             * Stack
             * 
             * 
             *                    interface
             * ICOllection
             * IComparer
             * IDictionary
             * IEnumerble
             * Ienumerator
             * IEqualityCompare
             * Ilist
             */
            //SortedList Rappresenta una raccolta di coppie chiave/valore,
            //ordinate in base alle chiavi e accessibili per chiave e per indice.
            SortedList mySL = new SortedList();
            mySL.Add("Third", "!");
            mySL.Add("Second", "World");
            mySL.Add("First", "Hello");


            //#if PIPPO  
            //#error DEBUG is defined  
            //#endif

            MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);

            #region altro

            testcons<Cicciolo> tc  = new testcons<Cicciolo>();

            CancellationToken myCancellationToken = new CancellationToken();
            Task mytask = Task.Run(() => { });
            Task<int> mytask1 = Task.Run(() => default(int));
            Task<int> mytask2 = Task.Run(() => default(int), myCancellationToken);

            mytask1.Wait();
            var i = mytask1.Result;
            Task[] tasks = new Task[3];
            try
            {
                Task.WaitAll(tasks);
                Task.WaitAny(tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
           

            var task = Task.Run(() => {
                Thread.Sleep(5000);
                return 5;
            }).ContinueWith((inputTask) => {
                Console.WriteLine(inputTask.Result * 2);
            }, TaskContinuationOptions.None);
            task.Wait();

            int conto = 0;
            Parallel.ForEach(
                Enumerable.Range(1, 100000),
                new ParallelOptions() { MaxDegreeOfParallelism = 1 },
            (elemento, parallelLoopState) => {
                Interlocked.Add(ref conto, elemento);
            });

            IEnumerable<int> elenco = new[] { 1, 2, 3, 4, };
            var somma = elenco.AsParallel().AsOrdered()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithDegreeOfParallelism(2)
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Sum();

            //il mutex è utile se non voglio che nessun altra applicazione sciva nel mio file
            try
            {
                _Mutex.WaitOne();
                File.AppendAllText("errors.log", "contenuto");
            }
            finally
            {
                _Mutex.ReleaseMutex();
            }

            dynamic sampleObject = new ExpandoObject();

            // Create a new event and initialize it with null.  
            sampleObject.sampleEvent = null;

            // Add an event handler.  
            sampleObject.sampleEvent += new EventHandler(SampleHandler);

            // Raise an event for testing purposes.  
            sampleObject.sampleEvent(sampleObject, new EventArgs());
           

            Publisher publisher = new Publisher();
            publisher.ProcessCompleted += (sender, e) => Console.WriteLine("ciao");
            publisher.ProcessCompleted += (parametro, parametro1) => { Console.WriteLine("ciao"); };
            publisher.ProcessCompleted += delegate (object parametro, MyEventArgs parametro1) { Console.WriteLine("ciao"); };
            publisher.StartProcess();
            Animal cat = new Animal();
            cat.Fai();
            ((IAnimal) cat).Fai();
        

            List<int> numbers = new List<int> { 1, 2, 3, 5, 7, 9 };
            using (var enumerator = numbers.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    Console.WriteLine(enumerator.Current);
            }

            using (var ss = new SecureString())
            {
                ss.AppendChar('a'); //magari il char viene da console
                ss.AppendChar('b'); //magari il char viene da console
                ss.AppendChar('c'); //magari il char viene da console
            }

            #endregion
        }

        static void SampleHandler(object sender, EventArgs e)
        {
            Console.WriteLine("SampleHandler for {0} event", sender);
        }
    }

    /// <summary>
    /// se metto un costruttore con parametri allora un eventuale constrain tnew() mi da errore di compilazioen
    /// perchè devo mettere obbligatoriamente cotruttore senza parametro
    /// </summary>
    internal class Cicciolo
    {
        public Cicciolo()
        {
                
        }
        public Cicciolo(int a, int b)
        {
                
        }
    }

    internal class Animal : IAnimal
    {
        public void Fai()
        {
            Console.WriteLine("fai");
        }

        void IAnimal.Fai()
        {
            Console.WriteLine("IAnimal fai");

        }
    }

    internal interface IAnimal
    {
        void Fai();
    }

    public class testcons<T> where T : new()
    {
        
    } 

    public class Publisher
    {
        public event EventHandler<MyEventArgs> ProcessCompleted = delegate { };

        public void StartProcess()
        {
            Console.WriteLine("Process Started!");
            // some code here..
            OnProcessCompleted(new MyEventArgs(){name="ciao"}); 
        }
       
        protected virtual void OnProcessCompleted(MyEventArgs e)
        {
            ProcessCompleted?.Invoke(this,e);
        }
    }

    public class MyEventArgs : EventArgs
    {
        public string name { get; set; }  
    }
}
