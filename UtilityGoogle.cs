//using Google = Google_Web_APIs_Demo.Google;
using System;
using System.Text;
using System.Xml;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityGoogle.
 /// http://www.mono-project.com/Web_Services
 ///</summary>
 public class UtilityGoogle
 {
  /// <summary>MaxResults</summary>
  public const  int    MaxResults                = 10;
  
  /// <summary>The XML configuration file.</summary>
  public const  string FilenameConfigurationXml  = @"WordEngineering.config";  

  /// <summary>Question</summary>
  public const  string Question                  = "API";

  /// <summary>The XPath LicenceKey.</summary>
  public const  string XPathLicenceKey           = @"/word/google/licenceKey";  
 
  ///<summary>Licence Key.</summary>
  public static string[] LicenceKey              = null;
 
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of arguments</param>
  public static void Main( string[] argv )
  {
   int           maxResults          =  MaxResults;
   string        exceptionMessage    =  null;
   string        question            =  Question;
   string        spellingSuggestion  =  null;
   StringBuilder sbResultElement     =  null;

   if ( argv.Length > 0 ) { question = argv[0]; }

   spellingSuggestion = doSpellingSuggestion(question);

   UtilityGoogle.Search
   (
    ref question,
    ref maxResults,
    ref exceptionMessage,
    ref sbResultElement
   );

  }//public static void Main(string[] argv)

  ///<summary>Search: Display the number of results.</summary>
  public static void Search
  (
   ref string        question,
   ref int           maxResults,
   ref string        exceptionMessage,
   ref StringBuilder sbResultElement
  )
  {
   int                  estimatedResult      = -1;
   HttpContext          httpContext          = HttpContext.Current;

   GoogleSearchService  googleSearchService  = null;
   GoogleSearchResult   googleSearchResult   = null;
   ResultElement[]      resultElements       = null;

   sbResultElement = new StringBuilder();
   
   //Create a Google Search object
   googleSearchService = new GoogleSearchService();
   try 
   {
    //Invoke the search method.
    googleSearchResult = googleSearchService.doGoogleSearch
    (
     LicenceKey[0], //license key.
     question,      //q: question.
     0,             //start: default is 0.
     maxResults,    //maxResults: maximum results, default is 10.
     false,         //filter: default is true.
     "",            //restrict:
     false,         //safeSearch: safe search, default is false.
     "",            //lr: Language restrict.
     "",            //ie: Input enconding.
     ""             //oe: Output enconding.
    );
    // Extract the estimated number of results for the search and display it
    estimatedResult = googleSearchResult.estimatedTotalResultsCount;
    
    // Returns an array of result elements. This corresponds to the actual list of search results.
    resultElements  = googleSearchResult.resultElements;
    
	System.Console.WriteLine("Estimated number of results: {0}", estimatedResult);
	if ( httpContext != null )
	{
	 httpContext.Response.Write
	 (
	  "Estimated number of results: " + 
	  System.Convert.ToString(estimatedResult) 
	 );
    }//if ( httpContext != null )
    foreach ( ResultElement resultElement in resultElements )
    {
	 System.Console.WriteLine
	 (
      "Summary:                            " + resultElement.summary                            + System.Environment.NewLine +
      "URL:                                " + resultElement.URL                                + System.Environment.NewLine +
      "Snippet:                            " + resultElement.snippet                            + System.Environment.NewLine +
      "Title:                              " + resultElement.title                              + System.Environment.NewLine +
      "CachedSize:                         " + resultElement.cachedSize                         + System.Environment.NewLine +
      "RelatedInformationPresent:          " + resultElement.relatedInformationPresent          + System.Environment.NewLine +
      "HostName:                           " + resultElement.hostName                           + System.Environment.NewLine +
      "DirectoryTitle:                     " + resultElement.directoryTitle                     + System.Environment.NewLine +
      "DirectoryCategory FullViewableName: " + resultElement.directoryCategory.fullViewableName + System.Environment.NewLine +
      "DirectoryCategory SpecialEncoding:  " + resultElement.directoryCategory.specialEncoding
     );

	 if ( httpContext != null )
	 {
	  httpContext.Response.Write
	  (
	   System.Environment.NewLine +
       "Summary:                            " + resultElement.summary                            + System.Environment.NewLine +
       "URL:                                " + resultElement.URL                                + System.Environment.NewLine +
       "Snippet:                            " + resultElement.snippet                            + System.Environment.NewLine +
       "Title:                              " + resultElement.title                              + System.Environment.NewLine +
       "CachedSize:                         " + resultElement.cachedSize                         + System.Environment.NewLine +
       "RelatedInformationPresent:          " + resultElement.relatedInformationPresent          + System.Environment.NewLine +
       "HostName:                           " + resultElement.hostName                           + System.Environment.NewLine +
       "DirectoryTitle:                     " + resultElement.directoryTitle                     + System.Environment.NewLine +
       "DirectoryCategory FullViewableName: " + resultElement.directoryCategory.fullViewableName + System.Environment.NewLine +
       "DirectoryCategory SpecialEncoding:  " + resultElement.directoryCategory.specialEncoding  + System.Environment.NewLine 
      );
     }//if ( httpContext != null )      

     sbResultElement.AppendFormat
     (
      UtilityURI.Anchor,
      resultElement.URL,
      resultElement.title
     );
     sbResultElement.Append( "<br/>" );
    }//foreach ( ResultElement resultElement in resultElements )
   }//try
   catch (System.Web.Services.Protocols.SoapException ex) 
   {
    System.Console.WriteLine(ex.Message);
   }//catch (System.Web.Services.Protocols.SoapException ex)
  }//Search()

  /// <summary>doSpellingSuggestion</summary>
  public static string doSpellingSuggestion(string question)
  {
    // Create a Google Search object
    GoogleSearchService googleSearchService = new GoogleSearchService();
    String spellingSuggestion = googleSearchService.doSpellingSuggestion(LicenceKey[0], question);
    System.Console.WriteLine("{0}", spellingSuggestion);
    return(spellingSuggestion);
  }

  /// <summary>Read the XML Configuration file.</summary>
  /// <param name="filenameConfigurationXml">The XML Configuration file.</param>
  /// <param name="exceptionMessage">The exception message.</param>
  public static void ConfigurationXml
  (
       string filenameConfigurationXml,
   ref string exceptionMessage
  )
  {
   XmlNodeList licenceKey = null;
   
   licenceKey = UtilityXml.SelectNodes
   (
         filenameConfigurationXml,
     ref exceptionMessage,
         XPathLicenceKey
   );

   UtilityXml.Convert
   (
        licenceKey,
    ref LicenceKey,
    ref exceptionMessage
   );
  }//public static void ConfigurationXml()

  static UtilityGoogle()
  {
   string       exceptionMessage         = null;
   string       filenameConfigurationXml = FilenameConfigurationXml;
   string       serverMapPath            = null;
   HttpContext  httpContext              = HttpContext.Current;   

   if ( httpContext != null )   
   {
    serverMapPath = httpContext.Request.MapPath("");
    filenameConfigurationXml = serverMapPath + @"\" + filenameConfigurationXml;
   }   
      
   ConfigurationXml
   ( 
        filenameConfigurationXml,
    ref exceptionMessage
   ); 
  }//static()

 }//public class UtilityGoogle
}//namespace WordEngineering