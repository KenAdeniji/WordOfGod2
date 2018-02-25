using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Security;
using System.Net;
using System.Net.Mail; //2.0
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mail; //1.0
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace WordEngineering
{

 /// <summary>UtilityRegularExpressionArgument</summary>
 public class UtilityRegularExpressionArgument
 {
  ///<summary>regexOptionsCompiled</summary>  
  public bool      regexOptionsCompiled                   =  false;

  ///<summary>regexOptionsCultureInvariant</summary>  
  public bool      regexOptionsCultureInvariant           =  false;

  ///<summary>regexOptionsECMAScript</summary>  
  public bool      regexOptionsECMAScript                 =  false;

  ///<summary>regexOptionsExplicitCapture</summary>  
  public bool      regexOptionsExplicitCapture            =  false;

  ///<summary>regexOptionsIgnoreCase</summary>  
  public bool      regexOptionsIgnoreCase                 =  false;

  ///<summary>regexOptionsIgnorePatternWhitespace</summary>  
  public bool      regexOptionsIgnorePatternWhitespace    =  false;

  ///<summary>regexOptionsMultiline</summary>  
  public bool      regexOptionsMultiline                  =  false;

  ///<summary>regexOptionsNone</summary>  
  public bool      regexOptionsNone                       =  false;

  ///<summary>regexOptionsRightToLeft</summary>  
  public bool      regexOptionsRightToLeft                =  false;

  ///<summary>regexOptionsSingleline</summary>  
  public bool      regexOptionsSingleline                 =  false;
    	
  ///<summary>regularExpressionInput</summary>  
  public string    regularExpressionInput                 =  null;

  ///<summary>regularExpressionIsMatch</summary>  
  public bool      regularExpressionIsMatch               =  false;

  ///<summary>regularExpressionMatch</summary>  
  public bool      regularExpressionMatch                 =  false;

  ///<summary>regularExpressionMatches</summary>  
  public bool      regularExpressionMatches               =  false;

  ///<summary>regularExpressionPattern</summary>  
  public string    regularExpressionPattern               =  null;

  ///<summary>regularExpressionReplace</summary>  
  public bool      regularExpressionReplace               =  false;

  ///<summary>regularExpressionReplacement</summary>  
  public string    regularExpressionReplacement           =  null;

  ///<summary>Split</summary>  
  public bool      regularExpressionSplit                 =  false;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[]  files;

  /// <summary>Constructor Overloading</summary>
  public UtilityRegularExpressionArgument()
  :this
  (
   false,
   false,
   false,
   false,
   false,
   false,
   false,
   false,
   false,
   false,
   null,
   false,
   false,
   false,
   null,
   false,
   null,
   false
  ) 
  {
  }//public UtilityRegularExpressionArgument()

  /// <summary>Constructor.</summary>
  public UtilityRegularExpressionArgument
  (
   bool   regexOptionsCompiled,
   bool   regexOptionsCultureInvariant,
   bool   regexOptionsECMAScript,
   bool   regexOptionsExplicitCapture,
   bool   regexOptionsIgnoreCase,
   bool   regexOptionsIgnorePatternWhitespace,
   bool   regexOptionsMultiline,
   bool   regexOptionsNone,
   bool   regexOptionsRightToLeft,
   bool   regexOptionsSingleline,
   string regularExpressionInput,
   bool   regularExpressionIsMatch,
   bool   regularExpressionMatch,
   bool   regularExpressionMatches,
   string regularExpressionPattern,
   bool   regularExpressionReplace,
   string regularExpressionReplacement,
   bool   regularExpressionSplit
  )
  {
   this.regexOptionsCompiled                 =  regexOptionsCompiled;
   this.regexOptionsCultureInvariant         =  regexOptionsCultureInvariant;
   this.regexOptionsECMAScript               =  regexOptionsECMAScript;
   this.regexOptionsExplicitCapture          =  regexOptionsExplicitCapture;
   this.regexOptionsIgnoreCase               =  regexOptionsIgnoreCase;
   this.regexOptionsIgnorePatternWhitespace  =  regexOptionsIgnorePatternWhitespace;
   this.regexOptionsMultiline                =  regexOptionsMultiline;
   this.regexOptionsNone                     =  regexOptionsNone;
   this.regexOptionsRightToLeft              =  regexOptionsRightToLeft;
   this.regexOptionsSingleline               =  regexOptionsSingleline;
   this.regularExpressionInput               =  regularExpressionInput;
   this.regularExpressionIsMatch             =  regularExpressionIsMatch;
   this.regularExpressionMatch               =  regularExpressionMatch;
   this.regularExpressionMatches             =  regularExpressionMatches;
   this.regularExpressionPattern             =  regularExpressionPattern;
   this.regularExpressionReplace             =  regularExpressionReplace;
   this.regularExpressionReplacement         =  regularExpressionReplacement;
   this.regularExpressionSplit               =  regularExpressionSplit;   
  }//public UtilityRegularExpressionArgument()

 }//public class UtilityRegularExpressionArgument

 ///<summary>UtilityRegularExpression</summary>
 ///<remarks>
 ///</remarks>
 public class UtilityRegularExpression
 {

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>DatabaseStoredProcedureInsert</summary>
  public static  String     DatabaseStoredProcedureInsert      = "uspRegularExpressionInsert";

  /// <summary>DatabaseStoredProcedureUpdate</summary>
  public static  String     DatabaseStoredProcedureUpdate      = "uspRegularExpressionUpdate";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>DateTime regular expression. It allows only alphanumeric input string between 2 to 40 character long. Regex(@"^[a-zA-Z]\w{1,39}$").</summary>
  public const   String     RegexDateTimeString                = @"^(?<year>\\d{4})-(?<month>\\d{2})-(?<day>\\d{2})T(?<hour>\\d{2}):(?<minute>\\d{2}):(?<second>\\d{7})-(?<coOrdinatedUniversalTimeHigh>\\d{2}):(?<coOrdinatedUniversalTimeLow>\\d{2})\\b";
   
  /// <summary>DateTime regular expression. It allows only alphanumeric input string between 2 to 40 character long. Regex(@"^[a-zA-Z]\w{1,39}$").</summary>
  public static  Regex      RegexDateTime                      = null;

  /// <summary>The XPath database connection String.</summary>
  public static  String     XPathDatabaseConnectionString      = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>Constructor.</summary>
  public UtilityRegularExpression()
  {

  }
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Boolean                           booleanParseCommandLineArguments          =  false;
   string                            exceptionMessage                          =  null;     
   UtilityRegularExpressionArgument  utilityRegularExpressionArgument          =  null;
   
   bool                              isMatch                                   =  false;
   Match                             match                                     =  null;
   MatchCollection                   matchCollection                           =  null;
   RegexOptions                      regexOptions                              =  new RegexOptions();
   string                            replace                                   =  null;
   string[]                          split                                    =  null;   
   
   utilityRegularExpressionArgument                =  new UtilityRegularExpressionArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityRegularExpressionArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityRegularExpressionArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   
   RegularExpressionRegex
   (
    ref utilityRegularExpressionArgument,
    ref regexOptions,
    ref exceptionMessage,
    ref isMatch,
    ref match,
    ref matchCollection,
    ref replace,
    ref split
   );//RegularExpressionRegex
    
  }//static void Main( String[] argv ) 

  ///<summary>RegexOptionsSet</summary>
  public static void RegexOptionsSet
  (
   ref UtilityRegularExpressionArgument  utilityRegularExpressionArgument,
   ref RegexOptions                      regexOptions,
   ref string                            exceptionMessage
  )
  {
   
   HttpContext  httpContext = HttpContext.Current;
   
   regexOptions = new RegexOptions();
   
   try
   {
    
    regexOptions = new RegexOptions();
    
   	if ( utilityRegularExpressionArgument.regexOptionsCompiled ) 
   	{
   	 regexOptions |= RegexOptions.Compiled;
   	}//if ( utilityRegularExpressionArgument.regexOptionsCompiled ) 

   	if ( utilityRegularExpressionArgument.regexOptionsCultureInvariant ) 
   	{
   	 regexOptions |= RegexOptions.CultureInvariant;
   	}//if ( utilityRegularExpressionArgument.regexOptionsCultureInvariant ) 

   	if ( utilityRegularExpressionArgument.regexOptionsECMAScript ) 
   	{
   	 regexOptions |= RegexOptions.ECMAScript;
   	}//if ( utilityRegularExpressionArgument.regexOptionsECMAScript ) 

   	if ( utilityRegularExpressionArgument.regexOptionsExplicitCapture ) 
   	{
   	 regexOptions |= RegexOptions.ExplicitCapture;
   	}//if ( utilityRegularExpressionArgument.regexOptionsExplicitCapture ) 

   	if ( utilityRegularExpressionArgument.regexOptionsIgnoreCase ) 
   	{
   	 regexOptions |= RegexOptions.IgnoreCase;
   	}//if ( utilityRegularExpressionArgument.regexOptionsIgnoreCase ) 

   	if ( utilityRegularExpressionArgument.regexOptionsIgnorePatternWhitespace ) 
   	{
   	 regexOptions |= RegexOptions.IgnorePatternWhitespace;
   	}//if ( utilityRegularExpressionArgument.regexOptionsIgnorePatternWhitespace ) 

   	if ( utilityRegularExpressionArgument.regexOptionsMultiline ) 
   	{
   	 regexOptions |= RegexOptions.Multiline;
   	}//if ( utilityRegularExpressionArgument.regexOptionsMultiline ) 

   	if ( utilityRegularExpressionArgument.regexOptionsNone ) 
   	{
   	 regexOptions |= RegexOptions.None;
   	}//if ( utilityRegularExpressionArgument.regexOptionsNone ) 

   	if ( utilityRegularExpressionArgument.regexOptionsRightToLeft ) 
   	{
   	 regexOptions |= RegexOptions.RightToLeft;
   	}//if ( utilityRegularExpressionArgument.regexOptionsRightToLeft ) 
   	
   	if ( utilityRegularExpressionArgument.regexOptionsSingleline ) 
   	{
   	 regexOptions |= RegexOptions.Singleline;
   	}//if ( utilityRegularExpressionArgument.regexOptionsSingleline ) 

   }//try
   catch( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;   	
   }//catch( Expression expression )
   finally
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( "RegexOptions: {0}", regexOptions );
    }//if ( httpContext == null )
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

  }//public static void RegexOptionsSet()

  ///<summary>RegularExpressionRegex</summary>
  public static void RegularExpressionRegex
  (
   ref   UtilityRegularExpressionArgument  utilityRegularExpressionArgument,
   ref   RegexOptions                      regexOptions,
   ref   string                            exceptionMessage,
   ref   bool                              isMatch,
   ref   Match                             match,
   ref   MatchCollection                   matchCollection,
   ref   string                            replace,
   ref   string[]                          split   
  )
  {
   
   HttpContext          httpContext               = HttpContext.Current;

   try
   {

    RegexOptionsSet
    (
     ref utilityRegularExpressionArgument,
     ref regexOptions,
     ref exceptionMessage
    );//RegexOptionsSet()

    isMatch = Regex.IsMatch
    (
     utilityRegularExpressionArgument.regularExpressionInput,
     utilityRegularExpressionArgument.regularExpressionPattern,
     regexOptions
    );

    match = Regex.Match
    (
     utilityRegularExpressionArgument.regularExpressionInput,
     utilityRegularExpressionArgument.regularExpressionPattern,
     regexOptions
    );

    matchCollection = Regex.Matches
    (
     utilityRegularExpressionArgument.regularExpressionInput,
     utilityRegularExpressionArgument.regularExpressionPattern,
     regexOptions
    );

    if ( utilityRegularExpressionArgument.regularExpressionReplace )
    {
     replace = Regex.Replace
     (
      utilityRegularExpressionArgument.regularExpressionInput,
      utilityRegularExpressionArgument.regularExpressionPattern,
      utilityRegularExpressionArgument.regularExpressionReplacement,
      regexOptions
     );
    }//if ( utilityRegularExpressionArgument.regularExpressionReplace )

    split = Regex.Split
    (
     utilityRegularExpressionArgument.regularExpressionInput,
     utilityRegularExpressionArgument.regularExpressionPattern,
     regexOptions
    );

   }//try
   catch( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;   	
   }//catch( Expression expression )   	

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

   if ( httpContext == null )
   {

    if ( utilityRegularExpressionArgument.regularExpressionIsMatch )
    {
     System.Console.WriteLine("IsMatch: {0}", isMatch);
    }//if ( utilityRegularExpressionArgument.regularExpressionIsMatch )   	

    if ( utilityRegularExpressionArgument.regularExpressionMatch )
    {
     System.Console.WriteLine("Match: {0}", match);
    }//if ( utilityRegularExpressionArgument.regularExpressionMatch )   	

    if ( utilityRegularExpressionArgument.regularExpressionMatches && matchCollection != null )
    {
     for( int matchIndex = 0; matchIndex < matchCollection.Count; ++matchIndex )
     {
      System.Console.WriteLine("Match[{0}]: {1}", matchIndex, matchCollection[matchIndex]);
     }//for( int matchIndex = 0; matchIndex < matchCollection.Count; ++matchIndex )
    }//if ( utilityRegularExpressionArgument.regularExpressionMatches )

    if ( utilityRegularExpressionArgument.regularExpressionReplace )
    {
     System.Console.WriteLine("Replace: {0}", replace);
    }//if ( utilityRegularExpressionArgument.regularExpressionReplace )

    if ( utilityRegularExpressionArgument.regularExpressionSplit && split != null )
    {
     for( int splitIndex = 0; splitIndex < split.Length; ++splitIndex )
     {
      System.Console.WriteLine("Split[{0}]: {1}", splitIndex, split[splitIndex]);
     }//for( int splitIndex = 0; splitIndex < split.Count; ++splitIndex )
    }//if ( utilityRegularExpressionArgument.regularExpressionSplit )

   }//if ( httpContext == null )
   
  }//public static bool RegularExpressionRegex()

  ///<summary>RegularExpressionPattern</summary>
  public static void RegularExpressionPattern
  (
   ref String    dateTime,
   ref bool      matchFlag
  )
  {
   matchFlag = RegexDateTime.Match(dateTime).Success; 
  }//RegularExpressionPattern

  ///<summary>RegularExpressionInsert</summary>
  public static void RegularExpressionInsert
  (
   ref string    databaseConnectionString,
   ref int       sequenceOrderId,
   ref string    input,
   ref string    pattern,
   ref string    replace,
   ref string    title,
   ref string    uri,
   ref DateTime  dated,
   ref string    exceptionMessage
  )
  {
   HttpContext      httpContext      =  HttpContext.Current;
   int              executeNonQuery  =  -1;
   OleDbCommand     oleDbCommand     =  null;
   OleDbConnection  oleDbConnection  =  null;

   try
   {
    oleDbConnection  =  UtilityDatabase.DatabaseConnectionInitialize( databaseConnectionString, ref exceptionMessage );
    if ( exceptionMessage != null || oleDbConnection == null )
    {
     return;
    }//if ( exceptionMessage != null || oleDbConnection == null )
    oleDbCommand = new OleDbCommand( DatabaseStoredProcedureInsert, oleDbConnection );
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    if ( sequenceOrderId > 0 )
    {
     oleDbCommand.Parameters.AddWithValue( "@sequenceOrderId", sequenceOrderId );
    }
    else
    {
     oleDbCommand.Parameters.AddWithValue( "@sequenceOrderId", DBNull.Value );
    }
    if ( !dated.Equals( DateTime.MinValue ) )
    {
     oleDbCommand.Parameters.AddWithValue( "@dated", dated );
    }
    else
    {
     oleDbCommand.Parameters.AddWithValue( "@dated", DBNull.Value );
    }
    oleDbCommand.Parameters.AddWithValue( "@input", @input );
    oleDbCommand.Parameters.AddWithValue( "@pattern", @pattern );
    oleDbCommand.Parameters.AddWithValue( "@replace", @replace );
    oleDbCommand.Parameters.AddWithValue( "@title", @title );
    oleDbCommand.Parameters.AddWithValue( "@uri", @uri );
    executeNonQuery = oleDbCommand.ExecuteNonQuery();
   }//try
   catch ( System.Exception exception )
   {
   	exceptionMessage = "System.Exception: " + exception.Message;
   }//catch ( System.Exception exception )
   if ( exceptionMessage != null || executeNonQuery < 1 )
   {
    System.Console.WriteLine( exceptionMessage );
    return;
   }//if ( exceptionMessage != null || executeNonQuery < 1 )
   UtilityDatabase.DatabaseConnectionHouseKeeping( oleDbConnection, ref exceptionMessage );
   if ( exceptionMessage != null || oleDbConnection != null )
   {
    return;
   }//if ( exceptionMessage != null || oleDbConnection != null )
  }//public static void RegularExpressionInsert()

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
  
  static UtilityRegularExpression()
  {
   RegexDateTime = new Regex( RegexDateTimeString, RegexOptions.IgnoreCase );
   ConfigurationXml();
  }//static UtilityRegularExpression()
  
 }//public class UtilityRegularExpression
 
}//namespace WordEngineering