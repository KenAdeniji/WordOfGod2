using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace WordEngineering
{
 /// <summary>UtilityUnicodePage.</summary>
 public class UtilityUnicodePage : Page
 {

  /// <summary>The database connection String.</summary>
  public String DatabaseConnectionString                  = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=URI;";

  /// <summary>The configuration XML filename.</summary>
  public String            FilenameConfigurationXml       = @"WordEngineering.config";

  /// <summary>The server map path.</summary>
  public String            ServerMapPath                  = null;

  /// <summary>The XPath database connection String.</summary>
  public const  String     XPathDatabaseConnectionString  = @"/word/database/sqlServer/bible/databaseConnectionString";  

  /// <summary>Hyperlink.</summary>  
  protected System.Web.UI.WebControls.HyperLink  HyperLinkWelcome;

  /// <summary>The exception message.</summary>
  protected Literal        LiteralFeedback;

  /// <summary>Page Load.</summary>
  public void Page_Load
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   PageBuild();
  }//Page_Load

  /// <summary>PageBuild.</summary>
  public void PageBuild()
  {
   String              exceptionMessage                =  null;
   String              feedbackMessage                 =  null;
   
   ServerMapPath = this.MapPath("");
   if ( ServerMapPath != null)
   {
    FilenameConfigurationXml = ServerMapPath + @"\" + FilenameConfigurationXml;
   }//if ( ServerMapPath != null)

   UtilityXml.XmlDocumentNodeInnerText
   (
        FilenameConfigurationXml,
    ref exceptionMessage,         
        XPathDatabaseConnectionString,
    ref DatabaseConnectionString
   );

   UtilityUnicode.Treasure
   (
    ref feedbackMessage,
    ref exceptionMessage
   );

   if ( exceptionMessage != null )
   {
    Feedback = exceptionMessage;
    return;
   }//if ( feedbackMessage != null )
   else
   {
    Feedback = feedbackMessage;
   }   	

  }//PageBuild()
  
  /// <summary>Feedback.</summary>
  public String Feedback
  {
   get
   {
    return ( LiteralFeedback.Text);
   } 
   set
   {
    LiteralFeedback.Text = value;
   }
  }//public String public String Feedback

 }//UtilityUnicodePage class.
}//WordEngineering namespace.