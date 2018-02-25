using  System;
using  System.Collections;
using  System.Web.UI;
using  System.Web.UI.WebControls;
using  System.Data;
using  System.Data.OleDb;
using  System.Data.SqlClient;
using  System.Text;

using  WordEngineering;

namespace WordEngineering
{
 /// <summary>WordSearchPage.</summary>
 /// <remarks>WordSearchPage.</remarks>
 public class WordSearchPage : Page
 {
 	
  /// <summary>The database connection string.</summary>
  public string databaseConnectionString      = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=Bible;";

  /// <summary>The configuration XML filename.</summary>
  public string exceptionMessage              = null;

  /// <summary>The configuration file.</summary>
  public string filenameConfigurationXml      = @"WordEngineering.config";

  /// <summary>The server map path.</summary>
  public string serverMapPath                 = null;
	
  /// <summary>The query button.</summary>
  protected Button           ButtonQuery;
  
  /// <summary>The bible word.</summary>
  protected DataGrid         DataGridBibleWord;

  /// <summary>The exception message.</summary>
  protected Label            LabelExceptionMessage;

  /// <summary>The scripture word.</summary>
  protected Label            LabelScriptureWord;

  /// <summary>The scripture word.</summary>
  protected DataGrid         DataGridScriptureWord;

  /// <summary>The bible book classification.</summary>
  protected ListBox          ListBoxBibleBookClassificationId;

  /// <summary>The bible book search range minimum.</summary>
  protected DropDownList     DropDownListBibleBookSearchRangeMinimum;

  /// <summary>The bible book search range maximum.</summary>
  protected DropDownList     DropDownListBibleBookSearchRangeMaximum;  

  /// <summary>Find the verse(s) containing all of the word(s).</summary>
  protected RadioButton      RadioButtonFindVersesContainingAllWords;

  /// <summary>Find the verse(s) containing any of the word(s).</summary>
  protected RadioButton      RadioButtonFindVersesContainingAnyWords;

  /// <summary>Find the verse(s) containing a phrase.</summary>
  protected RadioButton      RadioButtonFindVersesContainingPhrase;

  /// <summary>Search the entire bible.</summary>
  protected RadioButton      RadioButtonTestamentEntireBible;

  /// <summary>Search the old testament.</summary>
  protected RadioButton      RadioButtonTestamentOld;  

  /// <summary>Search the new testament.</summary>
  protected RadioButton      RadioButtonTestamentNew;  

  /// <summary>Search the scripture word.</summary>
  protected TextBox          TextBoxScriptureWord;

  /// <summary>Static message.</summary>
  protected Literal          LiteralStaticTextMessage;
   
  /// <summary>Page Load.</summary>
  public void Page_Load
  (
   object     sender, 
   EventArgs  e
  ) 
  {
  
   if (!Page.IsPostBack) 
   {
    DropDownListBibleBookSearchRangeMinimum.DataSource = BibleBookTitle.RetrieveDataSource();
    DropDownListBibleBookSearchRangeMinimum.DataTextField="StringValue";
    DropDownListBibleBookSearchRangeMinimum.DataValueField="IntegerValue";
    DropDownListBibleBookSearchRangeMinimum.DataBind();

    DropDownListBibleBookSearchRangeMaximum.DataSource = BibleBookTitle.RetrieveDataSource();
    DropDownListBibleBookSearchRangeMaximum.DataTextField="StringValue";
    DropDownListBibleBookSearchRangeMaximum.DataValueField="IntegerValue";
    DropDownListBibleBookSearchRangeMaximum.DataBind();
    
    DropDownListBibleBookSearchRangeMinimum.SelectedIndex = 0;
    DropDownListBibleBookSearchRangeMaximum.SelectedIndex = 65;
    
    ListBoxBibleBookClassificationId.DataSource = BibleBookClassification.RetrieveDataSource();
    ListBoxBibleBookClassificationId.DataTextField="StringValue";
    ListBoxBibleBookClassificationId.DataValueField="IntegerValue";
    ListBoxBibleBookClassificationId.DataBind();
    
    ListBoxBibleBookClassificationId.SelectedIndex = -1;
    
   }//if (!Page.IsPostBack)  

   serverMapPath = this.MapPath("");
   if ( serverMapPath != null)
   {
    filenameConfigurationXml = serverMapPath + @"\" + filenameConfigurationXml;
   }//if ( serverMapPath != null)
   WordSearch.ConfigurationXml
   (
    ref filenameConfigurationXml,
    ref exceptionMessage,
    ref databaseConnectionString
   );
   if ( exceptionMessage != null )
   {
    StaticTextMessage = exceptionMessage;
    return;
   }//if ( exceptionMessage != null ) 

   // Manually register the event-handling method for the CheckedChanged event of the CheckBox control
   RadioButtonTestamentEntireBible.CheckedChanged += new EventHandler(this.RadioButtonTestament_CheckedChanged);
   RadioButtonTestamentOld.CheckedChanged += new EventHandler(this.RadioButtonTestament_CheckedChanged);    
   RadioButtonTestamentNew.CheckedChanged += new EventHandler(this.RadioButtonTestament_CheckedChanged);        
   
  }//Page_Load

