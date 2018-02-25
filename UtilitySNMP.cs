using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace WordEngineering
{

 ///<summary>UtilitySNMP.</summary>
 ///<remarks>
 /// Adapted from Richard Blum: C# Network Programming ISBN: 0-7821-4176-5 Courtesy Sybex http://sybex.com/sybexbooks.nsf/Booklist/4176.
 /// Enable SNMP and SNMP trap service.
 /// Computer Management include host and community names security.
 /// Management Information Base (MIB).
 /// Protocol Data Unit (PDU): GetRequest, GetNextRequest, GetResponse, SetRequest.
 /// Structure Managment Information (SMI). iso = 1. org = 3. dod = 6. internet = 1. mgmt = 2. mib-2 = 1.
 /// Groups in MIB-II
 /// system       OBJECT IDENTIFIER ::= { mib-2 1 }
 /// interfaces   OBJECT IDENTIFIER ::= { mib-2 2 }
 /// at           OBJECT IDENTIFIER ::= { mib-2 3 }
 /// ip           OBJECT IDENTIFIER ::= { mib-2 4 }
 /// icmp         OBJECT IDENTIFIER ::= { mib-2 5 }
 /// tcp          OBJECT IDENTIFIER ::= { mib-2 6 }
 /// udp          OBJECT IDENTIFIER ::= { mib-2 7 }
 /// egp          OBJECT IDENTIFIER ::= { mib-2 8 }
 /// -- historical (some say hysterical)
 /// -- cmot      OBJECT IDENTIFIER ::= { mib-2 9 }
 /// transmission OBJECT IDENTIFIER ::= { mib-2 10 }
 /// snmp         OBJECT IDENTIFIER ::= { mib-2 11 }
 /// Windows SNMP Utilities: EventCmd, EventWin, MIBCC, SNMPUtil 
 ///</remarks>
 public class UtilitySNMP
 {

  /// <summary>The delimiter in character array format for the split string MIBSMI.</summary>
  public static  char[]   DelimiterSplitCharObjectIdentifier          = null;

  /// <summary>ArgumentCommunityName.</summary>
  public const   int      ArgumentCommunityName                       = 1;

  /// <summary>ArgumentHostName.</summary>
  public const   int      ArgumentHostName                            = 0;

  /// <summary>RankSNMPRequestOfficialName.</summary>
  public const   int      RankSNMPRequestOfficialName                 = 0;

  /// <summary>RankSNMPRequestProtocolDataUnit.</summary>
  public const   int      RankSNMPRequestProtocolDataUnit             = 1;

  /// <summary>RankSNMPRequestObjectIdentifier.</summary>
  public const   int      RankSNMPRequestObjectIdentifier             = 2;

  /// <summary>RankSNMPRequestMIBOjectType.</summary>
  public const   int      RankSNMPRequestMIBOjectType                 = 3;

  /// <summary>SocketHostAvailableTimeOut.</summary>
  public static  int      SocketHostAvailableTimeOut                  = 200000;

  /// <summary>CommunityName.</summary>
  public static  string   CommunityName                               = "public";

  /// <summary>The delimiter split string MIBSMI.</summary>
  public const   string   DelimiterSplitStringObjectIdentifier        = ".";

  /// <summary>HostName.</summary>
  public static  string   HostName                                    = null;

  /// <summary>SNMPRequestGet.</summary>
  public static  string   SNMPRequestGet                              = "get";

  /// <summary>SNMPRequestGetNext.</summary>
  public static  string   SNMPRequestGetNext                          = "getnext";

  /// <summary>SNMPRequest.</summary>
  public static  string[][] SNMPRequest = new String[][]
                                          {
                                           new String[] { "sysDescr",    SNMPRequestGet, "1.3.6.1.2.1.1.1.0", "Text" },
                                           new String[] { "sysObjectID", SNMPRequestGet, "1.3.6.1.2.1.1.2.0", "Text" },                        
                                           new String[] { "sysUptime",   SNMPRequestGet, "1.3.6.1.2.1.1.3.0", "Time" },
                                           new String[] { "sysContact",  SNMPRequestGet, "1.3.6.1.2.1.1.4.0", "Text" },
                                           new String[] { "sysName",     SNMPRequestGet, "1.3.6.1.2.1.1.5.0", "Text" },
                                           new String[] { "sysLocation", SNMPRequestGet, "1.3.6.1.2.1.1.6.0", "Text" },
                                           new String[] { "ifNumber",    SNMPRequestGet, "1.3.6.1.2.1.2.0.1", "Text" }
                                          };//                                            
  /// <summary>Constructor.</summary>
  public UtilitySNMP()
  {

  }

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   String  hostName          =  HostName;
   String  communityName     =  CommunityName; 
   String  exceptionMessage  =  null;

   if ( argv.Length > ArgumentHostName )
   {
    hostName = argv[ ArgumentHostName ];
   }

   if ( argv.Length > ArgumentCommunityName )
   {
    communityName = argv[ ArgumentCommunityName ];
   }

   SNMPInformation
   (
    ref  hostName,
    ref  communityName,
    ref  exceptionMessage
   );
   
  }//public static void Main( String[] argv )

  ///<summary>Stub.</summary>
  public static void Stub()
  {
   //SNMPInformation();
   //MacAddress();
  }

  ///<summary>SNMPInformation</summary>
  public static void SNMPInformation()
  {
   int     socketExceptionErrorCode     =  0;
   int     snmpHostRequestLength        =  0;
   int     socketHostRequestLength      =  0;
   int     socketHostAvailableTimeOut   =  SocketHostAvailableTimeOut;

   String  hostName                     =  null;
   String  communityName                =  null;
   String  exceptionMessage             =  null;
   
   SNMPInformation
   (
    ref  hostName,
    ref  communityName,
    ref  exceptionMessage,
    ref  socketExceptionErrorCode,
    ref  snmpHostRequestLength,
    ref  socketHostRequestLength,
    ref  socketHostAvailableTimeOut
   );
   
  }//public static void SNMPInformation()

  ///<summary>SNMPInformation</summary>
  ///<param name="hostName">Host name</param>
  ///<param name="communityName">Community name</param>
  ///<param name="exceptionMessage">Exception Message</param>
  public static void SNMPInformation
  (
   ref  String  hostName,
   ref  String  communityName,
   ref  String  exceptionMessage   
  )
  {
   int     socketExceptionErrorCode     =  0;
   int     snmpHostRequestLength        =  0;
   int     socketHostRequestLength      =  0;
   int     socketHostAvailableTimeOut   =  SocketHostAvailableTimeOut;

   SNMPInformation
   (
    ref  hostName,
    ref  communityName,
    ref  exceptionMessage,
    ref  socketExceptionErrorCode,
    ref  snmpHostRequestLength,
    ref  socketHostRequestLength,
    ref  socketHostAvailableTimeOut
   );
   
  }//public static void SNMPInformation()

  ///<summary>SNMPInformation</summary>
  ///<param name="hostName">Host name</param>
  ///<param name="communityName">Community name</param>
  ///<param name="exceptionMessage">Exception Message</param>
  ///<param name="socketExceptionErrorCode">socketExceptionErrorCode</param>
  ///<param name="snmpHostRequestLength">snmpHostRequestLength</param>
  ///<param name="socketHostRequestLength">socketHostRequestLength</param>
  ///<param name="socketHostAvailableTimeOut">socketHostAvailableTimeOut</param>
  public static void SNMPInformation
  (
   ref  String  hostName,
   ref  String  communityName,
   ref  String  exceptionMessage,
   ref  int     socketExceptionErrorCode,
   ref  int     snmpHostRequestLength,
   ref  int     socketHostRequestLength,
   ref  int     socketHostAvailableTimeOut
  )
  {

   byte[]       packetResponse                 =  null;

   int          commLength                     =  -1;

   int          dataType                       =  -1;
   int          dataLength                     =  -1;
   int          dataStart                      =  -1;

   int          MIBSMILength                   =  -1;

   int          socketHostAvailable            =  -1;
   int          socketHostAvailableTimeElapse  =  -1;
   
   int          MIBObjectTime                  =  -1;
   
   String       MIBOjectValue                  =  null;
   
   packetResponse = new byte[ 1024 ];
   MIBObjectTime  = 0;

   #if (DEBUG)
    System.Console.WriteLine("Device SNMP Information");
   #endif

   try
   {
    
    foreach ( string[] snmpRequest in SNMPRequest )
    {
     
     SNMPGet
     (
      ref  snmpRequest[ RankSNMPRequestProtocolDataUnit ], 
      ref  hostName, 
      ref  communityName, 
      ref  snmpRequest[ RankSNMPRequestObjectIdentifier ],
      ref  packetResponse,
      ref  exceptionMessage,
      ref  socketExceptionErrorCode,
      ref  snmpHostRequestLength,
      ref  socketHostRequestLength,
      ref  socketHostAvailable,
      ref  socketHostAvailableTimeOut,
      ref  socketHostAvailableTimeElapse
     );

     if ( packetResponse[0] == 0xff )
     {
      System.Console.WriteLine
      (
       "No response from Host: {0} Community: {1}", 
       hostName,
       communityName 
      );
      return;
     }

     //If response, get the community name and MIBSMI lengths
     commLength = Convert.ToInt16(packetResponse[6]);
     MIBSMILength = Convert.ToInt16(packetResponse[23 + commLength]);

     //Extract the MIBSMI data from the SNMP response
     dataType = Convert.ToInt16(packetResponse[24 + commLength + MIBSMILength]);
     dataLength = Convert.ToInt16(packetResponse[25 + commLength + MIBSMILength]);
     dataStart = 26 + commLength + MIBSMILength;
     MIBOjectValue = Encoding.ASCII.GetString(packetResponse, dataStart, dataLength);

     if ( String.Compare( snmpRequest[ RankSNMPRequestMIBOjectType ], "Time", true ) == 0 || dataType == 67 )
     {
      // The sysUptime value may by a multi-byte integer
      // Each byte read must be shifted to the higher byte order
      MIBObjectTime = 0;
      
      while( dataLength > 0 )
      {
       MIBObjectTime = ( MIBObjectTime << 8) + packetResponse[ dataStart++ ];
       dataLength--;
      }//while( dataLength > 0 )
      
      MIBOjectValue = (TimeSpan.FromMilliseconds( MIBObjectTime * 10 )).ToString();

      /*
      #if ( DEBUG )
       System.Console.WriteLine
       (
        "Request Name: {0} | Datatype: {1} | Value: {2} (hundredth second) | TimeSpan: {3}",
        snmpRequest[ RankSNMPRequestOfficialName ],
        dataType,
        MIBObjectTime,
        MIBOjectValue
       );
      #endif
      */
   
     } //if ( String.Compare( snmpRequest[ RankSNMPRequestMIBOjectType ], "Time", true ) == 0 || dataType == 67 )

     #if ( DEBUG )
      System.Console.WriteLine
      (
       "Request Name: {0} | Datatype: {1} | Value: {2}",
       snmpRequest[ RankSNMPRequestOfficialName ],
       dataType,
       MIBOjectValue
      );
     #endif
 
    }//foreach ( string [] snmpRequest in SNMPRequest )
  
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message);
   }//catch ( Exception exception )
  }//public static void SNMPInformation
 
  ///<summary>MacAddress</summary>
  public static void MacAddress()
  {
   int     snmpHostRequestLength        =  0;

   int     socketExceptionErrorCode     =  -1;
   int     socketHostAvailableTimeOut   =  -1;
   int     socketHostRequestLength      =  -1;

   String  hostName                     =  null;
   String  communityName                =  null;
   String  exceptionMessage             =  null;

   socketHostAvailableTimeOut           =  SocketHostAvailableTimeOut;

   MacAddress
   (
    ref  hostName,
    ref  communityName,
    ref  exceptionMessage,
    ref  socketExceptionErrorCode,
    ref  snmpHostRequestLength,
    ref  socketHostRequestLength,
    ref  socketHostAvailableTimeOut
   );
   	
  }//public static void MacAddress()  	

  ///<summary>MacAddress</summary>
  ///<param name="hostName">Host name</param>
  ///<param name="communityName">Community name</param>
  ///<param name="exceptionMessage">Exception Message</param>
  ///<param name="socketExceptionErrorCode">socketExceptionErrorCode</param>
  ///<param name="snmpHostRequestLength">snmpHostRequestLength</param>
  ///<param name="socketHostRequestLength">socketHostRequestLength</param>
  ///<param name="socketHostAvailableTimeOut">socketHostAvailableTimeOut</param>
  public static void MacAddress
  (
   ref  String  hostName,
   ref  String  communityName,
   ref  String  exceptionMessage,
   ref  int     socketExceptionErrorCode,
   ref  int     snmpHostRequestLength,
   ref  int     socketHostRequestLength,
   ref  int     socketHostAvailableTimeOut
  )
  {
   
   byte[]       packetResponse                 =  null;
   
   int          commLength                     =  -1;
   int          MIBSMILength                   =  -1;
   
   int          dataStart                      =  -1;
   int          dataLength                     =  -1;
   
   int          orgMIBSMILength                =  -1;

   int          socketHostAvailable            =  -1;
   int          socketHostAvailableTimeElapse  =  -1;
   
   String       MIBSMI                         =  null;
   String       nextMIBSMI                     =  null;
   String       value                          =  null;

   HttpContext  httpContext                    =  HttpContext.Current;

   packetResponse                              =  new byte[1024];


   MIBSMI                                      =  "1.3.6.1.2.1.17.4.3.1.1";

   orgMIBSMILength                             =  MIBSMI.Length;
   nextMIBSMI                                  =  MIBSMI;

   try
   {
    if ( hostName == null || hostName == String.Empty )
    {
   	 hostName = HostName;
    }	

    if ( communityName == null || communityName == String.Empty )
    {
   	 communityName = CommunityName;
    }	

    while ( true )
    {

     SNMPGet
     (
      ref  SNMPRequestGetNext, 
      ref  hostName, 
      ref  communityName, 
      ref  nextMIBSMI,
      ref  packetResponse,
      ref  exceptionMessage,
      ref  socketExceptionErrorCode,
      ref  snmpHostRequestLength,
      ref  socketHostRequestLength,
      ref  socketHostAvailable,
      ref  socketHostAvailableTimeOut,
      ref  socketHostAvailableTimeElapse
     );

     commLength = Convert.ToInt16(packetResponse[6]);
     MIBSMILength = Convert.ToInt16(packetResponse[23 + commLength]);
     dataLength = Convert.ToInt16(packetResponse[25 + commLength + MIBSMILength]);
     dataStart = 26 + commLength + MIBSMILength;
     value = BitConverter.ToString(packetResponse, dataStart, dataLength);
     
     GetNextMIBSMI
     (
      ref packetResponse,
      ref nextMIBSMI,
      ref exceptionMessage
     );
   
     if ( !( nextMIBSMI.Substring(0, orgMIBSMILength ) == MIBSMI ) )
     {
      break;
     }  

     System.Console.WriteLine
     (
      "{0} = {1}", 
      nextMIBSMI, 
      value
     );
     
    }//while ( true ) 
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;

    if ( httpContext == null )
    {
     System.Console.WriteLine("Exception Message: {0}", exception.Message);
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( "Exception Message: " + exception.Message );
    }//else if ( httpContext == null )
     	
   }//catch ( Exception exception )
  }//public static void MacAddress( ref String hostName, ref String communityName )

  ///<summary>SNMPGet</summary>
  ///<param name="request">Request</param>
  ///<param name="hostName">Host name</param>
  ///<param name="communityName">Community name</param>
  ///<param name="MIBSMIstring">MIBSMIstring</param>
  ///<param name="packetResponse">packetResponse</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  ///<param name="socketExceptionErrorCode">socketExceptionErrorCode</param>
  ///<param name="snmpHostRequestLength">snmpHostRequestLength</param>
  ///<param name="socketHostRequestLength">socketHostRequestLength</param>  
  ///<param name="socketHostAvailable">socketHostAvailable</param>
  ///<param name="socketHostAvailableTimeOut">socketHostAvailableTimeOut.</param>
  ///<param name="socketHostAvailableTimeElapse">socketHostAvailableTimeElapse.</param>  
  public static void SNMPGet
  (
   ref  String  request,
   ref  String  hostName, 
   ref  String  communityName, 
   ref  String  MIBSMIstring,
   ref  byte[]  packetResponse,   
   ref  String  exceptionMessage,
   ref  int     socketExceptionErrorCode,
   ref  int     snmpHostRequestLength,
   ref  int     socketHostRequestLength,
   ref  int     socketHostAvailable,
   ref  int     socketHostAvailableTimeOut,
   ref  int     socketHostAvailableTimeElapse
  )
  {
   byte[]       byteData                       =  null;
   byte[]       MIBSMI                            =  null;

   int          cnt                            =  -1;
   int          comlen                         =  -1;
   int          i                              =  -1;
   int          receive                        =  -1;
   int          MIBSMIlen                         =  -1;
   int          orgMIBSMIlen                      =  -1;
   int          pos                            =  -1;   
   int          socketHostAvailableTimeFrom    =  0;
   int          socketHostAvailableTimeUntil   =  0;
   int          temp                           =  -1;
      
   String[]     MIBSMIvals                        =  null;
   
   Socket       socketHost                     =  null;

   IPHostEntry  iPHostEntryHost                =  null;
   IPEndPoint   iPEndPointHost                 =  null;
   EndPoint     endPointHost                   =  null;
  
   cnt                      =  0;
   comlen                   =  -1;
   i                        =  0;
   receive                  =  -1;
   MIBSMIlen                   =  0;
   orgMIBSMIlen                =  -1;
   pos                      =  0;   
   snmpHostRequestLength    =  -1;
   socketHostAvailable      =  -1;
   socketHostRequestLength  =  -1;
   temp                     =  0;

   try
   {

    if ( hostName == null || hostName == string.Empty )
    {
     hostName = HostName;
    }       	

    if ( communityName == null || communityName == string.Empty )
    {
     communityName = CommunityName;
    }       	

    MIBSMI                =  new byte[1024];
    packetResponse     =  new byte[1024];
   
    comlen             =  communityName.Length;

    MIBSMIvals            =  MIBSMIstring.Split( DelimiterSplitCharObjectIdentifier );
    
    MIBSMIlen             =  MIBSMIvals.Length;
    orgMIBSMIlen          =  MIBSMIvals.Length;

    // Convert the string MIBSMI into a byte array of integer values
    // Unfortunately, values over 128 require multiple bytes
    // which also increases the MIBSMI length
    for ( i = 0; i < orgMIBSMIlen; i++)
    {
     temp = Convert.ToInt16(MIBSMIvals[i]);
     
     if ( temp > 127 )
     {
      MIBSMI[cnt] = Convert.ToByte(128 + (temp / 128));
      MIBSMI[cnt + 1] = Convert.ToByte(temp - ((temp / 128) * 128));
      cnt += 2;
      MIBSMIlen++;
     }//if ( temp > 127 )
     else
     {
      MIBSMI[cnt] = Convert.ToByte(temp);
      cnt++;
     }//else if ( temp > 127 )
    }//for ( i = 0; i < orgMIBSMIlen; i++)

    snmpHostRequestLength = 29 + comlen + MIBSMIlen - 1;  //Length of entire UtilitySNMP packet

    //The UtilitySNMP sequence start
    packetResponse[pos++] = 0x30; //Sequence start
    packetResponse[pos++] = Convert.ToByte( snmpHostRequestLength - 2 );  //sequence size

    //UtilitySNMP version
    packetResponse[pos++] = 0x02; //Integer type
    packetResponse[pos++] = 0x01; //length
    packetResponse[pos++] = 0x00; //UtilitySNMP version 1

    //Community name
    packetResponse[pos++] = 0x04; // String type
    packetResponse[pos++] = Convert.ToByte(comlen); //length

    //Convert community name to byte array
    byteData = Encoding.ASCII.GetBytes( communityName );
    for (i = 0; i < byteData.Length; i++)
    {
     packetResponse[pos++] = byteData[i];
    }

    //Add GetRequest or GetNextRequest value
    if ( request == SNMPRequestGet )
    {
     packetResponse[pos++] = 0xA0;
    } 
    else
    {
     packetResponse[pos++] = 0xA1;
    } 

    packetResponse[pos++] = Convert.ToByte(20 + MIBSMIlen - 1); //Size of total MIBSMI

    //Request ID
    packetResponse[pos++] = 0x02; //Integer type
    packetResponse[pos++] = 0x04; //length
    packetResponse[pos++] = 0x00; //UtilitySNMP request ID
    packetResponse[pos++] = 0x00;
    packetResponse[pos++] = 0x00;
    packetResponse[pos++] = 0x01;

    //Error status
    packetResponse[pos++] = 0x02; //Integer type
    packetResponse[pos++] = 0x01; //length
    packetResponse[pos++] = 0x00; //UtilitySNMP error status

    //Error index
    packetResponse[pos++] = 0x02; //Integer type
    packetResponse[pos++] = 0x01; //length
    packetResponse[pos++] = 0x00; //UtilitySNMP error index

    //Start of variable bindings
    packetResponse[pos++] = 0x30; //Start of variable bindings sequence

    packetResponse[pos++] = Convert.ToByte(6 + MIBSMIlen - 1); // Size of variable binding

    packetResponse[pos++] = 0x30; //Start of first variable bindings sequence
    packetResponse[pos++] = Convert.ToByte(6 + MIBSMIlen - 1 - 2); // size
    packetResponse[pos++] = 0x06; //Object type
    packetResponse[pos++] = Convert.ToByte(MIBSMIlen - 1); //length

    //Start of MIBSMI
    packetResponse[pos++] = 0x2b;
    
    //Place MIBSMI array in packet
    for( i = 2; i < MIBSMIlen; i++ )
    {
     packetResponse[pos++] = Convert.ToByte(MIBSMI[i]);
    }//for( i = 2; i < MIBSMIlen; i++ )
    
    packetResponse[pos++] = 0x05; //Null object value
    packetResponse[pos++] = 0x00; //Null

    //Send packet to destination
    socketHost = new Socket
    (
     AddressFamily.InterNetwork, 
     SocketType.Dgram,
     ProtocolType.Udp
    );
    
    socketHost.SetSocketOption
    (
     SocketOptionLevel.Socket,
     SocketOptionName.ReceiveTimeout,
     5000
    );

    iPHostEntryHost  =  Dns.Resolve( hostName );
    
    iPEndPointHost   =  new IPEndPoint
    (
     iPHostEntryHost.AddressList[0], 
     161
    );

    endPointHost     =  ( EndPoint ) iPEndPointHost;
       
    socketHostRequestLength = socketHost.SendTo
    (
     packetResponse, 
     snmpHostRequestLength, 
     SocketFlags.None, 
     iPEndPointHost
    );

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

    //Receive response from packet
    receive = socketHost.ReceiveFrom
    (
         packetResponse, 
     ref endPointHost
    );
    
   }//try
   catch ( SocketException socketException )
   {
    packetResponse[0] = 0xff;
    socketExceptionErrorCode = socketException.ErrorCode;
    exceptionMessage = "SocketException: " + socketException.Message;
    System.Console.WriteLine("Socket Exception: {0}", socketException.Message);
   }//catch ( SocketException socketException )
   catch ( Exception exception )
   {
    packetResponse[0] = 0xff;
    exceptionMessage = "Exception: " + exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message);    
   }//catch ( SocketException socketException )

   #if ( DEBUG )       
    System.Console.WriteLine
    (
     "SNMP Request: {0} | HostName: {1} | Community Name: {2} | MIBSMIString: {3} | PacketResponse: {4} | Exception Message: {5} | Socket Error Code: {6} | Socket Host Available: {7} bytes | Timeout: {8} | Elapse: {9}", 
     request,
     hostName,
     communityName,
     MIBSMIstring,
     packetResponse,
     exceptionMessage,
     socketExceptionErrorCode,
     socketHostAvailable,
     socketHostAvailableTimeOut,
     socketHostAvailableTimeElapse
    );
   #endif

  }//public static void SNMPGet()

  ///<summary>GetnextMIBSMI</summary>
  ///<param name="MIBSMIin">MIBSMIin</param>
  ///<param name="output">output</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void GetNextMIBSMI
  (
   ref byte[]  MIBSMIin,
   ref String  output,
   ref String  exceptionMessage
  )
  {
   int  commLength  =  -1;
   int  i           =  -1;
   int  MIBSMILength   =  -1;
   int  MIBSMIStart    =  -1;
   int  MIBSMIValue    =  -1;
      
   output           =  "1.3";
   commLength       =  MIBSMIin[6];
   
   //find the start of the MIBSMI section
   MIBSMIStart         =  6 + commLength + 17;
   
   //The MIBSMI length is the length defined in the SNMP packet
   // minus 1 to remove the ending .0, which is not used
   MIBSMILength        =  MIBSMIin[MIBSMIStart] - 1;

   //skip over the length and 0x2b values
   MIBSMIStart         += 2;

   try
   {
    for ( i = MIBSMIStart; i < MIBSMIStart + MIBSMILength; i++ )
    {
     MIBSMIValue = Convert.ToInt16( MIBSMIin[i] );
    
     if ( MIBSMIValue > 128 )
     {
      MIBSMIValue = ( MIBSMIValue/128 ) * 128 + Convert.ToInt16( MIBSMIin[i+1] );
      i++;
     }
    
     output += "." + MIBSMIValue;
    
    }//for ( i = MIBSMIStart; i < MIBSMIStart + MIBSMILength; i++ )
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )
    
  }//public static void GetnextMIBSMI( byte[] MIBSMIin, String output, String exceptionMessage )

  ///<summary>ConvertIntToTime</summary>
  ///<param name="timeHundredth">timeHundredth</param>
  ///<param name="timeValue">timeValue</param>
  public static void ConvertIntToTime
  (
   ref  int     timeHundredth, 
   ref  String  timeValue
  )
  {
  	
   int  hour          =  0;
   int  hundredth     =  0;   
   int  minute        =  0;
   int  second        =  0;
   int  timeDivisor   =  timeHundredth;
   
   hour          =  timeDivisor / 360000; 
   timeDivisor   =  timeDivisor - ( hour * 360000 );

   minute        =  timeDivisor / 3600;
   if ( minute > 60 )
   {
    hour   += minute / 60;
    minute %= 60;   	
   }   	
   timeDivisor   =  timeDivisor - ( minute * 3600 );

   second        =  timeDivisor / 60; 
   if ( second > 60 )
   {
    minute += second / 60;
    second %= 60;   	
   }   	
   hundredth     =  timeDivisor; 

   timeValue = String.Format("{0}:{1}:{2}:{3}", hour, minute, second, hundredth);
  
   /*
   #if (DEBUG)
    System.Console.WriteLine("Time: {0}={1}:{2}:{3}:{4}", timeValue, hour, minute, second, hundredth);
    System.Console.WriteLine("Time: {0}={1}", timeHundredth, hour * 360000 + minute * 3600 + second * 60 + hundredth );
   #endif
   */

  }//public static void ConvertTime
  
  static UtilitySNMP()
  {
   DelimiterSplitCharObjectIdentifier = DelimiterSplitStringObjectIdentifier.ToCharArray();
   
   HostName = IPAddress.Loopback.ToString();
  }//static UtilitySNMP()
   
 }//public class UtilitySNMP
}//namespace WordEngineering