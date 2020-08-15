	   ***	class library1 :
class1  : oop ref, struct
class2 : overload operatori
generics
exception
delegate
event

	  ***	consoleapp1:
miaclasse : prova interface
utility : extension method
program : operatori,tracesource,booleanswitch,trcaeswitch,eventlog,performanccounter,gene dinamica,
	reflect,icomparable,ref,cast,type,array,patternmatchc#7,event,exptree,del,generics,tpl,async


	***	consoleapp2:
progra: stream serializzaiozne

	***	consoleapp3:
progr:stringformat e cas

	***	consoleapp4:
progr:iserializable

	***	consumedata : adonet

	***	crypto

	***	debug

	***	freedom
	***	mutex
	***	semafore
	***	strong
	***	transaction


	---------------------------------------------------------------------
	PER CAPIRE PRENDI SPUNTO DAL CODICE MICROSOFT

checked
protected
equals 
abstarct interface 
override
is , as
compare
constraint
delegate
event
extension
interace

TraceSource configuro via code o via appseting
  1creo
  2creolistner
  3cancello defaultlistnwer
  4aggiungo mio listner
  5TraceEvent(traceventtype)

BooleanSwitch
SourceSwitch
TraceSwitch
		questi li passo al trace  Trace.writeif(switch)


  #region EventLog
            if (!EventLog.SourceExists("Mysource"))
            {
                EventLog.CreateEventSource("MySource", "MyLog");
            }

            EventLog eventLog = new EventLog();
            eventLog.Source = "MySource";
            eventLog.WriteEntry("Ciao J0n");

PerformanceCounter

emit generazione dinamica codice

compare serve per sortare miei ogg in lista altrimenti list non sa come sortare

dynamic
cast
typeof //OTTENGO IL TIPO
if (obj is string)//CONTROLLO IL TIPO
obj as string;////OTTENGO IL TIPO se obj non compatobole con string ritorna null

  

  //prende base dati la divide in segmenti e fa query in parallelo su questi segmentie  pio unisce iriaultati
            // se i lavori nonn sono complessi e la base dati è minima la programmazione parallela non da buoni risultati
            var qy = from num in Enumerable.Range(1, 8).AsParallel()
                     select Math.Pow(2, num);


     //iterazioni indipendenti l'una dall altra quindi per tutti e due ordine non è sequnziale
            Parallel.For(1, 10, g => Console.WriteLine("g-{0}", g));
            var result = Parallel.For(0, 50, (g, parallelLoopState) =>
             {
                 if (g > 5)
                 {
                     parallelLoopState.Break();
                 }
             });

    //non è sequnziale
            List<string> list = new List<string>() { "ciao", "come", "va?" };
            Parallel.ForEach(list, word => Console.WriteLine("{0}", word, word.Length));

Task tx1 = Task.Run(() =>
            {
                Console.WriteLine("ciao");
            });
            Task[] tasksarr = new Task[1];
            tasksarr[0] = tx1;

   //all block
      //.Wait() 
       //.Result 
        //.GetAwaiter()
       //.GetResult()
     //continuewheany
   //Task.WaitAll blocks the current thread until everything has completed.
   //Task.WhenAll returns a task which represents the action of waiting until everything has completed.
   //That means that from an async method, you can use:
            // A DIFFERENZA DI WAIT ALL TORNA SOLO LA PRIMA ECCEZIONE NON AGGREGATE

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

  List<Task> tasks = new List<Task>();
            for (int r = 0; r < 5; r++)
            {
                int number = r;
                tasks.Add(
                   Task.Run(() =>
                    {
                        Console.WriteLine(number);
                        Thread.Sleep(1000);
                    }));
            }

            //Task.WaitAll(tasks.ToArray());
            Task.WaitAny(tasks.ToArray());

           ThrowIfCancellationRequested


 static async Task MyAsyncWhenAll(Task[] tasks)
        {
            await Task.WhenAll(tasks);
   


   CSharpCodeProvider

   [Conditional("DEBUG")]

   I/O bounded  async aw ait
   CPU bounded   task

   interlocked
   mnitor

   sono arrivato a consoleapp2


