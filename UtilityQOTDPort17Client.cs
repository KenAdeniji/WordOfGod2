using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WordEngineering
{
 ///<summary>UtilityQOTDPort17Client</summary>
 public class UtilityQOTDPort17Client
 {
  ///<summary>Buffer_Size</summary>
  public const int Buffer_Size = 1024;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   string server = "localhost";
   int port = 17;
   string exceptionMessage;
   StringBuilder response;

   if ( argv.Length > 0 ) { server = argv[0]; }
   if ( argv.Length > 1 ) { Int32.TryParse(argv[1], out port); }
   QOTDPort17Client
   (
    server,
    port,
    out response,
    out exceptionMessage
   );
  }

  ///<summary>QOTDPort17Client</summary>
  public static void QOTDPort17Client
  (
   string server,
   int port,
   out StringBuilder response,
   out string exceptionMessage
  )
  {
   response = new StringBuilder();
   exceptionMessage = null;
   byte[] bufferRead = new byte[Buffer_Size];
   int byteRead;
   TcpClient tcpClient = null;
   NetworkStream networkStream = null;
   try
   {
    tcpClient = new TcpClient(server, port);
    networkStream = tcpClient.GetStream();
    for (;;)
    {
     byteRead = networkStream.Read(bufferRead, 0, Buffer_Size);
     response.Append( Encoding.ASCII.GetString( bufferRead, 0, byteRead ) );
     if ( byteRead < Buffer_Size ) { break; }
    }
   }
   catch( Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   finally
   {
    if ( networkStream != null ) { networkStream.Close(); };
    if ( tcpClient != null ) { tcpClient.Close(); };
   }
   if ( exceptionMessage != null ) { System.Console.WriteLine( exceptionMessage ); }
   System.Console.WriteLine( response );
  }

 }
}