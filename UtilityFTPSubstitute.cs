using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace WordEngineering
{

 /// <summary>UtilityFTPArgument</summary>
 public class UtilityFTPArgument
 {
  ///<summary>filenameSource</summary>  
  public string   filenameSource        =  null;

  ///<summary>uriTarget</summary>  
  public string   uriTarget              =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityFTPArgument():this
  (
   UtilityFTP.FilenameSource,
   UtilityFTP.URITarget
  ) 
  {
  }//public UtilityFTPArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityFTPArgument
  (
   string   filenameSource,
   string   uriTarget
  )
  {
   if ( filenameSource == null || filenameSource == String.Empty )
   {
    filenameSource = UtilityFTP.FilenameSource;
   }

   if ( uriTarget == null || uriTarget == String.Empty )
   {
    uriTarget = UtilityFTP.URITarget;
   }

   this.filenameSource   =  filenameSource;
   this.uriTarget        =  uriTarget;
   
  }//public UtilityFTPArgument()

 }//public class UtilityFTPArgument

 /// <summary>FtpState</summary>
 public class FtpState
 {
  /// <summary>wait</summary>
  private ManualResetEvent wait;
  
  /// <summary>request</summary>
  private FtpWebRequest    request;
  
  /// <summary>fileName</summary>
  private string           fileName;
  
  /// <summary>operationException</summary>
  private Exception        operationException = null;
  
  /// <summary>status</summary>  
  string  status;
        
  /// <summary>Constructor.</summary>
  public FtpState()
  {
   wait = new ManualResetEvent( false );
  }//public FtpState()
        
  /// <summary>OperationComplete</summary>
  public ManualResetEvent OperationComplete
  {
   get { return wait; }
  }//public ManualResetEvent OperationComplete
        
  /// <summary>Request</summary>
  public FtpWebRequest Request
  {
   get {return request;}
   set {request = value;}
  }//public FtpWebRequest Request
        
  /// <summary>Request</summary>
  public string FileName
  {
   get {return fileName;}
   set {fileName = value;}
  }//public string FileName
        
  /// <summary>OperationException</summary>
  public Exception OperationException
  {
   get {return operationException;}
   set {operationException = value;}
  }//public Exception OperationException
 
  /// <summary>StatusDescription</summary>
  public string StatusDescription
  {
   get {return status;}
   set {status = value;}
  }//public string StatusDescription
  
 }//public class FtpState
 
 /// <summary>UtilityFTP</summary>
 public class UtilityFTP
 {  

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>FilenameSource</summary>
  public static  string     FilenameSource                     = @"d:\WordOfGod\UtilityFTPSubstitute.cs";

  /// <summary>URITarget</summary>
  public static  string     URITarget                          = @"ftp://localhost/WordOfGod/UtilityFTPSupplement.cs";

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
   UtilityFTPArgument            utilityFTPArgument                =  null;
   
   utilityFTPArgument = new UtilityFTPArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityFTPArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityFTPArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Argument FilenameSource: {0} | URITarget: {1}",
     utilityFTPArgument.filenameSource,
     utilityFTPArgument.uriTarget
    );
   #endif

   FTPUploadFile
   (
    ref utilityFTPArgument,
    ref exceptionMessage
   );
   
  }//Main  

  /// <summary>FTPUploadFile</summary>
  public static void FTPUploadFile
  (
   ref UtilityFTPArgument  utilityFTPArgument,
   ref string              exceptionMessage
  )
  {
   string            fileName;
   FtpWebRequest     request;
   HttpContext       httpContext  =  HttpContext.Current;
   ManualResetEvent  waitObject;
   Uri               target;
   FtpState          state;
           
   try
   {

    // Create a Uri instance with the specified URI string.
    // If the URI is not correctly formed, the Uri constructor
    // will throw an exception.
            
    target = new Uri ( utilityFTPArgument.uriTarget );

    fileName = utilityFTPArgument.filenameSource;
    
    state = new FtpState();
     
    request = ( FtpWebRequest ) WebRequest.Create( target );
    request.Method = WebRequestMethods.Ftp.UploadFile;
            
    // This example uses anonymous logon.
    // The request is anonymous by default; the credential does not have to be specified. 
    // The example specifies the credential only to
    // control how actions are logged on the server.
            
    request.Credentials = new NetworkCredential ("anonymous","janeDoe@contoso.com");
            
    // Store the request in the object that we pass into the
    // asynchronous operations.
    state.Request = request;
    state.FileName = fileName;
            
    // Get the event to wait on.
    waitObject = state.OperationComplete;
            
    // Asynchronously get the stream for the file contents.
    request.BeginGetRequestStream
    (
     new AsyncCallback (EndGetStreamCallback), 
     state
    );
            
    // Block the current thread until all operations are complete.
    waitObject.WaitOne();
            
    // The operations either completed or threw an exception.
    if ( state.OperationException != null )
    {
     throw state.OperationException;
    }
    else
    {
     System.Console.WriteLine("The operation completed - {0}", state.StatusDescription);
    }
    
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
     
  }//public static void FTPUploadFile()

  /// <summary>EndGetStreamCallback</summary>
  private static void EndGetStreamCallback
  (
   IAsyncResult ar
  )
  {

   byte[]      buffer;

   const int   bufferLength = 2048;
   int         count        = 0;
   int         readBytes    = 0;
    
   FileStream  stream;

   Stream      requestStream = null;
   FtpState    state;
    
   state = (FtpState) ar.AsyncState;
            
   // End the asynchronous call to get the request stream.
   try
   {

    requestStream = state.Request.EndGetRequestStream(ar);

    // Copy the file contents to the request stream.
    buffer = new byte[bufferLength];
    stream = File.OpenRead(state.FileName);
                
    do
    {
     readBytes = stream.Read(buffer, 0, bufferLength);
     requestStream.Write(buffer, 0, readBytes);
     count += readBytes;
    }
     
    while ( readBytes != 0 );
    System.Console.WriteLine ("Writing {0} bytes to the stream.", count);
     
    // IMPORTANT: Close the request stream before sending the request.
    requestStream.Close();
     
    // Asynchronously get the response to the upload request.
    state.Request.BeginGetResponse
    (
      new AsyncCallback (EndGetResponseCallback), 
      state
    );
   }//try 
   // Return exceptions to the main application thread.
   catch (Exception e)
   {
    System.Console.WriteLine("Could not get the request stream.");
    state.OperationException = e;
    state.OperationComplete.Set();
    return;
   }
  }//private static void EndGetStreamCallback()
        
  // The EndGetResponseCallback method  
  // completes a call to BeginGetResponse.
  /// <summary>EndGetResponseCallback</summary>
  private static void EndGetResponseCallback
  (
   IAsyncResult ar
  )
  {
   FtpWebResponse  response = null;
   FtpState        state;

   state = (FtpState) ar.AsyncState;

   try 
   {
    response = (FtpWebResponse) state.Request.EndGetResponse(ar);
    response.Close();
    state.StatusDescription = response.StatusDescription;
     
    // Signal the main application thread that 
    // the operation is complete.
    state.OperationComplete.Set();
   }
   // Return exceptions to the main application thread.
   catch ( Exception e )
   {
    System.Console.WriteLine ("Error getting response.");
    state.OperationException = e;
    state.OperationComplete.Set();
   }//catch ( Exception e )
  }//private static void EndGetResponseCallback()
 }//public class UtilityFTP
}//namespace WordEngineering