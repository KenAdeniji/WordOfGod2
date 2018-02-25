using  System;
using  System.Collections;
using  System.Web.UI;
using  System.Web.UI.HtmlControls;
using  System.Web.UI.WebControls;
using  System.Data;
using  System.Data.OleDb;
using  System.Data.SqlClient;
using  System.Text;

using  WordEngineering;

namespace WordEngineering
{
 /// <summary>WordSaid page.</summary>
 /// <remarks>WordSaid page.</remarks>
 public class WordSaidPage : Page
 {

  /// <summary>The document unique Id.</summary>
  protected HtmlInputHidden HtmlInputHiddenDocumentUniqueId;

  /// <summary>The document dated.</summary>
  protected TextBox         TextBoxDocumentDated;
  
  /// <summary>The document title.</summary>
  protected TextBox         TextBoxDocumentTitle;  

  /// <summary>The database connection string.</summary>
  public string databaseConnectionString       = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=Bible;";
  
  /// <summary>The configuration XML filename.</summary>
  public string filenameConfigurationXml = @"WordEngineering.config";

  /// <summary>The server map path.</summary>
  public string serverMapPath                  = null;

  /// <summary>The database connection string.</summary>
  const  string XPathDatabaseConnectionString               = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>DocumentDated.</summary>
  public string DocumentDated
  {
   get
   {
    return ( TextBoxDocumentDated.Text.Trim() );
   } 
   set
   {
    TextBoxDocumentDated.Text = value;
   }
  }//public string DocumentDated
  
  /// <summary>DocumentTitle.</summary>
  public string DocumentTitle
  {
   get
   {
    return ( TextBoxDocumentTitle.Text.Trim() );
   } 
   set
   {
    TextBoxDocumentTitle.Text = value;
   }
  }//public string DocumentTitle

  /// <summary>DocumentUniqueId.</summary>
  public Guid DocumentUniqueId
  {
   get
   {
    return ( new Guid( HtmlInputHiddenDocumentUniqueId.Value.Trim() ) );
   } 
   set
   {
    HtmlInputHiddenDocumentUniqueId.Value = value.ToString();
   }
  }//public string DocumentUniqueId

  /// <summary>ButtonReset_Click().</summary>
  public void ButtonReset_Click
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   PageReset(); 
  }//ButtonReset_Click() 

  /// <summary>ButtonSubmit_Click().</summary>
  public void ButtonSubmit_Click
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   PageSubmit(); 
  }//ButtonSubmit_Click() 
       
  /// <summary>Page Load.</summary>
  public void Page_Load
  (
   object     sender, 
   EventArgs  e
  ) 
  {
  
   string exceptionMessage = null;
   
   if (!Page.IsPostBack) 
   {
    DocumentUniqueId = System.Guid.NewGuid();
   }//if (!Page.IsPostBack)  

   serverMapPath = this.MapPath("");
   if ( serverMapPath != null)
   {
    filenameConfigurationXml = serverMapPath + @"\" + filenameConfigurationXml;
   }//if ( serverMapPath != null)
   UtilityXml.XmlDocumentNodeInnerText
   (
        filenameConfigurationXml,
    ref exceptionMessage,
        XPathDatabaseConnectionString,
    ref databaseConnectionString
   );
   if ( exceptionMessage != null )
   {
    return;
   }//if ( exceptionMessage != null ) 
   
  }//Page_Load

  ///<summary>Page Reset.</summary>  
  protected void PageReset()
  {
   DocumentDated = null;
   DocumentTitle = null;
  }//protected void PageReset    

  ///<summary>Page Submit.</summary>  
  protected void PageSubmit()
  {
  }//protected void PageSubmit.
     	
 }//WordSaidPage class.
}//WordEngineering namespace.