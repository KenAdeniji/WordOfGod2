using System;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Text;

namespace WordEngineering
{
 ///<summary>UtilityImpersonate.</summary>
 ///<remarks>
 /// Support.Microsoft.com/default.aspx?scid=kb;EN-US;306158#4 Microsoft 306158 - Info Implementing Impersonation in an ASP.NET Application
 /// Local Security Setting %SystemRoot%\system32\secpol.msc /s Grant the "Act as part of the operating system" privilege to the ASPNET account.
 /// Create the impersonate element with the following sub elements, userName, password, domainName.
 ///</remarks>
 public class UtilityImpersonate
 {

  ///<summary>LOGON32_LOGON_INTERACTIVE</summary>
  public const  int    LOGON32_LOGON_INTERACTIVE      =  2;

  ///<summary>LOGON32_PROVIDER_DEFAULT</summary>
  public const  int    LOGON32_PROVIDER_DEFAULT       =  0;

  /// <summary>ImpersonateDomainName</summary>
  public static String ImpersonateDomainName          =  null;

  /// <summary>ImpersonatePassword</summary>
  public static string ImpersonatePassword            =  null;
   
  /// <summary>ImpersonateUserName</summary>
  public static String ImpersonateUserName            =  null;

  /// <summary>The configuration XML filename.</summary>
  public static string FilenameConfigurationXml       =  @"WordEngineering.config";

  ///<summary>The XPath ImpersonateDomain.</summary>
  public const  String XPathImpersonateDomainName     =  @"/word/impersonate/domainName";

  ///<summary>The XPath ImpersonateUsername.</summary>
  public const  String XPathImpersonateUsername       =  @"/word/impersonate/username";  

  ///<summary>The XPath ImpersonatePassword.</summary>
  public const  String XPathImpersonatePassword       =  @"/word/impersonate/password";

