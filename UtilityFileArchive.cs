using System;
using System.Collections;
using System.IO;
using System.Text;

namespace WordEngineering
{
 /// <summary>UtilityFile.</summary>
 public class UtilityFile
 {

  /// <summary>The file name strip extension default, false.</summary>
  public static readonly string[][] FileClassification = 
  {
   new string[] { "Html", @".html" },
   new string[] { "Xml",  @".xml"  },
   new string[] { "Xsd",  @".xsd"  },
   new string[] { "Xsl",  @".xsl"  },
   new string[] { "Xslt", @".xslt" }
  };

  /// <summary>The file name strip extension default, false.</summary>
  /// <remarks>
  ///  ContentType application/octet-stream Response is not interpretatable by the client; therefore, download.
  /// </remarks>
  public static readonly string[][] FileExtension = {
                                           new string[] { ".bmp", @"image/bmp" },
                                           new string[] { ".doc", @"application/ms-word" },
                                           new string[] { ".gif", @"image/gif" },
                                           new string[] { ".mp3", @"audio/mpeg" },
                                           new string[] { ".pdf", @"application/pdf" },
                                           new string[] { ".ppt", @"application/x-mspowerpoint" },
                                           new string[] { ".tif", @"image/tiff" },
                                           new string[] { ".xls", @"application/vnd.ms-excel" },
                                           new string[] { ".zip", @"application/x-zip-compressed" }
                                          }; 

  /// <summary>DefaultContentType</summary>
  public const string    DefaultContentType                = "application/octet-stream";

  /// <summary>The file name strip extension default, false.</summary>
  public static bool     FilenameStripExtensionDefault     = false;

  /// <summary>The file name extension separator (.).</summary>
  public static char     FilenameExtensionSeparator        = '.';
  
  /// <summary>The file rank classification.</summary>
  public const int       FileClassificationRankType        = 0;
   
  /// <summary>The file rank expression.</summary>   
  public const int       FileClassificationRankExtension   = 1;

  /// <summary>The file extension rank extension</summary>
  public const int       FileExtensionRankExt              = 0;
   
  /// <summary>The file extension rank content type</summary>   
  public const int       FileExtensionRankContentType      = 1;

  /// <summary>The file name datetime string format.</summary>
  public static string   FilenameDateTimeStringFormat      = "yyyyMMddhhmm";

  /// <summary>The file name extension separator (.).</summary>
  public static string[] FilenameStripWildcard             = { "*", "?" };

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line arguments.</param>
  public static void Main
  (
   string[] argv
  )
  {
   Strip( ref argv );
   #if (DEBUG)
    int argumentCount = 0;
    foreach ( string argvCurrent in argv )
    {
     System.Console.WriteLine("Argument Value [{0}]: {1}", argumentCount, argvCurrent);
     ++argumentCount;
    }//foreach ( string argvCurrent in argv )
   #endif
  }//public static void Main()
  
  /// <summary>FileExtensionIndex</summary>
  public static int FileExtensionIndex
  (
   string  fileExtension
  )
  {
   int  index     =  -1;
   fileExtension  =  System.IO.Path.GetExtension( fileExtension ).ToLower();
   for ( int current = 0; current < FileExtension.Length; ++current )
   {
   	if ( string.Compare( FileExtension[current][FileExtensionRankExt], fileExtension, true ) == 0 )
   	{
     index = current;
     break;
    }//if ( string.Compare( FileExtension[current][FileExtensionRankExt], fileExtension, true ) == 0 )
   }//for ( int current = 0; current < FileExtension.Length; ++current )	
   return ( index );
  }//FileExtensionIndex
  	  
  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  public static void Strip
  (
   ref string filename
  ) 
  {
   string[] filenames = { filename };
   Strip
   ( 
    ref filenames, 
        FilenameStripWildcard,
        FilenameStripExtensionDefault
   );
   filename = filenames[0];
  }//public static void Strip

  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  public static void Strip
  (
   ref string[] filename
  ) 
  {
   Strip
   ( 
    ref filename, 
        FilenameStripWildcard,
        FilenameStripExtensionDefault
   );
  }//public static void Strip

  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  /// <param name="strip">The strip string(s).</param>
  public static void Strip
  (
   ref string[] filename,
       string[] strip
  ) 
  {
   Strip
   ( 
    ref filename, 
        strip, 
        FilenameStripExtensionDefault 
   );
  }//public static void Strip
  	
  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  /// <param name="stripWildcard">The strip wildcard flag.</param>
  /// <param name="stripFilenameExtension">The strip filename extension flag</param>
  public static void Strip
  (
   ref string[] filename,
       bool     stripWildcard,
       bool     stripFilenameExtension
  ) 
  {
   string[] strip = null;
   
   if ( stripWildcard == true )
   {
    strip = FilenameStripWildcard;	
   }//if ( stripWildcard == true )
   	
   Strip
   (
    ref filename,
        strip,
        stripFilenameExtension
   );     
  }//public static void Strip

  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  /// <param name="stripWildcard">The strip wildcard flag.</param>
  /// <param name="stripFilenameExtension">The strip filename extension flag</param>
  public static void Strip
  (
   ref string filename,
       bool   stripWildcard,
       bool   stripFilenameExtension
  ) 
  {
   string[] filenames = { filename };
   string[] strip = null;
   
   if ( stripWildcard == true )
   {
    strip = FilenameStripWildcard;	
   }//if ( stripWildcard == true )
   	
   Strip
   (
    ref filenames,
        strip,
        stripFilenameExtension
   );
   filename = filenames[0];
  }//public static void Strip

  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  /// <param name="stripWildcard">The strip wildcard flag.</param>
  /// <param name="stripFilenameExtension">The strip filename extension flag</param>
  public static void Strip
  (
   ref string filename,
       string stripWildcard,
       bool   stripFilenameExtension
  ) 
  {
   string[] filenames      = { filename };
   string[] stripWildcards = { stripWildcard };
   
   Strip
   (
    ref filenames,
        stripWildcards,
        stripFilenameExtension
   );
   filename = filenames[0];
  }//public static void Strip
    	
