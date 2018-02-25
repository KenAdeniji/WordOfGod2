using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Security;
using System.Xml;
using System.Xml.Serialization;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityURI</summary>
 public class UtilityURI
 {
  /// <summary>Anchor.</summary>
  public const string   Anchor = @"<a target=_blank href={0}>{1}</a>";

 
  /// <summary>ListColumnNameURI</summary>
  public static List<string> ListColumnNameURI         = null;

  /// <summary>ColumnNameURI</summary>
  public static string[] ColumnNameURI                  = new string[] 
                                                          { 
                                                           "sequenceOrderId",
                                                           "dated",
                                                           "uri",
                                                           "title",
                                                           "keyword"
                                                          }; 

  /// <summary>The database connection string.</summary>
  public static string  DatabaseConnectionString        = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=URI;";

  /// <summary>The configuration XML filename.</summary>
  public const string   FilenameConfigurationXml        = @"WordEngineering.config";

  /// <summary>FilenameStylesheet</summary>
  public static string  FilenameStylesheet              = @"Comforter_-_URI.xslt";

  /// <summary>TableNameURI</summary>
  public static string[] TableNameURI                   = new string[] 
                                                          { 
                                                           "URIAdvance",
                                                           "URIBenediction",
                                                           "URIChrist",
                                                           "URIWordEngineering"
                                                          };

  /// <summary>TableNameURIDefault</summary>
  public static string   TableNameURIDefault            = "URIWordEngineering";

  /// <summary>XPathColumnNameURI</summary>
  public const  string  XPathColumnNameURI              = @"/word/uri/column";  
  
  /// <summary>The XPath database connection string.</summary>
  public const  string  XPathDatabaseConnectionString   = @"/word/database/sqlServer/bible/databaseConnectionString";  

  /// <summary>XPathStylesheet</summary>
  public const  string  XPathStylesheet                 = @"/word/uri/stylesheet";

  /// <summary>XPathTableNameURI</summary>
  public const  string  XPathTableNameURI               = @"/word/uri/table";

  /// <summary>XPathTableNameURIDefault</summary>
  public const  string  XPathTableNameURIDefault        = @"/word/uri/tableDefault";

  /// <summary>The DataSetURI.</summary>
  public        DataSet DataSetURI;
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main
  (
   string[] argv
  )
  {

  }//public static void Main()

  ///<summary>DatabaseQuery().</summary>
  ///<param name="databaseConnectionString">Database Connection String.</param>
  ///<param name="exceptionMessage">Exception Message.</param>
  ///<param name="dataSet">DataSet.</param>
  ///<param name="commandText">CommandText.</param>
  ///<param name="commandType">CommandType.</param>  
  public static void DatabaseQuery
  (
       string      databaseConnectionString,
   ref string      exceptionMessage,
   ref DataSet     dataSet,
       string      commandText,
       CommandType commandType
  )
  {
   UtilityDatabase.DatabaseQuery
   (
         databaseConnectionString,
     ref exceptionMessage,
     ref dataSet,
         commandText,
         commandType
   );
  }	

  /// <summary>ReadXml</summary>
  public static void ReadXml
  (
   ref string   filenameXml,
   ref DataSet  dataSet,
   ref string   exceptionMessage,
   ref string[] columnName
  )
  {
   HttpContext  httpContext  =  HttpContext.Current;
   
   try
   {
    UtilityXml.ReadXml
    (
     ref dataSet,
     ref exceptionMessage,
     ref filenameXml
    );
    if ( exceptionMessage != null )
    {
     return;
    }
    if ( dataSet != null )
    {
     UtilityDatabase.DataSetColumn
     (
      ref dataSet,
      ref columnName,
      ref exceptionMessage
     );
     if ( exceptionMessage != null )
     {
      return;
     }
    }
   }//try
   catch ( Exception exception )
   {
   	exceptionMessage = "Exception: " + exception.Message;
   }
   
   if ( httpContext == null )
   {
   	System.Console.WriteLine( exceptionMessage );
   }

  }//public static void ReadXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   string exceptionMessage = null;
   
   ConfigurationXml
   (
        FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString,
    ref FilenameStylesheet,
    ref ColumnNameURI,
    ref TableNameURI,
    ref TableNameURIDefault
   );
   
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml
  (
       string    filenameConfigurationXml,
   ref string    exceptionMessage,
   ref string    databaseConnectionString,
   ref string    filenameStylesheet,
   ref string[]  columnNameURI,
   ref string[]  tableNameURI,
   ref string    tableNameURIDefault
  )
  {
   UtilityXml.GetNodeValue
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathDatabaseConnectionString,
     ref databaseConnectionString
   );
   UtilityXml.GetNodeValue
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathStylesheet,
     ref filenameStylesheet 
   );
   UtilityXml.GetNodeValue
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathColumnNameURI,
     ref columnNameURI
   );
   UtilityXml.GetNodeValue
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathTableNameURI,
     ref tableNameURI
   );
   UtilityXml.GetNodeValue
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathTableNameURIDefault,
     ref tableNameURIDefault
   );
  }//ConfigurationXml	 

  ///<summary>Static</summary>
  static UtilityURI()
  {
   ListColumnNameURI = new List<string>();
   ListColumnNameURI.Add( "sequenceOrderId" );
   ListColumnNameURI.Add( "dated" );   
   ListColumnNameURI.Add( "uri" );
   ListColumnNameURI.Add( "title" );
   ListColumnNameURI.Add( "keyword" );

   ConfigurationXml();
  }//static UtilityURI()

 }//public class UtilityURI
}//namespace WordEngineering