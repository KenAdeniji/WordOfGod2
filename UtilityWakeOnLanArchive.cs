using System;
using System.Net;

namespace WordEngineering
{

 /// <summary>UtilityWakeOnLanArgument</summary>
 public class UtilityWakeOnLanArgument
 {
  ///<summary>UDPPort</summary>  
  public int      UDPPort       =  UtilityWakeOnLan.UDPPort;

  ///<summary>MACAddress</summary>  
  public string[] MACAddress    =  null;

  ///<summary>UDPBroadcast</summary>  
  public string   UDPBroadcast  =  UtilityWakeOnLan.UDPBroadcast;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityWakeOnLanArgument()
  :this
  (
   UtilityWakeOnLan.UDPPort,
   null,  //MACAddress
   UtilityWakeOnLan.UDPBroadcast
  ) 
  {
  }//public UtilityWakeOnLanArgument()

  /// <summary>Constructor Overloading</summary>
  public UtilityWakeOnLanArgument
  (
   int      UDPPort,
   string[] MACAddress,
   string   UDPBroadcast
  )
  {
   this.MACAddress    =  MACAddress;
   this.UDPBroadcast  =  UDPBroadcast;
   this.UDPPort       =  UDPPort;
  }//public UtilityWakeOnLanArgument()
 }//public class UtilityWakeOnLanArgument

 ///<summary>UtilityWakeOnLan</summary>
 ///<remarks>
 /// Alexander Duggleby MCC.in.tum.de/support/development/LanWaker/ Microsoft Competence Center am Lehrstuhl Informatik IV: Software &amp; Systems Engineering
 /// </remarks>
 public class UtilityWakeOnLan
 {
  ///<summary>MagicPacketHeader</summary>
  public static byte[] MagicPacketHeader = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

  ///<summary>MACAddressRepeat</summary>
  public const int     MACAddressRepeat    =  16;

  ///<summary>UDPPort</summary>
  public const int     UDPPort       = 40000;

  ///<summary>UDPBroadcast</summary>
  public const string  UDPBroadcast  = "255.255.255.255";

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of arguments</param>
  public static void Main( string[] argv )
  {
   Boolean                   booleanParseCommandLineArguments  =  false;
   string                    exceptionMessage                  =  null;
   UtilityWakeOnLanArgument  utilityWakeOnLanArgument          =  null;
   
   utilityWakeOnLanArgument         =  new UtilityWakeOnLanArgument();
   
   booleanParseCommandLineArguments  =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityWakeOnLanArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityWakeOnLanArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   SendMagicPacket
   (
    ref utilityWakeOnLanArgument,
    ref exceptionMessage
   );
   
  }//public static void Main()
 
  /// <summary>SendMagicPacket</summary>
  public static void SendMagicPacket
  (
   ref UtilityWakeOnLanArgument utilityWakeOnLanArgument,
   ref string                   exceptionMessage
  )
  {
   int                           magicPacketIndex  =  -1;
   byte[]                        magicPacket       =  null;
   byte[]                        byteMACAddress    =  null;
   string                        MACAddress        =  null;
   System.Net.Sockets.UdpClient  udpClient         =  null;
   try
   {
    udpClient = new System.Net.Sockets.UdpClient
    ( 
     UtilityWakeOnLan.UDPBroadcast, 
     UtilityWakeOnLan.UDPPort 
    );
    for( int MACAddressIndex = 0; MACAddressIndex < utilityWakeOnLanArgument.MACAddress.Length; ++MACAddressIndex )
    {
     MACAddress = utilityWakeOnLanArgument.MACAddress[MACAddressIndex];
     MACAddress = MACAddress.Replace("-", String.Empty);
     byteMACAddress = UtilityHex.ToByteArray( MACAddress );
     magicPacket     =  new byte[MagicPacketHeader.Length + ( byteMACAddress.Length * MACAddressRepeat ) ];
     for ( magicPacketIndex = 0; magicPacketIndex < MagicPacketHeader.Length; ++magicPacketIndex )
     {
      magicPacket[magicPacketIndex] = MagicPacketHeader[magicPacketIndex];
     }
     for ( int magicPacketRepeat = 0; magicPacketRepeat < MACAddressRepeat; ++magicPacketRepeat )
     {
      for ( int byteMACAddressIndex = 0; byteMACAddressIndex < byteMACAddress.Length; ++byteMACAddressIndex )
      {
       magicPacket[magicPacketIndex] = byteMACAddress[byteMACAddressIndex];
       ++magicPacketIndex;
      }
     }
     udpClient.Send( magicPacket, magicPacket.Length );
    }//for( int MACAddressIndex = 0; MACAddressIndex < utilityWakeOnLanArgument.MACAddress.Length; ++MACAddressIndex )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
  }//public static void SendMagicPacket
 }//public class UtilityWakeOnLan
}//namespace WordEngineering
