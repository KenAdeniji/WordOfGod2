using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace WordEngineering
{

 /// <summary>UtilityFileUploadArgument</summary>
 public class UtilityFileUploadArgument
 {
  ///<summary>filenameSource</summary>  
  public string   filenameSource        =  null;

  ///<summary>filenameTarget</summary>  
  public string   filenameTarget        =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityFileUploadArgument():this
  (
   UtilityFileUpload.FilenameSource,
   UtilityFileUpload.FilenameTarget
  ) 
  {
  }//public UtilityFileUploadArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityFileUploadArgument
  (
   string   filenameSource,
   string   filenameTarget
  )
  {
   HttpContext       httpContext  =  HttpContext.Current;
   
   if ( filenameSource == null || filenameSource == String.Empty )
   {
    filenameSource = UtilityFileUpload.FilenameSource;
   }

   if ( filenameTarget == null || filenameTarget == String.Empty )
   {
    if ( httpContext == null )
    {
     filenameTarget = filenameSource; 
    }//if ( httpContext == null ) 
    else
    {
     filenameTarget = Path.Combine
     ( 
      httpContext.Request.PhysicalApplicationPath, 
      Path.GetFileName( filenameSource )
     ); 
    }//else
   }//if ( filenameTarget == null || filenameTarget == String.Empty )

   if 
   ( 
    Path.GetDirectoryName( filenameTarget ) != null && 
    ( 
     Path.GetFileName( filenameTarget ) == null 
     || 
     Path.GetFileName( filenameTarget ) == string.Empty 
    ) 
   )
   {
    filenameTarget = Path.Combine
    ( 
     Path.GetDirectoryName( filenameTarget ), 
     Path.GetFileName( filenameSource )
    ); 
   }//if ( Path.GetDirectoryName( filenameTarget ) != null && ( Path.GetFileName( filenameTarget ) == null || Path.GetFileName( filenameTarget ) == string.Empty ) )

   this.filenameSource   =  filenameSource;
   this.filenameTarget   =  filenameTarget;
   
  }//public UtilityFileUploadArgument()

 }//public class UtilityFileUploadArgument
 
 /// <summary>UtilityFileUpload</summary>
 /// <remarks>http://msdn2.microsoft.com/library/ysf0192b(en-us,vs.80).aspx</remarks>
 public class UtilityFileUpload
 {  

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>FilenameSource</summary>
  public static  string     FilenameSource                     = @"d:\WordOfGod\UtilityFileUploadSubstitute.cs";

  /// <summary>FilenameTarget</summary>
  public static  string     FilenameTarget                          = @"ftp://localhost/WordOfGod/UtilityFileUploadSupplement.cs";

  /// <summary>The XPath database connection String.</summary>
  public static  String     XPathDatabaseConnectionString      = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>Main</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Boolean                       booleanParseCommandLineArguments  =  false;
   string                        exceptionMessage                  =  null;     
   string                        filenameApplication               =  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
   UtilityFileUploadArgument     utilityFileUploadArgument         =  null;
   
   FileUpload                    fileUploadSource                  =  null;
   
   utilityFileUploadArgument = new UtilityFileUploadArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityFileUploadArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityFileUploadArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Filename Application: {0}",
     filenameApplication
    );
   #endif

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Argument FilenameSource: {0} | FilenameTarget: {1}",
     utilityFileUploadArgument.filenameSource,
     utilityFileUploadArgument.filenameTarget
    );
   #endif

   fileUploadSource = new FileUpload();
   
   FileUploadSaveAs
   (
    ref utilityFileUploadArgument,
    ref exceptionMessage,
    ref fileUploadSource
   );
   
  }//Main  

  /// <summary>FileUploadSaveAs</summary>
  public static void FileUploadSaveAs
  (
   ref UtilityFileUploadArgument  utilityFileUploadArgument,
   ref string                     exceptionMessage,
   ref FileUpload                 fileUploadSource
  )
  {
   HttpContext       httpContext  =  HttpContext.Current;
   
   try
   {
    if ( !fileUploadSource.HasFile )
    {
     return;
    }//if ( !fileUploadSource.HasFile )
         	 
    fileUploadSource.SaveAs( utilityFileUploadArgument.filenameTarget );
    
   }//try
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
     
  }//public static void FileUploadSaveAs()

 }//public class UtilityFileUpload
}//namespace WordEngineering