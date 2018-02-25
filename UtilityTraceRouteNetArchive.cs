#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.NetworkInformation;

#endregion

namespace WordEngineering
{
 ///<summary>UtilityTraceRouteNet</summary>
 ///<remarks>http://weblogs.asp.net/justin_rogers/archive/2004/06/09/151634.aspx Justin Rogers</remarks>
 public class UtilityTraceRouteNet
 {
  private static string usageString = 
  @"Usage: TraceRoute [-d] [-h maximum_hops] [-j host-list] [-w timeout] target_name
    Options:
    -d                 Do not resolve addresses to hostnames.
    -h maximum_hops    Maximum number of hops to search for target.
    -w timeout         Wait timeout milliseconds for each reply.
   ";
  
  ///<summary>Main()</summary>
  static void Main( string[] args ) 
  {
   if ( args.Length == 0 ) 
   {
    System.Console.WriteLine( usageString );
    Environment.Exit( 1 );
   }
   
   string targetName = null;
   IPAddress target = IPAddress.None;
   bool resolveHostnames = true;
   int maxHops = 30;
   int timeout = 30000;
   int ttl = 1;
   Ping pinger = new Ping();
   bool finished = false;

   for ( int i = 0; i < args.Length; i++ )
   {
    if ( args[i].StartsWith( "-" ) ) 
    {
     if ( args[i] == "-d" ) 
     {
      resolveHostnames = false;
     } 
     else if ( args[i] == "-h" && ( i + 1 ) < args.Length ) 
     {
      i++;
      int.TryParse( args[i], out maxHops );
     } 
     else if ( args[i] == "-w" && ( i + 1 ) < args.Length ) 
     {
      i++;
      int.TryParse( args[i], out timeout );
     }
    } else 
    {
     try 
     {
      target = IPAddress.Parse( args[i] );
     } 
     catch 
     {
      try 
      {
       IPHostEntry hostEntry = Dns.GetHostByName( args[i] );
       targetName = hostEntry.HostName;
       target = hostEntry.AddressList[0];
      }//try 
      catch 
      {
       System.Console.WriteLine( usageString );
       Environment.Exit( 2 );
      }//catch
     }//catch
    }//else
   }//for ( int i = 0; i < args.Length; i++ )

   System.Console.WriteLine();
   System.Console.Write( "Tracing route to {0} ", ( targetName != null ) ? targetName : target.ToString() );
   if ( targetName != null ) 
   { 
    System.Console.Write( "[{0}] ", target.ToString() ); 
   }
   System.Console.WriteLine( "over a maximum of {0} hops:", maxHops );
   System.Console.WriteLine();

   do 
   {
    PingReply reply = pinger.Send( target, new byte[0], timeout, new PingOptions( ttl++, true ) );
    IPAddress replyAddress = reply.Address;
    if ( reply.Status == IPStatus.Success ) 
    {
     finished = true;
    }

    System.Console.Write( "{0, 3}", ttl - 1 );
    PingReply timing = pinger.Send( replyAddress, new byte[50], timeout, new PingOptions( 128, true ) );
    System.Console.Write( "{0, 5} ms", ( timing.Status == IPStatus.Success ) ? timing.RoundTripTime.ToString() : "*" );
    timing = pinger.Send( replyAddress, new byte[50], timeout, new PingOptions( 128, true ) );
    System.Console.Write( "{0, 5} ms", ( timing.Status == IPStatus.Success ) ? timing.RoundTripTime.ToString() : "*" );
    timing = pinger.Send( replyAddress, new byte[50], timeout, new PingOptions( 128, true ) );
    System.Console.Write( "{0, 5} ms", ( timing.Status == IPStatus.Success ) ? timing.RoundTripTime.ToString() : "*" );

    string hostName = null;
    if ( resolveHostnames ) 
    {
     try 
     {
      IPHostEntry hostEntry = Dns.GetHostByAddress( replyAddress );
      if ( hostEntry.HostName != null && hostEntry.HostName != string.Empty ) 
      {
       hostName = hostEntry.HostName;
      }
     } 
     catch 
     { 
     }
    }

    System.Console.WriteLine
    (
     ( hostName != null ) ? "  {0} [{1}]" : "  {0}",
     ( hostName != null ) ? hostName : replyAddress.ToString(),
     replyAddress 
    );
   
   } while ( !finished && ttl <= maxHops );

   System.Console.WriteLine();
   System.Console.WriteLine( "Trace complete, {0}", ( finished ) ? "destination reached" : "ttl expired reaching destination" );
  }//static void Main( string[] args )
 }//public class UtilityTraceRouteNet
}//namespace WordEngineering