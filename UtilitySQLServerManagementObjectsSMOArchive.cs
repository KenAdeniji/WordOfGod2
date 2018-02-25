using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security;
using System.Threading;
using System.Text;
using System.Web;

namespace WordEngineering
{

 /// <summary>UtilitySQLServerManagementObjectsSMOArgument</summary>
 public class UtilitySQLServerManagementObjectsSMOArgument
 {

  ///<summary>backup</summary>
  public bool       backup                 =  false;

  ///<summary>maintenance</summary>
  public bool       maintenance            =  false;

  ///<summary>script</summary>  
  public bool       script                 =  false;

  ///<summary>system</summary>  
  public bool       system                 =  false;

  ///<summary>user</summary>  
  public bool       user                   =  true;

  ///<summary>database</summary>  
  public string[]   database               =  UtilitySQLServerManagementObjectsSMO.DatabaseName;

  ///<summary>default</summary>
  public string[]   defaultName            =  UtilitySQLServerManagementObjectsSMO.DefaultName;

  ///<summary>directoryBackup</summary>  
  public string     directoryBackup        =  UtilitySQLServerManagementObjectsSMO.DirectoryBackup;

  ///<summary>directoryMaintenance</summary>  
  public string     directoryMaintenance   =  UtilitySQLServerManagementObjectsSMO.DirectoryMaintenance;

  ///<summary>directoryScript</summary>  
  public string     directoryScript        =  UtilitySQLServerManagementObjectsSMO.DirectoryScript;

  ///<summary>rule</summary>  
  public string[]   rule                   =  UtilitySQLServerManagementObjectsSMO.RuleName;

  ///<summary>server</summary>  
  public string[]   server                  =  UtilitySQLServerManagementObjectsSMO.ServerName;

  ///<summary>sqlServerLoginUserName</summary>  
  public string     sqlServerLoginUserName  =  UtilitySQLServerManagementObjectsSMO.SqlServerLoginUserName;

  ///<summary>sqlServerPassword</summary>  
  public string     sqlServerPassword       =  UtilitySQLServerManagementObjectsSMO.SqlServerPassword;
  
  ///<summary>storedProcedure</summary>  
  public string[]   storedProcedure         =  UtilitySQLServerManagementObjectsSMO.StoredProcedureName;

  ///<summary>table</summary>  
  public string[]   table                   =  UtilitySQLServerManagementObjectsSMO.TableName;

  ///<summary>trigger</summary>  
  public string[]   trigger                 =  UtilitySQLServerManagementObjectsSMO.TriggerName;

  ///<summary>userDefinedDataType</summary>  
  public string[]   userDefinedDataType     =  UtilitySQLServerManagementObjectsSMO.UserDefinedDataTypeName;

  ///<summary>userDefinedFunction</summary>  
  public string[]   userDefinedFunction     =  UtilitySQLServerManagementObjectsSMO.UserDefinedFunctionName;

  ///<summary>view</summary>  
  public string[]   view                    =  UtilitySQLServerManagementObjectsSMO.ViewName;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilitySQLServerManagementObjectsSMOArgument():this
  (
   false, //backup
   UtilitySQLServerManagementObjectsSMO.DatabaseName,
   UtilitySQLServerManagementObjectsSMO.DefaultName,
   UtilitySQLServerManagementObjectsSMO.DirectoryBackup,
   UtilitySQLServerManagementObjectsSMO.DirectoryMaintenance,
   UtilitySQLServerManagementObjectsSMO.DirectoryScript,
   false, //maintenance
   UtilitySQLServerManagementObjectsSMO.RuleName,
   false, //script
   UtilitySQLServerManagementObjectsSMO.ServerName,
   UtilitySQLServerManagementObjectsSMO.SqlServerLoginUserName,
   UtilitySQLServerManagementObjectsSMO.SqlServerPassword,   
   UtilitySQLServerManagementObjectsSMO.StoredProcedureName,
   false, //system
   UtilitySQLServerManagementObjectsSMO.TableName,
   UtilitySQLServerManagementObjectsSMO.TriggerName,   
   true,  //user
   UtilitySQLServerManagementObjectsSMO.UserDefinedDataTypeName,
   UtilitySQLServerManagementObjectsSMO.UserDefinedFunctionName,
   UtilitySQLServerManagementObjectsSMO.ViewName
  ) 
  {
  }//public UtilitySQLServerManagementObjectsSMOArgument()

  /// <summary>Constructor.</summary>
  public UtilitySQLServerManagementObjectsSMOArgument
  (
   bool      backup,
   string[]  database,
   string[]  defaultName,
   string    directoryBackup,
   string    directoryMaintenance,
   string    directoryScript,
   bool      maintenance,
   string[]  rule,
   bool      script,
   string[]  server,
   string    sqlServerLoginUserName,
   string    sqlServerPassword,   
   string[]  storedProcedure,
   bool      system,
   string[]  table,
   string[]  trigger,
   bool      user,
   string[]  userDefinedDataType,   
   string[]  userDefinedFunction,
   string[]  view
  )
  {
   this.backup                  =  backup;
   this.database                =  database;
   this.defaultName             =  defaultName;
   this.directoryBackup         =  directoryBackup;
   this.directoryMaintenance    =  directoryMaintenance;
   this.directoryScript         =  directoryScript;
   this.maintenance             =  maintenance;
   this.rule                    =  rule;
   this.script                  =  script;
   this.server                  =  server;
   this.sqlServerLoginUserName  =  sqlServerLoginUserName;
   this.sqlServerPassword       =  sqlServerPassword;
   this.storedProcedure         =  storedProcedure;
   this.system                  =  system;
   this.table                   =  table;
   this.trigger                 =  trigger;   
   this.user                    =  user;
   this.userDefinedDataType     =  userDefinedDataType;
   this.userDefinedFunction     =  userDefinedFunction;
   this.view                    =  view;
  }//public UtilitySQLServerManagementObjectsSMOArgument()

 }//public class UtilitySQLServerManagementObjectsSMOArgument

 /// <summary>UtilitySQLServerManagementObjectsSMO</summary>
 /// <remarks>
 ///  http://www.yukonxml.com/chapters/apress/ss2005revealed/mgmtstudio/                                                                 Tony Bain, Robin Dewson SQL Server 2005 Revealed ISBN 1-59059-385-5 APress.com Chapter 2 SQL Server Management Studio Chapter 2 SQL Server Management Studio
 ///  http://www.netacademia.net/tudastar/articlepage.aspx?upid=3779                                                                     SQL Server 2005 Server Management Objects példa
 ///  http://blogs.msdn.com/mwories/archive/2005/05/07/basic_scripting.aspx                                                              Michiel Wories' WebLog: SQL Server: SMO Scripting Basics
 ///  http://blog.xeraph.org/net/2005/06/20/dump-sql-server-schema                                                                       Leon Breedt Dump SQL Server Schema
 ///  http://blog.xeraph.org/files/DumpSqlServerSchema.cs
 ///  http://www.sqldbatips.com/showarticle.asp?ID=42                                                                                    Getting Started with SMO in SQL 2005 - Integrity Checks
 ///  http://download.microsoft.com/download/d/d/9/dd9f7a2e-a1ab-473f-928e-8d2b9598c80b/sqlhol117%20-%20smo%20and%20web%20services.doc   Microsoft: SMO and Web Services Hands-On Lab Manual
 /// </remarks>
 public class UtilitySQLServerManagementObjectsSMO
 {  

