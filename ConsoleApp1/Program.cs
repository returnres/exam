using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.CSharp;

namespace ConsoleApp1
{
    /// <summary>
    /// exam 
    /// C#6
    /// c#7
    /// C#8 (è supportato in .NET Core 3.x e .NET standard 2.1)
    /// </summary>
    class Program
    {
        private static object lockobj = new object();
        private static object lockMon = new object();
        static int n = 0;
        static int n1 = 0;
        static int n2 = 0;
        public delegate int myDelegate(string str);

        private static TraceSource mySource =
            new TraceSource("MyService");

        static void Main(string[] args)
        {

            #region opertori
            PippoTest pippoTest = new PippoTest(1);
            PippoTest pippoTest1 = new PippoTest(2);
            PippoTest pippoTest2 = new PippoTest(2);
            //implicit
            int testt = pippoTest2;

            //somma tra pippotest
            var rrrr = pippoTest1 + pippoTest;


            //explicit
            PippoTest testtt = (PippoTest)3;
            //somma tra int e pippotest
            var ressss = 3 + testtt;
            #endregion

            #region TraceSource
            /*
             * ConsoleTraceListener	Standard output or error stream
DelimitedListTraceListener	TextWriter
EventLogTraceListener	EventLog
EventSchemaTraceListener	XML-encoded, schema-compliant log file
TextWriterTraceListener	TextWriter
XmlWriterTraceListener	XML-encoded data to a TextWriter or stream.
             */
            //configuro da codice
            Stream outfile = File.Create("log.txt");
            TextWriterTraceListener textWriterTraceListener = new TextWriterTraceListener(outfile);
            TraceSource trace  = new TraceSource("myT",SourceLevels.All);

            trace.Listeners.Clear();
            trace.Listeners.Add(textWriterTraceListener);

            trace.TraceInformation("");
            trace.TraceEvent(TraceEventType.Critical,0,"");
            trace.TraceData(TraceEventType.Information,1,"");
            trace.Flush();
            trace.Close();

            //TraceSource configuro da appsetting
            mySource.TraceEvent(TraceEventType.Error, 1,
                "Error message.");
            mySource.TraceEvent(TraceEventType.Warning, 2,
                "Warning message.");

            #endregion

            #region BooleanSwitch
            var sw = new System.Diagnostics.BooleanSwitch("QueryLogger", "QueryLogger");
            if (sw.Enabled)
            {
                Console.WriteLine("Booleanswitchenabled");
            }
            #endregion

            #region TraceSwitch
            /*
             * SourceSwitch e TraceSwitch sono altri due tipi di switch che servono a controllare
             * la verbosità del trace e la “sorgente” del trace. 
             * Ovvero, se usi il tracing in maniera seria puoi, grazie al SourceSwitch, 
             * definire varie sorgenti di tracing e regolarle a tua discrezione con il TraceSwitch.
             */
            ////Define this in the web config
            TraceSwitch generalSwitch = new TraceSwitch("General",
                "Entire Application");

            string msgText = "1";

            // Write INFO type message, if switch is set to Verbose, type 4
            Trace.WriteIf(generalSwitch.TraceVerbose, msgText);

            msgText = "A2MC";

            // Write INFO type message, if switch is set to Verbose or Warning 4 0r 2
            Trace.WriteIf(generalSwitch.TraceWarning, msgText);

            // Write ERROR type message, if switch is set to Verbose, Warning, info or Error
            // 0 (off), 1 (error), 2 (warning), 3 (info), OR 4 (verbose)
            //If General switch in WEB CONFIG = 0 then it will not get into the if below
            if (generalSwitch.TraceError)
            {
                //Trace type, inthis case error will define how it will appear in the event log
                Trace.TraceError("Error");
                //Use your imagination to switch it on and off properly.
                //You can really imagine and apply.
            }
            #endregion

            #region EventLog
            if (!EventLog.SourceExists("Mysource"))
            {
                EventLog.CreateEventSource("MySource", "MyLog");
            }

            EventLog eventLog = new EventLog();
            eventLog.Source = "MySource";
            eventLog.WriteEntry("Ciao J0n");
            #endregion

            #region PerformanceCounter

            //string cat = "Contatore";
            //string catdsc = "Contatori vari";
            //string cpnt = "Click";
            //string contdesc = "Contatore Di Click";
            //if (!PerformanceCounterCategory.Exists(cat))
            //{
            //    PerformanceCounterCategory.Create(
            //        cat,
            //        catdsc,
            //        PerformanceCounterCategoryType.SingleInstance,
            //        new CounterCreationDataCollection(
            //            new[]
            //            {
            //                new CounterCreationData(
            //                    cpnt,
            //                    contdesc,
            //                    PerformanceCounterType.NumberOfItems32)
            //            })
            //    );
            //}
            //using (var counter = new PerformanceCounter(cat, catdsc, false))
            //    {
            //        counter.IncrementBy(-25);
            //    }


            #endregion

            MyMethodCond();

            string main = "Main";
            Trace.Write("trace");
            Debug.Write("debug");
            Debug.Assert(main == "Main");

            Ciccio c = new Ciccio();
            c.Prova();
            ICiccio s = (ICiccio)c;
            s.Prova();
            ICiccio1 s1 = (ICiccio1)c;
            s1.Prova();
            c.Nano();

            double xxx = 0.0;
            double yyy = 0.0;
            //var reszzzz = xxx / yyy;NAN
            IIbabbo babbo11 = new Figliolo();
            babbo11.MioMetodo();

            Mio("ciao");
            Mio("ciao", paperino: 1);
            Mio("ciao", pluto: true, paperino: 1);
            //Mio("ciao",pluto:false,1);ERRORE
            //Pippo pippos = new Pippo();
            //pippos.DoSOmething();


            #region  GENERZIONE DINAMICA CODICE
            /*
            
            //System.Reflection.Emit 
            //creazione , lancio e salvataggoi IL a runtime
            AssemblyName nameAss = new AssemblyName("DynamicAss");
            AssemblyBuilder assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(nameAss, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicMod", "DynamicAss.dll");
            //classe
            TypeBuilder tb = moduleBuilder.DefineType("HelloClass", TypeAttributes.Class | TypeAttributes.Public);
            //metodo
            MethodBuilder mb =
                tb.DefineMethod("PrintHello", MethodAttributes.Public, null, new Type[] { typeof(string) });
            ILGenerator myGenerator = mb.GetILGenerator();
            myGenerator.Emit(OpCodes.Ldstr, "Hello");
            myGenerator.Emit(OpCodes.Ldarg_1);
            MethodInfo concatMetod = typeof(String).GetMethod("Concat", new Type[] { typeof(string), typeof(string) });
            myGenerator.Emit(OpCodes.Call, concatMetod);
            MethodInfo wrMethodInfo = typeof(Console).GetMethod("Write", new Type[] { typeof(string) });
            myGenerator.Emit(OpCodes.Call, wrMethodInfo);
            myGenerator.Emit(OpCodes.Ret);

            //istanzio ogetto
            Type helloType = tb.CreateType();
            object helloobj = Activator.CreateInstance(helloType);
            MethodInfo hellMethodInfo = helloType.GetMethod("PrintHello", new[] { typeof(string) });

            //invoco metodo passando parametro
            hellMethodInfo.Invoke(helloobj, new object[] { "J0n" });

            //salvo dll
            assemblyBuilder.Save("DynamicAss.dll");
            #endregion

            HelloClass  helloClass = new HelloClass();
            helloClass.PrintHello("ciao");
            */


            //System.CodeDom 
            //creare  salvare e compilare  codice sorgente e runtime
            CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
            CodeNamespace codeNamespace = new CodeNamespace("CodeDOM");
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            //classe
            CodeTypeDeclaration myclass = new CodeTypeDeclaration("Pippo");

            //metodo
            CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
            codeMemberMethod.Name = "DoSOmething";
            codeMemberMethod.Attributes = MemberAttributes.Public;
            CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("Console"), "WriteLine", new CodePrimitiveExpression("Ciao Ciccio"));
            CodeMethodInvokeExpression codeMethodInvokeExpression1 = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("Console"), "ReadLine");
            codeCompileUnit.Namespaces.Add(codeNamespace);
            codeNamespace.Types.Add(myclass);
            myclass.Members.Add(codeMemberMethod);
            codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
            codeMemberMethod.Statements.Add(codeMethodInvokeExpression1);

