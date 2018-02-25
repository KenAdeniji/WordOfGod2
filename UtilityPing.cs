using System;
using System.Net;
using System.Net.Sockets;

namespace WordEngineering
{
 /// <summary>UtilityPing.</summary>
 /// <remarks>UtilityPing.</remarks>
 public class UtilityPing
 {
  /// <summary>SOCKET_ERROR</summary>
  const int SOCKET_ERROR = -1;

  /// <summary>ICMP_ECHO</summary>
  const int ICMP_ECHO = 8;

  /// <summary>PingAddress</summary>
  public static string[] PingAddress = {
                                        "127.0.0.1",
                                        "localhost"
                                       };

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    string[] argv
  )
  {
   string   exceptionMessage  =  null;
   String[] targetName        =  null;

   targetName = PingAddress;
   
   if ( argv.Length > 0 )
   {
    targetName = argv;
   }

   GetPingTime
   ( 
        targetName, 
    ref exceptionMessage
   );   
   
  }//public static void Main()

  ///<summary>Get ping time in milliseconds.</summary>
  ///<param name="host">Host</param>
  ///<param name="exceptionMessage">Exception Message.</param>
  public static void GetPingTime
  (
       string[] host,
   ref string   exceptionMessage
  )
  {
   foreach( String hostCurrent in host )
   {
    System.Console.WriteLine
    (
     "Reply from {0}: {1} milli-seconds", 
     hostCurrent, 
     GetPingTime
     ( 
          hostCurrent,
      ref exceptionMessage 
     )
    );
   }
  }

  ///<summary>Get ping time in milliseconds.</summary>
  ///<param name="host">Host</param>
  ///<param name="exceptionMessage">Exception Message.</param>
  public static int GetPingTime
  (
       string host,
   ref string exceptionMessage
   )
  {
   int PingTime = 0;
   IPHostEntry serverHE, fromHE;
   int nBytes = 0;
   int dwStart = 0, dwStop = 0;
   IcmpPacket packet = null;

   try
   {

   packet = new IcmpPacket();

   if (host == null)
   {
    return -1;
   }
	    
   Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

   // Get the server endpoint.
   serverHE = Dns.GetHostByName(host);

   if (serverHE == null) 
   {
    return -1; // fail
   }

   // Convert the server IPEndPoint to an EndPoint.
   IPEndPoint ipepServer = new IPEndPoint(serverHE.AddressList[0], 0);
   EndPoint epServer = (ipepServer);	

   // Set the receiving endpoint to the client machine.
   fromHE = Dns.GetHostByName(Dns.GetHostName());
   IPEndPoint ipEndPointFrom = new IPEndPoint(fromHE.AddressList[0], 0);        
   EndPoint EndPointFrom = (ipEndPointFrom);

   int PacketSize = 0;

   for (int j = 0; j < 1; j++)
   {
    // Construct the packet to send.
    packet.Type = ICMP_ECHO;
    packet.SubCode = 0;
    packet.CheckSum = UInt16.Parse("0");
    packet.Identifier   = UInt16.Parse("45"); 
    packet.SequenceNumber  = UInt16.Parse("0"); 

    int PingData = 32; 
    packet.Data = new Byte[PingData];
    for (int i = 0; i < PingData; i++)
    {
     packet.Data[i] = (byte)'#';
    }

    PacketSize = PingData + 8;

    // Make sure that the icmp_pkt_buffer byte array 
    // is an even size.
    if (PacketSize % 2 == 1)
    {
     ++PacketSize;
    }

    Byte [] icmp_pkt_buffer = new Byte[ PacketSize ]; 
    int index = 0;          
    index = Serialize
    (  
     packet, 
     icmp_pkt_buffer, 
     PacketSize, 
     PingData 
    );

    if (index == -1)
    {
     return -1;
    }

    //
    // now get this critter into a UInt16 array
    //
	        
    Double double_length = Convert.ToDouble(index);
    Double dtemp = Math.Ceiling( double_length / 2);
    int cksum_buffer_length = Convert.ToInt32(dtemp);
	        
    UInt16 [] cksum_buffer = new UInt16[cksum_buffer_length];

    int icmp_header_buffer_index = 0;

    for( int i = 0; i < cksum_buffer_length; i++ ) 
    {
     cksum_buffer[i] = BitConverter.ToUInt16(icmp_pkt_buffer,icmp_header_buffer_index);
     icmp_header_buffer_index += 2;
    }

    UInt16 u_cksum = checksum(cksum_buffer, cksum_buffer_length);
    packet.CheckSum  = u_cksum; 
	        
    // Now that we have the checksum, serialize the packet again
    Byte [] sendbuf = new Byte[ PacketSize ]; 
	        
    index = Serialize
    (  
     packet, 
     sendbuf, 
     PacketSize, 
     PingData 
    );

    if (index == -1)
    {
     return -1;
    }

    dwStart = System.Environment.TickCount; // Start timing

    if ((nBytes = socket.SendTo(sendbuf, PacketSize, 0, epServer)) == SOCKET_ERROR) 
    {
     System.Console.WriteLine("Error calling sendto");
     return -1; // fail
    }

    // Initialize the buffers. The receive buffer is the size of the
    // ICMP header plus the IP header (20 bytes)
    Byte [] ReceiveBuffer = new Byte[256]; 

    nBytes = 0;
    nBytes = socket.ReceiveFrom(ReceiveBuffer, 256, 0, ref EndPointFrom);

    if (nBytes == SOCKET_ERROR) 
    {
     dwStop = SOCKET_ERROR;
    } 
    else 
    {
     dwStop = System.Environment.TickCount - dwStart; // stop timing
    }
   }//for (int j = 0; j < 1; j++)
    
   socket.Close();     
   PingTime = (int)dwStop;
   }
   catch ( Exception exception )
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine("Exception: {0}", exception.Message );
   }

