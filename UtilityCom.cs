using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Reflection;

//[assembly: AssemblyKeyFile("WordEngineering.snk")]
namespace WordEngineering
{
 ///<summary>UtilityCom</summary>
 ///<remarks>
 /// http://support.microsoft.com/default.aspx?scid=kb;en-us;816141 HOW TO: Use COM+ Transactions in a Visual C# .NET Component
 /// http://support.microsoft.com/default.aspx?scid=kb;EN-US;Q306296 HOW TO: Create a Serviced .NET Component in Visual C# .NET
 ///</remarks>
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
   dbAccess(1, 777, 2, 888);
   dbAccess(1, 5, 2, -20);
  }

  ///<summary>dbAccess</summary> 
  public static void dbAccess(int pID1,int onOrder, int pID2, int inStock)
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
   if ( oleDbConnection != null ) { oleDbConnection.Close(); }
  }

 }
}