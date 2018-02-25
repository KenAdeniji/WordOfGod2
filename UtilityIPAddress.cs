using System;

namespace WordEngineering
{
 ///<summary>UtilityIPAddress</summary>
 ///<remarks>
 /// Class  MostSignifantBits  NetworkHostBits  LocalAddressBits
 /// A      0                  7                24
 /// B      10                 14               16
 /// C      110                21               8
 /// D      1110               Multicast Address 28
 ///</remarks>
 public static class UtilityIPAddress
 {
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(string[] argv)
  {
   string ipAddress = "127.0.0.1";
   if ( argv.Length > 0 ) { ipAddress = argv[0]; }
   System.Console.WriteLine("IP Address: {0} | Class: {1}", ipAddress, IPAddressClass(ipAddress));
  }

  /// <summary>IPAddressClass</summary>
  public static char IPAddressClass( string ipAddress )
  {
   char ipAddressClass = 'A';
   int firstByte;
   string[] ipAddressSplit;
   ipAddressSplit = ipAddress.Split('.');
   Int32.TryParse(ipAddressSplit[0], out firstByte);
   if ( firstByte >= 224 ) { ipAddressClass = 'D'; }
   else if ( firstByte >= 192 ) { ipAddressClass = 'C'; }
   else if ( firstByte >= 128 ) { ipAddressClass = 'B'; }
   return( ipAddressClass );
  }

 }
}