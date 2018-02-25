using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.EnterpriseServices;

namespace WordEngineering
{
 ///<summary>UtilityCom</summary>
 [Transaction(TransactionOption.Required)]
 public class UtilityCom : ServicedComponent
 {
  ///<summary>DatabaseConnectionString</summary>
  public const string DatabaseConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;Integrated Security=SSPI;";

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
  }

  ///<summary>dbAccess</summary> 
  public void dbAccess(int pID1,int onOrder, int pID2, int inStock)
  {
   OleDbCommand oleDbCommand = null;
   OleDbConnection oleDbConnection = null;
   try
   {
    oleDbConnection = new OleDbConnection(DatabaseConnectionString);
    oleDbConnection.Open();
    oleDbCommand = new OleDbCommand
    (
     "UPDATE myProducts SET UnitsonOrder = " + onOrder + " WHERE productID = " + pID1, 
     oleDbConnection 
    );
    oleDbCommand.ExecuteNonQuery();
    oleDbCommand.CommandText = "UPDATE myProducts SET UnitsinStock = " + inStock + " WHERE productID = " + pID2;
    oleDbCommand.ExecuteNonQuery();
    ContextUtil.SetComplete();
   }
   catch ( Exception ex )
   {
    ContextUtil.SetAbort();
    System.Console.WriteLine("Exception: {0}", ex.Message);
   }
  }

 }
}