using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace WordEngineering
{
 /// <summary>UtilityXmlArgument</summary>
 public class UtilityXmlArgument
 {
  ///<summary>filenameXmlDocument</summary>  
  public string[]   filenameXmlDocument   =  null;

  ///<summary>filenameSchema</summary>  
  public string[]   filenameSchema        =  null;

  ///<summary>filenameStylesheet</summary>  
  public string[]   filenameStylesheet    =  null;

  ///<summary>files</summary>
  //[DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor.</summary>
  public UtilityXmlArgument()
  {
  }//public UtilityXmlArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityXmlArgument
  (
   string[]  filenameXmlDocument,
   string[]  filenameSchema,
   string[]  filenameStylesheet
  )
  {

   this.filenameXmlDocument  =  filenameXmlDocument;
   this.filenameSchema       =  filenameSchema;
   this.filenameStylesheet   =  filenameStylesheet;   
   
  }//public UtilityXmlArgument()

 }//public class UtilityXmlArgument

 /// <summary>UtilityXml</summary>
 /// <remarks>UtilityXml.</remarks>
 public class UtilityXml
 {

  /// <summary>The XmlMarkupCharacters.</summary>
  public static char[] XmlMarkupCharacters           = { '<', '>',   };

  static string DatabaseConnectionString             = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;user id=WordEngineering;password=WordEngineering;";

  static string FilenameConfigurationWordEngineering = @"d:\WordOfGod\WordEngineering.config";
  
  static string FilenameInvoice                      = @"d:\WordOfGod\20050724Invoice.xml";
    
  const  string XmlSignificantWhitespaceSpaces       = @"  ";

  const  string XPathDatabaseConnectionString        = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";
   
  const  string XPathFilenameHtml                    = @"/word/file[@applicationName='Noise']/filenameHtml";    
  const  string XPathFilenameXml                     = @"/word/file[@applicationName='Noise']/filenameXml";       
  const  string XPathFilenameXsd                     = @"/word/file[@applicationName='Noise']/filenameXsd";    
  const  string XPathFilenameXsl                     = @"/word/file[@applicationName='Noise']/filenameXsl";
  const  string XPathFilenameXslt                    = @"/word/file[@applicationName='Noise']/filenameXslt";   

  static string XPathInvoiceLineitemsLineItemPrice   = "/invoices/invoice/lineItems/lineItem/price/text()";

  static string XPathInvoiceLineitemsLineItemPriceSum= "sum( /invoices/invoice/lineItems/lineItem/price/text() )";
  
  /// <summary>XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalent</summary>
  public static  String[][] XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalent = new String[][]
                                                       {
                                                        new String[] { "&", "&amp;" },
                                                        new String[] { "\\", "&quot;" },
                                                        new String[] { "'", "&apos;" },
                                                        new String[] { "<", "&lt;" },
                                                        new String[] { ">", "&gt;" },
                                                       }; 

  /// <summary>Regular expression XML Schema Data Type for the DateTime.</summary>
  public static readonly Regex  RegexXmlSchemaDatatypeDateTime;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Boolean             booleanParseCommandLineArguments  =  false;
   string              exceptionMessage                  =  null;     
   UtilityXmlArgument  utilityXmlArgument                =  null;
   
   utilityXmlArgument = new UtilityXmlArgument();
   
   booleanParseCommandLineArguments  =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityXmlArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityXmlArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   Stub
   (
    ref utilityXmlArgument,
    ref exceptionMessage
   );
   
  }//static void Main( String[] argv ) 

  ///<summary>Stub.</summary>
  public static void Stub
  (
   ref UtilityXmlArgument  utilityXmlArgument,
   ref string              exceptionMessage
  )
  {
   XmlSchemaValidation
   (
    ref utilityXmlArgument.filenameXmlDocument,
    ref utilityXmlArgument.filenameSchema,
    ref exceptionMessage
   );
  }

  /// <summary>GetNodeValue</summary>
  /// <remarks>http://msmvps.com/dvravikanth</remarks>
  public static void GetNodeValue
  (
        string filenameXml,
   ref  string exceptionMessage,
        string xPathExpression,
   ref  string defaultSetting
  )
  {
   XPathDocument      xPathDocument       =  null;
   XPathNavigator     xPathNavigator      =  null;
   XPathNavigator     xPathNavigatorNode  =  null;
   try
   {
    xPathDocument       =  new XPathDocument( filenameXml );
    xPathNavigator      =  xPathDocument.CreateNavigator();
    xPathNavigatorNode  =  xPathNavigator.SelectSingleNode( xPathExpression );
    if ( xPathNavigatorNode != null )
    {
     defaultSetting = xPathNavigatorNode.InnerXml;
    }
   }//try   
   catch (ArgumentException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "ArgumentException: {0}", exception.Message );
   }
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
  }//GetNodeValue

  /// <summary>GetNodeValue</summary>
  public static void GetNodeValue
  (
        string filenameXml,
   ref  string exceptionMessage,
        string xPathExpression,
   ref  int    defaultSetting
  )
  {
   string  stringDefaultSetting  =  defaultSetting.ToString();
   GetNodeValue
   (
        filenameXml,
    ref exceptionMessage,
        xPathExpression,
    ref stringDefaultSetting
   );
   if ( !String.IsNullOrEmpty( stringDefaultSetting ) )
   {
   	Int32.TryParse( stringDefaultSetting, out defaultSetting );
   }
  }//GetNodeValue()
  	
  /// <summary>GetNodeValue</summary>
  public static void GetNodeValue
  (
        string   filenameXml,
   ref  string   exceptionMessage,
        string   xPathExpression,
   ref  string[] defaultSetting
  )
  {
   int                xPathNodeIteratorIndex  =  -1;
   int                xPathNodeIteratorCount  =  -1;
   XPathDocument      xPathDocument           =  null;
   XPathNavigator     xPathNavigator          =  null;
   XPathNodeIterator  xPathNodeIterator       =  null;
   try
   {
    xPathDocument           =  new XPathDocument( filenameXml );
    xPathNavigator          =  xPathDocument.CreateNavigator();
    xPathNodeIterator       =  xPathNavigator.Select( xPathExpression );
    xPathNodeIteratorCount  =  xPathNodeIterator.Count;
    if ( xPathNodeIteratorCount > 0 )
    {
     defaultSetting = new string[ xPathNodeIteratorCount ];
     for ( xPathNodeIteratorIndex = 0; xPathNodeIteratorIndex < xPathNodeIteratorCount; ++xPathNodeIteratorIndex )
     { 
      xPathNodeIterator.MoveNext();
      defaultSetting[xPathNodeIteratorIndex] = xPathNodeIterator.Current.Value;
     }//for ( xPathNodeIteratorIndex = 1; xPathNodeIteratorIndex <= xPathNodeIteratorCount; ++xPathNodeIteratorIndex )
    }//if ( xPathNodeIteratorCount > 0 ) 
   }//try   
   catch (ArgumentException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "ArgumentException: {0}", exception.Message );
   }
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
  }//GetNodeValue

  /// <summary>GetNodeValue</summary>
  public static void GetNodeValue
  (
        string   filenameXml,
   ref  string   exceptionMessage,
        string   xPathExpression,
   ref  int[]    defaultSetting
  )
  {
   string[]  stringDefaultSetting  =  null;
   GetNodeValue
   (
        filenameXml,
    ref exceptionMessage,
        xPathExpression,
    ref stringDefaultSetting
   );
   if ( stringDefaultSetting != null )
   {
    defaultSetting = new int [ stringDefaultSetting.Length ];
    for ( int index = 0; index < stringDefaultSetting.Length; ++index )
    {
     Int32.TryParse( stringDefaultSetting[ index ], out defaultSetting[ index ] );    	
    }//for ( int index = 0; index < stringDefaultSetting.Count; ++index )
   }//if ( stringDefaultSetting != null )
  }//GetNodeValue()
  	
  ///<summary>XmlParse()</summary>
  ///<remarks>http://www.devx.com/assets/download/13685.pdf</remarks>
  public static void XmlParse
  (
   ref  string[]  filenameXmlDocument,
   ref  string[]  filenameSchema,
   ref  string    exceptionMessage
  )
  {

   HttpContext  httpContext  =  HttpContext.Current;

   XmlReader          xmlReader          =  null;

   try
   {
    foreach ( string filenameXmlDocumentCurrent in filenameXmlDocument )
    {
     xmlReader  =  XmlReader.Create( filenameXmlDocumentCurrent );
     while ( xmlReader.Read() )
     {
      if ( xmlReader.NodeType == XmlNodeType.XmlDeclaration || xmlReader.NodeType == XmlNodeType.Element )
      {
       System.Console.WriteLine
       ( 
        "Node Type: {0} | Name: {1} | Depth: {2} | Value: {3} | IsEmptyElement {4}",
        xmlReader.NodeType,
        xmlReader.Name,
        xmlReader.Depth,
        xmlReader.Value,
        xmlReader.IsEmptyElement
       );
       /*
       if ( xmlReader.HasValue )
       {
        System.Console.WriteLine
        ( 
         "Value: {0}",
         xmlReader.Value
        );
       }//if ( xmlReader.HasValue )
       */
       for ( int attributeCount = 0; attributeCount < xmlReader.AttributeCount; ++attributeCount )
       {
        xmlReader.MoveToNextAttribute();
        System.Console.WriteLine
        ( 
         "Attribute[{0}]: {1} | Value: {2}",
         attributeCount,
         xmlReader.Name,
         xmlReader[attributeCount] //xmlReader.GetAttribute(attributeCount)
        );
       }//for ( int attributeCount = 0; attributeCount < xmlReader.AttributeCount; ++attributeCount )
      }//if ( xmlReader.NodeType == XmlNodeType.XmlDeclaration || xmlReader.NodeType == XmlNodeType.Element )
     }//while ( xmlReader.Read() )
    }//foreach ( string filenameXmlDocumentCurrent in filenameXmlDocument )
   }//try
   catch ( System.Xml.Schema.XmlSchemaException  exception )
   {
   	exceptionMessage = "System.Xml.Schema.XmlSchemaException: " + exception.Message;
   }//catch ( System.Xml.Schema.XmlSchemaException  exception )	
   catch ( System.ArgumentNullException  exception )
   {
   	exceptionMessage = "System.ArgumentNullException: " + exception.Message;
   }//catch ( System.ArgumentNullException  exception )	
   catch ( System.IO.FileNotFoundException  exception )
   {
   	exceptionMessage = "System.IO.FileNotFoundException: " + exception.Message;
   }//catch ( System.IO.FileNotFoundException  exception )	
   catch ( System.UriFormatException  exception )
   {
   	exceptionMessage = "System.UriFormatException: " + exception.Message;
   }//catch ( System.UriFormatException  exception )	
   catch ( System.Exception  exception )
   {
   	exceptionMessage = "System.Exception: " + exception.Message;
   }//catch ( System.Exception  exception )
   finally
   {
    if ( xmlReader != null )
    {
     xmlReader.Close();    	
    }//if ( xmlReader != null )
   }//finally
   	
   #if ( DEBUG )
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMessage );
    }//else if ( httpContext == null )
   #endif

  }//XmlParse()
  
  ///<summary>XmlSchemaValidation()</summary>
  ///<remarks>http://msdn2.microsoft.com/library/as3tta56(en-us,vs.80).aspx</remarks>
  public static void XmlSchemaValidation
  (
   ref  string[]  filenameXmlDocument,
   ref  string[]  filenameSchema,
   ref  string    exceptionMessage
  )
  {

   HttpContext  httpContext  =  HttpContext.Current;

   XmlReader          xmlReader          =  null;
   XmlReaderSettings  xmlReaderSettings  =  null;
   XmlSchemaSet       xmlSchemaSet       =  null;
   
   try
   {
    // Create the XmlSchemaSet class.
    xmlSchemaSet = new XmlSchemaSet();

    foreach ( string filenameSchemaCurrent in filenameSchema )
    {
     if ( !String.IsNullOrEmpty( filenameSchemaCurrent ) )
     {
      xmlSchemaSet.Add( "", filenameSchemaCurrent );
     }//if ( !String.IsNullOrEmpty( filenameSchemaCurrent ) ) 	
    }//foreach ( string filenameSchemaCurrent in filenameSchema )    	

    // Set the validation settings.
    xmlReaderSettings = new XmlReaderSettings();
    xmlReaderSettings.ValidationType = ValidationType.Schema;
    xmlReaderSettings.Schemas = xmlSchemaSet;
    xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler ( XmlSchemaValidationCallBack );
 
    foreach ( string filenameXmlDocumentCurrent in filenameXmlDocument )
    {
     // Create the XmlReader object.
     xmlReader  =  XmlReader.Create( filenameXmlDocumentCurrent, xmlReaderSettings );
     // Parse the file. 
     while ( xmlReader.Read() );
    }//foreach ( string filenameXmlDocumentCurrent in filenameXmlDocument )
   }//try
   catch ( System.Xml.Schema.XmlSchemaException  exception )
   {
   	exceptionMessage = "System.Xml.Schema.XmlSchemaException: " + exception.Message;
   }//catch ( System.Xml.Schema.XmlSchemaException  exception )	
   catch ( System.ArgumentNullException  exception )
   {
   	exceptionMessage = "System.ArgumentNullException: " + exception.Message;
   }//catch ( System.ArgumentNullException  exception )	
   catch ( System.IO.FileNotFoundException  exception )
   {
   	exceptionMessage = "System.IO.FileNotFoundException: " + exception.Message;
   }//catch ( System.IO.FileNotFoundException  exception )	
   catch ( System.UriFormatException  exception )
   {
   	exceptionMessage = "System.UriFormatException: " + exception.Message;
   }//catch ( System.UriFormatException  exception )	
   catch ( System.Exception  exception )
   {
   	exceptionMessage = "System.Exception: " + exception.Message;
   }//catch ( System.Exception  exception )

   #if ( DEBUG )
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMessage );
    }//else if ( httpContext == null )
   #endif

  }//XmlSchemaValidation()
  
  /// <summary>Display any validation errors.</summary>
  public static void XmlSchemaValidationCallBack
  (
   object               sender, 
   ValidationEventArgs  validationEventArgs
  )
  {
   HttpContext  httpContext  =  HttpContext.Current;

   #if ( DEBUG )
    if ( httpContext == null )
    {
     System.Console.WriteLine( "Validation Error: {0}", validationEventArgs.Message );
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( "Validation Error: " + validationEventArgs.Message );
    }//else if ( httpContext == null )
   #endif
  }//public static void XmlSchemaValidationCallBack ( object sender, ValidationEventArgs  validationEventArgs )

  /// <summary>Read a particular XmlNode's inner text.</summary>
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

  /// <summary>SelectNodes</summary>
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
   XmlDocument  xmlDocument  =  null; 
   XmlNodeList  xmlNodeList  =  null;
   HttpContext  httpContext  =  null;

   xmlDocument  =  new XmlDocument(); 
   httpContext  =  HttpContext.Current;

   try
   {
    xmlDocument.Load( filenameXml );
    xmlNodeList = xmlDocument.SelectNodes(xPathExpression);

    /*
    #if (DEBUG)
     foreach ( XmlNode xmlNode in xmlNodeList )
     {
      if ( httpContext == null )
      {
       System.Console.WriteLine("UtilityXml.cs SelectNodes: {0}", xmlNode.InnerXml );
      }//if ( httpContext == null )
      else
      {
       httpContext.Response.Write( "UtilityXml.cs SelectNodes: " + xmlNode.InnerXml );
      }//else if ( httpContext == null )
     }//foreach ( XmlNode xmlNode in xmlNodeList )
    #endif
    */

   }//try   
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }
   catch (XmlException exception)
   {
    exceptionMessage = "XmlException: " + exception.Message;
   }
   catch (SystemException exception)
   {
    exceptionMessage = "SystemException: " + exception.Message;
   }
   catch (Exception exception)
   {
    exceptionMessage = "Exception: " + exception.Message;
   }
   finally
   {
   }//finally

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMessage );
    }//else if ( httpContext == null )
   }//if ( exceptionMessage != null )   
   
   return ( xmlNodeList );
   
  }//public static void SelectNodes

  /// <summary>SelectNodes</summary>
  public static void SelectNodes
  (
        String    filenameXml,
   ref  String    exceptionMessage,
        String    xPathExpression,
   ref  string[]  innerXml
  )
  {
   XmlNodeList  xmlNodeList  =  null;
   
   SelectNodes
   (
         filenameXml,
    ref  exceptionMessage,
         xPathExpression,
    ref  xmlNodeList
   );
   
   if ( !String.IsNullOrEmpty( exceptionMessage ) || xmlNodeList == null )
   {
   	return;
   }//if ( !String.IsNullOrEmpty( exceptionMessage ) || xmlNodeList == null )
   
   Convert
   (
         xmlNodeList,
    ref  innerXml,
    ref  exceptionMessage
   );
   
  }//public static void SelectNodes

  /// <summary>SelectNodes</summary>
  /// <param name="filenameXml">XML filename.</param>
  /// <param name="exceptionMessage">Exception Message.</param>
  /// <param name="xPathExpression">XPath expression.</param>
  /// <param name="xmlNodeList">XML node list.</param>  
  public static void SelectNodes
  (
        String      filenameXml,
   ref  String      exceptionMessage,
        String      xPathExpression,
   ref  XmlNodeList xmlNodeList
  )
  {

   XmlDocument  xmlDocument  =  null; 
   HttpContext  httpContext  =  null;

   httpContext  =  HttpContext.Current;

   /*
   #if (DEBUG)
    if ( httpContext == null )
    {
     System.Console.WriteLine
     (
      "UtilityXml.cs SelectNodes() xPathExpression: {0}", 
      xPathExpression
     );
    }
    else
    {
     httpContext.Response.Write("UtilityXml.cs SelectNodes() xPathExpression: " + xPathExpression );
     httpContext.Response.Write("UtilityXml.cs filenameXml: " + filenameXml );
    }
   #endif
   */

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
    }//foreach ( XmlNode xmlNode in xmlNodeList )
   }//if ( xmlNodeList != null && xmlNodeList.Count > 0 )
  }//public static Convert( XmlNodeList xmlNodeList, string[] innerXml, ref string exceptionMessage );
  
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
   HttpContext               httpContextCurrent               = HttpContext.Current; 
   XmlNode                   xmlNodeDocumentType              = null;   
   XmlNode                   xmlNodeRoot                      = null;      
   XmlDocument               xmlDocument                      = null;
   XmlProcessingInstruction  xmlProcessingInstruction         = null;

   if ( string.IsNullOrEmpty( filenameStylesheet ) == true )
   {
    return;
   }
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
   #if (DEBUG)
    if ( httpContextCurrent != null && exceptionMessage != null )
    {
     httpContextCurrent.Response.Write( "Exception: " + exceptionMessage );
    }//if ( httpContextCurrent != null && exceptionMessage != null )
   #endif
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
   string       dataSetName         =  null;
   string       directoryName       =  null;
   HttpContext  httpContextCurrent  =  HttpContext.Current;
  
   exceptionMessage = null;
   dataSetName      = dataSet.DataSetName;
 
   if ( filenameXml == null )
   {
    filenameXml = dataSetName + UtilityFile.Extension("xml");
   }

   try
   {
    UtilityDirectory.WebServerFullPath( ref filenameXml );
    directoryName = Path.GetDirectoryName( filenameXml );
    if ( directoryName != "" && Directory.Exists( directoryName ) == false )
    {
     Directory.CreateDirectory( directoryName );
    }
    //dataSet.WriteXmlSchema(filenameSchema);
    dataSet.WriteXml(filenameXml);
    if ( string.IsNullOrEmpty( filenameStylesheet ) == false )
    {
     WriteXmlStylesheet
     (
      ref exceptionMessage,
      ref filenameXml,
      ref filenameStylesheet
     );
    }
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
   #if (DEBUG)
    if ( httpContextCurrent != null && exceptionMessage != null )
    {
     httpContextCurrent.Response.Write( "Exception: " + exceptionMessage );
    }//if ( httpContextCurrent != null && exceptionMessage != null )
   #endif
  }//WriteXml()    

  ///<summary>Read XML.</summary>
  /// <param name="dataSet">Dataset.</param>  
  /// <param name="exceptionMessage">Exception Message.</param>
  /// <param name="filenameXml">Filename XML.</param>
  public static void ReadXml
  (
   ref DataSet dataSet,
   ref string  exceptionMessage,
   ref string  filenameXml
  )
  {
   string  filenameSchema  =  null;
   
   ReadXml
   (
    ref dataSet,
    ref exceptionMessage,
    ref filenameXml,
    ref filenameSchema
   ); 	
  }
  	
  ///<summary>Read XML.</summary>
  /// <param name="dataSet">Dataset.</param>  
  /// <param name="exceptionMessage">Exception Message.</param>
  /// <param name="filenameXml">Filename XML.</param>
  /// <param name="filenameSchema">Filename Schema.</param>  
  public static void ReadXml
  (
   ref  DataSet  dataSet,
   ref  string   exceptionMessage,
   ref  string   filenameXml,
   ref  string   filenameSchema
  )
  { 
   HttpContext httpContextCurrent = HttpContext.Current; 
   
   dataSet = new DataSet();
   exceptionMessage = null;
   
   try
   {
    if ( filenameSchema != null && filenameSchema != string.Empty )
    {
     UtilityDirectory.WebServerFullPath( ref filenameSchema );
     dataSet.ReadXmlSchema( filenameSchema );
    }//if ( filenameSchema != null && filenameSchema != string.empty )
     
    UtilityDirectory.WebServerFullPath( ref filenameXml );
    //if ( !File.Exists(filenameXml)) 
    dataSet.ReadXml( filenameXml );

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
   #if (DEBUG)
    if ( httpContextCurrent != null && exceptionMessage != null )
    {
     httpContextCurrent.Response.Write( "Exception: " + exceptionMessage );
    }//if ( httpContextCurrent != null && exceptionMessage != null )
   #endif
  }//ReadXml()    
  
  ///<summary>Valid Xml Datetime</summary>
  /// <param name="datetime">Datetime.</param>  
  public static bool IsValidRegexXmlSchemaDatatypeDateTime
  (
   string datetime
  )
  {
   return ( RegexXmlSchemaDatatypeDateTime.IsMatch( datetime ) );
  }  	

  ///<summary>Xml Significant Whitespace</summary>
  ///<param name="innerText">InnerXml</param>  
  public static void XmlSignificantWhitespace
  (
   ref string innerText
  )
  {
   int           substringCharacterPosition            = 0;   
   int           xmlSignificantWhitespaceIndexPosition = -1;
   StringBuilder sb                                    = new StringBuilder();
   
   while ( true )
   {
    xmlSignificantWhitespaceIndexPosition = innerText.IndexOf
    ( 
     XmlSignificantWhitespaceSpaces,
     substringCharacterPosition 
    );
    
    if ( xmlSignificantWhitespaceIndexPosition < 0 )
    {
     sb.Append( innerText.Substring( substringCharacterPosition ) ); 
     break;
    }//if ( xmlSignificantWhitespaceIndexPosition < 0 )    	
    else
    {
     sb.Append
     ( 
      innerText.Substring
      ( 
       substringCharacterPosition,
       xmlSignificantWhitespaceIndexPosition - substringCharacterPosition
      )
     );
    }//if ( xmlSignificantWhitespaceIndexPosition >= 0 ) 
    substringCharacterPosition = xmlSignificantWhitespaceIndexPosition + 1;
   }//while ( true )

   #if (DEBUG)
    System.Console.WriteLine
    (
     "UtilityXml.cs XmlSignificantWhitespace: {0} | {1}", 
     innerText,
     sb
    );
   #endif
   innerText = sb.ToString();
  }//public static XmlSignificantWhitespace()

  /// <summary>FormatForXML.</summary>
  public static String FormatForXML
  (
   object input
  )
  {
   string data = input.ToString();      // cast the input to a string

   // replace those characters disallowed in XML documents
   foreach (String [] XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalentCurrent in XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalent )
   {
   	data = data.Replace
   	(
   	 XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalentCurrent[0],
   	 XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalentCurrent[1]
   	); 
   }//foreach (String [] XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalentCurrent in XMLSubstituteIllegalCharactersWithTheirEscapedLegalEquivalent )   	
   return data;
  }//protected String FormatForXML(object input)

  /// <summary>XmlElementAppend</summary>
  public static void XmlElementAppend
  (
   ref XmlDocument  xmlDocument,
   ref XmlElement   xmlElement,
       String       elementName,
   ref object       elementValue,
   ref String       exceptionMessage
  )
  { 
   HttpContext httpContextCurrent = HttpContext.Current; 

   exceptionMessage = null;
   
   try
   {
    xmlElement            =  xmlDocument.CreateElement(elementName, null );
    xmlElement.SetAttribute("name", (String) elementValue);    
    xmlDocument.DocumentElement.AppendChild( xmlElement );
   }//try
   catch (SecurityException exception)
   {
    exceptionMessage = String.Format("SecurityException: {0}", exception.Message );
   }//catch (SecurityException exception)
   catch (XmlException exception)
   {
    exceptionMessage = String.Format("XmlException: {0}", exception.Message );
   }//catch (XmlException exception)
   catch (SystemException exception)
   {
    exceptionMessage = String.Format("SystemException: {0}", exception.Message );
   }//catch (SystemException exception)
   catch (Exception exception)
   {
    exceptionMessage = String.Format("Exception: {0}", exception.Message );
   }//catch (Exception exception)
   if ( exceptionMessage != null )
   {
    UtilityDebug.Write( exceptionMessage );
   }
  }//XmlElementAppend()

  /// <summary>XmlElementAppend</summary>
  public static void XmlElementAppend
  (
   ref XmlDocument  xmlDocument,
   ref XmlElement   xmlElement,
   ref XmlElement   xmlElementChild,
       String       elementName,
   ref object       elementValue,
   ref String       exceptionMessage
  )
  { 
   HttpContext httpContextCurrent = HttpContext.Current; 

   exceptionMessage = null;
   
   try
   {
    xmlElementChild            =  xmlDocument.CreateElement( elementName, null );
    xmlElementChild.SetAttribute("name", (String) elementValue);
    xmlElement.AppendChild( xmlElementChild );
   }//try
   catch (SecurityException exception)
   {
    exceptionMessage = String.Format("SecurityException: {0}", exception.Message );
   }//catch (SecurityException exception)
   catch (XmlException exception)
   {
    exceptionMessage = String.Format("XmlException: {0}", exception.Message );
   }//catch (XmlException exception)
   catch (SystemException exception)
   {
    exceptionMessage = String.Format("SystemException: {0}", exception.Message );
   }//catch (SystemException exception)
   catch (Exception exception)
   {
    exceptionMessage = String.Format("Exception: {0}", exception.Message );
   }//catch (Exception exception)
   if ( exceptionMessage != null )
   {
    UtilityDebug.Write( exceptionMessage );
   }
  }//XmlElementAppend()

  /// <summary>XmlElementAppend</summary>
  public static void XmlElementAppend
  (
   ref XmlDocument  xmlDocument,
   ref XmlElement   xmlElementParent,
   ref XmlElement   xmlElementChild,
       String       xmlElementName,
   ref ArrayList    attributeName,
   ref ArrayList    attributeValue,
   ref String       exceptionMessage
  )
  { 
   HttpContext httpContextCurrent    =  HttpContext.Current; 
   String      attributeValueString  =  null;

   exceptionMessage = null;
   
   try
   {
    xmlElementChild       =  xmlDocument.CreateElement( xmlElementName, null );
    
    for( int attributeCount = 0; attributeCount < attributeName.Count; ++attributeCount )
    {
     //UtilityDebug.Write(String.Format("[{0}] | {1} | {2}", attributeCount, attributeName[attributeCount], attributeValue[attributeCount]));
     
     if ( attributeValue[attributeCount] == null )
     { 
      attributeValueString = null;
     }//if ( attributeValue[attributeCount] == null )
     else 
     {
      attributeValueString  =  attributeValue[attributeCount].ToString();
      attributeValueString  =  attributeValueString.Trim();
      
      if ( attributeValueString == String.Empty )
      {
       attributeValueString = null;
      }//if ( attributeValueString == String.Empty )
      
     }//else if ( attributeValue[attributeCount] != null )
          	
     if ( attributeValueString != null )
     {
      xmlElementChild.SetAttribute
      (
       (String) attributeName[attributeCount], 
       attributeValueString
      );
     }//if ( attributeValueString != null ) 
     
     //xmlElementChild.Attributes[attributeCount].Value  = attributeValueString;
     
    }//for( int attributeCount = 0; attributeCount < attributeName.Count; ++attributeCount ) 
    
    if ( xmlElementParent == null )
    {
     xmlDocument.DocumentElement.AppendChild( xmlElementChild );
     xmlElementParent = xmlDocument.DocumentElement;
    }//if ( xmlElementParent == null ) 
    else
    {
     xmlElementParent.AppendChild( xmlElementChild );
    }
    
   }//try
   catch (SecurityException exception)
   {
    exceptionMessage = String.Format("SecurityException: {0}", exception.Message );
   }//catch (SecurityException exception)
   catch (XmlException exception)
   {
    exceptionMessage = String.Format("XmlException: {0}", exception.Message );
   }//catch (XmlException exception)
   catch (SystemException exception)
   {
    exceptionMessage = String.Format("SystemException: {0}", exception.Message );
   }//catch (SystemException exception)
   catch (Exception exception)
   {
    exceptionMessage = String.Format("Exception: {0}", exception.Message );
   }//catch (Exception exception)
   if ( exceptionMessage != null )
   {
    UtilityDebug.Write( exceptionMessage );
   }
  }//XmlElementAppend()

  ///<summary>InvoiceUseDOM</summary>
  ///<remarks>http://msdn.microsoft.com/msdnmag/issues/03/02/XMLFiles/</remarks>
  public static void InvoiceUseDOM()
  {
   XmlDocument  xmlDocument  =  null;
   XmlNodeList  xmlNodeList  =  null;
   double       sumPrice     =  0.0;
   
   try
   {
   	xmlDocument = new XmlDocument();
   	xmlDocument.Load( FilenameInvoice );
    xmlNodeList = xmlDocument.SelectNodes( XPathInvoiceLineitemsLineItemPrice );
    foreach( XmlNode xmlNode in xmlNodeList )
    {
     sumPrice += XmlConvert.ToDouble( xmlNode.InnerText);
    }//foreach( XmlNode xmlNode in xmlNodeList ) 
    System.Console.WriteLine("sumPrice: {0}", sumPrice);
   }//try
   catch ( Exception exception )
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message );
   }//catch ( Exception exception )   	
  }//public static void InvoiceUseDOM()

  ///<summary>InvoiceUseNavigator</summary>
  ///<remarks>http://msdn.microsoft.com/msdnmag/issues/03/02/XMLFiles/</remarks>
  public static void InvoiceUseNavigator()
  {

   double             sumPrice           =  0.0;
   
   XPathDocument      xPathDocument      =  null;
   XPathNavigator     xPathNavigator     =  null;
   XPathNodeIterator  xPathNodeIterator  =  null;

   try
   {
    xPathDocument      =  new XPathDocument( FilenameInvoice );
    xPathNavigator     =  xPathDocument.CreateNavigator();
    xPathNodeIterator  =  xPathNavigator.Select( XPathInvoiceLineitemsLineItemPrice );

    while ( xPathNodeIterator.MoveNext() )
    {
     sumPrice += XmlConvert.ToDouble( xPathNodeIterator.Current.Value );
    }//while ( xPathNodeIterator.MoveNext() )
    
    System.Console.WriteLine("sumPrice: {0}", sumPrice);

   }//try
   catch ( Exception exception )
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }   	
   	
  }//public static void InvoiceUseNavigator()

  ///<summary>InvoiceUseNavigatorSum</summary>
  ///<remarks>http://msdn.microsoft.com/msdnmag/issues/03/02/XMLFiles/</remarks>
  public static void InvoiceUseNavigatorSum()
  {

   double             sumPrice           =  0.0;
   
   XPathDocument      xPathDocument      =  null;
   XPathNavigator     xPathNavigator     =  null;

   try
   {
    xPathDocument      =  new XPathDocument( FilenameInvoice );
    xPathNavigator     =  xPathDocument.CreateNavigator();

    sumPrice           =  ( double ) xPathNavigator.Evaluate( XPathInvoiceLineitemsLineItemPriceSum );
    System.Console.WriteLine("sumPrice: {0}", sumPrice);

   }//try
   catch ( Exception exception )
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }//catch ( Exception exception )   	
   	
  }//public static void InvoiceUseNavigatorSum()

  /*
    public static void Convert
    (
         XmlNodeList xmlNodeList,
     ref string[]    innerXml,
     ref string      exceptionMessage
    )

  */
  ///<summary>SortNodeList</summary>
    ///<remarks>
    /// www.devnewsgroups.net/group/microsoft.public.dotnet.xml/topic693.aspx
    /// SortedList &lt;object, XmlNode&gt; weatherConditionsSorted = SortNodeList(weatherConditions, "weather-summary", TypeCode.String);
    ///</remarks>
    public static SortedList<object, XmlNode> SortNodeList
    (
         XmlNodeList xmlNodeList,
         String sortKeyAttribute,
         TypeCode typeCode
    )
    {
        SortedList<object, XmlNode> sortedList = new SortedList<object, XmlNode>();
        foreach (XmlNode xmlNode in xmlNodeList)
        {
             string elementAttribute = xmlNode.Attributes[sortKeyAttribute].Value;
             if (typeCode == TypeCode.String)
             {
                sortedList.Add(elementAttribute, xmlNode);
             }
             else
             {
                sortedList.Add(System.Convert.ChangeType(elementAttribute, typeCode), xmlNode);
             }
        }
        return sortedList;
    }

  ///<summary>Static.</summary>
  static UtilityXml()
  {
   RegexXmlSchemaDatatypeDateTime = new Regex( @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{7}-\d{2}:\d{2}" );
  }//static UtilityXml()	

 }//public class UtilityXml
}//namespace WordEngineering