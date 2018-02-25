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

namespace WordEngineering
{

 /*
  http://www.systemwebmail.com/faq/3.8.aspx 
  http://www.sunny-beach.net/manual/591.htm 
  http://www.codeproject.com/dotnet/SystemWeb_Mail_SMTP_AUTH.asp 

  //set your username here
  if (bAuthenticationRequested)
  {
   objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication       
   objMailMessage.Fields.Add(   "http://schemas.microsoft.com/cdo/configuration/sendusername", "Daniel"); 
   //set your password here
   objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "adegoke");
   }
 */
   
 /// <summary>UtilitySimpleMailTransferProtocolSMTP http://www.ietf.org/rfc/rfc0821.txt</summary>
 /// <remarks>UtilitySimpleMailTransferProtocolSMTP http://www.ietf.org/rfc/rfc0821.txt</remarks>
 [Serializable]
 [XmlRoot("UtilitySimpleMailTransferProtocolSMTP", IsNullable = false)] 
 public class UtilitySimpleMailTransferProtocolSMTP : System.Net.Sockets.TcpClient
 {

  /// <summary>Port 25 is the well-known port for SMTP servers.</summary>
  public const int PortSMTP = 25;

  /// <summary>The database connection string.</summary>
  public static string  DatabaseConnectionString                    = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static string FilenameConfigurationXml       = @"WordEngineering.config";

  /// <summary>NNTP servers for example, microsoft.public.access</summary>
  public static string   NewsGroup = "microsoft.public.access";

  /// <summary>NNTP servers for example, news.microsoft.com, nntp://news.microsoft.com, news.devx.com.</summary>
  public static string   NewsServer = "news.microsoft.com";

  /// <summary>The XPath database connection string.</summary>
  public const string   XPathDatabaseConnectionString = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";
  
  /// <summary>The XPath news server.</summary>
  public const string   XPathNewsGroup = @"/word/networkNewsTransferProtocolNNTP/newsGroup";

  /// <summary>The XPath news server.</summary>
  public const string   XPathNewsServer = @"/word/networkNewsTransferProtocolNNTP/newsServer";

  /// <summary>bodyText.</summary>
  public string bodyText = null;

  /// <summary>bodyHTML.</summary>
  public string bodyHtml = null;

  /// <summary>from.</summary>
  public string from = null;

  /// <summary>password.</summary>
  public string password = "adegoke";

  /// <summary>subject.</summary>
  public string subject = null;

  /// <summary>server.</summary>
  public string server = null;

  /// <summary>username.</summary>
  public string username = "Daniel";

  /// <summary>to.</summary>
  public ArrayList to;

  /// <summary>cc.</summary>
  public ArrayList cc;

  /// <summary>bcc.</summary>
  public ArrayList bcc;

  /// <summary>ASCIIEncoding.</summary>
  public static System.Text.ASCIIEncoding aSCIIEncoding = null;
 
  ///<summary>ConfigurationFile().</summary>
  public static void ConfigurationFile()
  {

   string databaseConnectionString  =  null;
   string exceptionMessage          = null;
   string newsServer                = null;
   string newsGroup                 = null;

   UtilityXml.XmlDocumentNodeInnerText
   (
         FilenameConfigurationXml,
     ref exceptionMessage,         
         XPathNewsServer,
     ref databaseConnectionString
   );
   if ( databaseConnectionString != null && databaseConnectionString != String.Empty )
   {
    DatabaseConnectionString = databaseConnectionString;
   } 

   UtilityXml.XmlDocumentNodeInnerText
   (
         FilenameConfigurationXml,
     ref exceptionMessage,         
         XPathNewsServer,
     ref newsServer
   );
   if ( newsServer != null && newsServer != String.Empty )
   {
    NewsServer = newsServer;
   } 

   UtilityXml.XmlDocumentNodeInnerText
   (
         FilenameConfigurationXml,
     ref exceptionMessage,         
         UtilitySimpleMailTransferProtocolSMTP.XPathNewsGroup,
     ref newsGroup
   );
   if ( newsGroup != null && newsGroup != String.Empty )
   {
    NewsGroup = newsGroup;
   } 
     	
  }//ConfigurationFile().

  ///<summary>Constructor.</summary>
  public UtilitySimpleMailTransferProtocolSMTP()
  {
   to = new ArrayList();
   cc = new ArrayList();
   bcc = new ArrayList();
  }//public UtilitySimpleMailTransferProtocolSMTP()

  ///<summary>Static.</summary>
  static UtilitySimpleMailTransferProtocolSMTP()
  {
   aSCIIEncoding = new System.Text.ASCIIEncoding();
  }//static UtilitySimpleMailTransferProtocolSMTP()