  /// <summary>Testament checked changed.</summary>

  public void RadioButtonTestament_CheckedChanged(object sender, System.EventArgs e)
  {
   if ( RadioButtonTestamentEntireBible.Checked )
   {
    BibleBookIdMinimum = 1;
    BibleBookIdMaximum = 66;
   }
   else if ( RadioButtonTestamentOld.Checked )
   {
    BibleBookIdMinimum = 1;
    BibleBookIdMaximum = 39;
   }
   else if ( RadioButtonTestamentNew.Checked )
   {
    BibleBookIdMinimum = 40;
    BibleBookIdMaximum = 66;
   }
  }//private void RadioButtonTestament_CheckedChanged(object sender, System.EventArgs e)

  /// <summary>Database Query.</summary>
  public void DatabaseQuery
  (
   Object sender, 
   EventArgs e
  ) 
  {
   string           exceptionMessage = null;
   IDataReader  iDataReader  = null;
   
   try
   {

    WordSearch.DatabaseQuery
    ( 
          databaseConnectionString,
      ref exceptionMessage,
      ref iDataReader,      
          ScriptureWord,
          FindVersesContaining,
          BibleBookIdMinimum,
          BibleBookIdMaximum,   
          null, //ScriptureReference,
          BibleBookClassificationId
    );

    if ( exceptionMessage != null )
    {
     StaticTextMessage = exceptionMessage;
     return;
    }//if ( exceptionMessage != null ) 
    
    DataGridBibleWord.DataSource = iDataReader;
    DataGridBibleWord.DataBind();

   }//try 
   catch (Exception exception) 
   {
    StaticTextMessage = exception.Message;
   }//catch
   finally 
   { 
    if ( iDataReader != null )
    {
     iDataReader.Close();
    }//if ( iDataReader != null ) 
   }//finally    
  }//DatabaseQuery()

  /// <summary>Bible Word.</summary>
  public DataGrid BibleWord
  {
   get
   {
    return( DataGridBibleWord );
   } 
   set
   {
    DataGridBibleWord = value;
   }
  }//public DataGrid BibleWord

  /// <summary>Bible Book Classification Titles.</summary>
  public ArrayList BibleBookClassificationId
  {
   get
   {
    ArrayList bibleBookClassificationId = null;
    if ( ListBoxBibleBookClassificationId.SelectedIndex > -1 )
    {
     bibleBookClassificationId = new ArrayList();
     foreach(ListItem listItem in ListBoxBibleBookClassificationId.Items)
     {
      if(listItem.Selected == true)
      {
       bibleBookClassificationId.Add(Convert.ToInt32(listItem.Value));
      }//if(listItem.Selected == true)
     }//foreach(ListItem listItem in DropDownListBibleBookClassification.Items) 
    }//if ( ListBoxBibleBookClassificationId.SelectedIndex > -1 ) 
    return ( bibleBookClassificationId );
   }//get
   set
   {
    //this.text = value;
   }//set
  }//public BibleBookClassification[] BibleBookClassification()

  /// <summary>Bible Book Id Minimum.</summary>
  public int BibleBookIdMinimum
  {
   get
   {
    return( Convert.ToInt32( DropDownListBibleBookSearchRangeMinimum.SelectedIndex + 1 ) );
   } 
   set
   {
    DropDownListBibleBookSearchRangeMinimum.SelectedIndex = value - 1;
   }
  }//public int BibleBookIdMinimum()

