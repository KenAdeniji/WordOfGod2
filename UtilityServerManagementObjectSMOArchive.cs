using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Web;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WordEngineering
{

 /// <summary>UtilityServerManagementObjectSMOArgument</summary>
 public class UtilityServerManagementObjectSMOArgument
 {

  /*
  [DefaultValue(UtilityServerManagementObjectSMO.SQLServerName)]
  */
  String  sqlServerName = null;
  
  /// <summary>Constructor.</summary>
  public UtilityServerManagementObjectSMOArgument():this
  (
   UtilityServerManagementObjectSMO.SQLServerName
  )
  {
  }//public UtilityServerManagementObjectSMOArgument()

  /// <summary>Constructor.</summary>
  public UtilityServerManagementObjectSMOArgument
  (
   String  sqlServerName
  )
  {

   if ( sqlServerName == null || sqlServerName == String.Empty )
   {
    sqlServerName = UtilityServerManagementObjectSMO.SQLServerName;
   }//if ( sqlServerName == null && sqlServerName == String.Empty )

   this.sqlServerName  =  sqlServerName;
   
  }//public UtilityServerManagementObjectSMOArgument()

  ///<summary>Property.</summary>
  ///<value>SqlServerName.</value>
  public String SqlServerName
  {
   get 
   {
    return ( sqlServerName );
   }//get
   set 
   {
    sqlServerName = value;
   }
  }//SqlServerName
  
 }//public class UtilityServerManagementObjectSMOArgument

 /// <summary>UtilityServerManagementObjectSMO.</summary>
 public class UtilityServerManagementObjectSMO
 {

  ///<summary>The connection String database.</summary>
  public static   String   DatabaseConnectionString                = @"Provider=SQLOLEDB; Data Source=localhost; Integrated Security=SSPI; Initial Catalog=UtilityServerManagementObjectSMO";

  ///<summary>SQLServerName.</summary>
  public static   String   SQLServerName                           = "localhost";

  /// <summary>The XML configuration file.</summary>
  public static   String   FilenameConfigurationXml                = @"WordEngineering.config";

  ///<summary>The XPath for the database connection String.</summary>  
  public static   String   XPathDatabaseConnectionString           = @"/word/database/sqlServer/utilityFax/databaseConnectionString";

  ///<summary>The XPath for the fax server name.</summary>  
  public static   String   XPathSQLServerName                      = @"/word/fax/serverName";

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of arguments</param>
  public static void Main( String[] argv )
  {
   Boolean                         booleanParseCommandLineArguments   =  false;
   String                          exceptionMessage                   =  null;
   
   UtilityServerManagementObjectSMOArgument  utilityServerManagementObjectSMOArgument    =  null;
   
   utilityServerManagementObjectSMOArgument = new UtilityServerManagementObjectSMOArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityServerManagementObjectSMOArgument
   );
   
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    UtilityDebug.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityServerManagementObjectSMOArgument ) )    
    );  
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   
   FaxSend
   (
    ref utilityServerManagementObjectSMOArgument,
    ref exceptionMessage
   );
   
  }//main()

  ///<summary>FaxSend</summary>
  public static void FaxSend
  (
   ref UtilityServerManagementObjectSMOArgument  utilityServerManagementObjectSMOArgument,
   ref String              exceptionMessage
  )
  {

   int             faxDocStatus                    =  -1;

   object          faxServerClassDocument          =  null;
   
   FaxDoc          faxDoc                          =  null;
   FaxServerClass  faxServerClass                  =  null;
   
   if ( utilityServerManagementObjectSMOArgument.FaxDocument == null || utilityServerManagementObjectSMOArgument.FaxDocument == String.Empty )
   {
    return;
   }//if ( utilityServerManagementObjectSMOArgument.FaxDocument == null || utilityServerManagementObjectSMOArgument.FaxDocument == String.Empty )    	

   try 
   {
    faxServerClass = new FaxServerClass();
    
    faxServerClass.Connect( utilityServerManagementObjectSMOArgument.SQLServerName ); //specifies the machinename
    
    faxServerClassDocument = faxServerClass.CreateDocument
    (
     utilityServerManagementObjectSMOArgument.FaxDocument
    );
    
    faxDoc = ( FaxDoc) faxServerClassDocument;
    
    faxDoc.FaxNumber = utilityServerManagementObjectSMOArgument.FaxNumber;
    faxDocStatus = faxDoc.Send();

    UtilityDebug.Write
    (
     String.Format
     (
      "Fax Status: {0}",
      faxDocStatus
     )
    );

    faxServerClass.Disconnect();

   }//try
   catch ( Exception exception )
   {
    UtilityDebug.Write
    (
     String.Format
     (
      "Exception: {0}",
      exception.Message
     )
    );
   }//catch ( Exception exception )   	
    
  }//public static void FileImport()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   String  exceptionMessage  =  null;
   
   ConfigurationXml
   (
    ref FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString,
    ref SQLServerName
   );
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml
  (
   ref String filenameConfigurationXml,
   ref String exceptionMessage,
   ref String databaseConnectionString,
   ref String sqlServerName
  )
  {
   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathDatabaseConnectionString,
     ref databaseConnectionString
   );

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathSQLServerName,
     ref sqlServerName
   );

   if ( sqlServerName == null || sqlServerName == String.Empty )
   {
    sqlServerName = Environment.MachineName;
   }//if ( sqlServerName == null || sqlServerName == String.Empty ) 

  }//ConfigurationXml	 

  static UtilityServerManagementObjectSMO()
  {
   ConfigurationXml();
  }//static()
  
 }//public class UtilityServerManagementObjectSMO.
}//namespace WordEngineering