using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace WordEngineering
{

 /// <summary>UtilityIndexingServiceArgument</summary>
 public class UtilityIndexingServiceArgument
 {
  ///<summary>catalogName</summary>
  public String catalogName                  =  null;
  
  ///<summary>freeTextSearch</summary>  
  public String freeTextSearch               =  null;

  /// <summary>Constructor.</summary>
  public UtilityIndexingServiceArgument():this
  (
   UtilityIndexingService.CatalogName,
   UtilityIndexingService.FreeTextSearch
  )
  {
  }//public UtilityIndexingServiceArgument()

  /// <summary>Constructor.</summary>
  public UtilityIndexingServiceArgument
  (
   String  catalogName,
   String  freeTextSearch
  )
  {
   this.catalogName     = catalogName.Trim();
   this.freeTextSearch  = freeTextSearch.Trim();
  }//public UtilityIndexingServiceArgument()

  ///<summary>Property.</summary>
  ///<value>catalogName.</value>
  public String CatalogName
  {
   get 
   {
    return ( catalogName );
   }//get
   set 
   {
    catalogName = value.Trim();
    if ( catalogName == null || catalogName == String.Empty )
    {
     catalogName = UtilityIndexingService.CatalogName;
    }//if ( catalogName == null || catalogName == String.Empty )    	
   }//set
  }//CatalogName

  ///<summary>Property.</summary>
  ///<value>freeTextSearch.</value>
  public String FreeTextSearch
  {
   get 
   {
    return ( freeTextSearch );
   }//get
   set 
   {
    freeTextSearch = value.Trim();
    if ( freeTextSearch == null || freeTextSearch == String.Empty )
    {
     freeTextSearch = UtilityIndexingService.FreeTextSearch;
    }//if ( freeTextSearch == null || freeTextSearch == String.Empty )    	
   }//set
  }//FreeTextSearch

 }//public class UtilityIndexingServiceArgument

 /// <summary>UtilityIndexingService.</summary>
 public class UtilityIndexingService
 {

  /// <summary>CatalogName.</summary>
  public static String          CatalogName                                 = @"TheWord";

  /// <summary>CatalogQuery.</summary>
  public static String          CatalogQueryFormat                          = @"SELECT Filename, PATH, URL FROM Scope() WHERE FREETEXT('{0}')";

  /// <summary>Connection String Format Indexing Service.</summary>
  public static String          ConnectionStringFormatIndexingService       = @"Provider=MSIDXS.1;Integrated Security .='';Data Source={0};";

  ///<summary>The connection string database.</summary>
  public static String          ConnectionStringDatabase                    = @"Provider=SQLOLEDB; Data Source=localhost; Integrated Security=SSPI; Initial Catalog=WordEngineering";

  /// <summary>The configuration XML filename.</summary>
  public static String          FilenameConfigurationXml                    = @"WordEngineering.config";

  /// <summary>The FreeTextSearch.</summary>
  public static String          FreeTextSearch                              = @"Zacharias";

  /// <summary>IndexingServiceResultFormat.</summary>
  public static String          IndexingServiceResultFormat                 = @"Filename: {0} | PATH = {1} | URL = {2}";

  /// <summary>The XPath Connection String Database.</summary>
  public static String          XPathConnectionStringDatabase               = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>The XPath Connection String Indexing Service.</summary>
  public static String          XPathConnectionStringFormatIndexingService  = @"/word/indexingService/connectionStringFormat";

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {
   Boolean                         booleanParseCommandLineArguments  =  false;
   String                          exceptionMessage                  =  null;

   UtilityIndexingServiceArgument  utilityIndexingServiceArgument    =  null;
   
   utilityIndexingServiceArgument = new UtilityIndexingServiceArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityIndexingServiceArgument
   );
   
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityIndexingServiceArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   Query
   (
    ref ConnectionStringDatabase,
    ref ConnectionStringFormatIndexingService,
    ref utilityIndexingServiceArgument,
    ref exceptionMessage
   );

  }//public static void Main()

  /// <summary>Query.</summary>
  public static void Query
  (
   ref String                          connectionStringDatabase,
   ref String                          connectionStringFormatIndexingService,
   ref UtilityIndexingServiceArgument  utilityIndexingServiceArgument,
   ref String                          exceptionMessage
  )
  {

   StringBuilder                       catalogQuery                     =  null;
   StringBuilder                       connectionStringIndexingService  =  null;
   
   DataSet                             dataSet                          =  null;
   IDataReader                         iDataReader                      =  null;

   Query
   (
    ref connectionStringDatabase,
    ref connectionStringFormatIndexingService,
    ref connectionStringIndexingService,
    ref exceptionMessage,
    ref utilityIndexingServiceArgument,
    ref catalogQuery,
    ref iDataReader,
    ref dataSet
   );
  }//public static void Query()
  
  /// <summary>Query.</summary>
  public static void Query
  (
   ref String                          connectionStringDatabase,
   ref String                          connectionStringFormatIndexingService,   
   ref StringBuilder                   connectionStringIndexingService,
   ref String                          exceptionMessage,
   ref UtilityIndexingServiceArgument  utilityIndexingServiceArgument,
   ref StringBuilder                   catalogQuery,
   ref IDataReader                     iDataReader,
   ref DataSet                         dataSet
  )
  {

   String       indexingServiceResult             =  null;
   HttpContext  httpContext                       =  HttpContext.Current;
   
   utilityIndexingServiceArgument.catalogName     =  utilityIndexingServiceArgument.CatalogName;
   utilityIndexingServiceArgument.freeTextSearch  =  utilityIndexingServiceArgument.FreeTextSearch;   

   connectionStringIndexingService = new StringBuilder();
   connectionStringIndexingService.AppendFormat
   (
    connectionStringFormatIndexingService,
    utilityIndexingServiceArgument.catalogName
   );

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Connection String Indexing Service: {0}", 
     connectionStringIndexingService
    );
   #endif
       	
   catalogQuery = new StringBuilder();
   catalogQuery.AppendFormat
   (
    CatalogQueryFormat,
    utilityIndexingServiceArgument.freeTextSearch   
   );

   try
   {
    UtilityDatabase.DatabaseQuery
    ( 
          connectionStringIndexingService.ToString(), 
      ref exceptionMessage, 
      ref iDataReader,
          catalogQuery.ToString(), 
          CommandType.Text
    );

    UtilityDatabase.DatabaseQuery
    ( 
          connectionStringIndexingService.ToString(), 
      ref exceptionMessage, 
      ref dataSet,
          catalogQuery.ToString(), 
          CommandType.Text
    );

    #if (DEBUG)
     while( iDataReader.Read() )
     {
      indexingServiceResult = String.Format
      (
       IndexingServiceResultFormat,
       iDataReader.GetString(0),
       iDataReader.GetString(1),      
       iDataReader.GetString(2)
      );       
      if ( httpContext == null )
      {
       System.Console.WriteLine(indexingServiceResult);
      }//if ( httpContext == null )
      else
      {
       //httpContext.Response.Write(indexingServiceResult);
      }//else if ( httpContext == null )
     }//while( iDataReader.Read() ) 
    #endif

   }//try
   catch (Exception exception)
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }//catch (Exception exception)   	
  }//public static void Query() 

  /// <summary>ConfigurationXml.</summary>
  public static void ConfigurationXml()
  {
   String  exceptionMessage                 =  null;
   
   ConfigurationXml
   (
    ref FilenameConfigurationXml,
    ref exceptionMessage,
    ref ConnectionStringDatabase,
    ref ConnectionStringFormatIndexingService
   );
  }//public static void ConfigurationXml()

  /// <summary>ConfigurationXml.</summary>
  public static void ConfigurationXml
  (
   ref String         filenameConfigurationXml,
   ref String         exceptionMessage,
   ref String         connectionStringDatabase,
   ref String         connectionStringFormatIndexingService
  )
  {

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,         
         XPathConnectionStringDatabase,
     ref connectionStringDatabase
   );

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Connection String Database: {0}", 
     connectionStringDatabase
    );
   #endif

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,         
         XPathConnectionStringFormatIndexingService,
     ref connectionStringFormatIndexingService
   );

   #if (DEBUG)
    System.Console.WriteLine
    (
     "Connection String Format Indexing Service: {0}", 
     connectionStringFormatIndexingService
    );
   #endif

  }//public static void ConfigurationXml()

  static UtilityIndexingService()
  {
   ConfigurationXml();
  }//static UtilityIndexingService()

 }//public class UtilityIndexingService
}//namespace WordEngineering
