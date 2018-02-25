using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace WordEngineering
{
 /// <summary>WindowClosePage</summary>
 /// <remarks>http://vb2themax.com/ShowContent.aspx?ID=ccc400c5-77d2-412b-bd5f-a89cf22ee854</remarks>
 public class WindowClosePage : Page
 {
  /// <summary>ButtonClose</summary>  
  protected System.Web.UI.WebControls.Button ButtonClose;

  /// <summary>Page_Load</summary>
  public void Page_Load
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   if ( !Page.IsPostBack )
   {
    ButtonClose.Attributes.Add("onClick", "javascript:window.close();");
   }
   	
  }

  /// <summary>ButtonClose_Click</summary>
  public void ButtonClose_Click(object sender, EventArgs e)
  {
   Response.Write("<script>window.close();</script>");
  }
	
 }
}