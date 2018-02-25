using System;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityRequest</summary>
 public class UtilityRequest
 {

  //************************************************************************
  //
  //   ROUTINE: requestIsFromLocalMachine
  //
  //   DESCRIPTION: This routine checks to see if the page request came
  //                from the local machine.
  //
  //   NOTE: Since requests on a local machine can be in the form
  //         http://localhost/site/page or http://server/site/page,
  //         two checks are required.  The first is for the localhost 
  //         loopback IP address (127.0.0.1) and the second is for the 
  //         actual IP address of the requestor.
  //------------------------------------------------------------------------
  /// <summary>RequestIsFromLocalMachine</summary>
  /// <remarks>http://localhost/MichaelKittel_GeoffLeBlond/CH10TestDynamicPageTracingCS.aspx</remarks>
  public static Boolean RequestIsFromLocalMachine()
  {
   Boolean          isLocal;
   string           localAddress;

   HttpContext      httpContext =  HttpContext.Current;

   if ( httpContext == null )
   {
    return ( false );
   }//if ( httpContext == null )

   // Is browser fielding request from localhost?
   isLocal = httpContext.Request.UserHostAddress.Equals("127.0.0.1");

   if ( !isLocal )
   {
    // Get local IP address from server variables
    localAddress = httpContext.Request.ServerVariables.Get("LOCAL_ADDR");

    // Compare local IP with IP address that accompanied request
    isLocal = httpContext.Request.UserHostAddress.Equals(localAddress);
   }//if ( !isLocal )

   return ( isLocal );

  }//public static Boolean RequestIsFromLocalMachine()
  
 }//public class UtilityRequest
}//namespace WordEngineering