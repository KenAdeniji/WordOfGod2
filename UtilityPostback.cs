using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Security;
using System.Net;
using System.Net.Mail; //2.0
using System.Net.Mime;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mail; //1.0
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace WordEngineering
{

 ///<summary>UtilityPostback</summary>
 ///<remarks>
 ///</remarks>
 public class UtilityPostback
 {

  /// <summary>Constructor.</summary>
  public UtilityPostback()
  {

  }
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
    
  }//static void Main( String[] argv ) 

  ///<summary>ResponseRedirectRequestUrlClearHeader()</summary>
  ///<details>
  ///http://aspalliance.com/687 Terri Morton ASPAlliance.com : ASP and ASP.NET Tutorials : Preventing Duplicate Record Insertion on Page Refresh
  ///Clearing the Header.
  ///A simple solution is to Response.Redirect back to the same page after the successful operation.  
  ///This will call up the page without transmitting any post headers to it.  
  ///Using Request.Url.ToString() as the first parameter of Response.Redirect will cause both the URL and 
  ///the page's querystring to be included in the redirect.  
  ///The use of false as the second parameter will suppress the automatic Response.End 
  ///that may otherwise generate a ThreadAbortedException.  
  ///A disadvantage of this approach is that any ViewState that had been built up will be lost.
  ///</details>
  public static void ResponseRedirectRequestUrlClearHeader()
  {
   HttpContext           httpContext         =  HttpContext.Current;
   
   if ( httpContext == null )
   {
   	return;
   }
   	
   try
   {
    httpContext.Response.Redirect
    (
     httpContext.Request.Url.ToString(), false
    ); //will include the querystring
   }//try
   catch( Exception exception )
   {
    httpContext.Response.Write( "Exception: " + exception.Message );   	
   }//catch( Expression expression )

  }//public static void ResponseRedirectRequestUrlClearHeader()
  
  static UtilityPostback()
  {
  }//static UtilityPostback()
  
 }//public class UtilityPostback
 
}//namespace WordEngineering