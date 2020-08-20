1create 
2manageprogramflow 
3debug 
4data 

	   
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
progra: stream ,serializzaiozne,linq

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

	***	consoleapp4:linq


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

CMPARE serve per sortare miei ogg in lista altrimenti list non sa come sortare

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
  


   CSharpCodeProvider

   [Conditional("DEBUG")]

   I/O bounded  async await
   CPU bounded  programmazione parallela/concorrente/multitasking

   interlocked
   monitor
   linq

-----------------------

             * TextReader
             * Stringreader/StringWriter (xmlreader)
             * StreamReader/StreamWriter
             * 
             * 
             *                  LETTURA SCRITTURA FILE
             * FileStream => Encoding usa File  che ritorna filestream
             * StreamReader/StreamWriter => no encoding usa FileInfo
             * 
             * 
             *                  LETTURA SCRITTURA XML
             * XmlDocument => lento facile non lettua sequanzaile
             * XmlReader => StringReader  veloce lettura sequnziale(XmlWriter StringWriter )
             * XDocument => linq to xml per leggere, editare
             * 
             * 
             *                  SERIALIZZAZIONE
             * XmlSerialize => StringWriter/StreamWriter, xmlignore, no priv, setter,serialize
             * DataContractSerializer  =>FileStream DataContract Datamember  obbligatori (wcf)
             * BynaryFormatter => FileStream, noserialize, priv si
             * DataConntractJsonSerializer => MemoryStream
             * javascriptserializer
             */

           




