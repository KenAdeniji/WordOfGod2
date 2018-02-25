using System;
using System.ComponentModel;
using System.Data;
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
using System.Threading;
using System.Web;
using System.Web.Mail; //1.0
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace WordEngineering
{

 /// <summary>UtilityMailArgument</summary>
 public class UtilityMailArgument
 {
  ///<summary>smtpPort</summary>  
  public int      smtpPort          =  UtilityMail.SmtpPort;

  ///<summary>smtpServer</summary>  
  public string   smtpServer        =  UtilityMail.SmtpServer;

  ///<summary>from</summary>  
  public string   from              =  null;

  ///<summary>to</summary>  
  public string   to                =  null;

  ///<summary>cc</summary>  
  public string   cc                =  null;

  ///<summary>bcc</summary>  
  public string   bcc               =  null;

  ///<summary>subject</summary>  
  public string   subject           =  null;

  ///<summary>body</summary>  
  public string   body               =  null;

  ///<summary>attachment</summary>  
  public String[] attachment         =  null;

  ///<summary>The userState can be any object that allows your callback method to identify this send operation. For this example, the userToken is a string constant.</summary>
  public string   userState          =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor.</summary>
  public UtilityMailArgument()
  {
  }//public UtilityMailArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityMailArgument
  (
   int      smtpPort,
   string   smtpServer,
   string   from,
   string   to,
   string   cc,
   string   bcc,
   string   subject,
   string   body,
   string[] attachment,
   string   userState
  )
  {
   if ( smtpPort < 0 )
   {
    smtpPort = UtilityMail.SmtpPort;
   }

   if ( string.IsNullOrEmpty( smtpServer ) )
   {
    smtpServer = UtilityMail.SmtpServer; //Environment.MachineName;
   }

   if ( string.IsNullOrEmpty( from ) )
   {
    from = Environment.UserName;
   }

   if ( string.IsNullOrEmpty( to ) )
   {
    to = from;
   }

   if ( string.IsNullOrEmpty( subject ) )
   {
    subject = "Subject";
   }

   if ( string.IsNullOrEmpty( body ) )
   {
    body = "Body";
   }
   
   if ( string.IsNullOrEmpty( userState ) )
   {
   	userState = UtilityMail.UserState;
   }
   
   this.smtpPort     =  smtpPort;
   this.smtpServer   =  smtpServer;
   this.from         =  from;
   this.to           =  to;
   this.cc           =  cc;
   this.bcc          =  bcc;
   this.subject      =  subject;
   this.body         =  body;   
   this.attachment   =  attachment;
   this.userState    =  userState;
   
  }//public UtilityMailArgument()

 }//public class UtilityMailArgument

 ///<summary>UtilityMail</summary>
 ///<remarks>
 /// Server Port:                              110                  
 /// Directory Root Mail Directory:            \InetPub\MailRoot\Mailbox
 /// Mail Root Drop Directory:                 \Inetpub\MailRoot\Drop
 /// WinPop.exe                                POP3 Service administration utility.
 /// TelNet smtp_name port_number              Telnet localhost 25
 /// TelNet pop3_name port_number              Telnet localhost 110
 /// WinPop Always create an associated user for new mailboxes (Uncheck)
 /// WinPop Add domain                         WinPop Add localhost
 /// WinPop Add user@domainname                WinPop Add Administrator@localhost
 /// Web Interface for message                 http://ComputerName:8090
 /// Web Interface for Remote Administration   https://ComputerName:8098
 /// http://msdn2.microsoft.com/library/4971yhhc(en-us,vs.80).aspx
 /// http://forums.asp.net/314034/ShowPost.aspx Smtp server and email FAQ
 /// http://www.systemwebmail.com Dave Wanta
 /// e.Headers.Add("Reply-To", "name@contoso.com")
 /// e.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");	//basic authentication
 /// e.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "my_username_here"); //set your username here
 /// e.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "super_secret");	//set your password here
 /// cdosys.dll cdonts.dll
 /// TELNET ServerNameSMTP PortNumber
 /// HELO ServerNamePOP3
 /// MAIL FROM:Administrator@ServerNameSMTP
 /// RCPT TO: User@ServerNamePOP3
 /// DATA
 /// Subject: Subject
 /// QUIT
 /// TELNET ServerNamePOP3 PortNumber
 /// USER <username>
 /// PASS <password>
 /// STAT
 /// LIST
 /// RETR <message number>
 /// DELE <message number>
 /// NOOP
 /// RSET
 /// QUIT
 /// TOP <message number> <number of lines>
 /// UIDL <message number> 
 ///</remarks>
 public class UtilityMail
 {

  /// <summary>mailSent</summary>
  public  static bool mailSent = false;
        
  /// <summary>Port POP3 110.</summary>
  public static int PortPOP3 = 110;

  /// <summary>SizeBuffer</summary>
  public static int SizeBuffer = 1024;

  /// <summary>Port Smtp 110.</summary>
  public static int SmtpPort = 25;

  /// <summary>Ready</summary>
  public static String DataReady = "ready";

  /// <summary>+OK</summary>
  public static String DataOK    = "+OK";

  /// <summary>MessageList</summary>
  public static String MessageList  = "list";

  /// <summary>MessageQuit</summary>
  public static String MessageQuit  = "quit";

  /// <summary>MessagePassword</summary>
  public static String MessagePassword  = "pass {0}";

  /// <summary>MessageUser</summary>
  public static String MessageUser  = "user {0}";

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>MailBoxDefault</summary>
  public static  String     MailBoxDefault                     = @"administrator";

  /// <summary>SmtpServer localhost</summary>
  public static  String     SmtpServer                         = @"localhost";  //Environment.MachineName;

  /// <summary>ServerNamePOP3.</summary>
  public static  String     ServerNamePOP3                     = @"localhost";  //Environment.UserDomainName;

  /// <summary>UserState</summary>
  public static  string     UserState                          = "UserState";

  /// <summary>The XPath database connection String.</summary>
  public static  String     XPathDatabaseConnectionString      = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>The XPath Port</summary>
  public static  String     XPathPort                          = @"/word/mail/smtp/port";

  /// <summary>The XPath server</summary>
  public static  String     XPathServer                        = @"/word/mail/smtp/server";

  /// <summary>Constructor.</summary>
  public UtilityMail()
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
   string                        exceptionMessage                  =  null;     
   UtilityMailArgument           utilityMailArgument               =  null;
   
   utilityMailArgument = new UtilityMailArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityMailArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityMailArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   Stub
   (
    ref utilityMailArgument,
    ref exceptionMessage
   );
   
  }//static void Main( String[] argv ) 

  ///<summary>Stub.</summary>
  public static void Stub
  (
   ref UtilityMailArgument utilityMailArgument,
   ref string              exceptionMessage
  )
  {
   
   MailSend
   (
    ref utilityMailArgument,
    ref exceptionMessage
   );

   //MailReceive();
   
  }//public static void Stub()

  /*
  public static void MailSend
  (
   ref String    SmtpServer,
   ref String    From,
   ref String    To,
   ref String    Cc,
   ref String    Bcc,
   ref String    Subject,
   ref String    Body,
   ref String[]  Attachment,
   ref String    exceptionMessage
  )
  {
   HttpContext     httpContext     =  HttpContext.Current;
   MailAttachment  mailAttachment  =  null;
   MailMessage          =  null;

   exceptionMessage                =  null;

   if ( string.IsNullOrEmpty( SmtpServer ) )
   {
    SmtpServer = "localhost"; //Environment.UserDomainName;
   }

   if ( string.IsNullOrEmpty( From ) )
   {
    From = Environment.UserName;
   }

   if ( string.IsNullOrEmpty( To ) )
   {
    To = From;
   }

   if ( string.IsNullOrEmpty( Subject ) )
   {
    Subject = "Subject: " + From + "@" + SmtpServer;
   }

   if ( string.IsNullOrEmpty( Body ) )
   {
    Body = "Body: " + From + "@" + SmtpServer;
   }

   try
   {
    SmtpMail.SmtpServer  =  SmtpServer;
              =  new MailMessage();

    .From     =  From;
    .To       =  To;
    .Cc       =  Cc;
    .Bcc      =  Bcc;
    .Subject  =  Subject;
    .Body     =  Body;

    if ( Attachment != null )
    {
     foreach( String AttachmentCurrent in Attachment )
     {
      if ( AttachmentCurrent == null || AttachmentCurrent.Trim() == String.Empty )
      {
       continue;
      }//if ( AttachmentCurrent == null || AttachmentCurrent.Trim() == String.Empty )
      mailAttachment = new MailAttachment( AttachmentCurrent );
      .Attachments.Add( mailAttachment );
     }//foreach( String AttachmentCurrent in Attachment )
    }//if ( Attachment != null )

    SmtpMail.Send(  );

   }//try
   catch( SocketException exception )
   {
    exceptionMessage = "SocketException: " + exception.Message;
   }   	
   catch( System.Web.HttpException exception )
   {
    exceptionMessage = "HttpException: " + exception.Message;
   }   	
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
     httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
  }//public static void MailSend()
  */
  
  ///<summary>MailSend.</summary>
  public static void MailSend
  (
   ref UtilityMailArgument  utilityMailArgument,
   ref String               exceptionMessage
  )
  {

   string                          keyboardEntry       =  null;

   HttpContext                     httpContext         =  HttpContext.Current;
 
   MailAddress                     mailAddressBcc      =  null;
   MailAddress                     mailAddressCc       =  null;
   MailAddress                     mailAddressFrom     =  null;
   MailAddress                     mailAddressTo       =  null;
   System.Net.Mail.Attachment      mailAttachment      =  null;
   System.Net.Mail.MailMessage     mailMessage         =  null;
   SmtpClient                      smtpClient          =  null;

   exceptionMessage                =  null;

   try
   {
    
    smtpClient            =  new SmtpClient( utilityMailArgument.smtpServer );
    
    if ( !String.IsNullOrEmpty( utilityMailArgument.bcc ) )
    {
     mailAddressBcc       =  new MailAddress( utilityMailArgument.bcc );
    }//if ( !String.IsNullOrEmpty( utilityMailArgument.bcc ) )

    if ( !String.IsNullOrEmpty( utilityMailArgument.cc ) )
    {
     mailAddressCc       =  new MailAddress( utilityMailArgument.cc );
    }//if ( !String.IsNullOrEmpty( utilityMailArgument.cc ) )
    
    mailAddressFrom       =  new MailAddress( utilityMailArgument.from );
    
    mailAddressTo         =  new MailAddress( utilityMailArgument.to );
    
    mailMessage           =  new System.Net.Mail.MailMessage
    (
     mailAddressFrom,
     mailAddressTo
    );
    
    if ( mailAddressBcc != null )
    {
     mailMessage.Bcc.Add( mailAddressBcc );	
    }//if ( mailAddressBcc != null )	

    if ( mailAddressCc != null )
    {
     mailMessage.Bcc.Add( mailAddressCc );	
    }//if ( mailAddressCc != null )	

    mailMessage.Subject   =  utilityMailArgument.subject;
    mailMessage.Body      =  utilityMailArgument.body;

    if ( utilityMailArgument.attachment != null )
    {
     foreach( String attachment in utilityMailArgument.attachment )
     {
      if ( !string.IsNullOrEmpty( attachment ) )
      {
       mailAttachment = new System.Net.Mail.Attachment( attachment );
       mailMessage.Attachments.Add( mailAttachment );
      }//if ( !string.IsNullOrEmpty( attachment ) )
     }//foreach( String attachment in utilityMailArgument.attachment )
    }//if ( utilityMailArgument.attachment != null )

    if ( httpContext != null )
    {
     smtpClient.Send
     (
      mailMessage 
     );
    }//if ( httpContext != null )
    else
    {        	
     // Set the method that is called back when the send operation ends.
     smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
    
     smtpClient.SendAsync
     (
      mailMessage, 
      utilityMailArgument.userState
     );
    
     System.Console.WriteLine("Sending message... press c to cancel mail. Press any other key to continue.");
     keyboardEntry = Console.ReadLine();
     
     // If the user canceled the send, and mail hasn't been sent yet,
     // then cancel the pending operation.
     if ( keyboardEntry.StartsWith("c") && mailSent == false )
     {
      smtpClient.SendAsyncCancel();
     }//if ( keyboardEntry.StartsWith("c") && mailSent == false )
     
    }//if ( httpContext == null )  
   }//try
   catch( SmtpException exception ) { UtilityException.ExceptionLog( exception, "SmtpException", ref exceptionMessage ); }
   catch( SocketException exception ) { UtilityException.ExceptionLog( exception, "SocketException", ref exceptionMessage ); }
   catch( System.Web.HttpException exception ) { UtilityException.ExceptionLog( exception, "System.Web.HttpException", ref exceptionMessage ); }
   catch( Exception exception ) { UtilityException.ExceptionLog( exception, "Exception", ref exceptionMessage ); }
   finally
   {
    mailMessage.Dispose();
    //smtpClient.Dispose();
   }
  }//public static void MailSend()

  ///<summary>MailSend</summary>
  ///<remarks>
  /// Scott Mitchell AspNet.4GuysFromRolla.com/articles/102203-1.aspx Enhancing the 'Email the Rendered Output of an ASP.NET Web Control' Code 
  /// Scott Mitchell AspNet.4GuysFromRolla.com/articles/091102-1.aspx Emailing the Rendered Output of an ASP.NET Web Control 
  ///</remarks>  
  public static void MailSend
  (
       System.Web.UI.Page               page,
   ref string                           emailRecipient,
   ref string                           exceptionMessage
  )
  {
   HttpContext                     httpContext      =  HttpContext.Current;
   HtmlTextWriter                  htmlTextWriter   =  null;
   MailAddress                     mailAddressFrom  =  null; 
   MailAddress                     mailAddressTo    =  null; 
   System.Net.Mail.MailMessage     mailMessage      =  null;
   SmtpClient                      smtpClient       =  null;
   StringBuilder                   stringBuilder    =  null;
   StringWriter                    stringWriter     =  null;
   if ( httpContext == null ) { return; }
   try
   {
    smtpClient              =  new SmtpClient();
    mailAddressFrom         =  new MailAddress( "administrator@LocalHost" );
    mailAddressTo           =  new MailAddress( emailRecipient );
    mailMessage             =  new System.Net.Mail.MailMessage( mailAddressFrom, mailAddressTo );
    mailMessage.IsBodyHtml  =  true;
    stringBuilder           =  new StringBuilder();
    stringWriter            =  new StringWriter( stringBuilder );
    htmlTextWriter          =  new HtmlTextWriter( stringWriter );
    page.RenderControl( htmlTextWriter );
    mailMessage.Body        =  stringBuilder.ToString();
    smtpClient.Send( mailMessage );
   }
   catch( SmtpException exception ) { UtilityException.ExceptionLog( exception, "SmtpException", ref exceptionMessage ); }
   catch( SocketException exception ) { UtilityException.ExceptionLog( exception, "SocketException", ref exceptionMessage ); }
   catch( System.Web.HttpException exception ) { UtilityException.ExceptionLog( exception, "System.Web.HttpException", ref exceptionMessage ); }
   catch( Exception exception ) { UtilityException.ExceptionLog( exception, "Exception", ref exceptionMessage ); }
   finally
   {
    if ( mailMessage    != null )  { mailMessage.Dispose(); }
    if ( htmlTextWriter != null )  { htmlTextWriter.Close(); }
    if ( stringWriter   != null )  { stringWriter.Close(); }
   }//finally
   httpContext.Server.Transfer( httpContext.Request.ServerVariables["PATH_INFO"] );
  }//MailSend()
  
  ///<summary>SendCompletedCallback</summary>
  public static void SendCompletedCallback
  ( 
   object                   sender, 
   AsyncCompletedEventArgs  e
  )
  {
   // Get the unique identifier for this asynchronous operation.
   String token = (string) e.UserState;
           
   if ( e.Cancelled )
   {
    System.Console.WriteLine("[{0}] Send canceled.", token);
   }
   
   if ( e.Error != null )
   {
    System.Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
   } 
   else
   {
    System.Console.WriteLine("Message sent.");
   }
   
   mailSent = true;
  }

  ///<summary>MailReceive.</summary>
  public static void MailReceive()
  {
   int       port              =  PortPOP3;

   String    domainName        =  null;
   String    exceptionMessage  =  null;
   String    password          =  null;
   String    serverNamePOP3    =  ServerNamePOP3;
   String    username          =  null;

   UtilityImpersonate.GetUsernamePasswordDomainName
   (
    ref  FilenameConfigurationXml,
    ref  username,
    ref  domainName,
    ref  password,
    ref  exceptionMessage
   );

   MailReceive
   (
    ref serverNamePOP3,
    ref port,
    ref username,
    ref password,
    ref exceptionMessage
   );

  }//public static void MailReceive()

  ///<summary>MailReceive.</summary>
  public static void MailReceive
  (
   ref String    serverNamePOP3,
   ref int       port,
   ref String    username,
   ref String    password,
   ref String    exceptionMessage
  )
  {

   int             responseCheckFlag  =  -1;
   HttpContext     httpContext        =  HttpContext.Current;
   NetworkStream   networkStream      =  null;
   StringBuilder   sbResponse         =  null;
   TcpClient       tcpClient          =  null;

   try
   {
    UtilityTcpIp.TcpClient
    (
     ref serverNamePOP3,
     ref port,
     ref tcpClient,
     ref exceptionMessage
    );

    networkStream = tcpClient.GetStream();

    //Connect
    UtilityTcpIp.Read
    (
     ref networkStream,
     ref sbResponse,
     ref exceptionMessage
    );

    ResponseCheck
    ( 
     ref sbResponse,
     ref responseCheckFlag
    );

    if ( responseCheckFlag != 0 )
    {
     return;
    }//if ( responseCheckFlag != 0 )

    SendUser
    (
     ref networkStream,
     ref username,
     ref sbResponse,
     ref responseCheckFlag,
     ref exceptionMessage
    );

    /*
    if ( responseCheckFlag != 0 )
    {
     return;
    }//if ( responseCheckFlag != 0 )
    */

    SendPassword
    (
     ref networkStream,
     ref password,
     ref sbResponse,
     ref responseCheckFlag,
     ref exceptionMessage
    );

    /*
    if ( responseCheckFlag != 0 )
    {
     return;
    }//if ( responseCheckFlag != 0 )
    */

    SendList
    (
     ref networkStream,
     ref sbResponse,
     ref responseCheckFlag,
     ref exceptionMessage
    );

    /*
    if ( responseCheckFlag != 0 )
    {
     return;
    }//if ( responseCheckFlag != 0 )
    */
  
    SendQuit
    (
     ref networkStream,
     ref sbResponse,
     ref responseCheckFlag,
     ref exceptionMessage
    );

   }//try
   catch( SocketException exception )
   {
    exceptionMessage = "SocketException: " + exception.Message;
   }   	
   catch( System.Web.HttpException exception )
   {
    exceptionMessage = "HttpException: " + exception.Message;
   }   	
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
     httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )

   if ( tcpClient != null )
   {
    tcpClient.Close();
   }//if ( tcpClient != null )

  }//public static void MailReceive

  /// <summary>EmailClient()</summary>
  /// <remarks>http://www.publicjoe.f9.co.uk/csharp/snip/snip007.html</remarks>
  public static void EmailClient()
  {
   string toEmail = "administrator@LocalHost";
   string subject = "Subject";
   string body    = "Body";
   string message = string.Format( "mailto:{0}?subject={1}&body={2}", toEmail, subject, body );

   Process.Start( message ); 
  }//public static void EmailClient() 

  /// <summary>ResponseCheck</summary>
  public static void ResponseCheck
  (
   ref StringBuilder sbResponse,
   ref int           responseCheckFlag
  )
  {  
   String response    =  null;

   responseCheckFlag  =  -1;
   response           =  sbResponse.ToString();
   response           =  response.Trim();

   if ( response != null && response != String.Empty )
   {
    responseCheckFlag  =  String.Compare( response.Substring(0,3), DataOK, true ) ;
   }//if ( response != null )

  }//public static int ResponseCheck

  /// <summary>SendList</summary>
  public static void SendList
  (
   ref NetworkStream  networkStream,
   ref StringBuilder  sbResponse,
   ref int            responseCheckFlag,
   ref String         exceptionMessage
  )
  {

   UtilityTcpIp.Write
   (
    ref networkStream,
    ref MessageList,
    ref exceptionMessage
   );

   UtilityTcpIp.Read
   (
    ref networkStream,
    ref sbResponse,
    ref exceptionMessage
   );

   ResponseCheck
   ( 
    ref sbResponse,
    ref responseCheckFlag
   );

  }//public static void SendList

  /// <summary>SendPassword</summary>
  public static void SendPassword
  (
   ref NetworkStream  networkStream,
   ref String         password,
   ref StringBuilder  sbResponse,
   ref int            responseCheckFlag,
   ref String         exceptionMessage
  )
  {
   String  message  =  null;

   message = String.Format( MessagePassword, password);

   UtilityTcpIp.Write
   (
    ref networkStream,
    ref message,
    ref exceptionMessage
   );

   /*
   UtilityTcpIp.Read
   (
    ref networkStream,
    ref sbResponse,
    ref exceptionMessage
   );

   ResponseCheck
   ( 
    ref sbResponse,
    ref responseCheckFlag
   );

   */

  }//public static void SendPassword

  /// <summary>SendUser</summary>
  public static void SendUser
  (
   ref NetworkStream  networkStream,
   ref String         username,
   ref StringBuilder  sbResponse,
   ref int            responseCheckFlag,
   ref String         exceptionMessage
  )
  {
   String  message  =  null;

   message = String.Format( MessageUser, username);

   UtilityTcpIp.Write
   (
    ref networkStream,
    ref message,
    ref exceptionMessage
   );

   /*
   UtilityTcpIp.Read
   (
    ref networkStream,
    ref sbResponse,
    ref exceptionMessage
   );

   ResponseCheck
   ( 
    ref sbResponse,
    ref responseCheckFlag
   );
   */

  }//public static void SendUser

  /// <summary>SendQuit</summary>
  public static void SendQuit
  (
   ref NetworkStream  networkStream,
   ref StringBuilder  sbResponse,
   ref int            responseCheckFlag,
   ref String         exceptionMessage
  )
  {

   UtilityTcpIp.Write
   (
    ref networkStream,
    ref MessageQuit,
    ref exceptionMessage
   );

  }//public static void SendQuit

  ///<summary>SmtpServiceStatus</summary>
  public static void SmtpServiceStatus
  (
   ref UtilityMailArgument  utilityMailArgument,
   ref string               exceptionMessage
  )
  {
   int            bytes          =  -1;
   //string         message        =  null;
   string         response       =  null;
   
   Byte[]         data;
   NetworkStream  networkStream  =  null;
   TcpClient      tcpClient      =  null;

   try
   {
    tcpClient      =  new TcpClient( utilityMailArgument.smtpServer, utilityMailArgument.smtpPort );
    networkStream  =  tcpClient.GetStream();
    data           =  new Byte[ SizeBuffer ];
    // Read the first batch of the TcpServer response bytes.
    bytes          =  networkStream.Read( data, 0, SizeBuffer );
    response       =  Encoding.ASCII.GetString( data, 0, bytes );
    if ( response.Substring(0, 3) != "220" )
    {
     return;
    }
    System.Console.WriteLine( response );
   }//try
   catch ( ArgumentNullException exception ) { UtilityException.ExceptionLog( exception, "ArgumentNullException", ref exceptionMessage ); }
   catch ( ArgumentOutOfRangeException exception ) { UtilityException.ExceptionLog( exception, "ArgumentOutOfRangeException", ref exceptionMessage ); }
   catch ( SocketException exception ) { UtilityException.ExceptionLog( exception, "SocketException", ref exceptionMessage ); }
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, "Exception", ref exceptionMessage ); }
   finally
   {
    if ( tcpClient != null )
    {
     tcpClient.Close();
    }//if ( tcpClient != null )
   }//finally
  }//public static void SmtpServiceStatus()
  
  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   string exceptionMessage = null;
   
   ConfigurationXml
   (
        FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString,
    ref SmtpPort,
    ref SmtpServer
   );
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml
  (
       string filenameConfigurationXml,
   ref string exceptionMessage,
   ref string databaseConnectionString,
   ref int    smtpPort,
   ref string smtpServer
  )
  {
   try
   {
    UtilityXml.XmlDocumentNodeInnerText
    (
          filenameConfigurationXml,
      ref exceptionMessage,
          XPathDatabaseConnectionString,
      ref databaseConnectionString
    );
    UtilityXml.GetNodeValue
    (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathPort,
     ref smtpPort
    );
    UtilityXml.GetNodeValue
    (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathServer,
     ref smtpServer
    );
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, "Exception", ref exceptionMessage ); }
  }//ConfigurationXml	 
  
  static UtilityMail()
  {
   ConfigurationXml();
  }//static UtilityMail()
  
 }//public class UtilityMail
 
}//namespace WordEngineering