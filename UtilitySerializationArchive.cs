using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace WordEngineering
{

 /// <summary>UtilitySerializationArgument</summary>
 public class UtilitySerializationArgument
 {
  ///<summary>dated</summary>
  public DateTime dated;

  ///<summary>theWordId</summary>  
  public int      theWordId;
  
  ///<summary>xmlDocument</summary>  
  public String   xmlDocument;

  ///<summary>styleSheet</summary>
  public String   styleSheet;
  
  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public string[] files;
 }//public class UtilitySerializationArgument

 ///<summary>UtilitySerialization</summary>
 ///<remarks>UtilitySerialization</remarks>
 public class UtilitySerialization
 {

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString                    = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml                    = @"WordEngineering.config";

  /// <summary>The XMLProcessingInstructionStyleSheet</summary>
  public const   String     XMLProcessingInstructionStyleSheet          = @"type='text/xsl' href='{0}'";

  /// <summary>The XPath database connection string.</summary>
  public const   String     XPathDatabaseConnectionString               = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";
  
  /// <summary>Constructor.</summary>
  public UtilitySerialization()
  {

  }

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Boolean                       booleanParseCommandLineArguments  =  false;
   UtilitySerializationArgument  utilitySerializationArgument      =  null;
   
   utilitySerializationArgument = new UtilitySerializationArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilitySerializationArgument
   );
   
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilitySerializationArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

  }//public static void Main( String[] argv )

  ///<summary>Stub.</summary>
  public static void Stub()
  {
   TheWord.TheWordSerialization();   
  }

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   string exceptionMessage = null;
   
   ConfigurationXml
   (
        FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString
   );
   
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  /// <param name="filenameConfigurationXml">The XML Configuration file.</param>
  /// <param name="exceptionMessage">The exception message.</param>
  /// <param name="databaseConnectionString">The database connection string.</param>  
  public static void ConfigurationXml
  (
       string filenameConfigurationXml,
   ref string exceptionMessage,
   ref string databaseConnectionString
  )
  {
   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathDatabaseConnectionString,
     ref databaseConnectionString
    );//ConfigurationXml()
  }//ConfigurationXml	 
  
  static UtilitySerialization()
  {
   ConfigurationXml();
  }//static UtilitySerialization()
  
 }//public class UtilitySerialization
 
}//namespace WordEngineering