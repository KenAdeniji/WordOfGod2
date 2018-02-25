using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text;


namespace WordEngineering
{
 ///<summary>WordSaid XML Load.</summary>
 class WordSaidXmlLoad
 {
  ///<summary>The command-line argument for the Xml Document.</summary>
  public const int           CommandLineArgumentXmlDocument = 0;

  ///<summary>The Element Rank Name.</summary>  
  public const int           ElementRankName                = 0;
  
  ///<summary>The Element Rank StoredProcedure.</summary>    
  public const int           ElementRankStoredProcedure     = 1;    

  ///<summary>The Node Rank Name.</summary>  
  public const int           NodeRankName                   = 0;
  
  ///<summary>The Node Rank Value.</summary>    
  public const int           NodeRankValue                  = 1;    

  /// <summary>The database connection string.</summary>
  public static string       DatabaseConnectionString       = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The configuration XML filename.</summary>
  public const  string       FilenameConfigurationXml       = @"WordEngineering.config";

  public static string[][]   ElementConfiguration           =
                             {
                              new string[] { "jehovahRophe", "uspJehovahRophe", "sequenceOrder", "dated", "commentary", "scriptureReference" }
                             };  

  /// <summary>The XPath database connection string.</summary>
  public const  string       XPathDatabaseConnectionString  = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";
   
  ///<summary>Main()</summary>
  static void Main( string[] xmlDocumentFilenames )
  {
   string databaseConnectionString = DatabaseConnectionString;
   string exceptionMessage         = null;
   
   ConfigurationXml( FilenameConfigurationXml, ref exceptionMessage, ref databaseConnectionString );

   XmlParse
   ( 
        databaseConnectionString,
    ref exceptionMessage,
        xmlDocumentFilenames
   );    
  }//static void Main( string[] argv )
  
