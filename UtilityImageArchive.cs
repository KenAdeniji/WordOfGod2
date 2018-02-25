using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WordEngineering
{

 /// <summary>UtilityImageArgument</summary>
 public class UtilityImageArgument
 {
  ///<summary>filenameSource</summary>  
  public string   filenameSource        =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityImageArgument()
  :this
  (
   null
  ) 
  {
  }//public UtilityImageArgument()

  /// <summary>Constructor Overloading</summary>
  public UtilityImageArgument
  (
   string filenameSource
  )
  {
   this.filenameSource  =  filenameSource;
  }//public UtilityImageArgument()

 }//public class UtilityImageArgument
 
 /// <summary>UtilityImage</summary>
 /// <remarks>http://www.developerfusion.co.uk/show/3905/5/</remarks>
 public class UtilityImage
 {  

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=ImageCarbon;";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>The XPath database connection String.</summary>
  public static  String     XPathDatabaseConnectionString      = @"/word/database/sqlServer/imageCarbon/databaseConnectionString";

  /// <summary>Main</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Boolean                  booleanParseCommandLineArguments  =  false;
   string                   exceptionMessage                  =  null;     
   string                   filenameApplication               =  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
   HtmlInputFile            htmlInputFileSource               =  null;
   
   UtilityImageArgument     utilityImageArgument              =  null;
   
   utilityImageArgument         =  new UtilityImageArgument();
   
   booleanParseCommandLineArguments  =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityImageArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityImageArgument ) )
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
     "Argument FilenameSource: {0}",
     utilityImageArgument.filenameSource
    );
   #endif

   htmlInputFileSource = new HtmlInputFile();
   
   DatabaseUpdate
   (
    ref DatabaseConnectionString,
    ref utilityImageArgument,
    ref exceptionMessage,
    ref htmlInputFileSource
   );
   
  }//Main  

  /// <summary>DatabaseSelect</summary>
  public static void DatabaseSelect
  (
   ref string                databaseConnectionString,
   ref UtilityImageArgument  utilityImageArgument,
   ref string                exceptionMessage,
   ref HtmlInputFile         htmlInputFileSource
  )
  {
   
   HttpContext      httpContext                   =  HttpContext.Current;
   
   string           filenameSource                =  null;
   string           sqlSelectStatement            =  null;

   IDataReader      iDataReader                   =  null;
   
   try
   {

    filenameSource       =  htmlInputFileSource.Value;
    
    sqlSelectStatement   = "SELECT ImageCarbonFormMatch, ImageType FROM ImageCarbonForm WHERE URIImage = '" 
                           + filenameSource 
                           + "'";
                           
    UtilityDatabase.DatabaseQuery
    (
         databaseConnectionString,
     ref exceptionMessage,
     ref iDataReader,
     sqlSelectStatement,
     CommandType.Text
    );

    if ( iDataReader.Read() )
    {
     httpContext.Response.ContentType = iDataReader["ImageType"].ToString();
     httpContext.Response.BinaryWrite( (byte[]) iDataReader["ImageCarbonFormMatch"] );
    }//if ( iDataReader.Read() )
    
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )
   finally
   {
   	if ( iDataReader != null )
   	{
     iDataReader.Close();
    }//if ( iDataReader != null )         		
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

  }//public static void DatabaseSelect()

  /// <summary>DatabaseUpdate</summary>
  public static void DatabaseUpdate
  (
   ref string                databaseConnectionString,
   ref UtilityImageArgument  utilityImageArgument,
   ref string                exceptionMessage,
   ref HtmlInputFile         htmlInputFileSource
  )
  {
   HttpContext      httpContext                   =  HttpContext.Current;
   
   int              databaseNumberOfRowsAffected  =  0;
   int              sourceContentLength           =  0;
   int              sourceContentRead             =  0;
   
   byte[]           byteSource                    =  null;
   
   string           filenameSource                =  null;
   string           sourceContentType             =  null;

   OleDbCommand     oleDbCommand                  =  null;
   OleDbConnection  oleDbConnection               =  null;
   OleDbParameter   oleDbParameter                =  null;
       
   Stream           streamSource                  =  null;
   
   try
   {
    streamSource         =  htmlInputFileSource.PostedFile.InputStream;
    sourceContentLength  =  htmlInputFileSource.PostedFile.ContentLength;
    sourceContentType    =  htmlInputFileSource.PostedFile.ContentType;
    filenameSource       =  htmlInputFileSource.Value;
    byteSource           =  new byte[ sourceContentLength ];
    
    /*
    TypeCode typeCode = Type.GetTypeCode( byteSource.GetType() );
    httpContext.Response.Write( typeCode.ToString() );
    */

    sourceContentRead    =  streamSource.Read( byteSource, 0, sourceContentLength );

    oleDbConnection      =  UtilityDatabase.DatabaseConnectionInitialize
                            ( 
                              databaseConnectionString, 
                              ref exceptionMessage 
                            );
      
    oleDbCommand         =  new OleDbCommand
                            (
                             "usp_ImageCarbonFormUpdate",
                             oleDbConnection
                            );
                            
    oleDbCommand.CommandType  =  CommandType.StoredProcedure;
                            
    oleDbParameter       =  new OleDbParameter( "@URIImage", OleDbType.VarChar, 255 );
    oleDbParameter.Value =  filenameSource;
    oleDbCommand.Parameters.Add( oleDbParameter );
    
    oleDbParameter       =  new OleDbParameter( "@ImageType", OleDbType.VarChar, 255 );
    oleDbParameter.Value =  sourceContentType;
    oleDbCommand.Parameters.Add( oleDbParameter );

    oleDbParameter       =  new OleDbParameter( "@ImageCarbonFormMatch", OleDbType.Binary );
    oleDbParameter.Value =  byteSource;
    oleDbCommand.Parameters.Add( oleDbParameter );

    databaseNumberOfRowsAffected = oleDbCommand.ExecuteNonQuery();
    
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )
   finally
   {
    if ( oleDbConnection != null )
    {
   	 oleDbConnection.Close();
   	}//if ( oleDbConnection != null ) 
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
     
  }//public static void DatabaseUpdate()
  
  ///<summary>FileUploadSource</summary>
  public static void FileUploadByte
  (
   ref FileUpload  fileUpload,
   ref byte[]      byteFileContent,
   ref string      exceptionMessage
  )
  {
   HttpContext       httpContext  =  HttpContext.Current;
   System.IO.Stream  stream       =  null;
   try
   {
    if ( fileUpload.HasFile )
    {
     byteFileContent    =  new byte[fileUpload.PostedFile.ContentLength];
     //stream  =  fileUpload.PostedFile.InputStream;
     stream  =  fileUpload.FileContent;
     stream.Read( byteFileContent, 0, fileUpload.PostedFile.ContentLength );
     //Convert byte[] to string 
     //imageContent       =  ( new System.Text.ASCIIEncoding()).GetString( byteFileContent );
    }//if ( fileUpload.HasFile )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
  }
 }//public class UtilityImage
}//namespace WordEngineering