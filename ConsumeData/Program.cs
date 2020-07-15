using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeData
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * ADO.NET è il componente per l'accesso ai dati di Microsoft .NET Framework

Il termine ADO.NET designa un insieme di classi che fornisce tutte le funzionalità 
necessarie per l’accesso ai dati e la loro elaborazione in memoria.

ADO.NET è composto da due componenti fondamentali il Data Provider ( che mantiene la 
connessione con la sorgente dati fisica) e il DataSet (che rappresenta i dati 
attuali, ovvero i dati su cui si sta lavorando). 
             * 
             * Il modello implementato da ADO.NET è suddiviso in due parti:
1) Oggetti Connessi consentono di eseguire le operazioni di lettura e aggiornamento 
   sul  DataBase
2) Oggetti Disconnessi consentono la memorizzazione e l’elaborazione dei dati 
   nella  memoria del programma client.
             * 
             *                    DataReader
             * 
Il DataReader consente di accedere al result set una riga alla volta e non fornisce la
possibilità di ritornare alla riga precedente. In quegli scenari in cui si desidera 
svolgere determinate elaborazioni sui dati, l’uso di un DataReader è probabilmente 
insufficiente. In questi casi è appropriato l’uso di un DataAdapter e di uno o più 
oggetti disconnessi.
             * 
             * 
             * DataSet e DataTable sono due classi che si trovano dentro il namespace System.Data. 
             * Sono dei contenitori di dati in memoria. 
             *                                              DataTable
             * DataTable è una collezione di:
•	DataColumn (.Columns è una DataColumnCollection)
•	DataRow (.Rows è una DataRowCollection)
•	Constraint (.Constraints è una ConstraintCollection, contengono i constraints :)
Di solito non aggiungi tu manualmente dei constraints, 
ma lasci che vengano aggiunti da altre operazioni. 
Ad esempio, un PrimaryKeyConstraint viene aggiunto alla collezione quando definisci la .
PrimaryKey di un DataTable. Un ForeignKeyConstraint e un PrimaryKeyConstraint 
invece vengono aggiunti (su 2 DataTable diverse) quando aggiungi una DataRelation alle .Relations di un DataSet.
DataTable è un oggetto disconnesso, vuol dire che può essere costruito e riempito 
anche senza una connessione ad un qualsiasi DB.
Infatti il metodo canonico per caricarci dentro dei dati è XML.

            
                                                            DataSet
            Il DataSet invece serve a raggruppare logicamente le DataTables, quindi consiste di due principali collezioni di:
•	DataTable (.Tables è una DataTableCollection)
•	DataRelation (.Relations è una DataRelationCollection, serve per creare delle relazioni 1-a-1 o 1-a-molti tra due tabelle)
Se vuoi fare qualcosa di significativo con i dati in memoria bisogna che leghi logicamente più DataTables in un DataSet. Infatti il DataSet ha due proprietà chiave:
•	.Tables è una DataTableCollection
•	.Relations è una DataRelationCollection

             * 
             * 
            
            il DataAdapter si trova al centro del
                modello di elaborazione disconnessa dei dati implementato in ADO.NET.Questo prevede
            che i dati da elaborare siano completamente caricati in memoria, in uno o più
            DataTable.Il vantaggio di un simile approccio risiede nel fatto che l’elaborazione
                può essere eseguita off - line e cioè senza tenere aperta una connessione e occupare
                risorse del database. Successivamente, se previsto dall’applicazione, sarà possibile
            aprire una nuova connesione ed inviare le modifiche al database.

            Ridotto alla sostanza, comunque, il suo duplice ruolo è quello di:
   ° ottenere i dati dal database e caricarli in uno o più DataTable;
   ° inviare al database le eventuali modifiche effettuate in uno o più DataTable.


            db => dataadapter => datatable(disconnesso) il datatable è memorizzato in un Dataset
     */
        }
    }
}