  /// <summary>The database connection string.</summary>
  public static  String               DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>DatabaseName</summary>
  public static  String[]             DatabaseName                       = null;

  ///<summary>DefaultName</summary>
  public static  string[]             DefaultName                        = null;

  /// <summary>DirectoryBackup</summary>
  public static  String               DirectoryBackup                    = @"D:\SQLServerBackup";

  /// <summary>DirectoryMaintenance</summary>
  public static  String               DirectoryMaintenance               = @"D:\SQLServerMaintenance";

  /// <summary>DirectoryScript</summary>
  public static  String               DirectoryScript                    = @"D:\SQLServerDataDefinitionLanguageDDL";

  /// <summary>The configuration XML filename.</summary>
  public static  String               FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>RuleName</summary>
  public static  String[]             RuleName                           = null;

  /// <summary>ServerName</summary>
  public static  String[]             ServerName                         = new string[] { "." };

  /// <summary>SqlServerInstanceSeparator</summary>
  public static  String               SqlServerInstanceSeparator         = @"\";

  /// <summary>SqlServerLoginUserName</summary>
  public static  String               SqlServerLoginUserName             = null;

  /// <summary>SqlServerPassword</summary>
  public static  String               SqlServerPassword                  = null;

  /// <summary>StoredProcedureName</summary>
  public static  String[]             StoredProcedureName                = null;

  /// <summary>TableName</summary>
  public static  String[]             TableName                          = null;

  /// <summary>TriggerName</summary>
  public static  String[]             TriggerName                        = null;

  /// <summary>UserDefinedDataTypeName</summary>
  public static  String[]             UserDefinedDataTypeName            = null;

  /// <summary>UserDefinedFunctionName</summary>
  public static  String[]             UserDefinedFunctionName            = null;

  /// <summary>ViewName</summary>
  public static  String[]             ViewName                           = null;

  /// <summary>The XPath database connection String.</summary>
  public static  String               XPathDatabaseConnectionString      = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>The XPath database</summary>
  public static  String               XPathDatabase                      = @"/word/sqlServerManagementObjectsSMO/database";

  /// <summary>The XPath default name</summary>
  public static  String               XPathDefault                       = @"/word/sqlServerManagementObjectsSMO/default";

  /// <summary>The XPath directoryBackup</summary>
  public static  String               XPathDirectoryBackup               = @"/word/sqlServerManagementObjectsSMO/directoryBackup";

  /// <summary>The XPath directoryMaintenance</summary>
  public static  String               XPathDirectoryMaintenance          = @"/word/sqlServerManagementObjectsSMO/directoryMaintenance";

  /// <summary>The XPath directoryScript</summary>
  public static  String               XPathDirectoryScript               = @"/word/sqlServerManagementObjectsSMO/directoryScript";

  /// <summary>The XPath rule</summary>
  public static  String               XPathRule                          = @"/word/sqlServerManagementObjectsSMO/rule";

  /// <summary>The XPath server</summary>
  public static  String               XPathServer                        = @"/word/sqlServerManagementObjectsSMO/server";

  /// <summary>The XPath SQLServerLogin</summary>
  public static  String               XPathSQLServerLoginUserName        = @"/word/sqlServerManagementObjectsSMO/sqlServerLoginUserName";

  /// <summary>The XPath SqlServerPassword</summary>
  public static  String               XPathSqlServerPassword             = @"/word/sqlServerManagementObjectsSMO/sqlServerPassword";

  /// <summary>The XPath stored procedure</summary>
  public static  String               XPathStoredProcedure               = @"/word/sqlServerManagementObjectsSMO/storedProcedure";

  /// <summary>The XPath table</summary>
  public static  String               XPathTable                         = @"/word/sqlServerManagementObjectsSMO/table";

  /// <summary>The XPath trigger</summary>
  public static  String               XPathTrigger                       = @"/word/sqlServerManagementObjectsSMO/trigger";

  /// <summary>The XPath UserDefinedDataType</summary>
  public static  String               XPathUserDefinedDataType           = @"/word/sqlServerManagementObjectsSMO/userDefinedDataType";

  /// <summary>The XPath UserDefinedFunction</summary>
  public static  String               XPathUserDefinedFunction           = @"/word/sqlServerManagementObjectsSMO/userDefinedFunction";

  /// <summary>The XPath View</summary>
  public static  String               XPathView                          = @"/word/sqlServerManagementObjectsSMO/view";

  /// <summary>ScriptingOptionsStandard</summary>
  public static  ScriptingOptions     ScriptingOptionsStandard;

  /// <summary>Main</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   bool                          booleanParseCommandLineArguments  =  false;
   string                        exceptionMessage                  =  null;     
   string                        filenameApplication               =  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
   UtilitySQLServerManagementObjectsSMOArgument            utilitySQLServerManagementObjectsSMOArgument                =  null;
   
   utilitySQLServerManagementObjectsSMOArgument = new UtilitySQLServerManagementObjectsSMOArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilitySQLServerManagementObjectsSMOArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilitySQLServerManagementObjectsSMOArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   /*
   #if ( DEBUG )
    System.Console.WriteLine
    (
     "Filename Application: {0}",
     filenameApplication
    );
   #endif
   */
   
   Stub
   (
    ref  utilitySQLServerManagementObjectsSMOArgument,
    ref  exceptionMessage
   );
    
  }//Main()  

  /// <summary>Stub</summary>
  public static void Stub
  (
   ref UtilitySQLServerManagementObjectsSMOArgument  utilitySQLServerManagementObjectsSMOArgument,
   ref string                                        exceptionMessage
  )
  {
   /*
   SQLServerList
   (
    ref  utilitySQLServerManagementObjectsSMOArgument,
    ref  exceptionMessage
   );
   */
   
   UtilitySMO
   (
    ref  utilitySQLServerManagementObjectsSMOArgument,
    ref  exceptionMessage
   );

  }//Stub()
  
