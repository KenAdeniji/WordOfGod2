using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Text;
namespace WordEngineering
{
 /// <summary>WordSearch</summary>
 /// <remarks>enum FindVersesContaining. DatabaseQuery()</remarks>
 public class WordSearch
 {
  /// <summary>The maximum length of a verse.</summary>
  const  int    ParameterSizeVerseText              =  550;  
  
  /// <summary>The maximum length of a scripture reference for a verse.</summary>
  const  int    ParameterSizeScriptureReference     =  20;

  /// <summary>The bible book search range is the Entire Bible.</summary>  
  public const  int    BibleBookSearchRangeEntireBible     =  0;

  /// <summary>The bible book search range is the Old Testament.</summary>  
  public const  int    BibleBookSearchRangeOldTestament    =  1;
  
  /// <summary>The bible book search range is the New Testament.</summary>    
  public const  int    BibleBookSearchRangeNewTestament    =  2;
    
  /// <summary>Find verses containing all words.</summary>
  public const  int    FindVersesContainingAllWords        =  1; 

  /// <summary>Find verses containing any word.</summary>
  public const  int    FindVersesContainingAnyWords        =  2; 

  /// <summary>Find verses containing a particular phrase.</summary>
  public const  int    FindVersesContainingPhrase          =  3; 

  /// <summary>The database result set ordinal verse text.</summary>
  const  int    ResultSetOrdinalVerseText                  =  0;  
  
  /// <summary>The database result set ordinal scripture reference.</summary>
  const  int    ResultSetOrdinalScriptureReference         =  1;

  /// <summary>The stored procedure for dynamic SQL.</summary>
  const  string BibleDynamicSQLQuery                        = "uspBibleDynamicSQLQuery";

  /// <summary>The database column name scripture reference.</summary>
  public static string DatabaseColumnNameScriptureReference  = "scriptureReferenceWithoutBracket";

  /// <summary>The database column name verse text.</summary>
  public static string DatabaseColumnNameVerseText           = "verseText";

  /// <summary>The database column ordinal scripture reference.</summary>
  public const int DatabaseColumnOrdinalScriptureReference   = 0;
  
  /// <summary>The database column ordinal verse text.</summary>
  public const int DatabaseColumnOrdinalVerseText            = 1;

  /// <summary>The database connention string.</summary>
  public static string DatabaseConnectionString              = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=Bible;";

  /// <summary>The database table name.</summary>
  public const string  DatabaseTableName                     = "KJV";

  /// <summary>The exception message</summary> 
  public static string ExceptionMessage                      = null;
  
  /// <summary>The XML Configuration file.</summary>  
  public static string FilenameConfigurationXml              = @"WordEngineering.config";
 
  /// <summary>The parameter to the dynamic to the stored procedure for dynamic SQL.</summary>
  const  string ParameterDynamicSQLQuery                     = "@dynamicSQLQuery";

  /// <summary>The string delimiter.</summary>
  const  string StringDelimiter                              = " ,.:";

  /// <summary>The string delimiter to character array.</summary>
  static char[] StringDelimiterCharacterArray                = null;

  /// <summary>The SQL statement from clause.</summary>
  const  string SQLStatementFrom                             = " FROM ";

  /// <summary>The SQL statement select clause.</summary>
  const  string SQLStatementSelect                           = " SELECT ";

  /// <summary>The SQL statement select lock clause.</summary>
  const  string SQLStatementSelectLock                       = " ( NOLOCK ) ";

  /// <summary>The SQL statement select where clause.</summary>
  const  string SQLStatementSelectWhere                      = " WHERE ";

  /// <summary>The SQL statement select order by clause.</summary>
  const  string SQLStatementSelectOrderBy                    = " ORDER BY verseIdSequence ";

  /// <summary>The XPath to the Database Connection String.</summary>
  const  string XPathDatabaseConnectionString                = @"/word/database/sqlServer/bible/databaseConnectionString";
  
  /// <summary>Find verses containing all the words, any word, phrase.</summary>
  public enum   FindVersesContaining 
  { 
   /// <summary>All words</summary>
   allWords = FindVersesContainingAllWords, 
   
   /// <summary>Any word</summary>
   anyWords = FindVersesContainingAnyWords, 

