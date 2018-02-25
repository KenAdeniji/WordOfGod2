using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace WordEngineering
{
 /// <summary>UserPage</summary>
 public class UserPage : Page
 {
  /// <summary>GridViewUser</summary>
  protected System.Web.UI.WebControls.GridView               GridViewUser;

  /// <summary>Page Load.</summary>
  public void Page_Load
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   String     exceptionMessage  =  null;
   DataSet    dataSet           =  null;
   Hashtable  userLogin         =  null;
   
   UtilityUser.UserLogin
   (
    ref userLogin,
    ref exceptionMessage
   );

   if ( exceptionMessage != null )
   {
    Response.Write( "Exception: " + exceptionMessage );
   	return;
   }	

   UtilityDatabase.DataSetFill
   (    
    ref userLogin,
    ref dataSet
   );
    
   GridViewUser.DataSource = dataSet;
   GridViewUser.DataBind();
  }//Page_Load

 }//UserPage class.
}//WordEngineering namespace.