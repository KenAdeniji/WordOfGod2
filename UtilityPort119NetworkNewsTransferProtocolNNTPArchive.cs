using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace WordEngineering
{
 ///<summary>UtilityPort119NetworkNewsTransferProtocolNNTP</summary>
 ///<remarks>
 /// Subject: Sockets and Arraylists   11/12/2005 6:31 PM PST 
 /// By:    Adrian McNally  In:    microsoft.public.dotnet.languages.csharp 
 ///</remarks>
 public class UtilityPort119NetworkNewsTransferProtocolNNTP
 {
  /// <summary>Hostname</summary>
  public const string Hostname = "news.microsoft.com";

  /// <summary>Port</summary>
  public const int Port = 119;

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(string[] argv)
  {
   string exceptionMessage;
   string hostname = Hostname;
   int port = Port;
   ArrayList newsGroup;
   if ( argv.Length > 0 ) { hostname = argv[0]; }
   if ( argv.Length > 1 ) { Int32.TryParse( argv[1], out port); }
   newsGroup = NewsGroupList( hostname, port, out exceptionMessage );
   foreach ( object obj in newsGroup ) { System.Console.WriteLine(obj); }
  }

  /// <summary>NewsGroupList</summary>
  public static ArrayList NewsGroupList
  ( 
   string hostname, 
   int port, 
   out string exceptionMessage
  )
  {
   string newsGroupCurrent = null;
   ArrayList newsGroup = null;
   NetworkStream networkStream = null;
   StreamReader streamReader = null;
   StreamWriter streamWriter = null;
   TcpClient tcpClient = null;
   exceptionMessage = null;
   try
   {
    tcpClient = new TcpClient(hostname, port);
    networkStream = tcpClient.GetStream();
    streamReader = new StreamReader(networkStream);
    streamWriter = new StreamWriter(networkStream);
    //200 NNTP Service 6.0.3790.206 Version: 6.0.3790.206 Posting Allowed
    streamReader.ReadLine();
    streamWriter.WriteLine("List");
    streamWriter.Flush();
    newsGroup = new ArrayList();
    for (;;)
    {
     newsGroupCurrent = streamReader.ReadLine();
     if ( newsGroupCurrent[0] == '.' ) { break; }
     newsGroup.Add(newsGroupCurrent);
    }
    streamWriter.WriteLine("Quit");
   }
   catch( Exception ex )
   {
    exceptionMessage = ex.Message;
   }
   return (newsGroup);
  }

 }
}