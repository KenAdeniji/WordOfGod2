/*
 20030718 Find: DirectorynameSeparator. Replace: DirectorySeparatorChar. Sweat, armpit, left.
*/ 

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace WordEngineering
{
 /// <summary>UtilityXml</summary>
 /// <remarks>UtilityXml.</remarks>
 public class UtilityXml
 {

  /// <summary>The XmlMarkupCharacters.</summary>
  public static char[] XmlMarkupCharacters            = { '<', '>',   };

  static string DatabaseConnectionString             = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;user id=WordEngineering;password=WordEngineering;";
  static string ExceptionMessage                     = null;  

  static string FilenameConfigurationWordEngineering = @"d:\WordOfGod\WordEngineering.config";
  
  static string FilenameHtmlNoise                    = @"d:\WordOfGod\Comforter_-_Noise.xml";
  static string FilenameXmlNoise                     = @"d:\WordOfGod\Comforter_-_Noise.xml";
  static string FilenameXsdNoise                     = @"d:\WordOfGod\Comforter_-_Noise.xsd";   
  static string FilenameXslNoise                     = @"d:\WordOfGod\Comforter_-_Noise.xsl";
  static string FilenameXsltNoise                    = @"d:\WordOfGod\Comforter_-_Noise.xslt";
 
  const  string XPathDatabaseConnectionString        = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";
   
  const  string XPathFilenameHtml                    = @"/word/file[@applicationName='Noise']/filenameHtml";    
  const  string XPathFilenameXml                     = @"/word/file[@applicationName='Noise']/filenameXml";       
  const  string XPathFilenameXsd                     = @"/word/file[@applicationName='Noise']/filenameXsd";    
  const  string XPathFilenameXsl                     = @"/word/file[@applicationName='Noise']/filenameXsl";
  const  string XPathFilenameXslt                    = @"/word/file[@applicationName='Noise']/filenameXslt";   

  /// <summary>Regular expression XML Schema Data Type for the DateTime.</summary>
  public static readonly Regex  RegexXmlSchemaDatatypeDateTime;
  
  /// <summary>Read a particular XmlNode's outer text.</summary>
  /// <param name="filenameXml">XML filename.</param>
  /// <param name="exceptionMessage">Exception Message.</param>  
  /// <param name="xPathExpression">XPath expression.</param>    
  /// <param name="defaultSetting">Default setting.</param>      
  public static void XmlDocumentNodeInnerText
  (
        string filenameXml,
   ref  string exceptionMessage,
        string xPathExpression,
   ref  string defaultSetting  
  )
  {
   string      xmlText        = null;
   XmlDocument xmlDocument    = null; 
   XmlNode     xmlNode        = null;
  
   try
   {
    xmlDocument = new XmlDocument(); 
    xmlDocument.Load( filenameXml );
    xmlNode = xmlDocument.SelectSingleNode(xPathExpression);
    if ( xmlNode != null )
    { 
     xmlText = xmlNode.InnerText;
     if ( xmlText != null )
     {
      xmlText = xmlText.Trim();
     }//if ( xmlText != null ) 
    }//if ( xmlNode != null ) 
   }//try   
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }
   catch (XmlException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "XmlException: {0}", exception.Message );
   }
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
   }
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }
   finally
   {
    if ( xmlText != null )
    {
     defaultSetting = xmlText;
    }//if ( xmlText != null )
   }//finally
  }//XmlDocumentNodeInnerText

  /// <summary>Read a particular XmlNode's outer text.</summary>
  /// <param name="filenameXml">XML filename.</param>
  /// <param name="exceptionMessage">Exception Message.</param>  
  /// <param name="xPathExpression">XPath expression.</param>    
  public static XmlNodeList SelectNodes
  (
        string filenameXml,
   ref  string exceptionMessage,
        string xPathExpression
  )
  {
   XmlDocument xmlDocument = null; 
   XmlNodeList xmlNodeList = null;

   #if (DEBUG)
    System.Console.WriteLine
    (
     "UtilityXml.cs SelectNodes() xPathExpression: {0}", 
     xPathExpression
    );
   #endif

   try
   {
    xmlDocument = new XmlDocument(); 
    xmlDocument.Load( filenameXml );
    xmlNodeList = xmlDocument.SelectNodes(xPathExpression);
    #if (DEBUG)
     foreach ( XmlNode xmlNode in xmlNodeList )
     {
      System.Console.WriteLine("UtilityXml.cs SelectNodes: {0}", xmlNode.InnerXml );
     }
    #endif
   }//try   
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }
   catch (XmlException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "XmlException: {0}", exception.Message );
   }
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
   }
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }
   finally
   {
   }//finally
   
   return ( xmlNodeList );
   
  }//public static void SelectNodes

  ///<summary>Convert.</summary>
  ///<param name="xmlNodeList">XML Node List format.</param>
  ///<param name="innerXml">innerXml.</param>    
  ///<param name="exceptionMessage">Exception message.</param>  
  public static void Convert
  (
       XmlNodeList xmlNodeList,
   ref string[]    innerXml,
   ref string      exceptionMessage
  )
  {
   int  xmlMarkupCharactersIndex  = -1;
   int  xmlNodeListIndex          = -1;
   int  xmlNodeListTotal          = -1;
   
   innerXml               =  null;
   exceptionMessage       =  null;
   xmlNodeListTotal       =  xmlNodeList.Count;
      
   if ( xmlNodeList != null && xmlNodeListTotal > 0 )
   {
    innerXml = new string[ xmlNodeListTotal ];
    for ( xmlNodeListIndex = 0; xmlNodeListIndex < xmlNodeListTotal; ++xmlNodeListIndex )
    {
     innerXml[xmlNodeListIndex] = xmlNodeList[xmlNodeListIndex].InnerXml;
     if ( innerXml[xmlNodeListIndex] == null || innerXml[xmlNodeListIndex] == "" || innerXml[xmlNodeListIndex] == string.Empty )
     {
      continue;
     }//if ( innerXml[xmlNodeListIndex] == null || innerXml[xmlNodeListIndex] == "" || innerXml[xmlNodeListIndex] == string.Empty )
     xmlMarkupCharactersIndex = innerXml[xmlNodeListIndex].IndexOfAny(XmlMarkupCharacters);
     if ( xmlMarkupCharactersIndex >= 0 )
     {
      innerXml[xmlNodeListIndex] = innerXml[xmlNodeListIndex].Substring( 0, xmlMarkupCharactersIndex ).Trim();
     }//if ( indexOf >= 0 )	
     #if (DEBUG)
      System.Console.WriteLine("UtilityXml.cs Convert(): {0}", innerXml[xmlNodeListIndex]);
     #endif
    }//foreach ( XmlNode xmlNode in xmlNodeList )
   }//if ( xmlNodeList != null && xmlNodeList.Count > 0 )
  }//public static Convert( XmlNodeList xmlNodeList, ArrayList arrayList, ref string exceptionMessage );
  
  ///<summary>Write XML.</summary>
  /// <param name="dataSet">Dataset.</param>  
  /// <param name="exceptionMessage">Exception Message.</param>
  public static void WriteXml
  (
       DataSet dataSet,
   ref string  exceptionMessage
  )
  {
   string  filenameXml        = null;
   string  filenameStylesheet = null;
   
   WriteXml
   (
        dataSet,
    ref exceptionMessage,
    ref filenameXml,
    ref filenameStylesheet
   );//WriteXml();
  }//public static void WriteXml

  ///<summary>Write XMLStylesheet.</summary>
  /// <param name="exceptionMessage">Exception Message.</param>
  /// <param name="filenameXml">Filename XML.</param>
  /// <param name="filenameStylesheet">Filename Stylesheet (XSLT).</param>
  public static void WriteXmlStylesheet
  (
   ref string  exceptionMessage,
   ref string  filenameXml,
   ref string  filenameStylesheet
  )
  { 
   string                    xmlProcessingInstructionText     = null;
   HttpContext               httpContext                      = HttpContext.Current; 
   XmlNode                   xmlNodeDocumentType              = null;   
   XmlNode                   xmlNodeRoot                      = null;      
   XmlDocument               xmlDocument                      = null;
   XmlProcessingInstruction  xmlProcessingInstruction         = null;
   
   try
   {
    xmlDocument = new XmlDocument();
    xmlDocument.Load( filenameXml );
    xmlProcessingInstructionText = "type='text/xsl' href='" + filenameStylesheet + "'";
    xmlProcessingInstruction = xmlDocument.CreateProcessingInstruction
    (
     "xml-stylesheet", 
     xmlProcessingInstructionText
    );
    xmlNodeDocumentType = xmlDocument.DocumentType;
    xmlNodeRoot         = xmlDocument.DocumentElement;
    xmlDocument.InsertBefore(xmlProcessingInstruction, xmlNodeRoot);
    xmlDocument.Save( filenameXml );
   }//try
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
    httpContext.Response.Write( "SecurityException: " + exception.Message );
   }//catch (SecurityException exception)
   catch (XmlException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "XmlException: {0}", exception.Message );
    httpContext.Response.Write( "XmlException: " + exception.Message );
   }//catch (XmlException exception)
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
    httpContext.Response.Write( "SystemException: " + exception.Message );
   }//catch (SystemException exception)
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
    httpContext.Response.Write( "Exception: " +  exception.Message );    
   }//catch (Exception exception)
  }//public static void WriteXmlStylesheet()  
  
  ///<summary>Write XML.</summary>
  /// <param name="dataSet">Dataset.</param>  
  /// <param name="exceptionMessage">Exception Message.</param>
  /// <param name="filenameXml">Filename XML.</param>
  /// <param name="filenameStylesheet">Filename Stylesheet (XSLT).</param>
  public static void WriteXml
  (
       DataSet dataSet,
   ref string  exceptionMessage,
   ref string  filenameXml,
   ref string  filenameStylesheet
  )
  { 

   string                    dataSetName                      = null;
   HttpContext               httpContext                      = HttpContext.Current; 
  
   exceptionMessage = null;
   dataSetName      = dataSet.DataSetName;
 
   if ( filenameXml == null )
   {
    filenameXml = dataSetName + UtilityFile.Extension("xml");
   }
   
   if ( filenameStylesheet == null )
   { 
    filenameStylesheet = dataSetName + UtilityFile.Extension("xslt");
   }

   try
   {
    UtilityDirectory.WebServerFullPath( ref filenameXml );

    //dataSet.WriteXmlSchema(filenameSchema);
    dataSet.WriteXml(filenameXml);
    WriteXmlStylesheet
    (
     ref exceptionMessage,
     ref filenameXml,
     ref filenameStylesheet
    );
   }//try
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
    httpContext.Response.Write( "SecurityException: " + exception.Message );
   }//catch (SecurityException exception)
   catch (XmlException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "XmlException: {0}", exception.Message );
    httpContext.Response.Write( "XmlException: " + exception.Message );
   }//catch (XmlException exception)
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
    httpContext.Response.Write( "SystemException: " + exception.Message );
   }//catch (SystemException exception)
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
    httpContext.Response.Write( "Exception: " +  exception.Message );    
   }//catch (Exception exception)
  }//WriteXml()    

  ///<summary>Read XML.</summary>
  /// <param name="dataSet">Dataset.</param>  
  /// <param name="exceptionMessage">Exception Message.</param>
  /// <param name="filenameXml">Filename XML.</param>
  public static void ReadXml
  (
       DataSet dataSet,
   ref string  exceptionMessage,
   ref string  filenameXml
  )
  { 
   HttpContext httpContext = HttpContext.Current; 
  
   exceptionMessage = null;
   
   try
   {
    UtilityDirectory.WebServerFullPath( ref filenameXml );
    dataSet.ReadXml(filenameXml);
   }//try
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }//catch (SecurityException exception)
   catch (XmlException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "XmlException: {0}", exception.Message );
   }//catch (XmlException exception)
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
   }//catch (SystemException exception)
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }//catch (Exception exception)
  }//ReadXml()    

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(String[] argv)
  {
   try
   {

    UtilityXml.XmlDocumentNodeInnerText
    (
         FilenameConfigurationWordEngineering,
     ref ExceptionMessage,         
         XPathDatabaseConnectionString,
     ref DatabaseConnectionString
    );

    UtilityXml.XmlDocumentNodeInnerText
    (
         FilenameConfigurationWordEngineering,
     ref ExceptionMessage,
         XPathFilenameHtml,
     ref FilenameHtmlNoise
    );

    UtilityXml.XmlDocumentNodeInnerText
    (
         FilenameConfigurationWordEngineering,
     ref ExceptionMessage,         
         XPathFilenameXml,
     ref FilenameXmlNoise
    );

    UtilityXml.XmlDocumentNodeInnerText
    (
         FilenameConfigurationWordEngineering,
     ref ExceptionMessage,         
         XPathFilenameXsd,
     ref FilenameXsdNoise
    );

    UtilityXml.XmlDocumentNodeInnerText
    (
         FilenameConfigurationWordEngineering,
     ref ExceptionMessage,
         XPathFilenameXsl,
     ref FilenameXslNoise
    );

    UtilityXml.XmlDocumentNodeInnerText
    (
         FilenameConfigurationWordEngineering,
     ref ExceptionMessage,         
         XPathFilenameXslt,
     ref FilenameXsltNoise
    );

    System.Console.WriteLine
    (
     "Database Connection String: {0} | HTML: {1} | XML: {2} | XSD: {3} |  XSL: {4} | XSLT: {5}.", 
     DatabaseConnectionString,
     FilenameHtmlNoise,
     FilenameXmlNoise,
     FilenameXsdNoise,     
     FilenameXslNoise,          
     FilenameXsltNoise     
    );
 
    if ( ExceptionMessage != null )
    {
     System.Console.WriteLine("{0}", ExceptionMessage);
    }//if ( ExceptionMessage != null ) 
   }//try
   catch (Exception exception)
   {
    System.Console.WriteLine( exception.Message );
   }//catch
   finally
   {

   }//finally
  }//public static void Main(String[] args)
  
  ///<summary>Valid Xml Datetime?</summary>
  /// <param name="datetime">Datetime.</param>  
  public static bool IsValidRegexXmlSchemaDatatypeDateTime
  (
   string datetime
  )
  {
   return ( RegexXmlSchemaDatatypeDateTime.IsMatch( datetime ) );
  }  	

  ///<summary>Static.</summary>
  static UtilityXml()
  {
   RegexXmlSchemaDatatypeDateTime = new Regex( @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{7}-\d{2}:\d{2}" );
  }//static UtilityXml()	

 }//public class UtilityXml
}//namespace WordEngineering