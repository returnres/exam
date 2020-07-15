using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace Trans
{
    class Program
    {
        static void Main(string[] args)
        {

            #region  Single Transactions
            try
            {
               
                using (var connection = new SqlConnection(""))
                using (var cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;

                    transaction = connection.BeginTransaction( IsolationLevel.ReadCommitted);
                    command.Connection = connection;
                    command.Transaction = transaction;

                    try
                    {
                        command.CommandText =
                            "Insert into Region (RegionID, RegionDescription) VALUES (100, 'Description')";
                        command.ExecuteNonQuery();
                        command.CommandText =
                            "Insert into Region (RegionID, RegionDescription) VALUES (101, 'Description')";
                        command.ExecuteNonQuery();

                        // Attempt to commit the transaction.
                        transaction.Commit();
                        Console.WriteLine("Both records are written to database.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);

                        // Attempt to roll back the transaction. 
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            // This catch block will handle any errors that may have occurred 
                            // on the server that would cause the rollback to fail, such as 
                            // a closed connection.
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }

            #endregion

            #region  Distributed Transactions
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                string connString = ConfigurationManager.ConnectionStrings["northwind"].ConnectionString;
                //la connessione si unirà automaticamente alla transazione
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        //nota che non devo valorizzare la proprietà .Transaction
                        cmd.CommandText = "UPDATE Customers SET CompanyName='My Company' WHERE CustomerID='ALFKI'";
                        cmd.ExecuteNonQuery();
                    }

                    //ovviamente chiamo il .Complete prima che la Connection venga chiusa
                    transaction.Complete();
                }
            }
            #endregion

        }
    }
}