   return PingTime;
  }


   private static Int32 Serialize(  IcmpPacket packet, Byte [] Buffer, Int32 PacketSize, Int32 PingData )
   {
		Int32 cbReturn = 0;

		//
		// serialize the struct into the array
		//
		int Index=0;

		Byte [] b_type = new Byte[1];
		b_type[0] = (packet.Type);

		Byte [] b_code = new Byte[1];
		b_code[0] = (packet.SubCode);

		Byte [] b_cksum = BitConverter.GetBytes(packet.CheckSum);
		Byte [] b_id = BitConverter.GetBytes(packet.Identifier);
		Byte [] b_seq = BitConverter.GetBytes(packet.SequenceNumber);
	    
		// Console.WriteLine("Serialize type ");
		Array.Copy( b_type, 0, Buffer, Index, b_type.Length );
		Index += b_type.Length;
	    
		// Console.WriteLine("Serialize code ");
		Array.Copy( b_code, 0, Buffer, Index, b_code.Length );
		Index += b_code.Length;

		// Console.WriteLine("Serialize cksum ");
		Array.Copy( b_cksum, 0, Buffer, Index, b_cksum.Length );
		Index += b_cksum.Length;

		// Console.WriteLine("Serialize id ");
		Array.Copy( b_id, 0, Buffer, Index, b_id.Length );
		Index += b_id.Length;

		Array.Copy( b_seq, 0, Buffer, Index, b_seq.Length );
		Index += b_seq.Length;

		// copy the data
	    
		Array.Copy( packet.Data, 0, Buffer, Index, PingData );

		Index += PingData;

		if( Index != PacketSize/* sizeof(IcmpPacket)  */) 
		{
			cbReturn = -1;
			return cbReturn;
		}

		cbReturn = Index;
		return cbReturn;
	}

	private static UInt16 checksum( UInt16[] buffer, int size )
	{
		Int32 cksum = 0;
		int counter;

		counter = 0;

		while ( size > 0 ) 
		{

			UInt16 val = buffer[counter];

			cksum += Convert.ToInt32( buffer[counter] );
			counter += 1;
			size -= 1;
		}

		cksum = (cksum >> 16) + (cksum & 0xffff);
		cksum += (cksum >> 16);
		return (UInt16)(~cksum);
	}
 } 

 /// <summary>IcmpPacket.</summary>
 /// <remarks>IcmpPacket.</remarks>
 public class IcmpPacket 
 { 
  /// <summary>Type of message.</summary>
  public Byte  Type;

  /// <summary>Type of subcode.</summary>
  public Byte  SubCode;

  /// <summary>Ones complement checksum of struct.</summary>
  public UInt16 CheckSum;

  /// <summary>Identifier.</summary>
  public UInt16 Identifier;

  /// <summary>Sequence number.</summary>
  public UInt16 SequenceNumber;

  /// <summary>Data.</summary>
  public Byte [] Data;
 }
}//namespace WordEngineering