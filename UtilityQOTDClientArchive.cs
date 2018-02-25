using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WordEngineering
{
 /// <summary>UtilityQOTDClient.</summary>
 /// <remarks>UtilityQOTDClient.</remarks>
 public class UtilityQOTDClient
 {

  ///<summary>PortNumber.</summary>
  public const int PortNumber =  13000;

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {

   // String to store the response ASCII representation.
   String         responseData    = String.Empty;

   Int32          byteRead        = -1;

   byte[]         byteCurrent     =  null;

   NetworkStream  networkStream   =  null;
   TcpClient      tcpClient       =  null;

   byteCurrent = new Byte[256];

   try
   {

    // Create a TcpClient.
    // Note, for this client to work you need to have a TcpServer 
    // connected to the same address as specified by the server, port
    // combination.

    tcpClient = new TcpClient("localhost", PortNumber);

    // Get a client stream for reading and writing.
    networkStream = tcpClient.GetStream();

    // Receive the TcpServer.response.
    // Buffer to store the response bytes.
    byteCurrent = new Byte[256];

    // Read the first batch of the TcpServer response bytes.
    byteRead = networkStream.Read(byteCurrent, 0, byteCurrent.Length);

    responseData = System.Text.Encoding.ASCII.GetString(byteCurrent, 0, byteRead);

    System.Console.WriteLine("Received: {0}", responseData);

    // Close everything.
    tcpClient.Close();

   }//try
   catch (ArgumentNullException e) 
   {
    System.Console.WriteLine("ArgumentNullException: {0}", e);
   }
   catch ( SocketException socketException )
   {
    System.Console.WriteLine
    ( 
     "Socket ErrorCode: {0} | Exception: {1}",
     socketException.ErrorCode,
     socketException.ToString()
    );
   }
   catch ( Exception e )
   {
    Console.WriteLine( e.ToString());
   }

  }//public static void Main()

 }//public class UtilityQOTDClient
}//namespace WordEngineering