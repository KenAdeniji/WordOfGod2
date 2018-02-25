using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityExcel</summary>
 ///<remarks>
 /// http://support.microsoft.com/default.aspx?scid=kb;en-us;311194
 ///  HOW TO: Use ASP.NET to Query and Display Database Data in Excel by Using Visual C# .NET
 /// http://support.microsoft.com/default.aspx?scid=kb;EN-US;Q306572
 ///  How to query and display excel data by using ASP.NET, ADO.NET, and Visual C# .NET
 ///</remarks> 
 public class UtilityExcel
 {
  ///<summary>ExcelConnectionType</summary>
  public enum ExcelConnectionType
  {
   ///<summary>OLEDB</summary>
   OLEDB = 0,
   ///<summary>ODBC</summary>
   ODBC = 1
  }

  ///<summary>DatabaseConnectionString</summary>
  public const string DatabaseConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;Integrated Security=SSPI;";

  ///<summary>ExcelConnectionString</summary>
  public static readonly string[] ExcelConnectionString = 
  {
   "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"",
   "Driver={Microsoft Excel Driver (*.xls)};DriverId=790;Dbq={0};DefaultDir={1};"
  };

  ///<summary>FilenameConfigurationWordEngineering</summary>
  public const string FilenameConfigurationWordEngineering = @"d:\WordOfGod\WordEngineering.config";

  /// <summary>The XPath database connection string.</summary>
  public const string XPathDatabaseConnectionString = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   string exceptionMessage;
   string sql = "SELECT * FROM Contact";
   string filenameExcel = @"D:\WordOfGod\Contact.xls";
   if (argv.Length > 0) { sql = argv[0]; }
   if (argv.Length > 1) { filenameExcel = argv[1]; }
   SQLExcel
   (
    DatabaseConnectionString,
    sql,
    filenameExcel,
    out exceptionMessage
   );
   ExcelOpen(@"D:\org\comforter\NET\WordEngineering\Excel\WordEngineeringAddress.xls",ExcelConnectionType.OLEDB, out exceptionMessage);
  }

  ///<summary>ExcelOpen</summary>
  public static void ExcelOpen
  (
   string filenameExcel,
   ExcelConnectionType excelConnectionType,
   out DataSet dataSet,
   out string exceptionMessage
  )
  {
   string excelConnectionString = null;
   HttpContext httpContext = HttpContext.Current;
   OleDbDataAdapter oleDbDataAdapter = null;
   OleDbCommand oleDbCommand = null;
   OleDbConnection oleDbConnection = null;
   dataSet = null;
   exceptionMessage = null;
   switch ( excelConnectionType )
   {
    case ExcelConnectionType.OLEDB:
     excelConnectionString = String.Format(ExcelConnectionString[(int)excelConnectionType], filenameExcel);
     break;
    case ExcelConnectionType.ODBC:
     excelConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DriverId=790;Dbq=" + filenameExcel +
                             ";DefaultDir=" + Path.GetDirectoryName(filenameExcel) + ';';
     break;
   }
   try
   {
    oleDbConnection = new OleDbConnection(excelConnectionString);
    oleDbConnection.Open();
    oleDbCommand = new OleDbCommand("SELECT * FROM [sheet1$]", oleDbConnection);
    oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
   }
   catch(Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   finally
   {
    //Clean up.
    if (oleDbConnection != null) { oleDbConnection.Close(); }
   }
   if ( httpContext == null && exceptionMessage != null )
   {
    System.Console.WriteLine(exceptionMessage);
   }
  }

  ///<summary>ExcelOpen</summary>
  public static void ExcelOpen
  (
   string filenameExcel,
   ExcelConnectionType excelConnectionType,
   out string exceptionMessage
  )
  {
   string excelConnectionString = null;
   HttpContext httpContext = HttpContext.Current;
   IDataReader dataReader = null;
   OleDbCommand oleDbCommand = null;
   OleDbConnection oleDbConnection = null;
   exceptionMessage = null;
   switch ( excelConnectionType )
   {
    case ExcelConnectionType.OLEDB:
     excelConnectionString = String.Format(ExcelConnectionString[(int)excelConnectionType], filenameExcel);
     break;
    case ExcelConnectionType.ODBC:
     excelConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DriverId=790;Dbq=" + filenameExcel +
                             ";DefaultDir=" + Path.GetDirectoryName(filenameExcel) + ';';
     break;
   }
   try
   {
    oleDbConnection = new OleDbConnection(excelConnectionString);
    oleDbConnection.Open();
    oleDbCommand = new OleDbCommand("SELECT * FROM [sheet1$]", oleDbConnection);
    dataReader = oleDbCommand.ExecuteReader();
    while (dataReader.Read())
    {
     //Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
     for ( int fieldIndex = 0; fieldIndex < dataReader.FieldCount; ++fieldIndex )
     {
      System.Console.Write(dataReader[fieldIndex]);
     }
     System.Console.WriteLine();
    }

   }
   catch(Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   finally
   {
    //Clean up.
    if (dataReader != null) { dataReader.Close(); }
    if (oleDbConnection != null) { oleDbConnection.Close(); }
   }
   if ( httpContext == null && exceptionMessage != null )
   {
    System.Console.WriteLine(exceptionMessage);
   }
  }

  ///<summary>SQLExcel</summary>
  public static void SQLExcel
  (
   string databaseConnectionString,
   string sql,
   string filenameExcel,
   out string exceptionMessage
  )
  {
   StringBuilder sb;
   FileStream fileStream = null;
   StreamWriter streamWriter = null;
   IDataReader dataReader = null;
   OleDbCommand oleDbCommand;
   OleDbConnection oleDbConnection = null;
   HttpContext httpContext = HttpContext.Current;
   exceptionMessage = null;
   try
   {
    fileStream = new FileStream( filenameExcel, FileMode.OpenOrCreate, FileAccess.Write);
    streamWriter = new StreamWriter(fileStream);
    oleDbConnection = new OleDbConnection( databaseConnectionString );
    oleDbConnection.Open();
    oleDbCommand = new OleDbCommand( sql, oleDbConnection );
    dataReader = oleDbCommand.ExecuteReader();

    //Initialize the string that is used to build the file.
    sb = new StringBuilder();

    //Enumerate the field names and the records that are used to build the file.
    for (int i = 0; i <= dataReader.FieldCount-1; i++)
    {
     sb.Append( dataReader.GetName(i).ToString() + Convert.ToChar(9) );
    }

    //Write the field name information to the file.
    streamWriter.WriteLine(sb);

    //Reinitialize the string for data.
    sb = new StringBuilder();

    //Enumerate the database that is used to populate the file.
    while (dataReader.Read())
    {
     for (int i = 0; i <= dataReader.FieldCount-1; i++)
     {
      sb.Append( dataReader.GetValue(i).ToString() + Convert.ToChar(9) );
     }
     streamWriter.WriteLine(sb);
     sb = new StringBuilder();;
    }
   }
   catch(Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   finally
   {
    //Clean up.
    if (dataReader != null) { dataReader.Close(); }
    if (oleDbConnection != null) { oleDbConnection.Close(); }
    if (streamWriter != null) { streamWriter.Close(); }
    if (fileStream != null) { fileStream.Close(); }
   }
   if ( httpContext == null && exceptionMessage != null )
   {
    System.Console.WriteLine(exceptionMessage);
   }
  }

  ///<summary>ConfigurationXml</summary>
  public static void ConfigurationXml
  (
   string filenameConfigurationXml,
   out string exceptionMessage,
   ref string databaseConnectionString
  )
  {
   XPathDocument xPathDocument;
   XPathNavigator xPathNavigator;
   XPathNavigator xPathNavigatorNode;
   exceptionMessage = null;
   try
   {
    xPathDocument = new XPathDocument( filenameConfigurationXml );
    xPathNavigator = xPathDocument.CreateNavigator();
    xPathNavigatorNode = xPathNavigator.SelectSingleNode( XPathDatabaseConnectionString );
    if ( xPathNavigatorNode != null )
    {
     databaseConnectionString = xPathNavigatorNode.InnerXml;
    }
   }
   catch (Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   if ( exceptionMessage != null )
   {
    System.Console.WriteLine( exceptionMessage );
   }
  }

  static UtilityExcel()
  {
   
  }

 }
}