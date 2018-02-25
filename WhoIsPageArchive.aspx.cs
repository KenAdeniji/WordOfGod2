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
 /// <summary>WhoIsPage</summary>
 /// <remarks>Written by: Christoph Wille Translated by: Bernhard Spuida http://www.aspheute.com/english/20000825.asp Displaying Event Log Entries the ASP.NET Way</remarks>
 public class WhoIsPage : Page
 {
  /// <summary>The server map path.</summary>
  public static string ServerMapPath                 = null;

  /// <summary>ButtonReset</summary>  
  protected System.Web.UI.WebControls.Button       ButtonReset;

  /// <summary>ButtonSubmit</summary>  
  protected System.Web.UI.WebControls.Button       ButtonSubmit;

  /// <summary>CheckBoxRegistryDomainSuffixOnly</summary>
  protected System.Web.UI.WebControls.CheckBox     CheckBoxRegistryDomainSuffixOnly;

  /// <summary>ListBoxRegistry</summary>
  protected System.Web.UI.WebControls.ListBox      ListBoxRegistry;

  /// <summary>LiteralFeedback</summary>
  protected System.Web.UI.WebControls.Literal      LiteralFeedback;

  /// <summary>TextBoxDomainName</summary>
  protected System.Web.UI.WebControls.TextBox      TextBoxDomainName;

  /// <summary>TextBoxPortWhoIs</summary>
  protected System.Web.UI.WebControls.TextBox      TextBoxPortWhoIs;

  /// <summary>Page Load.</summary>
  public void Page_Load
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   ServerMapPath = this.MapPath("");

   if ( !Page.IsPostBack )
   {
    if ( ServerMapPath != null)
    {
    }//if ( ServerMapPath != null)
    TextBoxDomainName.Focus();
    Page.SetFocus( TextBoxDomainName );
   }//if ( !Page.IsPostBack )
   	
  }//Page_Load

  /// <summary>ListBoxRegistry_PreRender</summary>
  public void ListBoxRegistry_PreRender
  (
   object     sender, 
   EventArgs  e
  ) 
  {
   string    exceptionMessage  =  null;
   string[]  registry          =  null;
   
   if ( !Page.IsPostBack )
   {
    if ( ListBoxRegistry.Items.Count < 1 )
    {
     UtilityString.ArrayCopy
     (
          UtilityWhoIs.RegistryWhoIs,
      ref registry,
          UtilityWhoIs.RankRegistryWhoIsName,
      ref exceptionMessage
     );
     if ( exceptionMessage != null ) { Feedback = exceptionMessage; }
     ListBoxRegistry.DataSource = registry;
     ListBoxRegistry.DataBind();
    }//if ( ListBoxRegistry.Items.Count < 1 )
    ListBoxRegistry.SelectedValue = registry[0];
   }//if ( !Page.IsPostBack ) 
  }//public void ListBoxRegistry_PreRender()

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

  /// <summary>DomainName</summary>
  public String DomainName  
  {
   get
   {
    return ( TextBoxDomainName.Text );
   } 
   set
   {
    TextBoxDomainName.Text = value;
   }
  }//public String DomainName

  /// <summary>RegistryDomainSuffixOnly</summary>
  public bool RegistryDomainSuffixOnly
  {
   get
   {
    return ( CheckBoxRegistryDomainSuffixOnly.Checked );
   } 
   set
   {
    CheckBoxRegistryDomainSuffixOnly.Checked = value;
   }
  }//public bool RegistryDomainSuffixOnly

  /// <summary>PortWhoIs</summary>
  public int PortWhoIs
  {
   get
   {
    int portWhoIs = UtilityWhoIs.PortWhoIs;
    Int32.TryParse( TextBoxPortWhoIs.Text, out portWhoIs );
    return ( portWhoIs );
   }
   set
   {
    TextBoxPortWhoIs.Text = value.ToString();
   }
  }//public String PortWhoIs
  
  /// <summary>ButtonSubmit_Click().</summary>
  public void ButtonSubmit_Click
  (
   Object sender, 
   EventArgs e
  )
  {
   WhoIsLookup();
  }//public void ButtonSubmit_Click()

  /// <summary>ButtonReset_Click().</summary>
  public void ButtonReset_Click
  (
   Object sender, 
   EventArgs e
  )
  {
   DomainName                     =  null;
   ListBoxRegistry.SelectedValue  =  UtilityWhoIs.RegistryWhoIs[0][UtilityWhoIs.RankRegistryWhoIsName];
   RegistryDomainSuffixOnly       =  false;
   PortWhoIs                      =  UtilityWhoIs.PortWhoIs;
   Page.SetFocus( TextBoxDomainName );
  }//public void ButtonReset_Click()

  /// <summary>WhoIsLookup</summary>
  public void WhoIsLookup()
  {
   string                exceptionMessage      =  null;
   string[]              registry              =  null;
   StringBuilder[][]     sbWhoIs               =  null;
   StringBuilder         sbJoin                =  null;
   UtilityWhoIsArgument  utilityWhoIsArgument  =  null;
   try
   {
    UtilityWebControl.SelectedItem( ListBoxRegistry, ref registry  );
    utilityWhoIsArgument  =  new UtilityWhoIsArgument
                             (
                              RegistryDomainSuffixOnly,
                              PortWhoIs,
                              new string[] { DomainName },
                              registry
                             );
    UtilityWhoIs.WhoisLookup
    (
     ref utilityWhoIsArgument,
     ref sbWhoIs,
     ref exceptionMessage
    );
    if ( exceptionMessage != null ) { Feedback = exceptionMessage; return; }
    sbJoin = new StringBuilder();
    for ( int indexDimension1 = 0; indexDimension1 < sbWhoIs.Length; ++indexDimension1 )
    {
     for ( int indexDimension2 = 0; indexDimension2 < sbWhoIs[indexDimension1].Length; ++indexDimension2 )
     {
      sbJoin.Append( sbWhoIs[indexDimension1][indexDimension2] );
     }//for ( int indexDimension2 = 0; indexDimension2 < sb[indexDimension1].Length; ++indexDimension2 )
    }//for ( int indexDimension1 = 0; indexDimension1 < sb.Length; ++indexDimension1 )  
   }//try
   catch ( Exception exception ) { exceptionMessage = exception.Message; }
   if ( exceptionMessage != null )
   {
   	Feedback = exceptionMessage;
   }
   Feedback = sbJoin.ToString();
  }//public void WhoIsLookup
 }//WhoIsPage class.
}//WordEngineering namespace.