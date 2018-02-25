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

using FaxComTypeLib;

using CommandLine;

namespace WordEngineering
{

 /// <summary>UtilityFaxArgument</summary>
 public class UtilityFaxArgument
 {

  /// <summary>faxDocument</summary>
  /*
  [DefaultValue(UtilityFax.FaxDocument)]
  */
  public String faxDocument = UtilityFax.FaxDocument;

  /// <summary>faxNumber</summary>
  /*
  [DefaultValue(UtilityFax.FaxNumber)]
  */
  public String faxNumber = UtilityFax.FaxNumber;

  /// <summary>faxServerName</summary>
  /*
  [DefaultValue(UtilityFax.FaxServerName)]
  */
  public String faxServerName = UtilityFax.FaxServerName;
  
  /// <summary>Constructor.</summary>
  public UtilityFaxArgument():this
  (
   UtilityFax.FaxDocument,
   UtilityFax.FaxNumber,
   UtilityFax.FaxServerName
  )
  {
  }//public UtilityFaxArgument()

  /// <summary>Constructor.</summary>
  public UtilityFaxArgument
  (
   String  faxDocument,
   String  faxNumber,
   String  faxServerName
  )
  {

   if ( faxDocument != null )
   {
    faxDocument = faxDocument.Trim();
   }//if ( faxDocument != String.Empty ) 
   
   if ( faxServerName == null && faxServerName == String.Empty )
   {
    faxServerName = UtilityFax.FaxServerName;  	
   }//if ( faxServerName == null && faxServerName == String.Empty )

   if ( faxNumber == null && faxNumber == String.Empty )
   {
    faxNumber = UtilityFax.FaxNumber;  	
   }//if ( faxNumber == null && faxNumber == String.Empty )   	  	

   this.faxDocument    =  faxDocument;
   this.faxNumber      =  faxNumber;
   this.faxServerName  =  faxServerName;
   
  }//public UtilityFaxArgument()

  ///<summary>Property.</summary>
  ///<value>FaxDocument.</value>
  public String FaxDocument
  {
   get 
   {
    return ( faxDocument );
   }//get
   set 
   {
    faxDocument = value;
   }
  }//FaxDocument

  ///<summary>Property.</summary>
  ///<value>FaxNumber.</value>
  public String FaxNumber
  {
   get 
   {
    return ( faxNumber );
   }//get
   set 
   {
    faxNumber = value;
   }
  }//FaxNumber

  ///<summary>Property.</summary>
  ///<value>FaxServerName.</value>
  public String FaxServerName
  {
   get 
   {
    return ( faxServerName );
   }//get
   set 
   {
    faxServerName = value;
   }
  }//FaxServerName
  
 }//public class UtilityFaxArgument

 /// <summary>UtilityFax.</summary>
 /// <remarks>
 ///  http://www.dotnetspider.com/Technology/KBPages/680.aspx Using Windows Fax Service of Windows 2000 in C# with Syed Irfan Ahmed.
 ///  UtilityFax /faxDocument:UtilityFax.cs /faxNumber:17734968121 /faxServerName:localhost
 /// </remarks>
 public class UtilityFax
 {

  ///<summary>The connection String database.</summary>
  public static   String   DatabaseConnectionString                = @"Provider=SQLOLEDB; Data Source=localhost; Integrated Security=SSPI; Initial Catalog=UtilityFax";

  ///<summary>The fax document.</summary>
  public static   String   FaxDocument                             = "UtilityFax.cs";

  ///<summary>The fax number.</summary>
  public static   String   FaxNumber                               = "17734968121";

  ///<summary>The fax server name.</summary>
  public static   String   FaxServerName                           = "localhost";

  /// <summary>The XML configuration file.</summary>
  public static   String   FilenameConfigurationXml                = @"WordEngineering.config";

  ///<summary>The XPath for the database connection String.</summary>  
  public static   String   XPathDatabaseConnectionString           = @"/word/database/sqlServer/utilityFax/databaseConnectionString";

  ///<summary>The XPath for the fax server name.</summary>  
  public static   String   XPathFaxServerName                      = @"/word/fax/serverName";

  ///<summary>The XPath for the fax number.</summary>  
  public static   String   XPathFaxNumber                          = @"/word/fax/number";

  ///<summary>The XPath for the DataFile.</summary>  
  public static   String   XPathDataFile                           = @"/word/database/sqlServer/utilityFax/dataFile";

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of arguments</param>
  public static void Main( String[] argv )
  {
   bool parseCommandLineArguments = false;        
   //Boolean                         booleanParseCommandLineArguments   =  false;
   String                          exceptionMessage                   =  null;
   
   UtilityFaxArgument  utilityFaxArgument    =  null;
   
   utilityFaxArgument = new UtilityFaxArgument();

   /*
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityFaxArgument
   );
   
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    UtilityDebug.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityFaxArgument ) )    
    );  
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   */

   parseCommandLineArguments = Parser.ParseArgumentsWithUsage
   (
    argv, 
    utilityFaxArgument
   );
   if ( parseCommandLineArguments == false )
   {
    return;
   }

   FaxSend
   (
    ref utilityFaxArgument,
    ref exceptionMessage
   );
   
  }//main()

  ///<summary>FaxSend</summary>
  public static void FaxSend
  (
   ref UtilityFaxArgument  utilityFaxArgument,
   ref String              exceptionMessage
  )
  {

   int             faxDocStatus                    =  -1;

   object          faxServerClassDocument          =  null;
   
   FaxDoc          faxDoc                          =  null;
   FaxServerClass  faxServerClass                  =  null;
   
   if ( utilityFaxArgument.FaxDocument == null || utilityFaxArgument.FaxDocument == String.Empty )
   {
    return;
   }//if ( utilityFaxArgument.FaxDocument == null || utilityFaxArgument.FaxDocument == String.Empty )    	

   try 
   {
    faxServerClass = new FaxServerClass();
    
    faxServerClass.Connect( utilityFaxArgument.FaxServerName ); //specifies the machinename
    
    faxServerClassDocument = faxServerClass.CreateDocument
    (
     utilityFaxArgument.FaxDocument
    );
    
    faxDoc = ( FaxDoc) faxServerClassDocument;
    
    faxDoc.FaxNumber = utilityFaxArgument.FaxNumber;
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
    ref FaxServerName,
    ref FaxNumber
   );
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml
  (
   ref String filenameConfigurationXml,
   ref String exceptionMessage,
   ref String databaseConnectionString,
   ref String faxServerName,
   ref String faxNumber
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
         XPathFaxServerName,
     ref faxServerName
   );

   if ( faxServerName == null || faxServerName == String.Empty )
   {
    faxServerName = Environment.MachineName;
   }//if ( faxServerName == null || faxServerName == String.Empty ) 

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathFaxNumber,
     ref faxNumber
   );
  }//ConfigurationXml	 

  static UtilityFax()
  {
   ConfigurationXml();
  }//static()
  
 }//public class UtilityFax.
}//namespace WordEngineering