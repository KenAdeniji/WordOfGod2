using System;
using System.Collections;
using System.IO;
using System.Text;

using SQLDMOTypeLib;

namespace WordEngineering
{
 /// <summary>Standard Query Language (SQL) Distributed Management Object (DMO).</summary>
 public class UtilitySQLDMO
 {		

  /// <summary>The SQLDMO service.</summary>
  public static string[]  Admonish = new String[]
                                     {
  	                                  "NameList"
  	                                 };

  /// <summary>The database connection string.</summary>
  public static string  DatabaseConnectionString       = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The database script.</summary>
  public static string  DatabaseScriptFilename         = @"\SQLServerScript\{0}\{1}.sql";

  /// <summary>The configuration XML filename.</summary>
  public static string  FilenameConfigurationXml       = @"WordEngineering.config";

  /// <summary>The ServerNameLocal (local).</summary>
  public static string  ServerNameLocal                = @"(local)";
		
  ///<summary>SQLServer Appication.</summary>
  public static SQLDMOTypeLib.Application  SQLDMOTypeLibApplication = null;

  /// <summary>The XPath database connection string.</summary>
  public const string   XPathDatabaseConnectionString = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>The XPath database script.</summary>
  public const string   XPathDatabaseScriptFilename           = @"/word/database/sqlServer/scriptFilename";
		
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main
  (
   string[] argv
  )
  {
   string        exceptionMessage = null;
   ArrayList     taskList         = null;
   StringBuilder sb               = null;

   SQLDMOTypeLib.NameList sQLDMOTypeLibNameList	= null;

   taskList = new ArrayList();
   sb       = new StringBuilder();
   
   NameList
   (
    ref sQLDMOTypeLibNameList,
    ref exceptionMessage,
    ref sb
   );

   foreach( string admonishCurrent in Admonish )
   {
    taskList.Add( admonishCurrent );
   } 

   AdmonishAdminister
   (
    ref taskList,
    ref exceptionMessage,
    ref sb
   );
   
   ScriptServerDatabase
   (
    ref exceptionMessage
   );
  
  }//public static void Main()

  ///<summary>AdmonishAdminister().</summary>
  public static void AdmonishAdminister
  (
   ref ArrayList      taskList,
   ref String         exceptionMessage,
   ref StringBuilder  sb
  )
  {
   SQLDMOTypeLib.NameList sQLDMOTypeLibNameList	= null;
   
   sb = new StringBuilder();
   
   foreach( object taskListCurrent in taskList )
   {
    switch ( ( string ) taskListCurrent )
    {
     case "NameList":
      NameList
      (
       ref sQLDMOTypeLibNameList,
       ref exceptionMessage,
       ref sb
      );
      break;
    }//switch ( taskListCurrent )
   }//foreach( string taskListCurrent in taskList )
  }//public static void AdmonishAdminister

