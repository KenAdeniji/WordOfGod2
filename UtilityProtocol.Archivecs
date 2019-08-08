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

 ///<summary>UtilityProtocol</summary>
 ///<remarks>
 ///	2019-08-08T15:00:00 @ precede .
 ///</remarks>
 public class UtilityProtocol
 {

  /// <summary>Constructor.</summary>
  public UtilityProtocol()
  {

  }
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
		System.Console.WriteLine(PrefixProtocol(argv[0]));
  }

  ///<summary>PrefixProtocol</summary>
  public static string PrefixProtocol
  (
   string URI
  ) 
  {
   if ( URI.IndexOf('@') > -1 && URI.IndexOf('@') < URI.IndexOf('.') )
   {
    return("mailto:" + URI);
   }
   else if ( URI.IndexOf(':') < 0 )
   {
    return("http://" + URI);
   }
   else 
   {
    return(URI);
   }
  }
  
  static UtilityProtocol()
  {
  }
  
 }
 
}