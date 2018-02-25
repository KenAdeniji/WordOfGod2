using System;
using System.Web.UI;

namespace WordEngineering
{
 ///<summary>UtilityJavaScript.</summary>
 ///<remarks>UtilityJavaScript.</remarks>
 public class UtilityJavaScript
 {
  /// <summary>SetFocus().</summary>
  public static void SetFocus
  (
   Page                  pageCurrent,
   System.Web.UI.Control controlCurrent
  )
  {
  	string setFocus = null;
  	
    // Define the JavaScript function for the specified control.
    setFocus = "<script language='javascript'>" + "document.getElementById('" + controlCurrent.ClientID + "').focus();</script>";

    // Add the JavaScript code to the page.
    pageCurrent.RegisterStartupScript("SetFocus", setFocus);
  }//private void SetFocus(System.Web.UI.Control controlCurrent)

  /// <summary>WindowOpen().</summary>
  public static String WindowOpen
  (
   String URI,
   String Option
  )
  {
   String  windowOpen  =  null;
   windowOpen  =  "window.open(" + '"' + URI + '"' + ", null, " + '"' + Option + '"' + ')';
   return ( windowOpen );
  }//public static void WindowOpen()

 }//public class UtilityJavaScript
}//namespace WordEngineering