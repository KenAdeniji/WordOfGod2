using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace WordEngineering
{
 /// <summary>UtilityTcpIp.</summary>
 /// <remarks>UtilityTcpIp.</remarks>
 public class UtilityTcpIp
 {

  /// <summary>NetworkStreamReadSize.</summary>
  public static int NetworkStreamDataSize = 256;

  /// <summary>ASCIIEncoding.</summary>
  public static System.Text.ASCIIEncoding ASCIIEncodingCurrent = null;

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {
  }//public static void Main

  /// <summary>TcpClient</summary>
  public static void TcpClient
  (
   ref String     serverName,
   ref int        port,
   ref TcpClient  tcpClient,
   ref String     exceptionMessage
  )
  {
   HttpContext     httpContext    =  HttpContext.Current;
   try
   {
    tcpClient = new TcpClient
    (
     serverName,
     port
    );
   }//
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }
   if ( httpContext == null )
   {
    System.Console.WriteLine(exceptionMessage);
   }
   else
   {
    httpContext.Response.Write( exceptionMessage ); 
   }
  }//public static void TcpClient

  /// <summary>Read</summary>
  public static void Read
  (
   ref NetworkStream  networkStream,
   ref StringBuilder  sbResponse,
   ref String         exceptionMessage
  )
  {

   Int32          byteLength      =  -1;
   byte[]         byteCurrent     =  null;
   
   String         streamData      =  null;

   HttpContext    httpContext     =  null;

   byteCurrent       =  new Byte[NetworkStreamDataSize];
   sbResponse        =  new StringBuilder();

   httpContext       =  HttpContext.Current;
   exceptionMessage  =  null;

   try
   {
    for ( ;; )
    {
     byteLength    =  networkStream.Read(byteCurrent, 0, byteCurrent.Length);
     
     if ( byteLength > 0 )
     {

      streamData    =  System.Text.Encoding.ASCII.GetString(byteCurrent, 0, byteLength);
      streamData    =  streamData.Trim();
      sbResponse.Append( streamData );

      if ( byteLength < byteCurrent.Length )
      { 
       break;
      }//if ( byteLength < byteCurrent.Length )

     }//if ( byteLength > 0 )

    }//for ( ;; )
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine(exceptionMessage);
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMessage ); 
    }//else
   }//if ( exceptionMessage != null )

   #if ( DEBUG )
    if ( httpContext == null )
    {
     System.Console.WriteLine("Response Data: {0}", sbResponse);
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write("Response Data: " + sbResponse); 
    }//else
   #endif
   
  }//public static void Read

  /// <summary>Write</summary>
  public static void Write
  (
   ref NetworkStream  networkStream,
   ref String         message,
   ref String         exceptionMessage
  )
  {

   byte[]         byteCurrent     =  null;
   
   HttpContext    httpContext     =  null;

   byteCurrent       =  new Byte[NetworkStreamDataSize];

   exceptionMessage  =  null;
   httpContext       =  HttpContext.Current;

   #if ( DEBUG )
    if ( httpContext == null )
    {
     System.Console.WriteLine("Message: {0}", message);
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write("Message: " + message); 
    }//else
   #endif

   try
   {
    //byteCurrent = System.Text.Encoding.ASCII.GetBytes( message );
    byteCurrent = ASCIIEncodingCurrent.GetBytes( message );

    /*
    #if ( DEBUG )
     if ( httpContext == null )
     {
      System.Console.WriteLine
      (
       "byteCurrent: {0} | Length: {1}", 
       byteCurrent,
       byteCurrent.Length
      );
     }//if ( httpContext == null )
     else
     {
      httpContext.Response.Write
      (
       "byteCurrent: " + byteCurrent + '|' + "Length: " + byteCurrent.Length
      ); 
     }//else
    #endif
    */

    networkStream.Write( byteCurrent, 0, byteCurrent.Length );

   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine(exceptionMessage);
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMessage ); 
    }//else
   }//if ( exceptionMessage != null )

  }//public static void Write

  ///<summary>Static.</summary>
  static UtilityTcpIp()
  {
   ASCIIEncodingCurrent = new System.Text.ASCIIEncoding();
  }//static UtilityTcpIp()

 }//public class UtilityTcpIp
}//namespace WordEngineering