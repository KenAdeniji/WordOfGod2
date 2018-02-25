using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WordEngineering
{
 ///<summary>UtilitySubnetMask.</summary>
 ///<remarks>Adapted from Richard Blum: C# Network Programming ISBN: 0-7821-4176-5 Courtesy Sybex http://sybex.com/sybexbooks.nsf/Booklist/4176.</remarks>
 public class UtilitySubnetMask
 {
  
  ///<summary>Argument SocketHostAvailableTimeOut.</summary>
  public const int ArgumentSocketHostAvailableTimeOut = 0;

  ///<summary>SocketHostAvailableTimeOut</summary>
  public const int SocketHostAvailableTimeOut = 200000;

  /// <summary>LocalHost 127.0.0.1</summary>
  public static string LocalHost        = null;

  /// <summary>The configuration XML filename.</summary>
  public string FilenameConfigurationXml       = @"WordEngineering.config";

  ///<summary>FormatSocketException</summary>
  public const String FormatSocketException = "SocketException ErrorCode: {0} | Available: {1} | Message: {2} | Byte Received: {3}";

  /// <summary>The XPath database connection string.</summary>
  public const String XPathDatabaseConnectionString  = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";  
    
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {

   int          socketHostAvailable            =  -1;
   int          socketHostAvailableTimeOut     =  200000;
   int          socketHostAvailableTimeElapse  =  -1;

   String       exceptionMessage               =  null;

   IPAddress    iPAddressSubnetMask            =  null;

   UtilityICMP  utilityICMPPacketRequest       =  null;
   UtilityICMP  utilityICMPPacketResponse      =  null;
   
   if 
   ( 
    argv.Length > ArgumentSocketHostAvailableTimeOut && 
    argv[ArgumentSocketHostAvailableTimeOut] != null &&
    argv[ArgumentSocketHostAvailableTimeOut] != String.Empty
   )
   {
    socketHostAvailableTimeOut = System.Convert.ToInt32( argv[ArgumentSocketHostAvailableTimeOut] );
   }
   
   SubnetMask
   (
    ref  exceptionMessage,
    ref  iPAddressSubnetMask,
    ref  utilityICMPPacketRequest,
    ref  utilityICMPPacketResponse,
    ref  socketHostAvailable,
    ref  socketHostAvailableTimeOut,
    ref  socketHostAvailableTimeElapse
   );

   if ( exceptionMessage != null )
   {
    return;
   }
   	
   #if (DEBUG)
    System.Console.WriteLine
    (
     "ICMP Response Type: {0} | IP Address Subnet Mask: {1}", 
     utilityICMPPacketResponse.Type,
     iPAddressSubnetMask.ToString()
    );
   #endif

   #if ( DEBUG )       
    System.Console.WriteLine
    (
     "Socket Host Available: {0} bytes | Timeout: {1} | Elapse: {2}", 
     socketHostAvailable,
     socketHostAvailableTimeOut,
     socketHostAvailableTimeElapse
    );
   #endif

  }//public static void Main( String argv[] )
  	
  ///<summary>SubnetMask.</summary>
  ///<param name="exceptionMessage">exceptionMessage.</param>
  ///<param name="iPAddressSubnetMask">IPAddressSubnetMask.</param>
  ///<param name="utilityICMPPacketRequest">utilityICMPPacketRequest.</param>
  ///<param name="utilityICMPPacketResponse">utilityICMPPacketResponse.</param>
  ///<param name="socketHostAvailable">socketHostAvailable</param>
  ///<param name="socketHostAvailableTimeOut">socketHostAvailableTimeOut.</param>
  ///<param name="socketHostAvailableTimeElapse">socketHostAvailableTimeElapse.</param>  
  public static void SubnetMask
  (
   ref String       exceptionMessage,
   ref IPAddress    iPAddressSubnetMask,
   ref UtilityICMP  utilityICMPPacketRequest,
   ref UtilityICMP  utilityICMPPacketResponse,
   ref int          socketHostAvailable,
   ref int          socketHostAvailableTimeOut,
   ref int          socketHostAvailableTimeElapse
  )
  {
   byte[]           byteData                       =  null;
   
   int              utilityICMPPacketRequestSize   =  -1;   
   int              byteReceive                    =  -1; 
   int              socketHostAvailableTimeFrom    =  0;
   int              socketHostAvailableTimeUntil   =  0;
   
   long             utilityICMPPacketAnswer        =  -1;
   
   StringBuilder    stringBuilderExceptionMessage  =  null;

   UInt16           checkSum;        
   
   EndPoint         endPoint                       =  null;
   IPEndPoint       iPEndPoint                     =  null;
   Socket           socketHost                     =  null;
   
   byteData                                        =  new byte[1024];
   
   iPAddressSubnetMask                             =  null;
   utilityICMPPacketRequest                        =  null;
   utilityICMPPacketResponse                       =  null;
   socketHostAvailable                             =  -1;
   
   stringBuilderExceptionMessage                   =  new StringBuilder();
   
   try
   {

    socketHost = new Socket
    (
     AddressFamily.InterNetwork, 
     SocketType.Raw, 
     ProtocolType.Icmp
    );
  
    iPEndPoint = new IPEndPoint
    (
     IPAddress.Broadcast, 
     0
    );
      
    endPoint = ( EndPoint ) iPEndPoint;
      
    utilityICMPPacketRequest = new UtilityICMP();

    utilityICMPPacketRequest.Type = 0x11;
    utilityICMPPacketRequest.Code = 0x00;
    utilityICMPPacketRequest.Checksum = 0;
    Buffer.BlockCopy
    (
     BitConverter.GetBytes(1),
     0,
     utilityICMPPacketRequest.Message, 
     0,
     2
    );
    Buffer.BlockCopy
    (
     BitConverter.GetBytes(1),
     0,
     utilityICMPPacketRequest.Message,
     2,
     2
    );
    Buffer.BlockCopy
    (
     BitConverter.GetBytes(0),
     0,
     utilityICMPPacketRequest.Message,
     4,
     4
    );

    utilityICMPPacketRequest.MessageSize = 8;
    utilityICMPPacketRequestSize = utilityICMPPacketRequest.MessageSize + 4;
    
    //The GetChecksum() method should set the Checksum property.
    checkSum = utilityICMPPacketRequest.GetChecksum();
    utilityICMPPacketRequest.Checksum = checkSum;

    socketHost.SetSocketOption
    (
     SocketOptionLevel.Socket, 
     SocketOptionName.ReceiveTimeout,
     3000
    );
    
    socketHost.SetSocketOption
    (
     SocketOptionLevel.Socket,
     SocketOptionName.Broadcast,
     1
    );
    
    socketHost.SendTo
    (
     utilityICMPPacketRequest.GetBytes(), 
     utilityICMPPacketRequestSize, 
     SocketFlags.None, 
     iPEndPoint
    );

    byteData = new byte[1024];
    
    if ( socketHost.Available <= 0 )
    {
     socketHostAvailableTimeFrom = Environment.TickCount;
     while ( socketHost.Available == 0 && Environment.TickCount - socketHostAvailableTimeFrom <= socketHostAvailableTimeOut )
     {
     }//while ( socketHost.Available == 0 && Environment.TickCount - socketHostAvailableTimeFrom <= socketHostAvailableTimeOut )
    }//if ( socketHost.Available < 0 )
     
    socketHostAvailable           = socketHost.Available;
    socketHostAvailableTimeUntil  = Environment.TickCount;
    socketHostAvailableTimeElapse = socketHostAvailableTimeUntil - socketHostAvailableTimeFrom;
    
    byteReceive = socketHost.ReceiveFrom
    (
         byteData, 
     ref endPoint
    );

    utilityICMPPacketResponse = new 
    UtilityICMP
    (
     byteData, 
     byteReceive
    );

    utilityICMPPacketAnswer = BitConverter.ToUInt32
    (
     utilityICMPPacketResponse.Message,
     4
    );

    iPAddressSubnetMask = new IPAddress
    ( 
     utilityICMPPacketAnswer
    );

   }//Try 
   catch ( SocketException socketException )
   {
    exceptionMessage = "SocketException: " + socketException.Message;
    stringBuilderExceptionMessage.AppendFormat
    (
     FormatSocketException,
     socketException.ErrorCode,
     socketHostAvailable, 
     socketException.Message,
     byteReceive
    );
    exceptionMessage = stringBuilderExceptionMessage.ToString(); 
    System.Console.WriteLine( stringBuilderExceptionMessage );
   }//catch ( SocketException socketException )
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
    System.Console.WriteLine
    (
     "Exception: {0}",
     exception.Message
    );
   }//catch ( Exception exception )
   finally
   {
    if ( socketHost != null )
    {
     socketHost.Close();
    }//if ( socketHost != null )
   }//finally
   
  }//public static void SubnetMask()

  /// <summary>Static.</summary>
  static UtilitySubnetMask()
  {
   LocalHost = IPAddress.Loopback.ToString();
  }//static UtilitySubnetMask()
 }//public class UtilitySubnetMask
}//namespace WordEngineering