  /// <summary>Strip.</summary>
  /// <param name="filename">The filename(s).</param>
  /// <param name="strip">The strip string(s).</param>
  /// <param name="stripFilenameExtension">The strip filename extension flag</param>
  public static void Strip
  (
   ref string[] filename,
       string[] strip,
       bool     stripFilenameExtension
  ) 
  {
   int filenameCount = 0;
   int filenameTotal = filename.Length;
   int stripCount    = 0;
   int stripLength   = 0;
   int stripTotal    = strip.Length;
   int stripIndex    = 0;
  
   for ( filenameCount = 0; filenameCount < filenameTotal; ++filenameCount )
   {
    if ( stripFilenameExtension )
    {
     stripIndex = filename[filenameCount].LastIndexOf( FilenameExtensionSeparator );
     if ( stripIndex >= 0 ) //strip, found.
     {
      filename[filenameCount] = filename[filenameCount].Remove
      ( 
       stripIndex, 
       filename[filenameCount].Length - stripIndex 
      );
     }//if ( stripIndex > 0 ) strip, found. 
    }//if ( stripFilenameExtension )	
    for ( stripCount = 0; stripCount < stripTotal; ++stripCount )
    {
     stripLength = strip[stripCount].Length;
     while ( true )
     {
      stripIndex = filename[filenameCount].IndexOf( strip[stripCount] );
      if ( stripIndex >= 0 ) //strip, found.
      {
       filename[filenameCount] = filename[filenameCount].Remove
       ( 
        stripIndex, 
        stripLength 
       );
      }//if ( stripIndex > 0 ) strip, found.
      else
      {
       break;
      }//if ( stripIndex < 0 ) strip, not found.
     }//while ( true ) 
    }//for ( stripCount = 0; stripCount <= stripTotal; ++stripCount ) 
   }//for ( filenameCount = 0; filenameCount <= filenameTotal; ++filenameCount )
  }//public static void Strip

  /// <summary>The filename extension.</summary>
  /// <param name="fileType">The file type, for example, HTML, XML, XSD, XSL, XSLT </param>
  public static string Extension(string fileType)
  {
   return
   (
    UtilityCollection.IndexOf
    ( 
     FileClassification,
     fileType, 
     FileClassificationRankType,
     FileClassificationRankExtension
    )
   );  
  }//public static string FilenameExtension(string fileType)

  ///<summary>DatePostfix</summary>
  public static string DatePostfix( string filename )
  {
   String        directory;
   StringBuilder sbFilename;
   sbFilename = new StringBuilder();
   directory = Path.GetDirectoryName(filename);
   if ( !String.IsNullOrEmpty(directory) )
   {
    sbFilename.Append( directory );
    sbFilename.Append( Path.DirectorySeparatorChar );
   }
   sbFilename.Append( Path.GetFileNameWithoutExtension(filename) );
   sbFilename.Append( String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) );
   sbFilename.Append( Path.GetExtension(filename) );   
   return( sbFilename.ToString() );
  }
  
  ///<summary>DatePrefix</summary>
  public static string DatePrefix( string filename )
  {
   String        directory;
   StringBuilder sbFilename;
   sbFilename = new StringBuilder();
   directory = Path.GetDirectoryName(filename);
   if ( !String.IsNullOrEmpty( directory ) )
   {
    sbFilename.Append( directory );
    sbFilename.Append( Path.DirectorySeparatorChar );
   }
   sbFilename.Append( String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) );
   sbFilename.Append( Path.GetFileName(filename) );
   return( sbFilename.ToString() );
  }

 }//public class UtilityFile.
}//namespace WordEngineering. 