  /// <summary>StubDatabaseTableCreate</summary>
  public static void StubDatabaseTableCreate
  (
   ref UtilitySQLServerManagementObjectsSMOArgument  utilitySQLServerManagementObjectsSMOArgument,
   ref string                                        exceptionMessage
  )
  {
   Database          database          =  null;
   ServerConnection  serverConnection  =  null;
   Server            server            =  null;
   Table             table             =  null;
   
   DatabaseCreate
   (
    ref utilitySQLServerManagementObjectsSMOArgument.server[0],
    ref serverConnection,
    ref server,
    ref exceptionMessage,
    ref utilitySQLServerManagementObjectsSMOArgument.sqlServerLoginUserName,
    ref utilitySQLServerManagementObjectsSMOArgument.sqlServerPassword,
    ref utilitySQLServerManagementObjectsSMOArgument.database[0],
    ref database
   );

   if ( database == null || exceptionMessage != null )
   {
   	return;
   }   	

   TableCreate
   (
    ref database,
    ref utilitySQLServerManagementObjectsSMOArgument.table[0],
    ref table,
    ref exceptionMessage
   );
  }//StubDatabaseTableCreate()

  /// <summary>DatabaseCreate</summary>
  public static void DatabaseCreate
  (
   ref string            serverName,
   ref ServerConnection  serverConnection,
   ref Server            server,
   ref string            exceptionMessage,
   ref string            sqlServerLoginUserName,
   ref string            sqlServerPassword,
   ref string            databaseName,
   ref Database          database
  )
  {
   HttpContext       httpContext     =  HttpContext.Current;
   server                            =  null;
   try
   {
    SQLServerConnection
    (
     ref  serverName,
     ref  serverConnection,
     ref  server,
     ref  exceptionMessage,
     ref  sqlServerLoginUserName,
     ref  sqlServerPassword
    );
    if ( server == null || exceptionMessage != null )
    {
     return;
    }
    database = new Database( server, databaseName );
    if ( database == null )
    {
     return;
    }
    database.Create();
   } 
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message; //exception.InnerException;
   }//catch ( SmoException exception )
   catch ( System.Exception exception )
   {
    exceptionMessage = "System.Exception: " + exception.Message; //exception.InnerException;
   }//catch ( System.Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }
   }//if ( exceptionMessage != null )
  }//DatabaseCreate()

  /// <summary>SQLServerScript</summary>
  public static void SQLServerScript
  (
   IScriptable   iScriptable,
   string        exceptionMessage,   
   StreamWriter  streamWriter
  )
  {
   HttpContext       httpContext       =  HttpContext.Current;
   StringBuilder     stringBuilder     =  null;
   StringCollection  stringCollection  =  null;
   
   try
   {
    stringCollection  =  iScriptable.Script( ScriptingOptionsStandard );
    UtilityIO.StreamWriterStringCollection
    (
     ref streamWriter,
     ref stringCollection,
     ref stringBuilder,
     ref exceptionMessage
    );
   }//try
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message;
   }//catch ( SmoException exception )
   catch ( System.ObjectDisposedException exception )
   {
    exceptionMessage = "System.ObjectDisposedException: " + exception.Message;
   }//catch ( System.ObjectDisposedException exception )
   catch ( System.IO.IOException exception )
   {
    exceptionMessage = "System.IOException: " + exception.Message;
   }//catch ( System.IO.IOException exception )
   catch ( System.Exception exception )
   {
    exceptionMessage = "System.Exception: " + exception.Message;
   }//catch ( System.Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
  }//public static void SQLServerScript()

  /// <summary>SQLServerList</summary>
  public static void SQLServerList
  (
   ref ArrayList  sqlServerInstance,
   ref string     exceptionMessage
  )
  {
   HttpContext       httpContext  =  HttpContext.Current;
   
   int               sqlServerIndex;
   
   string            sqlServerName;
   
   DataTable         sqlServer;
   
   sqlServerInstance = null;
   
   try
   {
    //  Get a list of SQL servers available on the networks
    sqlServer = SmoApplication.EnumAvailableSqlServers( false );
    if ( sqlServer.Rows.Count < 1 )
    {
     return;
    }
    sqlServerInstance = new ArrayList();
    foreach ( DataRow dataRow in sqlServer.Rows )
    {
     sqlServerName = dataRow["Server"].ToString();
     if ( dataRow["Instance"] != null && dataRow["Instance"].ToString().Length > 0 )
     {
      sqlServerName += SqlServerInstanceSeparator + dataRow["Instance"];
     }//if ( dataRow["Instance"] != null && dataRow["Instance"].ToString().Length > 0 )
     sqlServerIndex = sqlServerInstance.IndexOf( sqlServerName );
     if ( sqlServerIndex > -1 )
     {
      continue;
     } 
     sqlServerInstance.Add( sqlServerName );
     #if ( DEBUG )
      if ( httpContext == null )
      {
       System.Console.WriteLine( "SQL Server Name: {0}", sqlServerName );
      }//if ( httpContext == null )
      else
      {
       //httpContext.Response.Write( "SQL Server Name: " + sqlServerName );
      }//else 
     #endif
    }//foreach ( DataRow dataRow in sqlServer.Rows )
    sqlServerInstance.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
   }//try  
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message;
   }//catch ( SmoException exception )
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
  }//SQLServerList()

  /// <summary>SQLServerList</summary>
  public static void SQLServerList
  (
   ref string[]  sqlServerInstance,
   ref string    exceptionMessage
  )
  {
   ArrayList  arrayListSqlServer  =  null;
   SQLServerList
   (
    ref  arrayListSqlServer,
    ref  exceptionMessage
   );
   if ( arrayListSqlServer == null || arrayListSqlServer.Count < 1 )
   {
   	return;
   }
   sqlServerInstance  =  ( string[] ) arrayListSqlServer.ToArray( typeof( string ) );
  }//SQLServerList()

  /// <summary>DatabaseList</summary>
  public static void DatabaseList
  (
   ref string     serverName,
   ref string     exceptionMessage,
   ref ArrayList  arrayListDatabase,
   ref string     sqlServerLoginUserName,
   ref string     sqlServerPassword,
   ref bool       system,
   ref bool       user
  )
  {
   HttpContext       httpContext       =  HttpContext.Current;
   Server            server            =  null;
   ServerConnection  serverConnection  =  null;
   
   try
   {
    arrayListDatabase  =  null;
    SQLServerConnection
    (
     ref serverName,
     ref serverConnection,
     ref server,
     ref exceptionMessage,
     ref sqlServerLoginUserName,
     ref sqlServerPassword
    );
    if ( exceptionMessage != null || server == null )
    {
     return;
    }//if ( exceptionMessage != null || server == null )
    if ( server.Databases.Count == 0 )
    {
     return;
    }
    arrayListDatabase = new ArrayList();
    foreach ( Database database in server.Databases )
    {
     if ( database.IsSystemObject == true && system == false ) { continue; }
     if ( database.IsSystemObject == false && user == false ) { continue; }
     arrayListDatabase.Add( database.Name );            
    }//foreach ( Database database in server.Databases )
    arrayListDatabase.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
   }//try  
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message;
   }//catch ( SmoException exception )
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
  }//DatabaseList()

  /// <summary>DatabaseList</summary>
  public static void DatabaseList
  (
   ref string    serverName,
   ref string    exceptionMessage,
   ref string[]  database,
   ref string    sqlServerLoginUserName,
   ref string    sqlServerPassword,
   ref bool      system,
   ref bool      user
  )
  {
   ArrayList  arrayListDatabase  =  null;
   DatabaseList
   (
    ref  serverName,
    ref  exceptionMessage,
    ref  arrayListDatabase,
    ref  sqlServerLoginUserName,
    ref  sqlServerPassword,
    ref  system,
    ref  user
   );
   if ( arrayListDatabase == null || arrayListDatabase.Count < 1 )
   {
   	return;
   }
   database  =  ( string[] ) arrayListDatabase.ToArray( typeof( string ) );
  }//DatabaseList()
  
  /// <summary>DatabaseResource</summary>
  public static void DatabaseResource
  (
   ref string     serverName,
   ref string     databaseName,
   ref string     exceptionMessage,
   ref string     sqlServerLoginUserName,
   ref string     sqlServerPassword,
   ref bool       system,
   ref bool       user,
   ref ArrayList  arrayListDefault,
   ref ArrayList  arrayListRule,
   ref ArrayList  arrayListStoredProcedure,
   ref ArrayList  arrayListTable,
   ref ArrayList  arrayListTrigger,
   ref ArrayList  arrayListUserDefinedDataType,
   ref ArrayList  arrayListUserDefinedFunction,
   ref ArrayList  arrayListView
  )
  {
   HttpContext       httpContext       =  HttpContext.Current;
   Database          database          =  null;
   Server            server            =  null;
   ServerConnection  serverConnection  =  null;

   arrayListDefault              =  null;
   arrayListRule                 =  null;
   arrayListStoredProcedure      =  null;
   arrayListTable                =  null;
   arrayListTrigger              =  null;
   arrayListUserDefinedDataType  =  null;
   arrayListUserDefinedFunction  =  null;
   arrayListView                 =  null;
   
   try
   {
    SQLServerConnection
    (
     ref serverName,
     ref serverConnection,
     ref server,
     ref exceptionMessage,
     ref sqlServerLoginUserName,
     ref sqlServerPassword
    );
    if ( exceptionMessage != null || server == null )
    {
     return;
    }//if ( exceptionMessage != null || server == null )
    if ( server.Databases.Count == 0 )
    {
     return;
    }
    database        =  server.Databases[ databaseName ];
    if ( database == null )
    {
     return;
    }     	
    if ( database.Defaults.Count > 0 )
    {
     arrayListDefault = new ArrayList();
     foreach ( Microsoft.SqlServer.Management.Smo.Default defaultCurrent in database.Defaults )
     {
      arrayListDefault.Add( defaultCurrent.Name );            
     }//foreach ( Microsoft.SqlServer.Management.Smo.Default defaultCurrent in database.Defaults )
     arrayListDefault.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.Defaults.Count > 0 )
    if ( database.Rules.Count > 0 )
    {
     arrayListRule = new ArrayList();
     foreach ( Microsoft.SqlServer.Management.Smo.Rule rule in database.Rules )
     {
      arrayListRule.Add( rule.Name );            
     }//foreach ( Rule rule in database.Rules )
     arrayListRule.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.Rules.Count > 0 )
    if ( database.StoredProcedures.Count > 0 )
    {
     arrayListStoredProcedure = new ArrayList();
     foreach ( StoredProcedure storedProcedure in database.StoredProcedures )
     {
      if ( storedProcedure.IsSystemObject == true && system == false ) { continue; }
      if ( storedProcedure.IsSystemObject == false && user == false ) { continue; }
      arrayListStoredProcedure.Add( storedProcedure.Name );            
     }//foreach ( StoredProcedure storedProcedure in database.StoredProcedures )
     arrayListStoredProcedure.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.StoredProcedures.Count > 0 ) 
    if ( database.Tables.Count > 0 )
    {
     arrayListTable = new ArrayList();
     foreach ( Table table in database.Tables )
     {
      if ( table.IsSystemObject == true && system == false ) { continue; }
      if ( table.IsSystemObject == false && user == false ) { continue; }
      arrayListTable.Add( table.Name );
     }//foreach ( Table table in database.Tables )
     arrayListTable.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.Tables.Count > 0 ) 
    if ( database.Triggers.Count > 0 )
    {
     arrayListTrigger = new ArrayList();
     foreach ( Trigger trigger in database.Triggers )
     {
      if ( trigger.IsSystemObject == true && system == false ) { continue; }
      if ( trigger.IsSystemObject == false && user == false ) { continue; }
      arrayListTrigger.Add( trigger.Name );            
     }//foreach ( Trigger trigger in database.Triggers )
     arrayListTrigger.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.Triggers.Count > 0 ) 
    if ( database.UserDefinedDataTypes.Count > 0 )
    {
     arrayListUserDefinedDataType = new ArrayList();
     foreach ( UserDefinedDataType userDefinedDataType in database.UserDefinedDataTypes )
     {
      arrayListUserDefinedDataType.Add( userDefinedDataType.Name );            
     }//foreach ( UserDefinedDataType userDefinedDataType in database.UserDefinedDataTypes )
     arrayListUserDefinedDataType.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.UserDefinedDataTypes.Count > 0 ) 
    if ( database.UserDefinedFunctions.Count > 0 )
    {
     arrayListUserDefinedFunction = new ArrayList();
     foreach ( UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions )
     {
      if ( userDefinedFunction.IsSystemObject == true && system == false ) { continue; }
      if ( userDefinedFunction.IsSystemObject == false && user == false ) { continue; }
      arrayListUserDefinedFunction.Add( userDefinedFunction.Name );            
     }//foreach ( UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions )
     arrayListUserDefinedFunction.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.UserDefinedFunctions.Count > 0 ) 
    if ( database.Views.Count > 0 )
    {
     arrayListView = new ArrayList();
     foreach ( View view in database.Views )
     {
      if ( view.IsSystemObject == true && system == false ) { continue; }
      if ( view.IsSystemObject == false && user == false ) { continue; }
      arrayListView.Add( view.Name );            
     }//foreach ( View view in database.Views )
     arrayListView.Sort( System.Collections.CaseInsensitiveComparer.DefaultInvariant );
    }//if ( database.Views.Count > 0 ) 
   }//try  
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message;
   }//catch ( SmoException exception )
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
  }//DatabaseResource()

  /// <summary>DatabaseResource</summary>
  public static void DatabaseResource
  (
   ref string    serverName,
   ref string    databaseName,
   ref string    exceptionMessage,
   ref string    sqlServerLoginUserName,
   ref string    sqlServerPassword,
   ref bool      system,
   ref bool      user,
   ref string[]  defaultName,
   ref string[]  rule,
   ref string[]  storedProcedure,
   ref string[]  table,
   ref string[]  trigger,
   ref string[]  userDefinedDataType,
   ref string[]  userDefinedFunction,
   ref string[]  view   
  )
  {
   ArrayList  arrayListDefault              =  null;
   ArrayList  arrayListRule                 =  null;
   ArrayList  arrayListStoredProcedure      =  null;
   ArrayList  arrayListTable                =  null;
   ArrayList  arrayListTrigger              =  null;
   ArrayList  arrayListUserDefinedDataType  =  null;
   ArrayList  arrayListUserDefinedFunction  =  null;
   ArrayList  arrayListView                 =  null;

   DatabaseResource
   (
    ref  serverName,
    ref  databaseName,
    ref  exceptionMessage,
    ref  sqlServerLoginUserName,
    ref  sqlServerPassword,
    ref  system,
    ref  user,
    ref  arrayListDefault,
    ref  arrayListRule,
    ref  arrayListStoredProcedure,
    ref  arrayListTable,
    ref  arrayListTrigger,
    ref  arrayListUserDefinedDataType,
    ref  arrayListUserDefinedFunction,
    ref  arrayListView
   );
   if ( arrayListDefault != null && arrayListDefault.Count > 0 )
   {
    defaultName  =  ( string[] ) arrayListDefault.ToArray( typeof( string ) );
   }
   if ( arrayListRule != null && arrayListRule.Count > 0 )
   {
    rule  =  ( string[] ) arrayListRule.ToArray( typeof( string ) );
   }
   if ( arrayListStoredProcedure != null && arrayListStoredProcedure.Count > 0 )
   {
    storedProcedure  =  ( string[] ) arrayListStoredProcedure.ToArray( typeof( string ) );
   }
   if ( arrayListTable != null && arrayListTable.Count > 0 )
   {
    table  =  ( string[] ) arrayListTable.ToArray( typeof( string ) );
   }
   if ( arrayListTrigger != null && arrayListTrigger.Count > 0 )
   {
    trigger  =  ( string[] ) arrayListTrigger.ToArray( typeof( string ) );
   }
   if ( arrayListUserDefinedDataType != null && arrayListUserDefinedDataType.Count > 0 )
   {
    userDefinedDataType  =  ( string[] ) arrayListUserDefinedDataType.ToArray( typeof( string ) );
   }
   if ( arrayListUserDefinedFunction != null && arrayListUserDefinedFunction.Count > 0 )
   {
    userDefinedFunction  =  ( string[] ) arrayListUserDefinedFunction.ToArray( typeof( string ) );
   }
   if ( arrayListView != null && arrayListView.Count > 0 )
   {
    view  =  ( string[] ) arrayListView.ToArray( typeof( string ) );
   }
  }//public static void DatabaseResource()
  
  /// <summary>SQLServerList</summary>
  public static void SQLServerList
  (
   ref UtilitySQLServerManagementObjectsSMOArgument  utilitySQLServerManagementObjectsSMOArgument,
   ref string                                        exceptionMessage
  )
  {
   
   HttpContext       httpContext  =  HttpContext.Current;
   
   string            sqlServerName;
   
   DataTable         sqlServer;
   Server            server;
   
   try
   {
    //  Get a list of SQL servers available on the networks
    sqlServer = SmoApplication.EnumAvailableSqlServers( false );
    
    foreach ( DataRow dataRow in sqlServer.Rows )
    {

     sqlServerName = dataRow["Server"].ToString();

     if ( dataRow["Instance"] != null && dataRow["Instance"].ToString().Length > 0 )
     {
      sqlServerName += SqlServerInstanceSeparator + dataRow["Instance"];
     }//if ( dataRow["Instance"] != null && dataRow["Instance"].ToString().Length > 0 )
     
     if ( httpContext == null )
     {
      System.Console.WriteLine( "SQL Server Name: {0}", sqlServerName );
     }//if ( httpContext == null )
     else
     {
      httpContext.Response.Write( "SQL Server Name: " + sqlServerName );
     }//else 
   
     server = new Server( sqlServerName );

     if ( httpContext == null )
     {
      System.Console.WriteLine( "SQL Server Version: {0}", server.Information.VersionString );
     }//if ( httpContext == null )
     else
     {
      httpContext.Response.Write( "SQL Server Version: " + server.Information.VersionString );
     }//else 
     
     foreach ( Database database in server.Databases )
     {
      if ( httpContext == null )
      {
       System.Console.WriteLine( "Database: {0} | System: {1}", database.Name, database.IsSystemObject );
      }//if ( httpContext == null )
      else
      {
       httpContext.Response.Write( "Database: " + database.Name + " | System: " + database.IsSystemObject );
      }//else 
     }//foreach ( Database database in server.Databases )
          	
    }//foreach ( DataRow dataRow in sqlServer.Rows )

   }//try
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message;
   }//catch ( SmoException exception )
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
     
  }//public static void SQLServerList()

  /// <summary>SQLServerConnection</summary>
  public static void SQLServerConnection
  (
   ref string            serverName,
   ref ServerConnection  serverConnection,
   ref Server            server,
   ref string            exceptionMessage
  )
  {
   string            sqlServerLoginUserName  =  null;
   string            sqlServerPassword       =  null;

   SQLServerConnection
   (
    ref  serverName,
    ref  serverConnection,
    ref  server,
    ref  exceptionMessage,
    ref  sqlServerLoginUserName,
    ref  sqlServerPassword
   );
  }//public static void SQLServerConnection()
  	
  /// <summary>SQLServerConnection</summary>
  public static void SQLServerConnection
  (
   ref string            serverName,
   ref ServerConnection  serverConnection,
   ref Server            server,
   ref string            exceptionMessage,
   ref string            sqlServerLoginUserName,
   ref string            sqlServerPassword
  )
  {
   HttpContext       httpContext     =  HttpContext.Current;
   server                            =  null;
   try
   {
    serverConnection                 =  new ServerConnection();
    serverConnection.ServerInstance  =  serverName;
    if ( string.IsNullOrEmpty( sqlServerLoginUserName ) )
    {
     serverConnection.LoginSecure     =  true;
    }
    else
    { 
     serverConnection.LoginSecure     =  false;
     serverConnection.Login           =  sqlServerLoginUserName;
     serverConnection.Password        =  sqlServerPassword;
    } 
    server                           =  new Server( serverConnection );
   } 
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message; //exception.InnerException;
   }//catch ( SmoException exception )
   catch ( System.Exception exception )
   {
    exceptionMessage = "System.Exception: " + exception.Message; //exception.InnerException;
   }//catch ( System.Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }
   }//if ( exceptionMessage != null )
  }//SQLServerConnection()

  /// <summary>TableCreate</summary>
  public static void TableCreate
  (
   ref Database  database,
   ref string    tableName,
   ref Table     table,
   ref string    exceptionMessage
  )
  {
   HttpContext  httpContext     =  HttpContext.Current;
   Column       column;
   try
   {
    table   =  new Table( database, tableName );
    column  =  new Column( table, "column1", DataType.Int );
    table.Columns.Add( column );
    column  =  new Column( table, "column2", DataType.VarChar(30) );
    table.Columns.Add( column );
    table.Create();
   } 
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message; //exception.InnerException;
   }//catch ( SmoException exception )
   catch ( System.Exception exception )
   {
    exceptionMessage = "System.Exception: " + exception.Message; //exception.InnerException;
   }//catch ( System.Exception exception )
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }
   }//if ( exceptionMessage != null )
  }//TableCreate()
  
  /// <summary>UtilitySMO</summary>
  public static void UtilitySMO
  (
   ref UtilitySQLServerManagementObjectsSMOArgument  utilitySQLServerManagementObjectsSMOArgument,
   ref string                                        exceptionMessage
  )
  {
   HttpContext       httpContext   =  HttpContext.Current;

   int               indexServer               =  -1;
   
   string            databaseName              =  null;
   string            defaultName               =  null;
   string            directorynameBackup       =  null;
   string            directorynameMaintenance  =  null;
   string            directorynameScript       =  null;
   string            filenameBackup            =  null;
   string            filenameMaintenance       =  null;
   string            filenameScript            =  null;
   string            ruleName                  =  null;
   string            serverName                =  null;
   string            storedProcedureName       =  null;
   string            tableName                 =  null;
   string            triggerName               =  null;
   string            userDefinedDataTypeName   =  null;
   string            userDefinedFunctionName   =  null;
   string            viewName                  =  null;
   
   Array             databaseSort              =  null;
   Array             defaultSort               =  null;
   Array             ruleSort                  =  null;
   Array             storedProcedureSort       =  null;
   Array             tableSort                 =  null;
   Array             triggerSort               =  null;
   Array             userDefinedDataTypeSort   =  null;
   Array             userDefinedFunctionSort   =  null;
   Array             viewSort                  =  null;   
   
   Backup            backup;
   Server            server                    =  null;
   ServerConnection  serverConnection          =  null;
   
   StreamWriter      streamWriter              =  null;
   
   StringBuilder     stringBuilder             =  null;
   StringCollection  stringCollection          =  null;

   try
   {

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.database != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.database.Length > 0
    )
    {
     databaseSort = utilitySQLServerManagementObjectsSMOArgument.database;
     Array.Sort( databaseSort );
    }

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.defaultName != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.defaultName.Length > 0
    )
    {
     defaultSort = utilitySQLServerManagementObjectsSMOArgument.defaultName;
     Array.Sort( defaultSort );
    }

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.rule != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.rule.Length > 0
    )
    {
     ruleSort = utilitySQLServerManagementObjectsSMOArgument.rule;
     Array.Sort( ruleSort );
    }

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.storedProcedure != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.storedProcedure.Length > 0
    )
    {
     storedProcedureSort = utilitySQLServerManagementObjectsSMOArgument.storedProcedure;
     Array.Sort( storedProcedureSort );
    } 

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.table != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.table.Length > 0
    )
    {
     tableSort = utilitySQLServerManagementObjectsSMOArgument.table;
     Array.Sort( tableSort );
    } 

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.trigger != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.trigger.Length > 0
    )
    {
     triggerSort = utilitySQLServerManagementObjectsSMOArgument.trigger;
     Array.Sort( triggerSort );
    }

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.userDefinedDataType != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.userDefinedDataType.Length > 0
    )
    {
     userDefinedDataTypeSort = utilitySQLServerManagementObjectsSMOArgument.userDefinedDataType;
     Array.Sort( userDefinedDataTypeSort );
    }

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.userDefinedFunction != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.userDefinedFunction.Length > 0
    )
    {
     userDefinedFunctionSort = utilitySQLServerManagementObjectsSMOArgument.userDefinedFunction;
     Array.Sort( userDefinedFunctionSort );
    }

    if 
    ( 
     utilitySQLServerManagementObjectsSMOArgument.view != null 
     &&
     utilitySQLServerManagementObjectsSMOArgument.view.Length > 0
    )
    {
     viewSort = utilitySQLServerManagementObjectsSMOArgument.view;
     Array.Sort( viewSort );
    }

   	for 
   	( 
   	 indexServer = 0; 
   	 indexServer < utilitySQLServerManagementObjectsSMOArgument.server.Length; 
   	 ++indexServer 
   	)
   	{
     serverName = utilitySQLServerManagementObjectsSMOArgument.server[indexServer];
     SQLServerConnection
     (
      ref serverName,
      ref serverConnection,
      ref server,
      ref exceptionMessage,
      ref utilitySQLServerManagementObjectsSMOArgument.sqlServerLoginUserName,
      ref utilitySQLServerManagementObjectsSMOArgument.sqlServerPassword
     );

     if ( server == null )
     {
      return;
     }

     #if ( DEBUG )
      System.Console.WriteLine
      (
       "Name: {0} | Edition: {1} | Product: {2} | ProductLevel: {3} | Version: {4}",
        server.Name,
        server.Information.Edition,
        server.Information.Product,
        server.Information.ProductLevel,        
        server.Information.VersionString
      );
     #endif

     foreach ( Database database in server.Databases )
     {
      
      databaseName = database.Name;

      if 
      (
       ( database.IsSystemObject && utilitySQLServerManagementObjectsSMOArgument.system == false )
       ||
       ( database.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.user == false )
       ||
       (
        databaseSort != null
        &&
        Array.BinarySearch
        ( 
         databaseSort, 
         databaseName,
         System.Collections.CaseInsensitiveComparer.DefaultInvariant
        ) < 0
       ) 
      )
      {
       continue;
      }  

      #if ( DEBUG )
       System.Console.WriteLine
       (
        "Server: {0} | Database: {1} | Create Date: {2}",
        server,
        database,
        database.CreateDate
       );
      #endif

      if ( utilitySQLServerManagementObjectsSMOArgument.backup )
      {
       directorynameBackup      =  Path.Combine
                                   ( 
                                    utilitySQLServerManagementObjectsSMOArgument.directoryBackup,
                                    databaseName
                                   );
       filenameBackup           =  Path.Combine
                                   ( 
                                    directorynameBackup,
                                    databaseName +
                                    string.Format
                                    (
                                     "_db_{0}.BAK",
                                     DateTime.Now.ToString("yyyyMMddHHmm")
                                    )
                                   );

       /*
       backup             =  new Backup();
       backup.Action      =  BackupActionType.Database;
       backup.Database    =  databaseName;
       backup.DeviceType  =  DeviceType.File;
       backup.Devices.Add( filenameBackup );
       backup.SqlBackup( server );
       */

      }//if ( utilitySQLServerManagementObjectsSMOArgument.backup ) 

      if ( database.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.maintenance == true )
      {

       directorynameMaintenance      =  Path.Combine
                                   ( 
                                    utilitySQLServerManagementObjectsSMOArgument.directoryMaintenance,
                                    databaseName
                                   );
       filenameMaintenance           =  Path.Combine
                                   ( 
                                    directorynameMaintenance,
                                    databaseName +
                                    string.Format
                                    (
                                     "_db_{0}.RPT",
                                     DateTime.Now.ToString("yyyyMMddHHmm")
                                    )
                                   );
       #if ( DEBUG )
        System.Console.WriteLine
        (
         "DirectorynameMaintenance: {0} | FilenameMaintenance: {1}",
         directorynameMaintenance,
         filenameMaintenance
        );
       #endif
       if ( !Directory.Exists( directorynameMaintenance ) )
       {
       	Directory.CreateDirectory( directorynameMaintenance );
       }//if ( !Directory.Exists( directorynameMaintenance ) )
       streamWriter = new StreamWriter( filenameMaintenance );
       streamWriter.AutoFlush = true;
       stringCollection = database.CheckAllocations( RepairType.None );  //DBCC CHECKALLOC WITH NO_INFOMSGS 
       UtilityIO.StreamWriterStringCollection( ref streamWriter, ref stringCollection, ref stringBuilder, ref exceptionMessage );
       if ( exceptionMessage != null ) { return; }
       stringCollection = database.CheckAllocationsDataOnly(); //DBCC CHECKALLOC(N'databasename', NOINDEX)
       UtilityIO.StreamWriterStringCollection( ref streamWriter, ref stringCollection, ref stringBuilder, ref exceptionMessage );
       if ( exceptionMessage != null ) { return; }
       stringCollection = database.CheckCatalog(); //DBCC CHECKCATALOG
       UtilityIO.StreamWriterStringCollection( ref streamWriter, ref stringCollection, ref stringBuilder, ref exceptionMessage );
       if ( exceptionMessage != null ) { return; }
       stringCollection = database.CheckTables( RepairType.None ); //DBCC CHECKDB WITH NO_INFOMSGS
       UtilityIO.StreamWriterStringCollection( ref streamWriter, ref stringCollection, ref stringBuilder, ref exceptionMessage );
       if ( exceptionMessage != null ) { return; }
       stringCollection = database.CheckTablesDataOnly(); //DBCC CHECKDB(N'databasename', NOINDEX) 
       UtilityIO.StreamWriterStringCollection( ref streamWriter, ref stringCollection, ref stringBuilder, ref exceptionMessage );
       if ( exceptionMessage != null ) { return; }
       streamWriter.Close();
      }//if ( database.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.maintenance == true )
      
      if ( utilitySQLServerManagementObjectsSMOArgument.script )
      {
       directorynameScript      =  Path.Combine
                                   ( 
                                    utilitySQLServerManagementObjectsSMOArgument.directoryScript,
                                    databaseName
                                   );
       filenameScript           =  Path.Combine
                                   ( 
                                    directorynameScript,
                                    databaseName +
                                    string.Format
                                    (
                                     "_db_{0}.SQL",
                                     DateTime.Now.ToString("yyyyMMddHHmm")
                                    )
                                   );

       #if ( DEBUG )
        System.Console.WriteLine
        (
         "DirectorynameScript: {0} | FilenameScript: {1}",
         directorynameScript,
         filenameScript
        );
       #endif
                                   
       if ( !Directory.Exists( directorynameScript ) )
       {
       	Directory.CreateDirectory( directorynameScript );
       }//if ( !Directory.Exists( directorynameScript ) )
       
       streamWriter = new StreamWriter( filenameScript );
       streamWriter.AutoFlush = true;

       stringCollection  =  new StringCollection();
       stringCollection  =  database.Script();
        
       stringBuilder     =  new StringBuilder();
        
       foreach( string stringCollectionCurrent in stringCollection )
       {
        stringBuilder.AppendLine( stringCollectionCurrent );
       }//foreach( string stringCollectionCurrent in stringCollection )
        
       streamWriter.WriteLine( stringBuilder );
       #if ( DEBUG )
        System.Console.WriteLine
        (
         "Database: {0} | Script: {1}",
         database,
         stringBuilder
        );
       #endif

       foreach ( UserDefinedDataType userDefinedDataType in database.UserDefinedDataTypes )
       {
        userDefinedDataTypeName = userDefinedDataType.Name;

        if 
        (
         userDefinedDataTypeSort != null
         &&
         Array.BinarySearch
         ( 
          userDefinedDataTypeSort, 
          userDefinedDataTypeName,
          System.Collections.CaseInsensitiveComparer.DefaultInvariant
         ) < 0
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         userDefinedDataType,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( UserDefinedDataType userDefinedDataType in database.UserDefinedDataTypes )
       
       foreach ( Table table in database.Tables )
       {
        tableName = table.Name;

        if 
        (
         ( table.IsSystemObject && utilitySQLServerManagementObjectsSMOArgument.system == false )
         ||
         ( table.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.user == false )
         ||
         (
          tableSort != null
          &&
          Array.BinarySearch
          ( 
           tableSort, 
           tableName,
           System.Collections.CaseInsensitiveComparer.DefaultInvariant
          ) < 0
         ) 
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         table,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( Table table in database.Tables )

       foreach ( StoredProcedure storedProcedure in database.StoredProcedures )
       {
        storedProcedureName = storedProcedure.Name;

        if 
        (
         ( storedProcedure.IsSystemObject && utilitySQLServerManagementObjectsSMOArgument.system == false )
         ||
         ( storedProcedure.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.user == false )
         ||
         (      
          storedProcedureSort != null
          &&
          Array.BinarySearch
          ( 
           storedProcedureSort, 
           storedProcedureName,
           System.Collections.CaseInsensitiveComparer.DefaultInvariant
          ) < 0
         ) 
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         storedProcedure,
         exceptionMessage,
         streamWriter  
        );
	    
	   }//foreach ( StoredProcedure storedProcedure in database.StoredProcedures )

       //if ( database.version.Major > 8 ) 
       //Triggers can only be dumped in SQL Server 2005?
       foreach ( Trigger trigger in database.Triggers )
       {
        triggerName = trigger.Name;

        if 
        (
         ( trigger.IsSystemObject && utilitySQLServerManagementObjectsSMOArgument.system == false )
         ||
         ( trigger.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.user == false )
         ||
         (
          triggerSort != null
          &&
          Array.BinarySearch
          ( 
           triggerSort, 
           triggerName,
           System.Collections.CaseInsensitiveComparer.DefaultInvariant
          ) < 0
         ) 
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         trigger,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( Trigger trigger in database.Triggers )

       foreach ( UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions )
       {
        userDefinedFunctionName = userDefinedFunction.Name;

        if 
        (
         ( userDefinedFunction.IsSystemObject && utilitySQLServerManagementObjectsSMOArgument.system == false )
         ||
         ( userDefinedFunction.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.user == false )
         ||
         (
          userDefinedFunctionSort != null
          &&
          Array.BinarySearch
          ( 
           userDefinedFunctionSort, 
           userDefinedFunctionName,
           System.Collections.CaseInsensitiveComparer.DefaultInvariant
          ) < 0
         ) 
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         userDefinedFunction,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions )

       foreach ( View view in database.Views )
       {
        viewName = view.Name;

        if 
        (
         ( view.IsSystemObject && utilitySQLServerManagementObjectsSMOArgument.system == false )
         ||
         ( view.IsSystemObject == false && utilitySQLServerManagementObjectsSMOArgument.user == false )
         ||
         (
          viewSort != null
          &&
          Array.BinarySearch
          ( 
           viewSort, 
           viewName,
           System.Collections.CaseInsensitiveComparer.DefaultInvariant
          ) < 0
         ) 
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         view,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( View view in database.Views )

       foreach ( Microsoft.SqlServer.Management.Smo.Rule rule in database.Rules )
       {
        ruleName = rule.Name;

        if 
        (
         ruleSort != null
         &&
         Array.BinarySearch
         (
          ruleSort, 
          ruleName,
          System.Collections.CaseInsensitiveComparer.DefaultInvariant
         ) < 0
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         rule,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( Rule rule in database.Rules )

       foreach ( Default defaultCurrent in database.Defaults )
       {
        defaultName = defaultCurrent.Name;

        if 
        (
         defaultSort != null
         &&
         Array.BinarySearch
         ( 
          defaultSort, 
          defaultName,
          System.Collections.CaseInsensitiveComparer.DefaultInvariant
         ) < 0
        )
        {
         continue;
        }
        
        SQLServerScript
        (
         defaultCurrent,
         exceptionMessage,
         streamWriter
        );
	    
	   }//foreach ( Default defaultCurrent in database.Defaults )

       streamWriter.Close();

      }//if ( utilitySQLServerManagementObjectsSMOArgument.script ) 
      
     }//foreach ( string database in databaseName )
    }//foreach ( string server in utilitySQLServerManagementObjectsSMOArgument.server )
   }//try
   catch ( UnauthorizedAccessException exception )
   {
    exceptionMessage = "UnauthorizedAccessException: " + exception.Message;
   }//catch ( UnauthorizedAccessException exception )
   catch ( ArgumentNullException exception )
   {
    exceptionMessage = "ArgumentNullException: " + exception.Message;
   }//catch ( ArgumentNullException exception )
   catch ( ArgumentException exception )
   {
    exceptionMessage = "ArgumentException: " + exception.Message;
   }//catch ( ArgumentException exception )
   catch ( PathTooLongException exception )
   {
    exceptionMessage = "PathTooLongException: " + exception.Message;
   }//catch ( PathTooLongException exception )
   catch ( DirectoryNotFoundException exception )
   {
    exceptionMessage = "DirectoryNotFoundException: " + exception.Message;
   }//catch ( DirectoryNotFoundException exception )
   catch ( IOException exception )
   {
    exceptionMessage = "IOException: " + exception.Message;
   }//catch ( IOException exception )
   catch ( NotSupportedException exception )
   {
    exceptionMessage = "NotSupportedException: " + exception.Message;
   }//catch ( NotSupportedException exception )
   catch ( SmoException exception )
   {
    exceptionMessage = "SmoException: " + exception.Message;
   }//catch ( SmoException exception )
   catch ( System.Exception exception )
   {
    exceptionMessage = "System.Exception: " + exception.Message;
   }//catch ( System.Exception exception )
   finally
   {
   	if ( streamWriter != null )
   	{
     streamWriter.Close();
    }//if ( streamWriter != null )
   }//finally
   
   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
	
  }//public static void UtilitySMO()
  
  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   string exceptionMessage = null;
   
   ConfigurationXml
   (
        FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString,
    ref DatabaseName,
    ref DefaultName,
    ref DirectoryBackup,
    ref DirectoryMaintenance,    
    ref DirectoryScript,
    ref RuleName,
    ref ServerName,
    ref SqlServerLoginUserName,
    ref SqlServerPassword,
    ref StoredProcedureName,
    ref TableName,
    ref TriggerName,
    ref UserDefinedDataTypeName,
    ref UserDefinedFunctionName,
    ref ViewName
   );
   
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml
  (
       string    filenameConfigurationXml,
   ref string    exceptionMessage,
   ref string    databaseConnectionString,
   ref string[]  database,
   ref string[]  defaultName,
   ref string    directoryBackup,
   ref string    directoryMaintenance,
   ref string    directoryScript,
   ref string[]  rule,   
   ref string[]  server,
   ref string    sqlServerLoginUserName,
   ref string    sqlServerPassword,
   ref string[]  storedProcedure,
   ref string[]  table,
   ref string[]  trigger,
   ref string[]  userDefinedDataType,
   ref string[]  userDefinedFunction,
   ref string[]  view
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
        XPathDatabase,
    ref database
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathDefault,
    ref defaultName
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathDirectoryBackup,
    ref directoryBackup
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathDirectoryMaintenance,
    ref directoryMaintenance
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathDirectoryScript,
    ref directoryScript
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathRule,
    ref rule
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathServer,
    ref server
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathSQLServerLoginUserName,
    ref sqlServerLoginUserName
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathSqlServerPassword,
    ref sqlServerPassword
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathStoredProcedure,
    ref storedProcedure
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathTable,
    ref table
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathTrigger,
    ref trigger
   );
   
   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathUserDefinedDataType,
    ref userDefinedDataType
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathUserDefinedFunction,
    ref userDefinedFunction
   );

   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathView,
    ref view
   );

  }//ConfigurationXml	 

  static UtilitySQLServerManagementObjectsSMO()
  {
   ScriptingOptionsStandard                                      =  new ScriptingOptions();
   ScriptingOptionsStandard.Default                              =  true;
   ScriptingOptionsStandard.DriForeignKeys                       =  true;
   ScriptingOptionsStandard.DriPrimaryKey                        =  true;
   ScriptingOptionsStandard.DriUniqueKeys                        =  true;
   ScriptingOptionsStandard.IncludeHeaders                       =  true;
   ScriptingOptionsStandard.Indexes                              =  true;
   ScriptingOptionsStandard.SchemaQualify                        =  true;
   ScriptingOptionsStandard.SchemaQualifyForeignKeysReferences   =  true;
   ConfigurationXml();
  }//static UtilitySQLServerManagementObjectsSMO()

 }//public class UtilitySQLServerManagementObjectsSMO
}//namespace WordEngineering
