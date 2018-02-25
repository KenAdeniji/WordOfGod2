using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

using QuartzTypeLib;

namespace WordEngineering
{

 /// <summary>UtilityQuartz Allen Jones C# Programmer's Cookbook ISBN 0-7356-1930-1 http://www.microsoft.com/mspress/books/6456.asp</summary>
 /// <remarks>UtilityQuartz Allen Jones C# Programmer's Cookbook ISBN 0-7356-1930-1 http://www.microsoft.com/mspress/books/6456.asp</remarks>
 [Serializable]
 [XmlRoot("UtilityQuartz", IsNullable = false)] 
 public class UtilityQuartz
 {

  /// <summary>The database connection string.</summary>
  public static string  DatabaseConnectionString                    = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static string  FilenameConfigurationXml       = @"WordEngineering.config";

  /// <summary>QuartzTypeLib.FilgraphManager.</summary>
  public static QuartzTypeLib.FilgraphManager filgraphManager = null;

  /// <summary>QuartzTypeLib.IMediaControl.</summary>
  public static QuartzTypeLib.IMediaControl   mediaControl    = null;
   
  /// <summary>The XPath database connection string.</summary>
  public const string   XPathDatabaseConnectionString = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv) 
  {
   string exception = null;
   QuartzPlay
   ( 
    ref argv,
    ref exception
   );
   QuartzStop
   (
    ref exception
   );
   
  }//public static void Main(string[] args) 

  ///<summary>QuartzPlay.</summary>
  ///<param name="filename">Filename.</param>
  ///<param name="exceptionMessage">Exception message.</param>  
  public static void QuartzPlay
  (
   ref string[] filename,
   ref string   exceptionMessage
  )  
  {

   try
   {
    
    foreach( string filenameCurrent in filename )
    {
     System.Console.WriteLine("Filename: {0}", filenameCurrent);
     mediaControl.RenderFile( filenameCurrent );
     mediaControl.Run();
     System.Console.WriteLine("Press Enter to continue."); 
	 System.Console.ReadLine();
    }//foreach( string filenameCurrent in filename ) 
   }//try 
   catch (System.Exception exception) 
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message); 
   }
  }//public static void QuartzPlay()

  ///<summary>QuartzPause.</summary>
  ///<param name="exceptionMessage">Exception message.</param>  
  public static void QuartzPause
  (
   ref string   exceptionMessage
  )  
  {

   try
   {
    mediaControl.Pause();    
   }//try 
   catch (System.Exception exception) 
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message); 
   }
  }//public static void QuartzPause()

  ///<summary>QuartzStop.</summary>
  ///<param name="exceptionMessage">Exception message.</param>  
  public static void QuartzStop
  (
   ref string   exceptionMessage
  )  
  {

   try
   {
    mediaControl.Stop();    
   }//try 
   catch (System.Exception exception) 
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message); 
   }
  }//public static void QuartzStop()
  
  ///<summary>ConfigurationFile().</summary>
  public static void ConfigurationFile()
  {

   string databaseConnectionString  =  null;
   string exceptionMessage          = null;

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
  }//ConfigurationFile().

  ///<summary>Static.</summary>
  static UtilityQuartz()
  {
   filgraphManager = new QuartzTypeLib.FilgraphManager();
   mediaControl = (QuartzTypeLib.IMediaControl) filgraphManager;
  }//static UtilityQuartz()

 }//UtilityQuartz

}//namespace WordEngineering 