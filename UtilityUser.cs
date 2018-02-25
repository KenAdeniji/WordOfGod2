using System;
using System.Collections;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityUser</summary>
 ///<remarks>
 /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnnetsec/html/SecNetAP05.asp  Building Secure ASP.NET Applications: Authentication, Authorization, and Secure Communication
 ///</remarks>
 public class UtilityUser
 {
  /// <summary>Main()</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Stub();
  }//public static void Main

  /// <summary>Stub()</summary>
  public static void Stub()
  {
   Hashtable userLogin   =  null;
   string    exceptionMessage  =  null;
   
   UserLogin
   (
     ref userLogin,
     ref exceptionMessage
   );
   
  }//public static void Stub()

  /// <summary>UserLogin()</summary>
  public static void UserLogin
  (
   ref Hashtable userLogin,
   ref string    exceptionMessage
  )
  {
   HttpContext  httpContext  =  HttpContext.Current;
   exceptionMessage          =  null;
   userLogin           =  new Hashtable();

   try
   {
    userLogin.Add( "System.Environment.UserName", System.Environment.UserName );
    userLogin.Add( "System.Security.Principal.WindowsIdentity.GetCurrent().Name", System.Security.Principal.WindowsIdentity.GetCurrent().Name );
    if ( httpContext != null )
    {
     userLogin.Add( "HttpContext.Current.User.Identity.Name", HttpContext.Current.User.Identity.Name );
     userLogin.Add( "HttpContext.Current.Request.LogonUserIdentity", HttpContext.Current.Request.LogonUserIdentity.Name );
    } 
    userLogin.Add( "WindowsIdentity.GetCurrent().Name", WindowsIdentity.GetCurrent().Name );
    userLogin.Add( "Thread.CurrentPrincipal.Identity.Name", Thread.CurrentPrincipal.Identity.Name );
   }//try
   catch ( SecurityException exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   if ( httpContext == null ) { UtilityCollection.PrintKeysAndValues( userLogin ); }
  }//public static void UserLogin()
 }//public class UtilityUser
}//namespace WordEngineering