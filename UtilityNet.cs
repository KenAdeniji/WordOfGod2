using System;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WordEngineering
{
 /// <summary>UtilityNet</summary>
 /// <remarks>UtilityNet.</remarks>
 [Serializable]
 [XmlRoot("UtilityNet", IsNullable = false)] 
 public class UtilityNet
 {

  /// <summary>enum InternetGetConnectedState.</summary>
  enum EnumInternetGetConnectedState
  {
   LAN           = 0x2,   //&H2,        
   Modem         = 0x1,   //&H1,        
   Proxy         = 0x4,   //&H4,        
   Offline       = 0x20,  //&H20,        
   Configured    = 0x40,  //&H40,
   RasInstalled  = 0x10   //&H10    
  };

  /// <summary>wininet.InternetGetConnectedState</summary>
  [DllImport("wininet.dll")]
  private extern static long InternetGetConnectedState
  ( 
   out long dwFlags,
       long dwReserved 
  );

  /// <summary>Hostname regular expression. It allows only alphanumeric input string between 2 to 40 character long. Regex(@"^[a-zA-Z]\w{1,39}$").</summary>
  public const string RegexHostnameString = @"^[a-zA-Z]\w{1,39}$";

  /// <summary>Hostname regular expression. It allows only alphanumeric input string between 2 to 40 character long. Regex(@"^[a-zA-Z]\w{1,39}$").</summary>
  public static Regex RegexHostname = null;

  /// <summary>ASCIIEncoding.</summary>
  public static System.Text.ASCIIEncoding aSCIIEncoding = null;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main
  (
   string[] argv
  )
  {
   Stub();
  }

  ///<summary>Stub</summary>
  public static void Stub()
  {

   String      hostName         = null;
   String      exceptionMessage = null;
   
   IPHostEntry iPHostEntry      = null;
   
   DNSDomainNameResolution
   (
    ref hostName,
    ref iPHostEntry,
    ref exceptionMessage
   );

   RegularExpressionMatch
   (
    ref hostName   
   ); 

   UtilityInternetGetConnectedState( ref exceptionMessage );
   
   ShowNetworkInformation();
   
   PingLocalHost();

  }//public static void Stub()

  /// <summary>Domain name resolution (DNS).</summary>
  public static void DNSDomainNameResolution()
  {
  
   string exceptionMessage    = null;
   string hostName            = null;
   
   IPHostEntry   iPHostEntry  =  null;
   
   DNSDomainNameResolution
   (
    ref hostName,
    ref exceptionMessage
   );  	

   DNSDomainNameResolution 
   (
    ref hostName,
    ref iPHostEntry,
    ref exceptionMessage
   ); 

  }//public static void DNSDomainNameResolution()
  
  /// <summary>Domain name resolution (DNS).</summary>
  /// <param name="hostName">Hostname.</param>
  /// <param name="exceptionMessage">Exception Message.</param>  
  public static void DNSDomainNameResolution 
  (
   ref string hostName,
   ref string exceptionMessage
  )
  {

   try
   {
	hostName = Dns.GetHostName();
    #if (DEBUG)
     System.Console.WriteLine("Local hostname: {0}", hostName);
    #endif  
   }//try   
   catch (SocketException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SocketException: {0}", exception.Message );
   }
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
   }
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }
   finally
   {
   }//finally
  }////public static void DNSDomainNameResolution()

  /// <summary>Domain name resolution (DNS).</summary>
  /// <param name="hostName">Hostname.</param>
  /// <param name="iPHostEntry">IPHostEntry.</param>  
  /// <param name="exceptionMessage">Exception Message.</param>  
  public static void DNSDomainNameResolution 
  (
   ref string      hostName,
   ref IPHostEntry iPHostEntry,
   ref string      exceptionMessage
  )
  {

   long          scopeId = -1;
   
   try
   {
	
	DNSDomainNameResolution
	(
	 ref hostName,
	 ref exceptionMessage
	);
	 
	iPHostEntry = Dns.GetHostByName( hostName );

    #if (DEBUG)
     System.Console.WriteLine("Host name: " + iPHostEntry.HostName);
     if ( iPHostEntry.AddressList.Length > 0 )
     {
      System.Console.WriteLine("IP address List: ");
      foreach(IPAddress iPAddress in iPHostEntry.AddressList) 
      {
       if ( iPAddress.AddressFamily == AddressFamily.InterNetworkV6 )
       {
        scopeId = iPAddress.ScopeId;
       } 

       System.Console.WriteLine
       (
        "Address: {0} | Family: {1} | Scope: {2}", 
        iPAddress,
        iPAddress.AddressFamily, //Display the type of address family supported by the server. If the server is IPv6-enabled this value is: InternNetworkV6. If the server is also IPv4-enabled there will be an additional value of InterNetwork.
        scopeId
       );
      }//foreach(IPAddress iPAddress in iPHostEntry.AddressList) 
     }//if ( iPHostEntry.AddressList.Length > 0 )
     if ( iPHostEntry.Aliases.Length > 0 )
     {
      System.Console.WriteLine("Host aliases: ");     
      foreach(String hostAlias in iPHostEntry.Aliases) 
      {
       System.Console.WriteLine( hostAlias );
      }
     }//if ( iPHostEntry.Aliases.Length > 0 ) 
    #endif  
   }//try   
   catch (ArgumentNullException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "ArgumentNullException: {0}", exception.Message );
   }
   catch (SocketException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SocketException: {0}", exception.Message );
   }
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
   }
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }
   finally
   {
   }//finally
  }////public static void DNSDomainNameResolution()

  /// <summary>Hostname regular expression.</summary>
  /// <param name="hostName">Hostname.</param>
  public static bool RegularExpressionMatch
  (
   ref string hostName
  )
  {
   bool hostNameRegularExpressionMatch = false;
   
   hostNameRegularExpressionMatch = (RegexHostname.Match(hostName)).Success;

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Regular Expression Host name: {0} | Match: {1}", 
     hostName,
     hostNameRegularExpressionMatch
    );
   #endif  
   
   return ( hostNameRegularExpressionMatch );
  }

  /// <summary>UtilityInternetGetConnectedState.</summary>
  public static void UtilityInternetGetConnectedState
  (
   ref String exceptionMesssage
  )
  {
   long                           internetGetConnectedStateFlag    =  -1;
   long                           internetGetConnectedStateStatus  =  -1;

   EnumInternetGetConnectedState  enumInternetGetConnectedState;

   HttpContext                    httpContext                      =  HttpContext.Current;

   try
   {
    internetGetConnectedStateStatus = InternetGetConnectedState
    ( 
     out internetGetConnectedStateFlag,
         0
    );

    System.Console.WriteLine
    (
     "InternetGetConnectedState Status: {0} | Flag: {1}",
     internetGetConnectedStateStatus,
     internetGetConnectedStateFlag
    );

    /*
    if ( internetGetConnectedStateStatus != 0 )
    {
     if ( ( EnumInternetGetConnectedState.LAN && internetGetConnectedStateFlag ) == EnumInternetGetConnectedState.LAN )
     {
      //System.Console.WriteLine("enumInternetGetConnectedState");
     }
    }//if ( internetGetConnectedStateStatus != 0 )
    */
   }//try
   catch ( Exception exception )
   {
    exceptionMesssage = "Exception: " + exception.Message;
    if ( httpContext == null )
    {
     System.Console.WriteLine("Exception: {0}", exception.Message);
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMesssage );
    }//else
   }//catch ( Exception exception )
  }//public static void UtilityInternetGetConnectedState

  ///<summary>ShowNetworkInformation()</summary>
  ///<remarks>http://www.codeguru.com/Csharp/Csharp/cs_network/configurationfilesinis/article.php/c9061__1/</remarks>
  public static void ShowNetworkInformation()
  {
   IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

   System.Console.WriteLine("Host name: {0}", ipProperties.HostName);
   System.Console.WriteLine("Domain name: {0}", ipProperties.DomainName);

   foreach ( NetworkInterface networkCard in NetworkInterface.GetAllNetworkInterfaces() )
   {
    System.Console.WriteLine("Interface: {0}", networkCard.Id);
    System.Console.WriteLine("\t Name: {0}", networkCard.Name);
    System.Console.WriteLine("\t Description: {0}", networkCard.Description);
    System.Console.WriteLine("\t Status: {0}", networkCard.OperationalStatus);
    System.Console.WriteLine("\t MAC Address: {0}", networkCard.GetPhysicalAddress().ToString());
    System.Console.WriteLine("\t Gateway Address:");
    foreach ( GatewayIPAddressInformation gatewayAddr in networkCard.GetIPProperties().GatewayAddresses )
    {
     System.Console.WriteLine("\t\t Gateway entry: {0}", gatewayAddr.Address.ToString());
    }//foreach ( GatewayIPAddressInformation gatewayAddr in networkCard.GetIPProperties().GatewayAddresses )

    System.Console.WriteLine("\t DNS Settings:");
    foreach ( IPAddress address in networkCard.GetIPProperties().DnsAddresses )
    {
     System.Console.WriteLine("\t\t DNS entry: {0}", address.ToString());
    }//foreach ( IPAddress address in networkCard.GetIPProperties().DnsAddresses )

    System.Console.WriteLine("Current IP Connections:");
    foreach ( TcpConnectionInformation tcpConnection in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections() )
    {
     System.Console.WriteLine("\t Connection Info:");
     System.Console.WriteLine("\t\t Remote Address: {0}", tcpConnection.RemoteEndPoint.Address.ToString());
     System.Console.WriteLine("\t\t State:", tcpConnection.State.ToString());
    }//foreach ( TcpConnectionInformation tcpConnection in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections() )
   }//foreach ( NetworkInterface networkCard in NetworkInterface.GetAllNetworkInterfaces() )
  }//public static void ShowNetworkInformation()

  ///<summary>PingLocalHost()</summary>
  ///<remarks>http://www.codeguru.com/Csharp/Csharp/cs_network/configurationfilesinis/article.php/c9061__1/</remarks>
  public static void PingLocalHost()
  {
   Ping ping = new Ping();
   PingReply pingReply = null;

   // Synchronously ping the local host
   pingReply = ping.Send("127.0.0.1");

   // Display the status result
   System.Console.WriteLine("Ping Status: {0}", pingReply.Status.ToString());

   // Show the amount of time
   System.Console.WriteLine("Elapsed Time: {0}", pingReply.RoundtripTime);
  }//public static void PingLocalHost()
  
  /*
   LAN           = 0x2,   //&H2,        
   Modem         = 0x1,   //&H1,        
   Proxy         = 0x4,   //&H4,        
   Offline       = 0x20,  //&H20,        
   Configured    = 0x40,  //&H40,
   RasInstalled  = 0x10   //&H10    
  */

  ///<summary>Static.</summary>
  static UtilityNet()
  {
   RegexHostname = new Regex( RegexHostnameString );
   aSCIIEncoding = new System.Text.ASCIIEncoding();
  }//static UtilityNet()	

 }//public class UtilityNet
}//namespace WordEngineering