  ///<summary>NameList().</summary>
  public static void NameList
  (
   ref SQLDMOTypeLib.NameList sQLDMOTypeLibNameList,
   ref string                 exceptionMessage,
   ref StringBuilder          sb
  )
  {
   try
   {
    sQLDMOTypeLibNameList = SQLDMOTypeLibApplication.ListAvailableSQLServers();
    if ( sQLDMOTypeLibNameList == null )
    {
     return;    	
    }
    #if (DEBUG)
     if ( sQLDMOTypeLibNameList.Count > 0 )
     {
      sb.Append("<b>SQLServer NameList</b><br/>");
     } 
     foreach( object sQLDMOTypeLibNameListCurrent in sQLDMOTypeLibNameList )
     {
      System.Console.WriteLine("SQLServer NameList: " + sQLDMOTypeLibNameListCurrent );
      sb.Append( sQLDMOTypeLibNameListCurrent );
      sb.Append( "<br/>" );
     }//foreach( object sQLDMOTypeLibNameListCurrent in sQLDMOTypeLibNameList )
    #endif
   }//try 
   catch (System.Exception exception) 
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message); 
   }
  }//public static void NameList()

  ///<summary>ScriptServerDatabase().</summary>
  public static void ScriptServerDatabase
  (
   ref string exceptionMessage
  )
  {
   string        databaseName                   = null;
   string        databaseScriptDirectory        = null;
   string        serverName                     = null;
   StringBuilder databaseScriptFilename         = null;
      
   SQLDMOTypeLib.NameList sQLDMOTypeLibNameList = null;
   
   try
   {
   
    sQLDMOTypeLibNameList = SQLDMOTypeLibApplication.ListAvailableSQLServers();
  
    foreach( object sQLDMOTypeLibNameListCurrent in sQLDMOTypeLibNameList )
    {
     serverName = sQLDMOTypeLibNameListCurrent.ToString();
    
     if ( string.Compare(serverName, ServerNameLocal, true ) == 0 )
     {
      UtilityNet.DNSDomainNameResolution 
      (
       ref serverName,
       ref exceptionMessage
      );
     }  
    
     SQLDMOTypeLib.SQLServer sqlServerCurrent = new SQLDMOTypeLib.SQLServerClass();
     // Connect to a SQL Server Instance, use windows authentication
     sqlServerCurrent.LoginSecure=true;
     sqlServerCurrent.Connect(serverName,null,null);
     Console.WriteLine("Connected...");
     foreach (Database2 database in sqlServerCurrent.Databases)
     {
      databaseName = database.Name;
      databaseScriptFilename = new StringBuilder();
      databaseScriptFilename.AppendFormat
      (
       DatabaseScriptFilename,
       serverName,
       databaseName
      );  
      databaseScriptDirectory = Path.GetDirectoryName( databaseScriptFilename.ToString() );
      if ( Directory.Exists( databaseScriptDirectory ) == false )
      {
       Directory.CreateDirectory
       (
        databaseScriptDirectory
       ); 	
      }//if ( Directory.Exists( databaseScriptDirectory ) == false )     	
      System.Console.WriteLine
      (
       "{0}", 
       database.Script(SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default,databaseScriptFilename.ToString(),SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default)
      );
     }
     sqlServerCurrent.DisConnect();
    }//foreach (Database2 database in sqlServerCurrent.Databases)
   }//if ( Directory.Exists( databaseScriptDirectory ) == false )
   catch( System.Runtime.InteropServices.COMException exception )
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "System.Runtime.InteropServices.COMException: {0}", exceptionMessage );  	
   }//catch( System.Runtime.InteropServices.COMException exception )	
  }//public static void ScriptServerDatabase()

  ///<summary>ConfigurationFile().</summary>
  public static void ConfigurationFile()
  {

   string databaseConnectionString  =  null;
   string databaseScript            =  null;
   string exceptionMessage          =  null;

   UtilityXml.XmlDocumentNodeInnerText
   (
         FilenameConfigurationXml,
     ref exceptionMessage,         
         XPathDatabaseConnectionString,
     ref databaseConnectionString
   );
   if ( databaseConnectionString != null && databaseConnectionString != String.Empty )
   {
    DatabaseConnectionString = databaseConnectionString;
   } 

   UtilityXml.XmlDocumentNodeInnerText
   (
         FilenameConfigurationXml,
     ref exceptionMessage,         
         XPathDatabaseScriptFilename,
     ref databaseScript
   );
   if ( databaseScript != null && databaseScript != String.Empty )
   {
    DatabaseScriptFilename = databaseScript;
   } 

  }//ConfigurationFile().
        	 
  /// <summary>Static.</summary>
  static UtilitySQLDMO()
  {
   SQLDMOTypeLibApplication = new SQLDMOTypeLib.Application();
  }//static UtilitySQLDMO()
 }//public class UtilitySQLDMO
}//namespace WordEngineering
