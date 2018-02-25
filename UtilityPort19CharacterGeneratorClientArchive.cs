using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WordEngineering
{
 ///<summary>UtilityPort19CharacterGeneratorClient</summary>
 public class UtilityPort19CharacterGeneratorClient
 {
  ///<summary>Buffer_Size</summary>
  public const int Buffer_Size = 1024;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   string server = "localhost";
   int port = 19;
   string exceptionMessage;
   StringBuilder response;

   if ( argv.Length > 0 ) { server = argv[0]; }
   if ( argv.Length > 1 ) { Int32.TryParse(argv[1], out port); }
   Port19CharacterGeneratorClient
   (
    server,
    port,
    out response,
    out exceptionMessage
   );
  }

  ///<summary>Port19CharacterGeneratorClient</summary>
  public static void Port19CharacterGeneratorClient
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
   String data;
   TcpClient tcpClient = null;
   NetworkStream networkStream = null;
   try
   {
    tcpClient = new TcpClient(server, port);
    networkStream = tcpClient.GetStream();
    for (;;)
    {
     byteRead = networkStream.Read(bufferRead, 0, Buffer_Size);
     data = Encoding.ASCII.GetString( bufferRead, 0, byteRead );
     response.Append( data );
     System.Console.WriteLine( data );
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
   //System.Console.WriteLine( response );
  }

 }
}