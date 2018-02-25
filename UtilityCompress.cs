using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace WordEngineering
{
 /// <summary>UtilityCompressArgument</summary>
 public class UtilityCompressArgument
 {
  ///<summary>source</summary>
  public string[] source       =  null;

  ///<summary>destination</summary>  
  public string   destination  =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityCompressArgument()
  :this
  (
   null,  //source
   null   //destination
  ) 
  {
  }

  /// <summary>Constructor Overloading</summary>
  public UtilityCompressArgument
  (
   string[] source,
   string   destination
  )
  {
   this.source       =  source;
   this.destination  =  destination;
  }
 }

 ///<summary>UtilityCompress</summary>
 ///<remarks>
 /// http://lab.msdn.microsoft.com/vs2005/downloads/101samples/default.aspx
 /// </remarks>
 public class UtilityCompress
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of arguments</param>
  public static void Main( string[] argv )
  {
   Boolean                  booleanParseCommandLineArguments  =  false;
   string                   exceptionMessage                  =  null;
   UtilityCompressArgument  utilityCompressArgument           =  null;
   
   utilityCompressArgument         =  new UtilityCompressArgument();
   
   booleanParseCommandLineArguments  =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityCompressArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityCompressArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   CompressFile
   (
    ref utilityCompressArgument,
    ref exceptionMessage
   );
  }
 
  /// <summary>CompressFile</summary>
  public static void CompressFile
  (
   ref UtilityCompressArgument  utilityCompressArgument,
   ref string                   exceptionMessage
  )
  {
   // Create the streams and byte arrays needed
   byte[]  buffer  =  null;
   string  directoryName  =  null;
   string  fileNamePattern  =  null;
   ArrayList  filenames  =  null;   
   FileStream sourceStream       =  null;
   FileStream destinationStream  =  null;
   GZipStream compressedStream   =  null;
   try
   {
    directoryName  =  Path.GetDirectoryName( utilityCompressArgument.destination );
    if ( Directory.Exists( directoryName ) == false )
    {
     Directory.CreateDirectory( directoryName );
    }
   	// Open the FileStream to write to
    destinationStream = new FileStream ( utilityCompressArgument.destination, FileMode.OpenOrCreate, FileAccess.Write );
    // Create a compression stream pointing to the destination stream
    compressedStream = new GZipStream ( destinationStream, CompressionMode.Compress, true );
    foreach( string source in utilityCompressArgument.source )
    {
     if ( File.Exists( source ) )
     {
      filenames = new ArrayList();
      filenames.Add( source );
     }
     else
     {
      directoryName  =  Path.GetDirectoryName( source );
      fileNamePattern  =  Path.GetFileName( source );
      if ( Directory.Exists ( directoryName ) )
      {
       UtilityDirectory.Dir
       (
            directoryName,
            fileNamePattern,
        ref filenames
       );
      }
     }
     foreach( object filenameCurrent in filenames )
     {
      // Read the bytes from the source file into a byte array
      sourceStream = new FileStream ( filenameCurrent.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read );
      // Read the source stream values into the buffer
      buffer = new byte[sourceStream.Length];
      sourceStream.Read ( buffer, 0, buffer.Length );
      System.Console.WriteLine("Filename: {0} | Length: {1}", filenameCurrent, buffer.Length);
      // Now write the compressed data to the destination file
      compressedStream.Write ( buffer, 0, buffer.Length );
      if ( sourceStream != null )
      {
       sourceStream.Close();
      }
     }
    }
   }
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   finally
   {
    if ( compressedStream != null )
    {
     compressedStream.Close();
    }
    if ( destinationStream != null )
    {  
     destinationStream.Close ( );
    }
   }
  }
 }
}
