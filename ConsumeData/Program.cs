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
             */



        }
    }
}
