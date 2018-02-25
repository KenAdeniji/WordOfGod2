using System;
using System.Net;
using System.Net.NetworkInformation;

namespace WordEngineering
{
 /// <summary>UtilityNetworkInformation</summary> 
 /// <remarks>
 ///  Mark Strawmyer Access Newly Available Network Information with .NET 2.0 http://www.codeguru.com/Csharp/Csharp/cs_network/configurationfilesinis/article.php/c9061/
 /// </remarks>
 public class UtilityNetworkInformation
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main(string[] argv)
  {
   NetworkInformationStub();
  }

  ///<summary>NetworkInformationStub</summary>  
  public static void NetworkInformationStub()
  {
   IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

   System.Console.WriteLine("Host name: {0}", ipGlobalProperties.HostName);
   Console.WriteLine("Domain name: {0}", ipGlobalProperties.DomainName);

   foreach ( NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces() )
   {
    System.Console.WriteLine("Interface: {0}", networkInterface.Id);
    System.Console.WriteLine("\t Name: {0}", networkInterface.Name);
    System.Console.WriteLine("\t Description: {0}", networkInterface.Description);
    System.Console.WriteLine("\t Status: {0}", networkInterface.OperationalStatus);
    System.Console.WriteLine("\t MAC Address: {0}", networkInterface.GetPhysicalAddress());
    System.Console.WriteLine("\t Gateway Address:");
    foreach ( GatewayIPAddressInformation gatewayIPAddressInformation in networkInterface.GetIPProperties().GatewayAddresses )
    {
     System.Console.WriteLine("\t\t Gateway entry: {0}", gatewayIPAddressInformation.Address);
    }
    System.Console.WriteLine("\t DNS Settings:");
    foreach ( IPAddress ipAddress in networkInterface.GetIPProperties().DnsAddresses )
    {
     System.Console.WriteLine("\t\t DNS entry: {0}", ipAddress);
    }
    System.Console.WriteLine("Current IP Connections:");
    foreach ( TcpConnectionInformation tcpConnectionInformation in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections() )
    {
     System.Console.WriteLine("\t Connection Info:");
     System.Console.WriteLine("\t\t Remote Address: {0}", tcpConnectionInformation.RemoteEndPoint.Address);
     System.Console.WriteLine("\t\t State:", tcpConnectionInformation.State);
    }
   }
  }
 }
}