            //salva codice sorgente
            CSharpCodeProvider provider = new CSharpCodeProvider();

            string pathSource = @"../../bin/Debug/Pippo.cs";
            string pathOut = @"../../bin/Debug/Pippos.dll";

            //string pathSource = @"../../autogenerated/Pippo.cs";
            //string pathOut = @"../../autogenerated/Pippos.exe";
            using (StreamWriter stream = new StreamWriter(pathSource, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(stream, "   ");
                provider.GenerateCodeFromCompileUnit(codeCompileUnit, tw, new CodeGeneratorOptions());
                tw.Close();
            }

            CompileCSharpCode(pathSource, pathOut);

            #endregion

            #region reflection
            //REFLECTION System.Assembly
            //assembly (IL codice compilato) exe/dll
            //carico in memoria assembly

            //PLUGIN
            //caricamento dinamico  assembly da path
            var assm = Assembly.LoadFrom(@"C:\Users\rob\source\repos\Exam\Plugin1\bin\Debug\Plugin1.dll");
            Type[] tips = assm.GetTypes();
            foreach (var item in tips)
            {
                //var objectType = Type.GetType(objectToInstantiate);
                Type objectType = assm.GetType(item.FullName);
                var instantiatedObject = Activator.CreateInstance(objectType);
            }
            //PLUGIN

            //carica solo metadati dei tipi SENZA instanziare i tipi
            var ass2 = Assembly.ReflectionOnlyLoadFrom("ClassLibrary1.dll");

            //var instantiatedObject1 = Activator.CreateInstance(typeof(Tipo));
            //var instantiatedObject2 = Activator.CreateInstance<Tipo>;
            string sss = new String(new char[3]);
            //assembly attualmente in esecuzione
            var ass = Assembly.GetExecutingAssembly();
            var ass1 = Assembly.GetAssembly(typeof(string));
            var ass12 = Assembly.GetAssembly(sss.GetType());
            var mytype = ass.GetType("ConsoleApp1.Program");
            var fullname = ass.FullName;
            var nome = mytype.Name;
            var mytypeNamespace = mytype.Namespace;

            var typeinfo = typeof(MiaclasseProva).GetTypeInfo();
            var name = typeinfo.FullName;

            //ottengo tipo classe
            MiaclasseProva miaclasseProva = new MiaclasseProva();

            var typeinfo1 = miaclasseProva.GetType().GetTypeInfo();

            //ottengo interafccia
            var interfaccie = typeinfo1.GetInterfaces();

            //ottengo metodi e parametri
            MethodInfo[] metodi = typeinfo1.GetMethods(BindingFlags.Instance);
            foreach (var metodo in metodi)
            {
                var pi = metodo.GetParameters();
                foreach (var parinfo in pi)
                {
                    Console.WriteLine($" parametro : {parinfo.ParameterType.Name}");
                }
            }

            //ottengo membri
            MemberInfo[] membri = typeinfo1.GetMembers(BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var mambro in membri)
            {
                var mi = mambro.MemberType;
            }
            #endregion

            #region IComparable eccezioni
            //confrontare oggetti
            ComparableMoto comparableMoto = new ComparableMoto();
            comparableMoto.Targa = "pippo";
            ComparableMoto comparableMoto1 = new ComparableMoto();
            comparableMoto1.Targa = "pluto";

            int ord = comparableMoto.CompareTo(comparableMoto1);

            List<ComparableMoto> comparableMotos = new List<ComparableMoto>();
            comparableMotos.Add(new ComparableMoto() { Targa = "A" });
            comparableMotos.Add(new ComparableMoto() { Targa = "B" });
            comparableMotos.Add(new ComparableMoto() { Targa = "C" });
            comparableMotos.Sort();

            //AppDomain.CurrentDomain.UnhandledException += OnUnhandleException;
            //ManageException manage = new ManageException();
            //manage.GeneroEccezioneNonGestita();
            #endregion

            #region ref out static class
            Figlio figlio1 = new Figlio();
            Figlio figlio = new Figlio("as");
            Macchina macchina = new Macchina("320", "bmw");
            macchina.Parti();
            SmartPhone.Battery battery = new SmartPhone.Battery();
            ConstructorStaticClass constructorStaticClass = new ConstructorStaticClass();
            ConstructorStaticClass miaclasse2 = new ConstructorStaticClass();
            Console.WriteLine(ConstructorStaticClass.contatore);
            ClassTest1 babbo = new ClassTest1();
            var testref = 0;
            double resp2;
            double resp3;

            //ref
            babbo.cambiaNumero(ref testref);
            //out scarta valore resp3
            babbo.potenza(2, out resp2, out _);
            var media = babbo.CalcolaMedia(18, 27, 27, 27);
            babbo.ParametriNome(nome: "rob", anni: 43);

            //c#7
            //double xresp2;
            //double xresp3;
            //babbo.potenza1(2,  xresp2,  xresp3);

            int x1 = 0;
            //block
            if (x1 == 0)//expression
            {
                //codice 
            }

            int? nullable = null;
            var cliente = new { nome = "rob" };
            Mesi giorno = Mesi.febbraio;
            int mese = (int)Mesi.febbraio;
            Mesi mese1 = (Mesi)1;
            Pippolo pippo;
            pippo.a = 1;


            #endregion

            #region cast box unbox 
            //cast
            int i = 123;
            object box = i;
            int n = (int)box;//unbox
            dynamic dd = 1;
            int inc = dd + 1;
            var ssss = Convert.ToString(1);
            dynamic dy = new ExpandoObject();
            #endregion

            #region type

            //OTTENGO IL TIPO
            //GetType
            var ti = typeof(string).GetTypeInfo();
            Type ts = Type.GetType("System.String");//se si trova in Mscorelib.dll

            TestClass tc = new TestClass();
            Type tipo = tc.GetType();

            //typeof
            Type type = typeof(Type);

            //CONTROLLO IL TIPO
            //is tipo | as tipo
            string obj = "";
            if (obj is string)
            {
                //cast ()
                string a = (string)obj;//se obj non è compatibile con string cast exception
                string a1 = obj as string;//se obj non compatobole con string ritorna null
            }


            #endregion

            #region array
            Console.WriteLine($"metodo {nameof(Main)}");

            /*
           * stack => vet
           * heap 1
           * heap 2
           * heap 3
           * 
          ARRAY MULTID:
          -array rettangoli
          1234
          1234

          -array jagged (irregolare)
          123456
          234
          2343434343
           */

            //array
            int[] vet = new[] { 1, 2, 3 };

            //array regolari matrici
            int[,] matrix = new int[3, 4];
            matrix[0, 0] = 1;

            int[,] matrix1 =
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8}
            };

            //jagged array irregolari
            int[][] jagged = new int[3][];
            jagged[0] = new int[2] { 1, 2 };
            jagged[1] = new int[4] { 1, 2, 3, 4 };
            #endregion

            #region c7 PATTERN MATCHING
            /*
             * type => espr is tipo v
             * const => espr is constante
             * var
             * 
             * */

            //type
            object obj1 = "";
            if (obj1 is string str)
                Console.WriteLine(str);

            //invece che

            if (obj1 is string)
            {
                string str1 = (string)obj;
                Console.WriteLine(str1);
            }

            //const
            Mesi giornomese = Mesi.febbraio;
            bool feb = giornomese is Mesi.febbraio;//true se giorno di febbraio

            #endregion

            #region  Event
            //Event
            Pub pub = new Pub();
            pub.OnChange += () => Console.WriteLine("lambda");
            pub.OnChange += delegate { Console.WriteLine("delegate"); };
            pub.Raise();

            Pub1 p1 = new Pub1();
            p1.OnChange += () => Console.WriteLine("lambda");
            p1.Raise();

            Pub2 p2 = new Pub2();
            p2.CreateAndRaise();

            Car car = new Car();
            CarMonitor cm = new CarMonitor(car);

            car.Decelerate();
            car.Decelerate();

            #endregion

            #region Expression Tree
            //Expression Tree
            TestExpres testExpres = new TestExpres();
            Func<int, bool> isPari = testExpres.exp1.Compile();
            if (isPari(4))
                Console.WriteLine("pari");
            #endregion

            #region delegate 

            //assegno lambda (anonyous method)
            myDelegate myd = (x) => 1;
            Console.WriteLine(myd("0ciao"));

            //assegno metodo
            myDelegate myd1 = MethodForDel;
            Console.WriteLine(myd1("1ciao"));

            //assegno delegate 
            myDelegate myd2 = delegate { return 1; };
            Console.WriteLine(myd2("2ciao"));
            #endregion

            #region VARIANZA 
            //COVARIANZA OUT
            //ti faccio assegnare sia metodo che ritorna stringa
            //ma anche metodo che torna object perchè stringa deriva
            //da object (è piu specifica)
            Del d1 = new Del();
            Cov<string> cov = d1.Cov1;
            Cov<object> cov1 = d1.Cov2;
            Cov<object> cov3 = d1.Cov2;
            //Cov<string> cov2 = d1.Cov2;//no object non deriva da stringa 

            //CONTROVARIANZA IN
            //ti faccio assegnare sia metodo che prende ingresso
            //classe piu generica object  ma ache classe piu specifica string
            Con<object> con = d1.Con1;
            Con<string> con3 = d1.Con1;
            Con<string> con1 = d1.Con2;
            //Con<object> con2 = d1.Con2;//no

            var res3 = d1.MyCon(Convert.ToInt32, "5");
            var res4 = d1.MyCon1(Convert.ToInt32, "5");
            var res5 = d1.MyConGen(Convert.ToInt32, "5");
            var res6 = d1.MyConGen(Convert.ToBoolean, 0);
            var res7 = d1.MyConGen(Convert.ToString, 5);
            Console.WriteLine(res3);
            Console.WriteLine(res4);

            d1.UseMulticast();

            #endregion


            #region generics
            Task tx1 = Task.Run(() =>
            {
                Console.WriteLine("ciao");
            });
            Task[] tasksarr = new Task[1];
            tasksarr[0] = tx1;

            Gen1<ConstructorStaticClass, Task> gen1 = new Gen1<ConstructorStaticClass, Task>();

            if (gen1.GetDefault(tx1) != null)
            {
                gen1.ContinueTaskOrDefault(tx1, Console.WriteLine);
                Console.WriteLine("start task generic");
            }

            var mytaskList = gen1.ContinueTasksOrDefault(tasksarr, Console.WriteLine);


            if (mytaskList != null)
                Console.WriteLine("starts tasks generic");

          
            Gen2<Constructor> gen2 = new Gen2<Constructor>();
            Gen2Figlia<Constructor> gen2figlia = new Gen2Figlia<Constructor>();
            Gen1Figlia<Constructor, Task> tesFiglia = new Gen1Figlia<Constructor, Task>();

            //variabile statico è diverso per tipo
            TestStaticGen<string>.Status = "rob";
            TestStaticGen<int>.Status = 1;
            Console.WriteLine(TestStaticGen<string>.Status);
            Console.WriteLine(TestStaticGen<int>.Status);

            TestStatic.Status = "ciao";
            TestStatic.Status = "ciau";
            Console.WriteLine(TestStatic.Status);
            //

            Generica<string, int, DateTime, bool> gen = new Generica<string, int, DateTime, bool>();


            List<Lista<string>> genericiInnestati = new List<Lista<string>>();
            genericiInnestati.Add(new Lista<string>(5));
            genericiInnestati[0][0] = "ciao";
            genericiInnestati[0][1] = "come ";
            genericiInnestati[0][2] = "stai";
            genericiInnestati[0][3] = "rob";
            genericiInnestati[0][4] = "?";

            foreach (var item in genericiInnestati)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(item[j]);
                }
            }

            Lista<string> lista = new Lista<string>(5);
            lista[0] = "";
            #endregion

            #region programmazione parallela
            //PLINQ solo linq to onject NO linq to sql
            //prende base dati la divide in segmenti e fa query in parallelo su questi segmentie  pio unisce iriaultati
            // se i lavori nonn sono complessi e la base dati è minima la programmazione parallela non da buoni risultati
            var qy = from num in Enumerable.Range(1, 8).AsParallel()
                     select Math.Pow(2, num);

            //stampa da 1 a 9 (9 numeri NON in ordine)
            //iterazioni indipendenti l'una dall altra quindi per tutti e due ordine non è sequnziale
            Parallel.For(1, 10, g => Console.WriteLine("g-{0}", g));
            var result = Parallel.For(0, 50, (g, parallelLoopState) =>
             {
                 if (g > 5)
                 {
                     parallelLoopState.Break();
                 }
             });

            if (!result.IsCompleted)
            {
                var o = result.LowestBreakIteration;
            }

            //non è sequnziale
            List<string> list = new List<string>() { "ciao", "come", "va?" };
            Parallel.ForEach(list, word => Console.WriteLine("{0}", word, word.Length));
            #endregion

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

            #region task tpl

            //manca continuewhenall
            //continuewheany
            //task figli innestati

            //Task.WaitAll blocks the current thread until everything has completed.
            //Task.WhenAll returns a task which represents the action of waiting until everything has completed.
            //That means that from an async method, you can use:
            // A DIFFERENZA DI WAIT ALL TORNA SOLO LA PRIMA ECCEZIONE NON AGGREGATE
            Task[] tasksArray = new Task[3];
            tasksArray[0] = Task.Run(() => { Thread.Sleep(4000); });
            tasksArray[1] = Task.Run(() => { Thread.Sleep(4000); });
            tasksArray[2] = Task.Run(() => { Thread.Sleep(4000); });
            var ry = MyAsyncWhenAll(tasksArray);
            Console.WriteLine("tornato da MyAsyncWhenAll");


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
                   for (int d = 0; d < 2; d++)
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
            #endregion tpl

            #region thread
            for (int w = 0; w < 5; w++)
            {
                ThreadPool.QueueUserWorkItem(LongOperation);
            }

          
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
                for (int z = 0; z < 10; z++)
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
                for (int x = 0; x < 10; x++)
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

        static int MethodForDel(string ss)
        {
            return 1;
        }
        public static void Mio(string pippo, bool pluto = false, int paperino = 1)
        {

        }
        public static bool CompileCSharpCode(string sourceFile, string exeFile)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();

            // Build the parameters for source compilation.
            CompilerParameters cp = new CompilerParameters();

            // Add an assembly reference.
            cp.ReferencedAssemblies.Add("System.dll");

            // Generate an executable instead of
            // a class library.
            //cp.GenerateExecutable = true;
            cp.GenerateExecutable = false;

            // Set the assembly file name to generate.
            cp.OutputAssembly = exeFile;

            // Save the assembly as a physical file.
            cp.GenerateInMemory = false;

            // Invoke compilation.
            CompilerResults cr = provider.CompileAssemblyFromFile(cp, sourceFile);

            if (cr.Errors.Count > 0)
            {
                // Display compilation errors.
                Console.WriteLine("Errors building {0} into {1}",
                    sourceFile, cr.PathToAssembly);
                foreach (CompilerError ce in cr.Errors)
                {
                    Console.WriteLine("  {0}", ce.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Source {0} built into {1} successfully.",
                    sourceFile, cr.PathToAssembly);
            }

            // Return the results of compilation.
            if (cr.Errors.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [Conditional("DEBUG")]
        static void MyMethodCond()
        {
            Console.WriteLine("CHIAMATO PERCHE SONO IN DEBUGGGG!!!!!!!!!!!!!!!!");
        }


        static async Task MyAsyncWhenAll(Task[] tasks)
        {
            await Task.WhenAll(tasks);
            Console.WriteLine("USCITO DAL WAIT");
        }


        /// <summary>
        /// in questi casi uso async/await + Task.run perchè ho sia I/O bounded che CPU bounded
        /// se non avessi cpu bounded terrei core impegnati nel non fare nulla ma solo aspettare che le operazioni
        /// di I/O download dell immagine siano completate
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static Task<byte[]> DownloadImage(string url)
        {
            var client = new HttpClient();
            return client.GetByteArrayAsync(url);
        }

        static async Task<byte[]> BlurImage(string imagePath)
        {
            return await Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    return memoryStream.ToArray();
                }
            });
        }

        static async Task SaveImage(byte[] bytes, string imagePath)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }
        #region private
        private static void OnUnhandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("eccezione non gstita");
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
        #endregion
    }

    //Expression Tree
    //il codice rappresentato da un albero di espressione
    //puo essere compilato in codice IL ed eseguito
    //oppure nel caso di un db convertitot in SQL
    public class TestExpres
    {
        //Expression Tree
        //il codice rappresentato da un albero di espressione
        //puo essere compilato in codice IL ed eseguito
        //oppure nel caso di un db convertitot in SQL
        public Expression<Func<int, bool>> exp1 = x => x % 2 == 0;

    }


}
