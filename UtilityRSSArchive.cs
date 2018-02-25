using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace WordEngineering
{

 ///<summary>UtilityRSS</summary>
 ///<remarks>Courtesy: UberASP.net RSSFeed().
 /// Courtesy http://msdn.microsoft.com/asp.net/using/understanding/XML/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-createrssw-aspnet.asp.
 ///</remarks>
 public class UtilityRSS
 {

  /// <summary>RSSFeedTop</summary>
  public static  int        TopRSSFeed                                  = 10;

  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString                    = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml                    = @"WordEngineering.config";

  /// <summary>The filename for the RSSFeed.xml.</summary>
  public static  String     FilenameRSSFeedXml                          = @"Comforter_-_RSSFeed.xml";

  /// <summary>The filename for the RSSFeed.xslt.</summary>
  public static  String     FilenameRSSFeedXslt                         = @"Comforter_-_20040627RSSFeed.xslt";

  /// <summary>The RSS Element channel 3 mandatory elements: title, link and description.</summary>
  public static  String[][] RSSElementChannel                           = new String[][]
                                                                          {
                                                                           new String[] { "title",       "RSS Feed" },
                                                                           new String[] { "link",        "RSSFeedWebForm.aspx" },
                                                                           new String[] { "description", "RSSFeed" },                                                                           
                                                                          }; 


  /// <summary>The SQL select for the RSSFeed.</summary>
  public static  String     SQLSelectRSSFeed                            = "SELECT TOP {0} AlphabetSequence.Word AS AlphabetSequenceWord, TheWord.Filename AS TheWordFilename, TheWord.Dated AS TheWordDated FROM TheWord ( NOLOCK ) JOIN AlphabetSequence ( NOLOCK ) ON TheWord.SequenceOrderId = AlphabetSequence.TheWordId WHERE AlphabetSequence.SequenceOrderId IN ( SELECT MIN( AlphabetSequence2.SequenceOrderId ) FROM AlphabetSequence AS AlphabetSequence2 WHERE TheWord.SequenceOrderId = AlphabetSequence2.TheWordId ) ORDER BY TheWord.Dated DESC";

  /// <summary>The SQL select for the RSS Syndication Feed</summary>
  public static  String     SQLSelectRSSSyndicationFeed                 = "SELECT SequenceOrderId AS FeedId, Title FROM RSSFeed ( NOLOCK ) ORDER BY Title DESC";

  /// <summary>The SQL select for the RSS Syndication Feed Id</summary>
  public static  String     SQLSelectRSSSyndicationFeedId               = "SELECT URI, UpdateInterval FROM RSSFeed ( NOLOCK ) WHERE SequenceOrderId = {0}";

  /// <summary>URITheWord</summary>
  public const   String     URITheWord                                  = @"..\TheWord\";

  /// <summary>The XMLProcessingInstructionStyleSheet</summary>
  public const   String     XMLProcessingInstructionStyleSheet          = @"type='text/xsl' href='{0}'";

  /// <summary>The XPath database connection string.</summary>
  public const   String     XPathDatabaseConnectionString               = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";
  
  /// <summary>Constructor.</summary>
  public UtilityRSS()
  {

  }

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Stub();
  }//public static void Main( String[] argv )

  ///<summary>Stub.</summary>
  public static void Stub()
  {
   RSSFeed();    
  }

  ///<summary>RSSFeed</summary>
  public static void RSSFeed()
  {
   String         exceptionMessage          =  null;
   DataSet        dataSet                   =  null;
   XmlTextWriter  xmlTextWriter             =  null;
      
   RSSFeed
   (
    ref  DatabaseConnectionString,
    ref  exceptionMessage,
    ref  dataSet,
    ref  SQLSelectRSSFeed,
    ref  TopRSSFeed,
    ref  FilenameRSSFeedXml,
    ref  FilenameRSSFeedXslt,
    ref  xmlTextWriter
   );//RSSFeed 
  }//public static void RSSFeed()

  ///<summary>RSSFeed</summary>
  public static void RSSFeed
  (
   ref String         databaseConnectionString,
   ref String         exceptionMessage,
   ref DataSet        dataSet,
   ref String         SQLSelect,
   ref int            topNumberOfRows,
   ref String         FilenameRSSFeedXml,
   ref String         FilenameRSSFeedXslt,
   ref XmlTextWriter  xmlTextWriter
  )
  {
   
   StringBuilder  sb                  =  null;
   HttpContext    HttpContextCurrent  =  HttpContext.Current;
   
   sb = new StringBuilder();
   sb.AppendFormat( SQLSelect, topNumberOfRows );

   try
   {
    UtilityDatabase.DatabaseQuery
    (
         databaseConnectionString,
     ref exceptionMessage,
     ref dataSet,
         sb.ToString(),
         CommandType.Text 
    );
   
    if ( FilenameRSSFeedXml == null )
    {
     if ( HttpContextCurrent == null )
     {
      return;
     }//if ( HttpContextCurrent == null )
                	
     xmlTextWriter = new XmlTextWriter
     (
      HttpContextCurrent.Response.OutputStream,
      Encoding.UTF8
     ); 
    }//if ( FilenameRSSFeedXml == null )
    else
    {
     xmlTextWriter = new XmlTextWriter
     (
      FilenameRSSFeedXml,
      Encoding.UTF8
     ); 
    }//else ( FilenameRSSFeedXml == null )

    xmlTextWriter.WriteStartDocument();

    if ( FilenameRSSFeedXslt != null )
    {
     sb = new StringBuilder();
     sb.AppendFormat( XMLProcessingInstructionStyleSheet, FilenameRSSFeedXslt );
     xmlTextWriter.WriteProcessingInstruction("xml-stylesheet", sb.ToString() );
    }

    xmlTextWriter.WriteStartElement("rss");
    xmlTextWriter.WriteAttributeString("version", "2.0");
    xmlTextWriter.WriteStartElement("channel");
   
    foreach ( String[] RSSElementChannelCurrent in RSSElementChannel )
    {
     xmlTextWriter.WriteElementString( RSSElementChannelCurrent[0], RSSElementChannelCurrent[1] );
    };//foreach ( String[] RSSElementChannelCurrent in RSSElementChannel )
    
    foreach(DataTable dataTable in dataSet.Tables)
    {
     foreach(DataRow dataRow in dataTable.Rows)
     {
      xmlTextWriter.WriteStartElement("item");
      xmlTextWriter.WriteElementString("title",       Convert.ToString( dataRow[0] ) );
      xmlTextWriter.WriteElementString("link",        URITheWord + dataRow[1] );      
      xmlTextWriter.WriteElementString("pubDate",     Convert.ToDateTime( dataRow[2]).ToString("R") );
      xmlTextWriter.WriteEndElement(); //Element item
     }//foreach(DataRow dataRow in dataTable.Rows)
    }//foreach(DataTable dataTable in dataSet.Tables)

    xmlTextWriter.WriteEndElement();   //Element channel
    xmlTextWriter.WriteEndElement();   //Element rss
    xmlTextWriter.WriteEndDocument();
    xmlTextWriter.Flush();
    xmlTextWriter.Close();

   }//try
   catch ( ArgumentException exception )
   {
    exceptionMessage = "ArgumentException: " + exception.Message;   	
    System.Console.WriteLine("ArgumentException: {0}", exception.Message);
   } 
   catch ( UnauthorizedAccessException exception )
   {
    exceptionMessage = "UnauthorizedAccessException: " + exception.Message;   	
    System.Console.WriteLine("UnauthorizedAccessException: {0}", exception.Message);
   } 
   catch ( DirectoryNotFoundException exception )
   {
    exceptionMessage = "DirectoryNotFoundException: " + exception.Message;   	
    System.Console.WriteLine("DirectoryNotFoundException: {0}", exception.Message);
   } 
   catch ( IOException exception )
   {
    exceptionMessage = "IOException: " + exception.Message;   	
    System.Console.WriteLine("IOException: {0}", exception.Message);
   } 
   catch ( SecurityException exception )
   {
    exceptionMessage = "SecurityException: " + exception.Message;   	
    System.Console.WriteLine("SecurityException: {0}", exception.Message);
   } 
    	
  }//public static void RSSFeed()

  ///<summary>RSSFeed</summary>
  public static void RSSFeed
  (
   ref IDataReader  iDataReader
  )
  {
   String         exceptionMessage  =  null;

   RSSFeed
   (
    ref DatabaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
    ref SQLSelectRSSFeed,
    ref TopRSSFeed
   );
  }//public static void RSSFeed

  ///<summary>RSSFeed</summary>
  public static void RSSFeed
  (
   ref String         databaseConnectionString,
   ref String         exceptionMessage,
   ref IDataReader    iDataReader
  )
  {

   StringBuilder  sb                  =  null;
   
   sb = new StringBuilder();
   sb.AppendFormat( SQLSelectRSSFeed, TopRSSFeed );

   RSSFeed
   (
    ref databaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
    ref SQLSelectRSSFeed,
    ref TopRSSFeed
   );
  }//public static void RSSFeed
  	
  ///<summary>RSSFeed Courtesy http://msdn.microsoft.com/asp.net/using/understanding/XML/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-createrssw-aspnet.asp Microsoft: Creating an Online RSS News Aggregator with ASP.NET with Scott Mitchell</summary>
  public static void RSSFeed
  (
   ref String         databaseConnectionString,
   ref String         exceptionMessage,
   ref IDataReader    iDataReader,
   ref String         SQLSelect,
   ref int            topNumberOfRows
  )
  {
   
   StringBuilder  sb = null;
  
   sb = new StringBuilder();
   sb.AppendFormat( SQLSelect, topNumberOfRows );

   UtilityDatabase.DatabaseQuery
   (
        databaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
        sb.ToString(),
        CommandType.Text 
   );
    	
  }//public static void RSSFeed()

  ///<summary>RSSSyndicationFeed</summary>
  public static void RSSSyndicationFeed
  (
   ref String         exceptionMessage,
   ref IDataReader    iDataReader
  )
  {
   RSSSyndicationFeed
   (
    ref DatabaseConnectionString,
    ref exceptionMessage,
    ref iDataReader
   );
  }//public static void RSSSyndicationFeed()

  ///<summary>RSSSyndicationFeed Courtesy http://msdn.microsoft.com/asp.net/using/understanding/XML/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-createrssw-aspnet.asp Microsoft: Creating an Online RSS News Aggregator with ASP.NET with Scott Mitchell</summary>
  public static void RSSSyndicationFeed
  (
   ref String         databaseConnectionString,
   ref String         exceptionMessage,
   ref IDataReader    iDataReader
  )
  {
   UtilityDatabase.DatabaseQuery
   (
        databaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
        SQLSelectRSSSyndicationFeed,
        CommandType.Text 
   );
  }//public static void RSSSyndicationFeed()

  ///<summary>RSSSyndicationFeedId Courtesy http://msdn.microsoft.com/asp.net/using/understanding/XML/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-createrssw-aspnet.asp Microsoft: Creating an Online RSS News Aggregator with ASP.NET with Scott Mitchell</summary>
  public static void RSSSyndicationFeedId
  (
   ref int    feedId,
   ref String feedURI,
   ref Xml    xml
  )
  {
   String  exceptionMessage  =  null;
     	
   RSSSyndicationFeedId
   (
    ref DatabaseConnectionString,
    ref exceptionMessage,
    ref feedId,
    ref feedURI,
    ref xml
   );
  }
  
  ///<summary>RSSSyndicationFeedId Courtesy http://msdn.microsoft.com/asp.net/using/understanding/XML/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-createrssw-aspnet.asp Microsoft: Creating an Online RSS News Aggregator with ASP.NET with Scott Mitchell</summary>
  public static void RSSSyndicationFeedId
  (
   ref String  databaseConnectionString,
   ref String  exceptionMessage,
   ref int     feedId,
   ref String  feedURI,
   ref Xml     xml
  )
  {

   object            databaseReturnValue  =  null;
   StringBuilder     sb                   =  null;

   XmlDocument       xmlDocument          =  null;
   XsltArgumentList  xsltArgumentList     =  null;
   
   sb                = new StringBuilder();
   xmlDocument       = new XmlDocument();
   xsltArgumentList  = new XsltArgumentList();
   
   sb.AppendFormat( SQLSelectRSSSyndicationFeedId, feedId );

   databaseReturnValue = UtilityDatabase.DatabaseQuery
   (
        databaseConnectionString,
    ref exceptionMessage,
        sb.ToString(),
        CommandType.Text 
   );
   
   if ( databaseReturnValue == null || databaseReturnValue == DBNull.Value )
   {
    return;
   }//if ( databaseReturnValue == null || databaseReturnValue == DBNull.Value )    	
   
   feedURI = databaseReturnValue.ToString();
   
   xmlDocument.Load( feedURI );
   
   xml.Document = xmlDocument;

   // Add the FeedID parameter to the XSLT stylesheet
   xsltArgumentList.AddParam("FeedID", "", feedId);
   xml.TransformArgumentList = xsltArgumentList;
   
  }//public static void RSSSyndicationFeedId()

  ///<summary>RSSSyndicationFeedId Courtesy http://msdn.microsoft.com/asp.net/using/understanding/XML/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-createrssw-aspnet.asp Microsoft: Creating an Online RSS News Aggregator with ASP.NET with Scott Mitchell</summary>
  public static void RSSSyndicationFeedId
  (
   ref String         databaseConnectionString,
   ref String         exceptionMessage,
   ref int            feedId,
   ref String         feedURI,
   ref int            updateInterval,
   ref int            id,
   ref IDataReader    iDataReader,
   ref Xml            xml,
   ref XmlDocument    xmlDocument
  )
  {

   StringBuilder     sb                  =  null;

   XsltArgumentList  xsltArgumentList    =  null;

   HttpContext       httpContextCurrent  =  null;
   
   sb               = new StringBuilder();
   xmlDocument      = new XmlDocument();
   xsltArgumentList = new XsltArgumentList();
   
   httpContextCurrent = HttpContext.Current;

   sb.AppendFormat( SQLSelectRSSSyndicationFeedId, feedId );

   /*
   #if (DEBUG)
    if ( httpContextCurrent != null  )
    {
     httpContextCurrent.Response.Write( "SQL: " + sb );
    }//if ( httpContextCurrent != null )
    else
    {
     System.Console.WriteLine("SQL: {0}", exceptionMessage = sb.ToString());
    }//else ( httpContextCurrent == null ) 
   #endif
   */

   UtilityDatabase.DatabaseQuery
   (
        databaseConnectionString,
    ref exceptionMessage,
    ref iDataReader,
        sb.ToString(),
        CommandType.Text 
   );
   
   if ( exceptionMessage != null )
   {
    exceptionMessage = sb.ToString() + exceptionMessage;
    return;
   }       	

   try
   {
    // Always call Read before accessing data.
    if ( iDataReader.Read() )
    {
   	 feedURI = iDataReader["URI"].ToString();
     updateInterval = Int32.Parse(iDataReader["UpdateInterval"].ToString());
     /*
     System.Console.WriteLine
     (
      "FeedURI: {0} | Update Interval: {1}", 
      feedURI,
      updateInterval
     );
     */
    }//if ( iDataReader.Read() )

    xmlDocument.Load( feedURI );
    //xmlDocument.Save( "UtilityRSS.xml" );

    /*
    xml.Document = xmlDocument;

    // Add the ID parameter to the XSLT stylesheet
    xsltArgumentList.AddParam("ID", "", id);
    xml.TransformArgumentList = xsltArgumentList;
    */

   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception:" + exception.Message;
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }	
  }//public static void RSSSyndicationFeedId()

  /// <summary>Read the XML Configuration file.</summary>
  public static void ConfigurationXml()
  {  
   string exceptionMessage = null;
   
   ConfigurationXml
   (
        FilenameConfigurationXml,
    ref exceptionMessage,
    ref DatabaseConnectionString
   );
   
  }//public static void ConfigurationXml()

  /// <summary>Read the XML Configuration file.</summary>
  /// <param name="filenameConfigurationXml">The XML Configuration file.</param>
  /// <param name="exceptionMessage">The exception message.</param>
  /// <param name="databaseConnectionString">The database connection string.</param>  
  public static void ConfigurationXml
  (
       string filenameConfigurationXml,
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
  
  static UtilityRSS()
  {
   ConfigurationXml();
  }//static UtilityRSS()
  
 }//public class UtilityRSS
 
}//namespace WordEngineering