  ///<summary>advapi32.dll DuplicateToken</summary>
  [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
  public static extern int DuplicateToken
  (
        IntPtr  hToken, 
	    int     impersonationLevel,  
   ref  IntPtr  hNewToken
  );

  ///<summary>advapi32.dll LogonUserA</summary>
  [DllImport("advapi32.dll")]
  public static extern int LogonUserA
  (
        String  lpszUserName, 
        String  lpszDomain,
        String  lpszPassword,
        int     dwLogonType, 
        int     dwLogonProvider,
   ref  IntPtr  phToken
  );
           
  ///<summary>advapi32.dll RevertToSelf</summary>
  [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
  public static extern bool RevertToSelf();

  ///<summary>kernel32.dll CloseHandle</summary>
  [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
  public static extern  bool CloseHandle
  (
       IntPtr handle
  );
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   bool                         impersonateValidUser;
   String                       exceptionMessage               =  null;
   
   WindowsImpersonationContext  windowsImpersonationContext    =  null;
     
   impersonateValidUser  =  ImpersonateValidUser
   (
    ref ImpersonateDomainName,
    ref ImpersonatePassword,
    ref ImpersonateUserName,
    ref windowsImpersonationContext,
    ref exceptionMessage
   );
   UndoImpersonation
   (
    ref windowsImpersonationContext,
    ref exceptionMessage
   );

  }//public static void Main( String[] argv )

  ///<summary>ImpersonateValidUser</summary>
  public static bool ImpersonateValidUser
  (
   ref  String                       domainName, 
   ref  String                       password,
   ref  String                       userName, 
   ref  WindowsImpersonationContext  windowsImpersonationContext,
   ref  String                       exceptionMessage
  )
  {
   HttpContext      httpContext                      =  HttpContext.Current;
   bool             impersonateValidUser             =  false;
   bool             revertToSelf                     =  false;
   int              duplicateToken                   =  -1;
   int              logonUserA                       =  -1;
   IntPtr           token                            =  IntPtr.Zero;
   IntPtr           tokenDuplicate                   =  IntPtr.Zero;
   WindowsIdentity  windowsIdentity                  =  null;
   
   if ( string.IsNullOrEmpty( userName ) )
   {
    userName = Environment.UserName; 
   } 

   if ( string.IsNullOrEmpty( domainName ) )
   {
   	if ( httpContext == null )
    {
     if ( userName.IndexOf('\\') > -1 ) { domainName = Environment.UserDomainName; }
     else                              { domainName = Environment.MachineName; }
    }
    else
    {     	
     domainName = Environment.MachineName;
    } 
   }

   #if (DEBUG)
   System.Console.WriteLine
   (
    "DomainName: {0} | Password: {1} | UserName: {2}",
    domainName,
    password,
    userName
   );
   #endif

   try
   {
    revertToSelf = RevertToSelf();
    if ( revertToSelf == true )
    {
     logonUserA = LogonUserA
     (
           userName, 
           domainName, 
           password, 
           LOGON32_LOGON_INTERACTIVE, 
           LOGON32_PROVIDER_DEFAULT, 
      ref  token
     );
     if ( logonUserA != 0 )
     {
      duplicateToken = DuplicateToken
      (
           token,
           2, 
       ref tokenDuplicate
      );

      if ( duplicateToken != 0 )
      {
       windowsIdentity = new WindowsIdentity
       (
        tokenDuplicate
       );

       windowsImpersonationContext = windowsIdentity.Impersonate();

       if ( windowsImpersonationContext != null )
       {
        impersonateValidUser = true;
        CloseHandle(token);
        CloseHandle(tokenDuplicate);
       }//if ( windowsImpersonationContext != null )
      }//if ( duplicateToken == 0 )
     }//if ( logonUserA != 0 )
    }//if ( revertToSelf == true )   

    if ( impersonateValidUser == false )
    {
     if ( token!= IntPtr.Zero )
     {             
      CloseHandle( token );
     }
     if ( tokenDuplicate!=IntPtr.Zero )
     {      
      CloseHandle( tokenDuplicate );
     }
    }//if ( impersonateValidUser )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   #if (DEBUG)
   System.Console.WriteLine
   (
    "revertToSelf: {0} | logonUserA: {1} | duplicateToken: {2} | impersonateValidUser: {3}",
    revertToSelf,
    logonUserA,
    duplicateToken,
    impersonateValidUser
   );
   #endif
   #if (DEBUG)
    if ( impersonateValidUser )
    {
     System.Console.WriteLine
     (
      "windowsIdentity AuthenticationType: {0} | IsAnonymous: {1} | IsAuthenticated: {2} | IsGuest: {3} | IsSystem: {4} | Name {5}",
      windowsIdentity.AuthenticationType,
      windowsIdentity.IsAnonymous,
      windowsIdentity.IsAuthenticated,
      windowsIdentity.IsGuest,
      windowsIdentity.IsSystem,    
      windowsIdentity.Name
     );
    }
   #endif
   return ( impersonateValidUser );
  }//private void ImpersonateValidUser()

  ///<summary>GetUsernamePasswordDomainName</summary>
  ///<remarks>Get the userName, password, and domain name.</remarks>  
  public static void GetUsernamePasswordDomainName
  (
   ref  String  filenameConfigurationXml,
   ref  String  domainName,
   ref  String  password,
   ref  String  userName,
   ref  String  exceptionMessage
  )
  {
   if ( string.IsNullOrEmpty( filenameConfigurationXml ) )
   {
   	filenameConfigurationXml = FilenameConfigurationXml;
   }   		
   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,         
        XPathImpersonateDomainName,
    ref domainName
   );
   if ( exceptionMessage != null )
   {
   	return;
   }	
   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,         
        XPathImpersonatePassword,
    ref password
   );
   if ( exceptionMessage != null )
   {
   	return;
   }	
   UtilityXml.GetNodeValue
   (
        filenameConfigurationXml,
    ref exceptionMessage,         
        XPathImpersonateUsername,
    ref userName
   );
  }//public static void GetUserNamePasswordDomainName( ref String userName, ref String password, ref String domainName, ref String exceptionMessage )

  ///<summary>UndoImpersonation</summary>  
  ///<param name="windowsImpersonationContext">windowsImpersonationContext</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void UndoImpersonation
  (
   ref  WindowsImpersonationContext  windowsImpersonationContext,
   ref  String                       exceptionMessage
  )
  {
   try
   {
    windowsImpersonationContext.Undo();
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = exception.Message;
   }//catch ( Exception exception )
  }//private void UndoImpersonation()
 
  static UtilityImpersonate()
  {
   String  exceptionMessage  =  null;
   GetUsernamePasswordDomainName
   (
    ref FilenameConfigurationXml,
    ref ImpersonateDomainName,
    ref ImpersonatePassword,
    ref ImpersonateUserName,
    ref exceptionMessage
   );
  }//static UtilityImpersonate()
 }//public class UtilityImpersonate
 
}//namespace WordEngineering