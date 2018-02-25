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

 /// <summary>UtilityMultiMedia.</summary>
 /// <remarks>Stephen Toub http://msdn.microsoft.com/msdnmag/issues/04/04/NETMatters/ .NET Matters. Const in C#, Exception Filters, IWin32Window, and More. netqa@microsoft.com.</remarks>
 public class UtilityMultiMedia
 {

  ///<summary>winmm.dll MciSendString</summary>
  [DllImport("WinMM.dll", EntryPoint="mciSendStringA", CharSet=CharSet.Ansi)]
  public static extern int MciSendString
  (
   String         lpszCommand, 
   StringBuilder  lpszReturnString, 
   int            cchReturn, 
   IntPtr         hwndCallback
  );

  [DllImport("WinMM.dll")]
  private static extern bool PlaySound
  ( 
   String  lpszName, 
   int     hModule, 
   int     dwFlags 
  ); 

  ///<summary>The connection String database.</summary>
  public static   String   DatabaseConnectionString                = @"Provider=SQLOLEDB; Data Source=localhost; Integrated Security=SSPI; Initial Catalog=UtilityMultiMedia";

  /// <summary>The XML configuration file.</summary>
  public static   String   FilenameConfigurationXml                = @"WordEngineering.config";

  ///<summary>The XPath for the database connection String.</summary>  
  public static   String   XPathDatabaseConnectionString           = @"/word/database/sqlServer/utilityFax/databaseConnectionString";

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of arguments</param>
  public static void Main( String[] argv )
  {

   CloseCdTray();
   PlaySound( @"C:\WINNT\Media\Mozart's Symphony No. 40.RMI" );
   
  }//main()

  private static void CloseCdTray()
  {
   MciSendString("Set cdaudio door closed wait", null, 0, IntPtr.Zero);
  }

  private static bool PlaySound
  (
   String  lpszName 
  )
  {
   return ( PlaySound( lpszName, 0, 1 ) );
  }

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

  static UtilityMultiMedia()
  {
   ConfigurationXml();
  }//static()
  
 }//public class UtilityMultiMedia.
}//namespace WordEngineering