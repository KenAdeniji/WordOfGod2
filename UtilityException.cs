using System;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityException</summary>
 public class UtilityException
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
   
  }//public static main( string[] argv )

  ///<summary>ExceptionLog</summary>
  public static void ExceptionLog
  (
       Exception exception,
       string    exceptionClass,    
   ref string    exceptionMessage
  )
  {
   HttpContext       httpContext     =  HttpContext.Current;
   try
   {
   	if ( exceptionMessage == null )
   	{
   	 exceptionMessage = string.Format
   	 (
   	  "{0} Message: {1} | TargetSite: {2} | StackTrace: {3} | InnerException: {4} | Source: {5} | HelpLink: {6}",
      exceptionClass,
      exception.Message,
      exception.TargetSite,
      exception.StackTrace,
      exception.InnerException,
      exception.Source,
      exception.HelpLink
     );
    }//if ( exceptionMessage == null )
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );    	
    }
    UtilityEventLog.WriteEntry
    (
     exceptionMessage
    );
   }//try
   catch ( Exception e )
   {
    exceptionMessage = "Exception: " + e.Message;
   }//catch ( Exception exception )
  }//public static ExceptionLog()

  static UtilityException()
  {
  }//static UtilityException()

 }//public class UtilityException
}//namespace WordEngineering