   /// <summary>Containing Phrase</summary>   
   phrase = FindVersesContainingPhrase
  };
	
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
   string           exceptionMessage = null;
   string           word             = null;
   IDataReader  iDataReader  = null;
   
   if ( argv.Length > 0 ) { word = argv[0]; }	

   /*
   DatabaseQuery
   (
        DatabaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
        word,
        FindVersesContaining.allWords,
        3,
        45, 
        null,  
        new BibleBookClassification[] 
        { 
         BibleBookClassification.PaulineEpistles, 
         BibleBookClassification.GeneralEpistles, 
         BibleBookClassification.PastoralEpistles, 
         BibleBookClassification.PrisonEpistles 
        }
   );//DatabaseQuery
   */
   
   /*
   DatabaseQuery
   (
        DatabaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
        word,
        FindVersesContaining.anyWords,
        40,
        66, 
        null,
        new BibleBookClassification[] 
        { 
         BibleBookClassification.PaulineEpistles, 
         BibleBookClassification.GeneralEpistles, 
         BibleBookClassification.PastoralEpistles, 
         BibleBookClassification.PrisonEpistles 
        }
   );//DatabaseQuery
   */
   
   ArrayList arrayListBibleBookClassificationId = new ArrayList();
   arrayListBibleBookClassificationId.Add(0);
   arrayListBibleBookClassificationId.Add(15);
   arrayListBibleBookClassificationId.Add(16);
   arrayListBibleBookClassificationId.Add(17);

   DatabaseQuery
   (
        DatabaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,    
        word,
        FindVersesContaining.anyWords,
        40,
        66, 
        null,
        arrayListBibleBookClassificationId
   );//DatabaseQuery
   
   if ( iDataReader != null )
   {
    while( iDataReader.Read() ) 
    {
     System.Console.WriteLine
     (
      "{0} {1}", 
      iDataReader.GetString(DatabaseColumnOrdinalScriptureReference),
      iDataReader.GetString(DatabaseColumnOrdinalVerseText)
     );//System.Console.WriteLine
    }//while( iDataReader.Read() )
    iDataReader.Close();
   }//if ( iDataReader != null ) 
  }//public static void Main

  /// <summary>Database Query.</summary>
  /// <param name="databaseConnectionString">The database connection string, for example, "Provider=SQLOLEDB;Data Source=localhost;user id=WordEngineering;password=WordEngineering;Initial Catalog=Bible;"</param>  
  /// <param name="exceptionMessage">The exception message.</param>
  /// <param name="iDataReader">The IDataReader.</param>      
  /// <param name="word">The word to find.</param>
  /// <param name="findVersesContaining">All Words = 0, Any Word = 1, Phrase = 2</param>
  /// <param name="bibleBookIdMinimum">The search should begin from this particular bible book.</param>
  /// <param name="bibleBookIdMaximum">The search should end in this particular bible book.</param>
  /// <param name="scriptureReference">The scripture reference.</param>
  /// <param name="arrayListBibleBookClassificationId">The bible book classification Ids.</param>  
  public static void DatabaseQuery
  (
       string                    databaseConnectionString,
   ref string                    exceptionMessage,
   ref IDataReader           iDataReader,       
       string                    word,
       FindVersesContaining      findVersesContaining,
       int                       bibleBookIdMinimum,
       int                       bibleBookIdMaximum,   
       ScriptureReference[]      scriptureReference,
       ArrayList                 arrayListBibleBookClassificationId
  )
  {
   int                       bibleBookClassificationCount;
   int                       bibleBookClassificationId;
   int                       bibleBookClassificationTotal = -1;
   
   BibleBookClassification[] bibleBookClassification      = null;
   
   if ( arrayListBibleBookClassificationId != null ) 
   {
    bibleBookClassificationTotal = arrayListBibleBookClassificationId.Count;
    if ( bibleBookClassificationTotal > 0 )
    {
     bibleBookClassification = new BibleBookClassification[ bibleBookClassificationTotal ];
     for ( bibleBookClassificationCount = 0; bibleBookClassificationCount < bibleBookClassificationTotal; ++bibleBookClassificationCount)
     {
      bibleBookClassificationId = (int) arrayListBibleBookClassificationId[ bibleBookClassificationCount ];
      bibleBookClassification[bibleBookClassificationCount] = ( BibleBookClassification ) BibleBookClassification.arrayListCollection[ bibleBookClassificationId ];
      #if (DEBUG)
       System.Console.WriteLine
       (
        "bibleBookClassificationCount[{0}]: {1} ", 
        bibleBookClassificationCount,
        bibleBookClassification[bibleBookClassificationCount]
       );
      #endif
     }//for ( bibleBookClassificationCount = 0; bibleBookClassificationCount < bibleBookClassificationTotal; ++bibleBookClassificationCount)	
    }//if ( bibleBookClassificationTotal > 0 ) 
   }//if ( bibleBookClassificationTotal > 0 )
   
   DatabaseQuery
   (	
         databaseConnectionString,
     ref exceptionMessage,
     ref iDataReader,
         word,
         findVersesContaining,
         bibleBookIdMinimum,
         bibleBookIdMaximum,   
         scriptureReference,
         bibleBookClassification
   );
   
  }//DatabaseQuery
  	