  ///<summary>XmlParse()</summary>
  static void XmlParse
  ( 
       string    databaseConnectionString,
   ref string    exceptionMessage,
       string[]  xmlDocumentFilenames
  )
  {
   
   int                       childrenCount                    =  0;
   int                       childrenTotal                    =  0;
   
   int                       elementConfigurationCount        =  0;
   int                       elementConfigurationIndex        =  0;
   int                       elementConfigurationTotal        =  ElementConfiguration.Length;
   
   int                       grandChildrenCount               =  0;
   int                       grandChildrenTotal               =  0;
   int                       grandChildrenValueLength         =  0;   
   
   int                       oleDbParameterCollectionCount    =  0;
   int                       oleDbParameterCollectionIndex    =  0;   
   int                       oleDbParameterCollectionTotal    =  0;
   
   string[][]                children                         =  null;
   string[][]                grandChildren                    =  null;   

   DateTime                  dateTime                         =  new DateTime();

   OleDbConnection           oleDbConnection                  =  null;
   OleDbCommand              oleDbCommand                     =  null;
   OleDbParameterCollection  oleDbParameterCollection         =  null;
  
   XPathDocument             xPathDocument                    = null;

   XPathNavigator            xPathNavigatorChildren           = null;
   XPathNavigator            xPathNavigatorGrandChildren      = null;
   XPathNavigator            xPathNavigatorParent             = null;
   
   XPathNodeIterator         xPathNodeIteratorChildren        = null;
   XPathNodeIterator         xPathNodeIteratorGrandChildren   = null;
   XPathNodeIterator         xPathNodeIteratorParent          = null;   

   //Open the database connection.
   oleDbConnection = UtilityDatabase.DatabaseConnectionInitialize( databaseConnectionString, ref exceptionMessage );
   
   foreach ( string xmlDocumentFilename in xmlDocumentFilenames )
   {
    if ( !File.Exists( xmlDocumentFilename ) )
    {
     System.Console.WriteLine("File Not Found: {0}", xmlDocumentFilename );
     continue;
    }//if ( !File.Exists( xmlDocumentFilename ) )

    #if ( DEBUG )
     System.Console.WriteLine("XML Document Filename: {0}", xmlDocumentFilename);      
    #endif
         
    try
    {
     //Create an XML document instance, and load XML data.
     xPathDocument             = new XPathDocument( xmlDocumentFilename );

     xPathNavigatorParent      = xPathDocument.CreateNavigator();
     xPathNodeIteratorParent   = xPathNavigatorParent.SelectChildren( XPathNodeType.Element );

     xPathNodeIteratorParent.MoveNext();

     xPathNavigatorChildren    = xPathNodeIteratorParent.Current;
     xPathNodeIteratorChildren = xPathNavigatorChildren.SelectChildren( XPathNodeType.Element );
     childrenCount             = -1;
     childrenTotal             = xPathNodeIteratorChildren.Count;
    
     children                  = new string[childrenTotal][];

     while ( xPathNodeIteratorChildren.MoveNext() )
     {
      ++childrenCount;
      children[childrenCount]  = new string[2];
      children[childrenCount][NodeRankName]  = xPathNodeIteratorChildren.Current.Name.Trim();
      children[childrenCount][NodeRankValue] = xPathNodeIteratorChildren.Current.ToString().Trim();     

      /*
      #if ( DEBUG )
       System.Console.WriteLine
       (
        "Children Name: {0} | Value: {1}",
        children[childrenCount][NodeRankName],
        children[childrenCount][NodeRankValue]
       );      
      #endif
      */
      
      for 
      ( 
       elementConfigurationCount = 0, elementConfigurationIndex = -1; 
       elementConfigurationCount < elementConfigurationTotal; 
       ++elementConfigurationCount 
      )
      {       
       if ( ElementConfiguration[elementConfigurationCount][ElementRankName] == children[childrenCount][NodeRankName] )
       {
        elementConfigurationIndex = elementConfigurationCount;
       }//if ( ElementConfiguration[elementConfigurationCount][ElementRankName] == children[childrenCount][NodeRankName] )        
      }//for ( elementConfigurationCount = 0, elementConfigurationIndex = -1; elementConfigurationCount < elementConfigurationTotal; ++elementConfigurationCount )       
     
      //Element not found; therefore, do not process.
      if ( elementConfigurationIndex == -1 )
      {
       continue;        
      }//if ( elementConfigurationIndex == -1 )
        
      oleDbCommand = new OleDbCommand
      ( 
       ElementConfiguration[elementConfigurationIndex][ElementRankStoredProcedure], 
       oleDbConnection 
      );
      
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbCommandBuilder.DeriveParameters(  oleDbCommand );
      oleDbParameterCollection       = oleDbCommand.Parameters;
      oleDbParameterCollectionTotal  = oleDbParameterCollection.Count;
     
      xPathNavigatorGrandChildren    = xPathNodeIteratorChildren.Current;
      xPathNodeIteratorGrandChildren = xPathNavigatorGrandChildren.SelectChildren( XPathNodeType.Element );
      grandChildrenCount             = -1;
      grandChildrenTotal             = xPathNodeIteratorGrandChildren.Count;
      grandChildren                  = new string[grandChildrenTotal][];

      while ( xPathNodeIteratorGrandChildren.MoveNext() )
      {
       ++grandChildrenCount;        
       grandChildren[grandChildrenCount] = new string[2];        
       grandChildren[grandChildrenCount][NodeRankName]  = xPathNodeIteratorGrandChildren.Current.Name.Trim();
       grandChildren[grandChildrenCount][NodeRankValue] = xPathNodeIteratorGrandChildren.Current.ToString().Trim();
       grandChildrenValueLength                         = grandChildren[grandChildrenCount][NodeRankValue].Length;
       for 
       ( 
        oleDbParameterCollectionCount = 0, oleDbParameterCollectionIndex = -1; 
        oleDbParameterCollectionCount < oleDbParameterCollectionTotal; 
        ++oleDbParameterCollectionCount
       ) 
       {
        if ( string.Compare ( grandChildren[grandChildrenCount][NodeRankName], oleDbParameterCollection[oleDbParameterCollectionCount].ParameterName, true ) == 0 )
        {
         oleDbParameterCollectionIndex = oleDbParameterCollectionCount;
         switch ( oleDbParameterCollection[oleDbParameterCollectionCount].OleDbType )
         {
          case OleDbType.DBTimeStamp:
           if ( grandChildrenValueLength == 12 || grandChildrenValueLength == 14 )
           {
            dateTime = UtilityDateTimeParse.DateTimeParse( grandChildren[grandChildrenCount][NodeRankValue] );
           }//if ( grandChildrenValueLength == 12 || grandChildrenValueLength == 14 )
           else if ( grandChildrenValueLength == 33 )
           {
            dateTime = XmlConvert.ToDateTime( grandChildren[grandChildrenCount][NodeRankValue] ); 
           }//else if ( grandChildren[grandChildrenCount][NodeRankValue].Length == 33 )            
           oleDbParameterCollection[oleDbParameterCollectionCount].Value = dateTime; 
           break;
          
          default:                 
           oleDbParameterCollection[oleDbParameterCollectionCount].Value = grandChildren[grandChildrenCount][NodeRankValue];
           break;
         }//switch ( oleDbParameterCollection[oleDbParameterCollectionCount].OleDbType )
         break; 
        }//if ( string.Compare ( grandChildren[grandChildrenCount][NodeRankName], oleDbParameterCollection[oleDbParameterCollectionCount].ParameterName, true ) == 0 )
       }//foreach ( OleDbParameter oleDbParameter in oleDbParameters )
       #if ( DEBUG )
        if ( oleDbParameterCollectionIndex > -1 ) //Database Parameter Found.
        {
         System.Console.WriteLine
         (        
          "OleDbParameter Name: {0} | Type: {1} | Value: {2}",
          oleDbParameterCollection[oleDbParameterCollectionIndex].ParameterName,
          oleDbParameterCollection[oleDbParameterCollectionIndex].OleDbType,
          oleDbParameterCollection[oleDbParameterCollectionIndex].Value           
         );
        }//Database Parameter Found.
        else
        {
         System.Console.WriteLine
         (        
          "GrandChildren Name: {0} | Value: {1}",
          grandChildren[grandChildrenCount][NodeRankName],
          grandChildren[grandChildrenCount][NodeRankValue]
         );
        }//Database Parameter Not Found.
        #endif            
      }//while ( xPathNodeIteratorGrandChildren.MoveNext() )

      for 
      ( 
       oleDbParameterCollectionCount = 0; 
       oleDbParameterCollectionCount < oleDbParameterCollectionTotal; 
       ++oleDbParameterCollectionCount
      )
      {
       switch ( oleDbParameterCollection[oleDbParameterCollectionCount].OleDbType )
       {
        case OleDbType.Guid:
         if ( oleDbParameterCollection[oleDbParameterCollectionCount].Value == null )
         {
          oleDbParameterCollection[oleDbParameterCollectionCount].Value = System.Guid.NewGuid();  
         }   
         break;
       }//switch ( oleDbParameterCollection[oleDbParameterCollectionCount].OleDbType )
      }//for ( oleDbParameterCollectionCount = 0; oleDbParameterCollectionCount < oleDbParameterCollectionTotal; ++oleDbParameterCollectionCount )

      oleDbCommand.ExecuteNonQuery();
     }//while ( xPathNodeIteratorChildren.MoveNext() )
    }//try    
    catch( OleDbException oleDbException )
    {
     exceptionMessage = UtilityEventLog.WriteEntryOleDbErrorCollection( oleDbException );
     System.Console.WriteLine("OleDbException: {0}", exceptionMessage);
    }//catch( OleDbException oleDbException )
    catch( IOException ioException )
    {
     System.Console.WriteLine("IOException: {0}", ioException.Message);
    }//catch( OleDbException oleDbException )
    catch( XPathException xPathException )
    {
     System.Console.WriteLine("XPathException: {0}", xPathException.Message );
    }//catch( XPathException xPathException )
    catch( XmlException xmlException )
    {
     System.Console.WriteLine("XmlException: {0}", xmlException.Message);
    }//catch(XmlException xmlEx)
    catch( Exception exception )
    {
     System.Console.WriteLine( "Exception: {0}", exception.Message );
    }//catch(Exception ex)
   }//foreach ( string xmlDocumentFilename in xmlDocumentFilenames )
   
   //Close the database connection.
   UtilityDatabase.DatabaseConnectionHouseKeeping( oleDbConnection, ref exceptionMessage );
   
  }//static void XmlParse( string[] xmlDocumentFilenames )

  ///<summary>Read the XML Configuration file.</summary>
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

  ///<summary>Read the XML Configuration file.</summary>
  ///<param name="filenameConfigurationXml">The XML Configuration file.</param>
  ///<param name="exceptionMessage">The exception message.</param>
  ///<param name="databaseConnectionString">The database connection string.</param>  
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
 }//class WordSaidXmlLoad. 
}//namespace WordEngineering.