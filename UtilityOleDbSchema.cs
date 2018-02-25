using System;
using System.Data;
using System.Data.OleDb;
using System.Web;

namespace WordEngineering
{
 public class UtilityOleDbSchema
 {
  public static string[] DatabaseConnectionString = 
  {  
   "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;Integrated Security=SSPI;",
   @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Bible\HTMLBible_-_KingJamesVersion.mdb;User Id=admin;Password=;"
  };
  public static void Main(string[] argv)
  {
   DataTable dataTable = null;
   HttpContext httpContext = HttpContext.Current;
   OleDbConnection oleDbConnection = null;
   try
   {
    oleDbConnection = new OleDbConnection(DatabaseConnectionString[0]);
    oleDbConnection.Open();
    dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, new object[] {null});
    foreach(DataRow dataRow in dataTable.Rows)
    {
     System.Console.WriteLine(dataRow["CATALOG_NAME"]);
    }
    dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {"WordEngineering", null, null, null});
    foreach(DataRow dataRow in dataTable.Rows)
    {
     System.Console.WriteLine(dataRow["TABLE_NAME"]);
    }
    dataTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] {"WordEngineering", null, "Contact", null});
    foreach(DataRow dataRow in dataTable.Rows)
    {
     System.Console.WriteLine(dataRow["COLUMN_NAME"]);
    }
   }
   catch (Exception exception)
   {
    System.Console.WriteLine("Exception Message: {0}", exception.Message);
   }
  }
 }
}