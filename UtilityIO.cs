using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;

namespace WordEngineering
{

 ///<summary>UtilityIO</summary>
 ///<remarks></remarks>
 public class UtilityIO
 {

  /// <summary>Constructor.</summary>
  public UtilityIO()
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
  }//static void Main( String[] argv ) 

  ///<summary>Stub.</summary>
  public static void Stub()
  {
   System.Console.WriteLine( PathDirectoryName() );   
  }//public static void Stub()

  ///<summary>DriveInformation()</summary>
  public static void DriveInformation()
  {
   foreach (System.IO.DriveInfo drive in System.IO.DriveInfo.GetDrives())
   {
    System.Console.WriteLine("{0} | {1}", drive.Name, drive.DriveType);
   }
  }//public static void DriveInformation()

	/// <summary>
	/// Encodes the file in a Base64 string format.
	/// </summary>
	/// <param name="file">Name of the file.</param>
	/// <returns></returns>
	public string EncodeFile(HtmlInputFile file)
	{
      try
      {
        if (file.PostedFile.ContentLength > 0)
        {
            byte[] fsBytes = new byte[file.PostedFile.ContentLength];
            System.IO.Stream fileStream = file.PostedFile.InputStream;
            fileStream.Read(fsBytes, 0, file.PostedFile.ContentLength);
            return Convert.ToBase64String(fsBytes);
        }
        else
		{
			return String.Empty;
		}	
      }
      catch (Exception )
      {
        //Handle the Error
		return String.Empty;
      }
	}


  /// <summary>PathDirectoryName</summary>
  /// <remarks>System.Console.WriteLine( PathDirectoryName() );</remarks>
  public static string PathDirectoryName()
  {
   return ( System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) );
  }

  /// <summary>StreamWriterStringCollection</summary>
  public static void StreamWriterStringCollection
  (
   ref StreamWriter      streamWriter,
   ref StringCollection  stringCollection,
   ref StringBuilder     stringBuilder,
   ref string            exceptionMessage
  )
  {
   HttpContext       httpContext   =  HttpContext.Current;
   try
   {
    stringBuilder     =  new StringBuilder();
    foreach( string stringCollectionCurrent in stringCollection )
    {
     stringBuilder.AppendLine( stringCollectionCurrent );
    }//foreach( string stringCollectionCurrent in stringCollection )
    #if ( DEBUG )
     System.Console.WriteLine
     (
      "{0}",
      stringBuilder
     );
    #endif
    streamWriter.WriteLine( stringBuilder );
   }//try
   catch ( System.ObjectDisposedException exception )
   {
    exceptionMessage = "System.ObjectDisposedException: " + exception.Message;
   }//catch ( System.ObjectDisposedException exception )
   catch ( System.IO.IOException exception )
   {
    exceptionMessage = "System.IOException: " + exception.Message;
   }//catch ( System.IO.IOException exception )
   catch ( System.Exception exception )
   {
    exceptionMessage = "System.Exception: " + exception.Message;
   }//catch ( System.Exception exception )
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
  }//public static void StreamWriterStringCollection()
  
  static UtilityIO()
  {

  }//static UtilityIO()
  
 }//public class UtilityIO
 
}//namespace WordEngineering