  /// <summary>Bible Book Id Maximum.</summary>
  public int BibleBookIdMaximum
  {
   get
   {
    return ( DropDownListBibleBookSearchRangeMaximum.SelectedIndex + 1 );
   } 
   set
   {
    DropDownListBibleBookSearchRangeMaximum.SelectedIndex = value - 1;
   }
  }//public int BibleBookIdMaximum()
 
  /// <summary>Static text message.</summary>
  public string StaticTextMessage
  {
   get
   {
    return( LiteralStaticTextMessage.Text );
   } 
   set
   {
    LiteralStaticTextMessage.Text = value;
   }
  }//public ExceptionMessage()

  /// <summary>Find Verses Containing.</summary>
  public WordSearch.FindVersesContaining FindVersesContaining
  {
   get
   {
    int findVersesContaining = -1;

    if ( FindVersesContainingAllWords )
    {
     findVersesContaining = WordSearch.FindVersesContainingAllWords;	
    }//if ( FindVersesContainingAllWords )

    if ( FindVersesContainingAnyWords )
    {
     findVersesContaining = WordSearch.FindVersesContainingAnyWords;
    }//if ( FindVersesContainingAnyWords )

    if ( FindVersesContainingPhrase )
    {
     findVersesContaining = WordSearch.FindVersesContainingPhrase;
    }//if ( FindVersesContainingPhrase )
    
    switch ( findVersesContaining )
    {
     case WordSearch.FindVersesContainingAllWords:
          return ( WordSearch.FindVersesContaining.allWords );
     case WordSearch.FindVersesContainingAnyWords:
          return ( WordSearch.FindVersesContaining.anyWords );
     case WordSearch.FindVersesContainingPhrase:
          return ( WordSearch.FindVersesContaining.phrase );
    }//switch ( findVersesContaining )	
    return ( WordSearch.FindVersesContaining.allWords );
   }//get 
  }//public public WordSearch.FindVersesContaining FindVersesContaining

  /// <summary>Find Verses Containing All Words.</summary>
  public bool FindVersesContainingAllWords
  {
   get
   {
    return( RadioButtonFindVersesContainingAllWords.Checked );
   } 
   set
   {
    RadioButtonFindVersesContainingAllWords.Checked = value;
   }
  }//public FindVersesContainingAllWords()

  /// <summary>Find Verses Containing Any Word.</summary>
  public bool FindVersesContainingAnyWords
  {
   get
   {
    return( RadioButtonFindVersesContainingAnyWords.Checked );
   } 
   set
   {
    RadioButtonFindVersesContainingAnyWords.Checked = value;
   }
  }//public FindVersesContainingAnyWords()

  /// <summary>Find Verses Containing Phrase.</summary>
  public bool FindVersesContainingPhrase
  {
   get
   {
    return( RadioButtonFindVersesContainingPhrase.Checked );
   } 
   set
   {
    RadioButtonFindVersesContainingPhrase.Checked = value;
   }
  }//public FindVersesContainingAnyWords()

  /// <summary>Scripture Reference.</summary>
  public ScriptureReference[] ScriptureReference
  {
   get
   {
    return( null );
   } 
   set
   {
    //this.text = value;
   }
  }//public ScriptureReference()

  /// <summary>Scripture Word.</summary>
  public string ScriptureWord
  {
   get
   {
    return( TextBoxScriptureWord.Text.Trim() );
   } 
   set
   {
    TextBoxScriptureWord.Text = value;
   }
  }//public ScriptureWord()

  /// <summary>Testament Entire Bible.</summary>
  public bool TestamentEntireBible
  {
   get
   {
    return( RadioButtonTestamentEntireBible.Checked );
   } 
   set
   {
    RadioButtonTestamentEntireBible.Checked = value;
   }
  }//public TestamentEntireBible()

  /// <summary>Testament New.</summary>
  public bool TestamentNew
  {
   get
   {
    return( RadioButtonTestamentNew.Checked );
   } 
   set
   {
    RadioButtonTestamentNew.Checked = value;
   }
  }//public TestamentNew()

  /// <summary>Testament Old.</summary>
  public bool TestamentOld
  {
   get
   {
    return( RadioButtonTestamentOld.Checked );
   } 
   set
   {
    RadioButtonTestamentOld.Checked = value;
   }
  }//public TestamentOld()
 }//WordSearchPage
}//WordEngineering namespace.