  /// <summary>Database Query.</summary>
  /// <param name="databaseConnectionString">The database connection string, for example, "Provider=SQLOLEDB;Data Source=localhost;user id=WordEngineering;password=WordEngineering;Initial Catalog=Bible;"</param>  
  /// <param name="exceptionMessage">The exception message.</param>
  /// <param name="iDataReader">The IDataReader.</param>    
  /// <param name="word">The word to find.</param>
  /// <param name="findVersesContaining">All Words = 0, Any Word = 1, Phrase = 2</param>
  /// <param name="bibleBookIdMinimum">The search should begin from this particular bible book.</param>
  /// <param name="bibleBookIdMaximum">The search should end in this particular bible book.</param>
  /// <param name="scriptureReference">The scripture reference.</param>
  /// <param name="bibleBookClassification">The bible book classification Ids.</param>  
  public static void DatabaseQuery
  (
       string                    databaseConnectionString,
   ref string                    exceptionMessage,
   ref IDataReader           iDataReader,
       string                    word,
       FindVersesContaining      findVersesContaining,
       int                       bibleBookIdMinimum,
       int                       bibleBookIdMaximum,   
       ScriptureReference[]      scriptureReference,
       BibleBookClassification[] bibleBookClassification
  )
  {
   
   int            wordCount                           = 0;
   int            wordTotal                           = 0;
   
   string         wordDelimiter                       = null;
   string[]       wordSplit                           = null;

   ArrayList      arrayListClassificationBibleBookId  = null;
   
   StringBuilder  SQLStatement                        = null;
   StringBuilder  WhereWord                           = null;
   StringBuilder  WhereBookId                         = null;
   StringBuilder  WhereBookClassification             = null;
   
   SQLStatement = new StringBuilder( SQLStatementSelect );
   SQLStatement.Append( DatabaseColumnNameScriptureReference );
   SQLStatement.Append( "," );
   SQLStatement.Append( DatabaseColumnNameVerseText );
   SQLStatement.Append( SQLStatementFrom );
   SQLStatement.Append( " " );
   SQLStatement.Append( DatabaseTableName );
   SQLStatement.Append( " " );   
   SQLStatement.Append( SQLStatementSelectLock );   

   if ( word != null )
   {
    switch(findVersesContaining)
    {         
     case FindVersesContaining.allWords:   
      wordDelimiter = " AND ";
      break;
     
     case FindVersesContaining.anyWords:
      wordDelimiter = " OR ";
      break;
     
     case FindVersesContaining.phrase:
      wordDelimiter = null;
      break;
    }//switch(findVersesContaining) 

    word = word.Trim();
   
    if ( findVersesContaining == FindVersesContaining.allWords || findVersesContaining == FindVersesContaining.anyWords )
    {
     wordSplit = word.Split( StringDelimiterCharacterArray );
     wordTotal = wordSplit.Length;
     WhereWord = new StringBuilder(); 	
     WhereWord.Append( " ( " );     
     for ( wordCount = 0; wordCount < wordTotal; ++wordCount )
     {
      WhereWord.Append( DatabaseColumnNameVerseText );	
      WhereWord.Append( " LIKE '%" );
      WhereWord.Append( wordSplit[wordCount] );    
      WhereWord.Append( "%'" );
      if ( wordCount + 1 < wordTotal )
      {
       WhereWord.Append( wordDelimiter );	
      }//if ( wordCount + 1 < wordTotal )
     }//for ( wordCount = 0; wordCount < wordTotal; ++wordCount )	
     WhereWord.Append( " ) " );     
    }//if ( findVersesContaining == FindVersesContainingAllWords or findVersesContaining == FindVersesContainingAnyWords )
   
    if ( findVersesContaining == FindVersesContaining.phrase )
    {
     WhereWord = new StringBuilder(); 	
     WhereWord.Append( " ( " );
     WhereWord.Append( DatabaseColumnNameVerseText );	
     WhereWord.Append( " LIKE '%" );
     WhereWord.Append( word );    
     WhereWord.Append( "%' ) " );
    }//if ( findVersesContaining == FindVersesContainingPhrase )
   }//if ( word != null ) 
   
   if ( bibleBookIdMinimum > 1 || bibleBookIdMaximum < 66 )
   {
    WhereBookId  = new StringBuilder();
    WhereBookId.Append(" ( bookId BETWEEN ");
    WhereBookId.Append(bibleBookIdMinimum);
    WhereBookId.Append(" AND ");
    WhereBookId.Append(bibleBookIdMaximum);
    WhereBookId.Append(" ) ");
   } 

   if ( bibleBookClassification != null )
   {
    int bibleBookClassificationTotal = 0;
    WhereBookClassification = new StringBuilder();
    arrayListClassificationBibleBookId = BibleBookClassification.BibleBookIds( bibleBookClassification );
    bibleBookClassificationTotal = arrayListClassificationBibleBookId.Count;
    WhereBookClassification.Append(" bookId IN ( ");
    for ( int i = 0; i < bibleBookClassificationTotal; ++i )
    {
     WhereBookClassification.Append( arrayListClassificationBibleBookId[i] );
     if ( i + 1 < bibleBookClassificationTotal )
     {
      WhereBookClassification.Append( "," );
     }//if ( i + 1 < bibleBookClassificationTotal ) 
    }//for ( int i = 0; i < bibleBookClassificationTotal; ++i )    
    WhereBookClassification.Append(" ) ");    
   }//if ( bibleBookClassification != null ) 

   if ( WhereWord != null || WhereBookId != null || WhereBookClassification != null ) 
   {
    bool appendAnd = false;	
    SQLStatement.Append( SQLStatementSelectWhere );
    if ( WhereWord != null )
    {
     SQLStatement.Append( WhereWord );
     appendAnd = true;
    }//if ( WhereWord != null )	
    if ( WhereBookId != null )
    {
     if ( appendAnd )
     {
      SQLStatement.Append( " AND " );
     } 
     SQLStatement.Append( WhereBookId );
     appendAnd = true;
    }//if ( WhereBookId != null )	
    if ( WhereBookClassification != null )
    {
     if ( appendAnd )
     {
      SQLStatement.Append( " AND " );
     } 
     SQLStatement.Append( WhereBookClassification );
     appendAnd = true;
    }//if ( WhereBookClassification != null )	
   }//if ( WhereWord != null or WhereBookId != null or WhereBookClassification != null ) 	
   SQLStatement.Append(SQLStatementSelectOrderBy);
   UtilityDatabase.DatabaseQuery
   ( 
        databaseConnectionString, 
    ref exceptionMessage, 
    ref iDataReader,
        SQLStatement.ToString(), 
        CommandType.Text
   );
   
   System.Console.WriteLine("SQLStatement: {0}", SQLStatement);
   
  }//public static DatabaseQuery()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   ConfigurationXml
   (
    ref FilenameConfigurationXml,
    ref ExceptionMessage,
    ref DatabaseConnectionString
   );
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  /// <param name="filenameConfigurationXml">The configuration XML filename.</param>  
  /// <param name="exceptionMessage">The exception message.</param>  
  /// <param name="databaseConnectionString">The database connection string, for example, "Provider=SQLOLEDB;Data Source=localhost;user id=WordEngineering;password=WordEngineering;Initial Catalog=Bible;"</param>  
  public static void ConfigurationXml
  (
   ref string filenameConfigurationXml,
   ref string exceptionMessage,
   ref string databaseConnectionString
  )
  {
   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathDatabaseConnectionString,
     ref databaseConnectionString
    );//ConfigurationXml()
  }//ConfigurationXml	 
  
  static WordSearch()
  {
   StringDelimiterCharacterArray = StringDelimiter.ToCharArray();
  }//static()
   
 }//public class WordSearch
}//namespace WordEngineering