using System;
using System.Text;
using System.Web.UI;

namespace WordEngineering
{
 ///<summary>UtilityJavaScript.</summary>
 ///<remarks>UtilityJavaScript.</remarks>
 public class UtilityJavaScript
 {

  /// <summary>GetPostBackControl</summary>
  /// <remarks>http://ryanfarley.com</remarks>
  public static Control GetPostBackControl
  (
   Page page
  )
  {
   Control control = null;

   string ctrlname = page.Request.Params.Get("__EVENTTARGET");
   if ( ctrlname != null && ctrlname != string.Empty )
   {
    control = page.FindControl(ctrlname);
   }//if ( ctrlname != null && ctrlname != string.Empty )
   else
   {
    foreach ( string ctl in page.Request.Form )
    {
     Control c = page.FindControl(ctl);
     if ( c is System.Web.UI.WebControls.Button )
     {
      control = c;
      break;
     }//if ( c is System.Web.UI.WebControls.Button )
    }//foreach ( string ctl in page.Request.Form )
   }//else
   return control;
  }//public static Control GetPostBackControl()
  
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

  /// <summary>SetFocus</summary>
  /// <remarks>
  ///  http://ryanfarley.com/blog/archive/2004/12/21/1325.aspx
  ///  SetFocus(DataGrid1.Items[DataGrid1.EditItemIndex].Cells[1].Controls[0]);
  /// </remarks>
  public static void SetFocus
  (
   Control control
  )
  {
   StringBuilder sb = new StringBuilder();
 
   sb.Append("\r\n<script language='JavaScript'>\r\n");
   sb.Append("<!--\r\n"); 
   sb.Append("function SetFocus()\r\n"); 
   sb.Append("{\r\n"); 
   sb.Append("\tdocument.");
 
   Control p = control.Parent;
   
   while (!(p is System.Web.UI.HtmlControls.HtmlForm)) 
   {
   	p = p.Parent;
   }
   	 
   sb.Append(p.ClientID);
   sb.Append("['"); 
   sb.Append(control.UniqueID); 
   sb.Append("'].focus();\r\n"); 
   sb.Append("}\r\n"); 
   sb.Append("window.onload = SetFocus;\r\n"); 
   sb.Append("// -->\r\n"); 
   sb.Append("</script>");
 
   control.Page.RegisterClientScriptBlock("SetFocus", sb.ToString());
  }//public static void SetFocus()
  
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