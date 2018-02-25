using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Web;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace WordEngineering
{

 /// <summary>UtilityMSCorEE.</summary>
 /// <remarks>Stephen Toub http://msdn.microsoft.com/msdnmag/issues/04/04/NETMatters/ .NET Matters. Const in C#, Exception Filters, IWin32Window, and More. netqa@microsoft.com.</remarks>
 public class UtilityMSCorEE
 {

  ///<summary>MScoree.dll GetCORSystemDirectory</summary>
  [DllImport
   (
    "MSCorEE.dll" 
   )
  ]
  public static extern int GetCORSystemDirectory
  (
       [MarshalAs(UnmanagedType.LPWStr)]StringBuilder  pbuffer, 
       int                                             cchBuffer, 
   ref int                                             dwlength
  );

  ///<summary>The connection String database.</summary>
  public static   String   DatabaseConnectionString                = @"Provider=SQLOLEDB; Data Source=localhost; Integrated Security=SSPI; Initial Catalog=UtilityMSCorEE";

  /// <summary>The XML configuration file.</summary>
  public static   String   FilenameConfigurationXml                = @"WordEngineering.config";

  ///<summary>The XPath for the database connection String.</summary>  
  public static   String   XPathDatabaseConnectionString           = @"/word/database/sqlServer/utilityFax/databaseConnectionString";

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of arguments</param>
  public static void Main( String[] argv )
  {
   StringBuilder  sbGetCORSystemDirectory  =  null;
   
   GetCORSystemDirectory
   (
    ref sbGetCORSystemDirectory
   );
   
  }//main()

  private static void GetCORSystemDirectory
  (
   ref StringBuilder sbGetCORSystemDirectory
  )
  {
   int            MAX_PATH  =  260;
   
   sbGetCORSystemDirectory  =  new StringBuilder(MAX_PATH);
   
   try
   {
   	GetCORSystemDirectory
    (
         sbGetCORSystemDirectory, 
         MAX_PATH, 
     ref MAX_PATH
    );
   
    UtilityDebug.Write
    (
     String.Format("MSCorEE.dll GetCORSystemDirectory: {0}", sbGetCORSystemDirectory)
    );
   }//try
   catch ( Exception exception )
   {
    UtilityDebug.Write
    (
     String.Format("Exception: {0}", exception.Message)
    );
   }//catch ( Exception exception )   	
   
  }//private static String sbGetCORSystemDirectory()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   String  exceptionMessage  =  null;
   
   ConfigurationXml
   (
    ref FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString
   );
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml
  (
   ref String filenameConfigurationXml,
   ref String exceptionMessage,
   ref String databaseConnectionString
  )
  {
   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathDatabaseConnectionString,
     ref databaseConnectionString
   );

  }//ConfigurationXml	 

  static UtilityMSCorEE()
  {
   ConfigurationXml();
  }//static()
  
 }//public class UtilityMSCorEE.
}//namespace WordEngineering