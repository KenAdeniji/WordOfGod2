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

 /// <summary>UtilitySimpleMailTransferProtocolSNMP</summary>
 /// <remarks>UtilitySimpleMailTransferProtocolSNMP</remarks>
 [Serializable]
 [XmlRoot("UtilitySimpleMailTransferProtocolSNMP", IsNullable = false)] 
 public class UtilitySimpleMailTransferProtocolSNMP : System.Net.Sockets.TcpClient
 {

  /// <summary>Port 119 is the well-known port for NNTP servers.</summary>
  public const int PortNNTP = 119;

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

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv) 
  {
   StringBuilder sbResponse = null;

   ConfigurationFile();
   
   if ( argv.Length > 0 )
   {
   	NewsServer = argv[0];
   }	

   if ( argv.Length > 1 )
   {
   	NewsGroup = argv[1];
   }	

   NewsClient
   ( 
    ref NewsServer,
    ref NewsGroup,
    ref sbResponse
   );	
  }//public static void Main(string[] args) 

  ///<summary>The news server.</summary>
  ///<param name="newsServer">NewsServer.</param>
  ///<param name="newsGroup">NewsGroup.</param>  
  ///<param name="sbResponse">Response.</param>
  public static void NewsClient
  (
   ref string        newsServer,
   ref string        newsGroup,
   ref StringBuilder sbResponse
  )  
  {
  	
   string                                 newsGroupCurrent                       = null;

   ArrayList                              newsGroupList                          = null;
   ArrayList                              news                                   = null;
   
   UtilityNetworkNewsTransferProtocolNNTP utilityNetworkNewsTransferProtocolNNTP = null;

   sbResponse = new StringBuilder();

   try
   {
    utilityNetworkNewsTransferProtocolNNTP = new UtilityNetworkNewsTransferProtocolNNTP(); 
    utilityNetworkNewsTransferProtocolNNTP.Connect(newsServer); 

    if ( newsGroup != null && newsGroup != String.Empty )
    {
     newsGroupList = new ArrayList();
     newsGroupList.Add( newsGroup );
    }
    else
    {
     newsGroupList = utilityNetworkNewsTransferProtocolNNTP.GetNewsgroups();
    }    	

    foreach(object arrayListCurrent in newsGroupList)
    {
     newsGroupCurrent = (String) arrayListCurrent;
     
     sbResponse.Append( "<b>NewsGroup:</b>" );
     sbResponse.Append( newsGroupCurrent );
     sbResponse.Append( "<br />" );
     System.Console.WriteLine("Newsgroup: {0}", newsGroupCurrent);
     news = utilityNetworkNewsTransferProtocolNNTP.GetNews(newsGroupCurrent);
     foreach (string newsCurrent in news)
     {
      sbResponse.Append( newsCurrent );
      sbResponse.Append( "<br />" );
      System.Console.WriteLine("News :{0}", newsCurrent);
     }//foreach (string newsCurrent in news)
    }//foreach(string newsGroupCurrent in newsGroup)  

    utilityNetworkNewsTransferProtocolNNTP.Disconnect(); 
   }//try 
   catch (UtilityNetworkNewsTransferProtocolNNTPException e ) 
   {
    sbResponse = new StringBuilder();
    sbResponse.Append( "Exception: " );
    sbResponse.Append( e.ToString() );
    System.Console.WriteLine( e.ToString() ); 
   } 
   catch (System.Exception exception) 
   {
    sbResponse = new StringBuilder();
    sbResponse.Append( "Exception: " );
    sbResponse.Append( exception.Message );
    System.Console.WriteLine("Exception: {0}", exception.Message); 
   } 
  }//}//public static void NewsClient()
    
  /// <summary>Connect to port 119 and receive a connect 200 status code.</summary>
  public void Connect
  (
   string hostname
  ) 
  {
   string response; 

   Connect(hostname, PortNNTP); 
   response = Response(); 
   Debug.WriteLine("Connect Response: {0}", response);
   if (response.Substring(0, 3) != "200")
   {
    throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
   }//if (response.Substring(0, 3) != "200") 
  }//public void Connect 

  /// <summary>Issue a QUIT message and receive a disconnect 205 status.</summary>
  public void Disconnect() 
  {
   string message; 
   string response; 

   message = "QUIT\r\n"; 
   Write(message); 
   response = Response(); 
   Debug.WriteLine("Disconnect QUIT Response: {0}", response);
   if (response.Substring(0, 3) != "205") 
   {
    throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
   }//if (response.Substring(0, 3) != "205")  
  }//public void Disconnect()

  /// <summary></summary>
  public ArrayList GetNewsgroups()
  {
   string message; 
   string response; 

   ArrayList retval = new ArrayList(); 
   message = "LIST\r\n"; 
   Write(message); 
   response = Response(); 
   if (response.Substring(0, 3) != "215") 
   {
    throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
   } 

   while (true)
   {
    response = Response();
    if (response == ".\r\n" || response == ".\n") 
    {
     return retval; 
    }//if (response == ".\r\n" || response == ".\n")  
    else 
    {
     char[] seps = {' ' }; 
     string[] values = response.Split(seps); 
     retval.Add(values[0]); 
     continue; 
    }//!if (response == ".\r\n" || response == ".\n")  
   }//while (true) 
  }//public ArrayList GetNewsgroups()
  
  /// <summary></summary>
  public ArrayList GetNews(string newsgroup)
  {
   string message; 
   string response; 

   ArrayList retval = new ArrayList(); 
   message = "GROUP " + newsgroup + "\r\n"; 
   Write(message); 
   response = Response(); 
   if (response.Substring(0, 3) != "211") 
   {
    throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
   } 

   char[] seps = {' ' }; 
   string[] values = response.Split(seps); 

   long start = Int32.Parse(values[2]); 
   long end = Int32.Parse(values[3]); 

   if (start+ 100 < end && end > 100) 
   {
    start = end-100; 
   } 

   for (long i= start; i< end; i++)
   {
    message = "ARTICLE " + i + "\r\n"; 
    Write(message); 
    response = Response(); 
    if (response.Substring(0, 3) == "423") 
    {
     continue; 
    } 
    if (response.Substring(0, 3) != "220") 
    {
     throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
    } 

    string article = ""; 
    while (true) 
    {
     response = Response(); 
     if (response == ".\r\n") 
     {
      break; 
     } 
     if (response == ".\n") 
     {
      break; 
     }
     if (article.Length < 1024) 
     {
      article += response; 
     }; 
    }//while (true)
    retval.Add(article);
   }//for (long i= start; i< end; i++)
   return retval;
  }//public ArrayList GetNews(string newsgroup)

  /// <summary></summary>
  public void Post
  (
   string newsgroup,
   string subject, 
   string from, 
   string content
  ) 
  {
   string message; 
   string response; 

   message = "POST\r\n"; 
   Write(message); 
   response = Response(); 
   if (response.Substring(0, 3) != "340") 
   {
    throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
   } 

   message = "From: " + from + "\r\n" +
             "Newsgroups: " + newsgroup + "\r\n" +
             "Subject: " + subject + "\r\n\r\n" +
             content + "\r\n.\r\n"; 
   Write(message); 
   response = Response(); 
   if (response.Substring(0, 3) != "240") 
   {
    throw new UtilityNetworkNewsTransferProtocolNNTPException(response); 
   } 
  }//public void Post  
  
  /// <summary></summary>
  private void Write(string message)
  {
   System.Text.ASCIIEncoding en = new System.Text.ASCIIEncoding() ; 

   byte[] WriteBuffer = new byte[1024] ; 
   WriteBuffer = en.GetBytes(message) ; 

   NetworkStream stream = GetStream() ; 
   stream.Write(WriteBuffer, 0, WriteBuffer.Length); 

   Debug.WriteLine(" WRITE:" + message); 
  }//private void Write(string message) 
  
  /// <summary></summary>
  private string Response()
  {
   System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding(); 
   byte [] serverbuff = new Byte[1024]; 
   NetworkStream stream = GetStream(); 
   int count = 0; 
   while (true) 
   {
    byte [] buff = new Byte[2]; 
    int bytes = stream.Read(buff, 0, 1 ); 
    if (bytes == 1) 
    {
     serverbuff[count] = buff[0]; 
     count++; 
     if (buff[0] == '\n') 
     {
      break; 
     } 
    } 
    else 
    {
     break; 
    }; 
   }; 
   string retval = enc.GetString(serverbuff, 0, count ); 
   Debug.WriteLine(" READ:" + retval); 
   return retval; 
  }//private string Response()
  
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
         UtilityNetworkNewsTransferProtocolNNTP.XPathNewsGroup,
     ref newsGroup
   );
   if ( newsGroup != null && newsGroup != String.Empty )
   {
    NewsGroup = newsGroup;
   } 
     	
  }//ConfigurationFile().

  ///<summary>Static.</summary>
  static UtilityNetworkNewsTransferProtocolNNTP()
  {
  }//static UtilityNetworkNewsTransferProtocolNNTP()

 }//UtilityNetworkNewsTransferProtocolNNTPworkNewsTransferProtocolNNTP : System.Net.Sockets.TcpClient 
 
 /// <summary>UtilityNetworkNewsTransferProtocolNNTPworkNewsTransferProtocolNNTPException.</summary>
 /// <remarks>UtilityNetworkNewsTransferProtocolNNTPworkNewsTransferProtocolNNTPException.</remarks>
 [Serializable]
 [XmlRoot("UtilityNetworkNewsTransferProtocolNNTPException", IsNullable = false)] 
 public class UtilityNetworkNewsTransferProtocolNNTPException : System.ApplicationException
 {
  ///<summary>Constructor.</summary>
  public UtilityNetworkNewsTransferProtocolNNTPException(string str) 
   :base(str) 
  {
  } 
 };//public class UtilityNetworkNewsTransferProtocolNNTPException : System.ApplicationException
 
}//namespace WordEngineering 