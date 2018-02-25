using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Security;
using System.Net;
using System.Net.Mail; //2.0
using System.Net.Mime;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mail; //1.0
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace WordEngineering
{

 /// <summary>UtilityResourceArgument</summary>
 public class UtilityResourceArgument
 {
  ///<summary>resourceFilename</summary>  
  public string[]  resourceFilename           =  null;
  
  ///<summary>resourceAddFilenameTarget</summary>  
  public string    resourceAddFilenameTarget  =  null;

  ///<summary>resourceAdd</summary>  
  public string[]  resourceAdd                =  null;

  ///<summary>resourceList</summary>  
  public bool      resourceList               =  false;

  ///<summary>resourceOverwrite</summary>  
  public bool      resourceOverwrite          =  false;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[]  files;

  /// <summary>Constructor Overloading</summary>
  public UtilityResourceArgument()
  :this
  (
   null,  //resourceFilename
   null,  //resourceAddFilenameTarget
   null,  //resourceAdd
   false, //resourceList
   false  //resourceOverwrite
  ) 
  {
  }//public UtilityResourceArgument()

  /// <summary>Constructor.</summary>
  public UtilityResourceArgument
  (
   string[]  resourceFilename,
   string    resourceAddFilenameTarget,
   string[]  resourceAdd,
   bool      resourceList,
   bool      resourceOverwrite
  )
  {
   this.resourceFilename           =  resourceFilename;
   this.resourceAddFilenameTarget  =  resourceAddFilenameTarget;
   this.resourceAdd                =  resourceAdd;
   this.resourceList               =  resourceList;
   this.resourceOverwrite          =  resourceOverwrite;
  }//public UtilityResourceArgument()

 }//public class UtilityResourceArgument

 ///<summary>UtilityResource</summary>
 ///<remarks>
 /// Windows Forms Resource Editor (Winres.exe)
 /// Resource File Generator (Resgen.exe)
 ///</remarks>
 public class UtilityResource
 {

  /// <summary>The delimiter in character array format for the resourceAdd</summary>
  public static  char[]     DelimiterCharResourceAdd           = null;

  /// <summary>ResourceAdd name, value [,type]</summary>
  public const   int        ResourceAddIndexRankName           = 0;

  /// <summary>ResourceAdd name, value [,type]</summary>
  public const   int        ResourceAddIndexRankValue          = 1;

  /// <summary>ResourceAdd name, value [,type]</summary>
  public const   int        ResourceAddIndexRankType           = 2;
  
  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>The delimiter string for the resourceAdd</summary>
  public const   string     DelimiterStringResourceAdd         = ",;|";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";

  /// <summary>ResourceTypeFile</summary>
  public const   String     ResourceTypeFile                   = "FILE";

  /// <summary>ResourceTypeString</summary>
  public const   String     ResourceTypeString                 = "STRING";

  /// <summary>The XPath database connection String.</summary>
  public static  String     XPathDatabaseConnectionString      = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>Constructor.</summary>
  public UtilityResource()
  {

  }
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Boolean                           booleanParseCommandLineArguments          =  false;
   string                            exceptionMessage                          =  null;     
   UtilityResourceArgument           utilityResourceArgument                   =  null;
   
   utilityResourceArgument                =  new UtilityResourceArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityResourceArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityResourceArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   if 
   ( 
    utilityResourceArgument.resourceAdd != null  &&
    !String.IsNullOrEmpty( utilityResourceArgument.resourceAddFilenameTarget )    
   ) 
   {
    ResourceAdd
    (
     ref utilityResourceArgument,
     ref exceptionMessage
    );
   }

   if ( utilityResourceArgument.resourceList )
   {
    ResourceList
    (
     ref utilityResourceArgument,
     ref exceptionMessage
    );
   }//if ( utilityResourceArgument.resourceList )
    
  }//static void Main( String[] argv ) 

  ///<summary>ResourceAdd</summary>
  public static void ResourceAdd
  (
   ref UtilityResourceArgument  utilityResourceArgument,
   ref string                   exceptionMessage
  )
  {
   
   bool                  fileCreate          =  false;

   string[]              resourceAlias       =  null;
   string                resourceName        =  null;
   string                resourceType        =  null;
   string                resourceValue       =  null;
   
   HttpContext           httpContext         =  HttpContext.Current;
   System.Drawing.Image  image               =  null;
   ResXResourceWriter    resXResourceWriter  =  null;
   
   try
   {

    foreach( string resourceAddCurrent in utilityResourceArgument.resourceAdd )
    {
     resourceName   =  null;
     resourceType   =  null;
     resourceValue  =  null;
     
     if ( string.IsNullOrEmpty( resourceAddCurrent ) )
     { 
      continue;
     }//if ( !String.IsNullOrEmpty( resourceAddCurrent ) )

     resourceAlias = resourceAddCurrent.Split( DelimiterCharResourceAdd );

     if ( resourceAlias.Length < ResourceAddIndexRankType )
     {
      throw new Exception("/resourceAdd:name| value [|type]");
     }
     else if ( resourceAlias.Length > ResourceAddIndexRankType )
     {
      resourceType = resourceAlias[ ResourceAddIndexRankType ].Trim().ToUpper();
     }
     else
     {
      resourceType = ResourceTypeString;
     }

     resourceName   =  resourceAlias[ ResourceAddIndexRankName ].Trim();
     resourceValue  =  resourceAlias[ ResourceAddIndexRankValue ].Trim();

     if ( resourceName.Length < 1 )
     {
      throw new Exception("/resourceAdd:name| value [|type]");
     }//if ( resourceName.Length < 1 )
     
     if ( !fileCreate )
     {
      if ( File.Exists( utilityResourceArgument.resourceAddFilenameTarget ) )
      {
       if ( !utilityResourceArgument.resourceOverwrite )
       {
       	throw new Exception("/resourceAdd:name| value [|type] /resourceOverwrite");
       }//if ( !utilityResourceArgument.resourceOverwrite )
       File.Delete( utilityResourceArgument.resourceAddFilenameTarget );
      }//if ( File.Exists( utilityResourceArgument.resourceAddFilenameTarget ) )  
      resXResourceWriter = new ResXResourceWriter
      ( 
       utilityResourceArgument.resourceAddFilenameTarget 
      );
      fileCreate = true;
     }//if ( !fileCreate )

     switch ( resourceType )
     {
      case "":
      case ResourceTypeString:
       resXResourceWriter.AddResource( resourceName, resourceValue ); 
       break;
      case ResourceTypeFile:
       image = System.Drawing.Image.FromFile( resourceValue );
       resXResourceWriter.AddResource( resourceName, image ); 
       break;
     }//switch ( resourceType )

     #if (DEBUG)
      System.Console.WriteLine
      ( 
       "Name: {0} | Value: {1} | Type: {2}",
       resourceName,
       resourceValue,
       resourceType
      );
     #endif

    }//foreach( string resourceAddCurrent in resourceAdd )

    if ( resXResourceWriter != null )
    {
     resXResourceWriter.Generate();
     resXResourceWriter.Close();
    }//if ( resXResourceWriter. != null ) 
        
   }//try
   catch( InvalidOperationException exception )
   {
    exceptionMessage = "InvalidOperationException: " + exception.Message;   	
   }//catch( Expression expression )
   catch( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;   	
   }//catch( Expression expression )
   finally
   {
    if ( resXResourceWriter != null )
    {
     resXResourceWriter.Close();
    }//if ( resXResourceWriter. != null ) 
   }//finally

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )

  }//public static void ResourceAdd()

  ///<summary>ResourceList</summary>
  public static void ResourceList
  (
   ref UtilityResourceArgument  utilityResourceArgument,
   ref string                   exceptionMessage
  )
  {
   
   HttpContext            httpContext            =  HttpContext.Current;
   IDictionaryEnumerator  iDictionaryEnumerator  =  null;
   ResXResourceReader     resXResourceReader     =  null;
   
   try
   {
    foreach( string filenameCurrent in utilityResourceArgument.resourceFilename )
    {
     // Create a ResXResourceReader for the file items.resx.
     resXResourceReader    =  new ResXResourceReader( filenameCurrent );

     // Create an IDictionaryEnumerator to iterate through the resources.
     iDictionaryEnumerator  =  resXResourceReader.GetEnumerator();       

     // Iterate through the resources and display the contents to the console.
     foreach ( DictionaryEntry  dictionaryEntry in resXResourceReader ) 
     {
      System.Console.WriteLine
      (
       dictionaryEntry.Key.ToString() + ":\t" + dictionaryEntry.Value.ToString()
      );
     }//foreach ( DictionaryEntry  dictionaryEntry in iDictionaryEnumerator )

     //Close the reader.
     resXResourceReader.Close();
    }//foreach( string filenameCurrent in utilityResourceArgument.resourceFilename )
   }//try
   catch( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;   	
   }//catch( Expression expression )
   finally
   {
    if ( resXResourceReader != null )
    {
     resXResourceReader.Close();
    }//if ( resXResourceReader != null ) 
   }//finally   	

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )

  }//public static void ResourceList()

  /// <summary>ThreadCurrentCulture</summary>
  public static void ThreadCurrentCulture()
  {  
   HttpContext            httpContext            =  HttpContext.Current;
   
   if ( httpContext == null )
   {
   	return;
   }//if ( httpContext == null )	
   
   // Set the CurrentCulture property to the culture associated with the Web
   // browser's current language setting.
   Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture
   (
    httpContext.Request.UserLanguages[0]
   );

   // It is good practice to explicitly set the CurrentUICulture property.
   // Initialize the CurrentUICulture property
   // with the CurrentCulture property.
   Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
  }//public static void ThreadCurrentCulture() 

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
  
  static UtilityResource()
  {
   ConfigurationXml();
   DelimiterCharResourceAdd  =  DelimiterStringResourceAdd.ToCharArray();
  }//static UtilityResource()
  
 }//public class UtilityResource
 
}//namespace WordEngineering