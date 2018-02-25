using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WordEngineering
{
 ///<summary>UtilityTraceRoute.</summary>
 ///<remarks>Adapted from Richard Blum: C# Network Programming ISBN: 0-7821-4176-5 Courtesy Sybex http://sybex.com/sybexbooks.nsf/Booklist/4176.</remarks>
 public class UtilityTraceRoute
 {
  
  ///<summary>Argument HostName.</summary>
  public const int ArgumentHostName     = 0;

  ///<summary>Argument maximum hops.</summary>
  public const int ArgumentMaximumHops  = 1;

  ///<summary>Argument timeout reply.</summary>
  public const int ArgumentTimeoutReply = 2;

  /// <summary>LocalHost 127.0.0.1</summary>
  public static string LocalHost        = null;

  ///<summary>Maximum Hops.</summary>
  public const int MaximumHops          = 50;
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {

   int          hopCount          = -1;
   int          iCMPTimeExceeded  = -1;
   int          maximumHops       = -1;
   int          timeoutReply      = -1;

   String       hostNameTarget    = null;
   String       exceptionMessage  = null;
   
   IPHostEntry  iPHostEntry       = null;

   if ( argv.Length > ArgumentHostName )
   {
    hostNameTarget = argv[ArgumentHostName];
   }

   if 
   ( 
    argv.Length > ArgumentMaximumHops && 
    argv[ArgumentMaximumHops] != null &&
    argv[ArgumentMaximumHops] != String.Empty
   )
   {
    maximumHops = System.Convert.ToInt32( argv[ArgumentMaximumHops] );
   }

   if 
   ( 
    argv.Length > ArgumentTimeoutReply && 
    argv[ArgumentTimeoutReply] != null &&
    argv[ArgumentTimeoutReply] != String.Empty
   )
   {
    timeoutReply = System.Convert.ToInt32( argv[ArgumentTimeoutReply] );
   }
   
   TraceRoute
   (
    ref hostNameTarget,
    ref exceptionMessage,
    ref iPHostEntry,
    ref maximumHops,
    ref hopCount,
    ref iCMPTimeExceeded,
    ref timeoutReply
   );
   
   if ( exceptionMessage != null )
   {
    return;
   }
   	
   #if (DEBUG)
    System.Console.WriteLine
    (
     "Tracing route to {0} [{1}] over a maximum of {2} hops {3}ms Trace complete.", 
     iPHostEntry.HostName,
     iPHostEntry.AddressList[0],
     hopCount,
     iCMPTimeExceeded
    );
   #endif

  }//public static void Main( String argv[] )
  	
  ///<summary>TraceRoute.</summary>
  ///<param name="hostNameTarget">HostName target.</param>
  ///<param name="exceptionMessage">Exception message</param>
  ///<param name="iPHostEntry">IPHostEntry</param>
  ///<param name="maximumHops">MaximumHops</param>
  ///<param name="hopCount">Hop count</param>
  ///<param name="iCMPTimeExceeded">ICMPTimeExceeded</param>
  ///<param name="timeoutReply">Timeout Reply</param>
  public static void TraceRoute
  (
   ref String       hostNameTarget,
   ref String       exceptionMessage,
   ref IPHostEntry  iPHostEntry,
   ref int          maximumHops,
   ref int          hopCount,
   ref int          iCMPTimeExceeded,
   ref int          timeoutReply
  )
  {
   byte[]       byteData           = null;
   int          packetsize;   
   int          receive; 
   int          timestart;
   int          timestop;

   UInt16       checkSum;        
   
   EndPoint     endPoint                  =  null;
   IPEndPoint   iPEndPoint                =  null;
   Socket       socketHost                =  null;
   UtilityICMP  utilityICMPPacketRequest  =  null;
   UtilityICMP  utilityICMPPacketResponse =  null;
   exceptionMessage                       =  null;

   if ( maximumHops < 0 )
   {
    maximumHops = MaximumHops;
   }

   hopCount         = 0;
   iCMPTimeExceeded = -1;

   try
   {
    byteData    = new byte[1024];
    socketHost  = new Socket( AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp );
   
    if ( hostNameTarget == null || hostNameTarget == String.Empty )
    {
     hostNameTarget = IPAddress.Loopback.ToString();
    }
 	
    iPHostEntry = Dns.Resolve ( hostNameTarget );
    iPEndPoint  = new IPEndPoint ( iPHostEntry.AddressList[0], 0 );
    endPoint = ( EndPoint ) iPEndPoint;
    utilityICMPPacketRequest = new UtilityICMP();

    utilityICMPPacketRequest.Type = 0x08;
    utilityICMPPacketRequest.Code = 0x00;
    utilityICMPPacketRequest.Checksum = 0;
    Buffer.BlockCopy(BitConverter.GetBytes(1), 0, utilityICMPPacketRequest.Message, 0, 2);
    Buffer.BlockCopy(BitConverter.GetBytes(1), 0, utilityICMPPacketRequest.Message, 2, 2);
    byteData = Encoding.ASCII.GetBytes("test utilityICMPPacketRequest");
    Buffer.BlockCopy(byteData, 0, utilityICMPPacketRequest.Message, 4, byteData.Length);
    utilityICMPPacketRequest.MessageSize = byteData.Length + 4;
    packetsize = utilityICMPPacketRequest.MessageSize + 4;

    checkSum = utilityICMPPacketRequest.GetChecksum();
    utilityICMPPacketRequest.Checksum = checkSum;
    
    socketHost.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);

    for ( hopCount = 1; hopCount < maximumHops; hopCount++ )
    {
     socketHost.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, hopCount);
     timestart = Environment.TickCount;
     socketHost.SendTo
     (
      utilityICMPPacketRequest.GetBytes(), 
      packetsize, 
      SocketFlags.None, 
      iPEndPoint
     );
     byteData = new byte[1024];
     receive = socketHost.ReceiveFrom(byteData, ref endPoint);
     timestop = Environment.TickCount;
     utilityICMPPacketResponse = new UtilityICMP(byteData, receive);
     if ( utilityICMPPacketResponse.Type == 11 )
     {
      System.Console.WriteLine("hop {0}: utilityICMPPacketResponse from {1}, {2}ms", hopCount, endPoint.ToString(), timestop-timestart);
     }//if ( utilityICMPPacketResponse.Type == 11 ) 
     if ( utilityICMPPacketResponse.Type == 0 )
     {
      //System.Console.WriteLine("{0} reached in {1} hops, {2}ms.", endPoint.ToString(), hopCount, timestop-timestart);
      iCMPTimeExceeded = timestop-timestart;
      break;
     }//if ( utilityICMPPacketResponse.Type == 0 )
     if ( timeoutReply > 0 )
     {
      Thread.Sleep( timeoutReply );
     }
    }//for ( hopCount = 1; hopCount < maximumHops; hopCount++ ) 
   }//try
   catch ( SocketException socketException )
   {
    exceptionMessage = "SocketException: " + socketException.Message;
    System.Console.WriteLine
    (
     "Socket Exception: {0} | Hop {1}",
     socketException.Message,
     hopCount
    );
   }//catch ( SocketException socketException )
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
    System.Console.WriteLine
    (
     "Exception: {0} | Hop {1}",
     exception.Message,
     hopCount
    );
   }//catch ( Exception exception )
   finally
   {
    if ( socketHost != null )
    {
     socketHost.Close();
    }//if ( socketHost != null )
   }//finally
  }//public static void TraceRoute()

  /// <summary>Static.</summary>
  static UtilityTraceRoute()
  {
   LocalHost = IPAddress.Loopback.ToString();
  }//static UtilityTraceRoute()
 }//public class UtilityTraceRoute
}//namespace WordEngineering