  ///<summary>Send().</summary>
  public void Send()
  {
   string message;
   string response;
   Connect(server, PortSMTP);
   response = Response();
   if (response.Substring(0, 3) != "220")
   {
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   };

   message = "HELO me\r\n";
   Write(message);
   response = Response();
   if (response.Substring(0, 3) != "250")
   {
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }

   /*
   message = "VRFY " + username + "\r\n"; 
   Write(message); 
   response = Response(); 
   if (response.Substring(0, 3) != "+OK") 
   { 
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }
   */ 
   
   /*
   message = "PASS " + password + "\r\n"; 
   Write(message); 
   response = Response(); 
   if (response.Substring(0, 3) != "+OK") 
   { 
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }
   */
   
   message = "MAIL FROM:<" + from + ">\r\n";
   Write(message);
   response = Response();
   if (response.Substring(0, 3) != "250")
   {
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }
   foreach ( string address in to )
   {
    try
    {
     message = "RCPT TO:<" + address + ">\r\n";
     Write(message);
     response = Response();
     if (response.Substring(0, 3) != "250")
     {
      throw new UtilitySimpleMailTransferProtocolSMTPException(response);
     }
    }
    catch( UtilitySimpleMailTransferProtocolSMTPException e)
    {
     System.Console.WriteLine("{0}", e.What());
    }
   }

   foreach ( string address in cc )
   {
    try
    {
     message = "RCPT TO:<" + address + ">\r\n";
     Write(message);
     response = Response();
     if (response.Substring(0, 3) != "250")
     {
      throw new UtilitySimpleMailTransferProtocolSMTPException(response);
     }
    }
    catch( UtilitySimpleMailTransferProtocolSMTPException e)
    {
     System.Console.WriteLine("{0}", e.What());
    }
   }
   foreach ( string address in bcc )
   {
    try
    {
     message = "RCPT TO:<" + address + ">\r\n";
     Write(message);
     response = Response();
     if (response.Substring(0, 3) != "250")
     {
      throw new UtilitySimpleMailTransferProtocolSMTPException(response);
     }
    }
    catch( UtilitySimpleMailTransferProtocolSMTPException e)
    {
     System.Console.WriteLine("{0}", e.What());
    }
   }
   message = "DATA\r\n";
   Write(message);
   response = Response();
   if (response.Substring(0, 3) != "354")
   {
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }
   message = "Subject: " + subject + "\r\n";
   foreach ( string address in to )
   {
    message += "To: " + address + "\r\n";
   }
   foreach ( string address in cc )
   {
    message += "Cc: " + address + "\r\n";
   }
   message += "From: " + from + "\r\n";
   if (bodyHtml.Length > 0)
   {
    message += "MIME-Version: 1.0\r\n" +
    "Content-Type: text/html;\r\n" +
    " charset=\"iso-8859-1\"\r\n";
    message += "\r\n" + bodyHtml; 
   }
   else
   {
    message += "\r\n" + bodyText; 
   };
   message += "\r\n.\r\n";
   Write(message);
   response = Response();
   if (response.Substring(0, 3) != "250")
   {
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }
   message = "QUIT\r\n";
   Write(message);
   response = Response();
   if (response.IndexOf("221") == -1)
   {
    throw new UtilitySimpleMailTransferProtocolSMTPException(response);
   }
  }//Send()
  
  ///<summary>Write().</summary>
  ///<param name="message">Message.</param>  
  public void Write
  (
   string message
  )
  {
   byte[] WriteBuffer = new byte[1024] ;
   WriteBuffer = aSCIIEncoding.GetBytes(message) ;
   NetworkStream stream = GetStream() ;
   stream.Write(WriteBuffer,0,WriteBuffer.Length);
  }//public void Write()

  ///<summary>Response().</summary>
  public string Response()
  {
   byte []serverbuff = new Byte[1024];
   NetworkStream stream = GetStream();
   int count = stream.Read( serverbuff, 0, 1024 ); 
   if (count == 0)
   {
    return "";
   }
   return aSCIIEncoding.GetString( serverbuff, 0, count ); 
  }//public string Response()
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main
  (
   string[] argv
  )
  {
   MailClient();
  }//public static void Main(string[] argv)
  
  ///<summary>MailClient().</summary>
  public static void MailClient()
  {
   //StringBuilder sbResponse = null;

   ConfigurationFile();
   try
   {
    UtilitySimpleMailTransferProtocolSMTP  utilitySimpleMailTransferProtocolSMTP = null;
    
    utilitySimpleMailTransferProtocolSMTP = new UtilitySimpleMailTransferProtocolSMTP();
    utilitySimpleMailTransferProtocolSMTP.server   = "smtp.ephraimtech.com";
    utilitySimpleMailTransferProtocolSMTP.server   = "harvest.ephraimtech.com";
    utilitySimpleMailTransferProtocolSMTP.from     = "kenadeniji@hotmail.com";
    utilitySimpleMailTransferProtocolSMTP.subject  = "Hello World";
    utilitySimpleMailTransferProtocolSMTP.bodyHtml = "<HTML><BODY>Hello World</BODY></HTML>";
    utilitySimpleMailTransferProtocolSMTP.to.Add( "kenadeniji@hotmail.com" );
    utilitySimpleMailTransferProtocolSMTP.Send();
   }
   catch( UtilitySimpleMailTransferProtocolSMTPException e)
   {
    System.Console.WriteLine("{0}", e.What());
   }
  }//public static void MailClient()

 }//UtilitySimpleMailTransferProtocolSMTP : System.Net.Sockets.TcpClient 
 
 /// <summary>UtilitySimpleMailTransferProtocolSMTPException.</summary>
 /// <remarks>UtilitySimpleMailTransferProtocolSMTPException.</remarks>
 [Serializable]
 [XmlRoot("UtilitySimpleMailTransferProtocolSMTPException", IsNullable = false)] 
 public class UtilitySimpleMailTransferProtocolSMTPException : System.Exception
 {
  private string message;
  
  ///<summary>Constructor.</summary>
  public UtilitySimpleMailTransferProtocolSMTPException(string str) 
  {
   message = str;
  }

  ///<summary>What().</summary>  
  public string What()
  {
   return message;
  }
 };//public class UtilitySimpleMailTransferProtocolSMTPException : System.Exception
 
}//namespace WordEngineering 