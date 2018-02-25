using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WordEngineering
{
 /// <summary>UtilityQOTDServer.</summary>
 /// <remarks>UtilityQOTDServer.</remarks>
 public class UtilityQOTDServer
 {

  ///<summary>PortNumber.</summary>
  public const int PortNumber =  13000;

  ///<summary>Quotes.</summary>
  public static String[] Quote = 
  {
   @"Sufficiently advanced magic is indistinguishable from technology -- Terry Pratchett",
   @"Sufficiently advanced technology is indistinguishable from magic -- Arthur C Clarke"
  };

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {

   int          quoteCounter = 0;
   int          quoteTotal   = Quote.Length;
   byte[]       byteCurrent  =  null;
   char[]       charCurrent  =  null;
   IPAddress    ipAddress    =  null;
   Socket       socket       =  null;
   TcpListener  tcpListener  =  null;

   //Creates an instance of the TcpListener class by providing a local port number.  
   ipAddress = Dns.Resolve("localhost").AddressList[0];

   try
   {
    tcpListener =  new TcpListener(ipAddress, PortNumber);
    tcpListener.Start();

    while ( true )
    {
     socket = tcpListener.AcceptSocket();

     //Encode alternatives as byes for send.
     charCurrent = Quote[ quoteCounter % quoteTotal ].ToCharArray();
     byteCurrent = Encoding.ASCII.GetBytes( charCurrent );

     // Return data to client, then clean up socket & repeat
     socket.Send(byteCurrent, byteCurrent.Length, 0);
     socket.Shutdown(SocketShutdown.Both);
     socket.Close();
     System.Console.WriteLine("Quotes Counter: {0}", quoteCounter);
     ++quoteCounter;

    }//while ( true )
   }
   catch ( SocketException socketException )
   {
    Console.WriteLine
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

 }//public class UtilityQOTDServer
}//namespace WordEngineering