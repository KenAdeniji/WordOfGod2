using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;

namespace WordEngineering
{
 ///<summary>UtilityResponse</summary>
 public class UtilityResponse
 {
  ///<summary>ByteSize</summary>
  public const int ByteSize = 1024;

  /// <summary>RenderImageQueryFormat</summary>
  public static String RenderImageQueryFormat = "SELECT {0}, {1}, {2} FROM {3} WHERE sequenceOrderId = {4}";

  ///<summary>RenderImage</summary>
  public static void RenderImage
  (
   ref string  databaseConnectionString,
   ref string  exceptionMessage,
   ref int     sequenceOrderId,
   ref string  dataSource,
   ref string  contentColumn,
   ref string  sourceColumn,
   ref string  typeColumn,
   ref byte[]  imageContent,
   ref string  imageSource,
   ref string  imageType
  )
  {
   HttpContext  httpContext  =  HttpContext.Current;
   string       sqlQuery     =  null; 
   IDataReader  iDataReader  =  null;
   try
   {
    if ( httpContext == null ) { return; }
    sqlQuery = string.Format
               ( 
                RenderImageQueryFormat, 
                contentColumn,
                sourceColumn,
                typeColumn,
                dataSource, 
                sequenceOrderId
	           ); 
    UtilityDatabase.DatabaseQuery
    ( 
         databaseConnectionString, 
     ref exceptionMessage, 
     ref iDataReader,
         sqlQuery,
         CommandType.Text
    );
    if ( exceptionMessage != null ) { return; }
    if ( iDataReader.Read() )
    {
     imageContent  =  ( byte[] ) iDataReader[contentColumn];
     imageSource   =  ( string ) iDataReader[sourceColumn];
     imageType     =  ( string ) iDataReader[typeColumn];
     UtilityResponse.ResponseOutputStreamWrite
     (
      ref imageContent,
      ref imageSource,
      ref imageType,
      ref exceptionMessage
     );
    }//if ( iDataReader.Read() )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   if ( iDataReader != null && iDataReader.IsClosed == false ) { iDataReader.Close(); }
  }
  
  ///<summary>ResponseOutputStreamWrite</summary>
  ///<remarks>Ben Reichelt DotNetJunkies.com/WebLog/barblog/archive/2005/01/02/40588.aspx Can't stream mp3 file in .net</remarks>
  public static void ResponseOutputStreamWrite
  (
   ref string path,
   ref string exceptionMessage
  )
  { 
   HttpContext           httpContext         =  System.Web.HttpContext.Current;
   byte[]                buffer              =  null;
   int                   bytesRead           =  -1;
   int                   fileExtensionIndex  =  -1;
   long                  fileLength          =  -1;
   string                filename            =  null;
   System.IO.FileInfo    fileInfo            =  null;
   System.IO.FileStream  fileStream          =  null;
   try
   {
    if ( string.IsNullOrEmpty( path ) )
    {
     path = @"D:\Audio\Christian\Junko\Junko_-_AsForMeAndMyHouse.mp3";    	
    }    	
    if ( File.Exists( path ) == false ) { return; }	
    fileInfo            =  new FileInfo( path );
    filename            =  System.IO.Path.GetFileName( path );
    fileExtensionIndex  =  UtilityFile.FileExtensionIndex( filename );
    fileLength          =  fileInfo.Length;
    if ( httpContext == null ) { return; }
    httpContext.Response.Buffer = true;
    httpContext.Response.Clear();
    httpContext.Response.ClearContent();
    httpContext.Response.ClearHeaders();
    //httpContext.Response.AddHeader( "Accept-Header", fileLength.ToString() );
    httpContext.Response.AppendHeader( "Content-Disposition", "inline; filename=" + httpContext.Server.UrlEncode( filename ) );
    //httpContext.Response.AddHeader( "Content-Disposition", "attachment;filename=" + filename );
    /*
    if ( fileLength > -1 )
    {
     httpContext.Response.AppendHeader( "Content-Length", fileLength.ToString() );
    }
    */ 
    //httpContext.Response.WriteFile( path );
    if ( fileExtensionIndex > - 1 )
    {
     httpContext.Response.ContentType = UtilityFile.FileExtension[fileExtensionIndex][UtilityFile.FileExtensionRankContentType];
    }
    else
    {
     httpContext.Response.ContentType = UtilityFile.DefaultContentType;
    }
    fileStream = new System.IO.FileStream
    ( 
     path, 
     System.IO.FileMode.Open, 
     System.IO.FileAccess.Read,
     System.IO.FileShare.ReadWrite
    );
    buffer = new byte[ByteSize];
    while ( true )
    {
     bytesRead = fileStream.Read( buffer, 0, ByteSize );
     if ( bytesRead <= 0 ) { break; }
     httpContext.Response.OutputStream.Write( buffer, 0, buffer.Length );
     httpContext.Response.Flush();
    }//while ( true )
    fileStream.Close();
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   finally
   {
    if ( fileStream != null ) { fileStream.Close(); }   	
   }   	
   httpContext.Response.End();
  }//public static void ResponseOutputStreamWrite() 

  ///<summary>ResponseOutputStreamWrite</summary>
  ///<remarks>MSDNBrasil.com.br/forum/ShowPost.aspx?PostID=91538</remarks>
  public static void ResponseOutputStreamWrite
  (
   ref System.Web.UI.WebControls.FileUpload  fileUpload,
   ref string                                exceptionMessage
  )
  {
   HttpContext  httpContext          =  System.Web.HttpContext.Current;
   byte[]       buffer               =  null;
   int          bytesRead            =  -1;
   int          fileExtensionIndex   =  -1;
   string       responseContentType  =  null;
   Stream       stream               =  null;
   try
   {
    if ( httpContext == null ) { return; }
    if ( fileUpload.HasFile == false ) { return; }
    responseContentType  =  httpContext.Response.ContentType;
    fileExtensionIndex   =  UtilityFile.FileExtensionIndex( fileUpload.PostedFile.FileName );
    stream               =  fileUpload.PostedFile.InputStream;
    httpContext.Response.Clear();
    httpContext.Response.AppendHeader( "Content-Length", fileUpload.PostedFile.ContentLength.ToString() );
    if ( fileExtensionIndex > - 1 )
    {
     httpContext.Response.ContentType = UtilityFile.FileExtension[fileExtensionIndex][UtilityFile.FileExtensionRankContentType];
    }
    else
    {
     httpContext.Response.ContentType = UtilityFile.DefaultContentType;
    }
    buffer = new byte[ByteSize];
    while ( true )
    {
     bytesRead = stream.Read( buffer, 0, ByteSize );
     if ( bytesRead <= 0 ) { break; }
     httpContext.Response.OutputStream.Write( buffer, 0, buffer.Length );
    }//while ( true )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   httpContext.Response.End();
   httpContext.Response.ContentType  =  responseContentType;
   /*
   httpContext.Server.Transfer( httpContext.Request.ServerVariables["PATH_INFO"] );
   httpContext.Response.Redirect( httpContext.Request.ServerVariables["PATH_INFO"] );
   */
   httpContext.Server.Transfer( httpContext.Request.ServerVariables["PATH_INFO"] );
  }//public static void ResponseOutputStreamWrite()  

  ///<summary>ResponseOutputStreamWrite</summary>
  public static void ResponseOutputStreamWrite
  (
   ref byte[]  bufferSource,
   ref string  path,
   ref string  contentType,
   ref string  exceptionMessage
  )
  {
   HttpContext   httpContext          =  System.Web.HttpContext.Current;
   byte[]        bufferBlock          =  null;
   int           bytesRead            =  -1;
   string        filename             =  null;
   string        responseContentType  =  null;
   MemoryStream  memoryStream         =  null;
   try
   {
    if ( httpContext == null ) { return; }
    responseContentType  =  httpContext.Response.ContentType;
    httpContext.Response.Clear();
    httpContext.Response.AppendHeader( "Content-Length", bufferSource.Length.ToString() );
    httpContext.Response.ContentType = contentType;
    filename = Path.GetFileName( path );
    httpContext.Response.AddHeader( "Content-Disposition", "attachment;filename=" + filename );
    memoryStream = new MemoryStream( bufferSource, 0, bufferSource.Length );
    bufferBlock  = new byte[ByteSize];
    while ( true )
    {
     bytesRead = memoryStream.Read( bufferBlock, 0, ByteSize );
     if ( bytesRead <= 0 ) { break; }
     httpContext.Response.OutputStream.Write( bufferBlock, 0, bufferBlock.Length );
    }//while ( true )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   httpContext.Response.End();
   httpContext.Response.ContentType  =  responseContentType;
   /*
   httpContext.Server.Transfer( httpContext.Request.ServerVariables["PATH_INFO"] );
   httpContext.Response.Redirect( httpContext.Request.ServerVariables["PATH_INFO"] );
   */
   httpContext.Server.Transfer( httpContext.Request.ServerVariables["PATH_INFO"] );
  }//public static void ResponseOutputStreamWrite()  

 }//public class UtilityResponse
}//namespace WordEngineering
