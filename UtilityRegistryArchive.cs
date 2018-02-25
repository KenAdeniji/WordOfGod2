using  Microsoft.Win32;
using  System;

namespace WordEngineering
{
 /// <summary>UtilityRegistry.</summary>
 public class UtilityRegistry
 {
  ///<summary>ADSIProvider</summary>
  public const string ADSIProvider = @"SOFTWARE\Microsoft\ADs\Providers";

  /// <summary>The delimiter in character array format for the split string.</summary>
  public static char[]  DelimiterSplitChar       = null;

  ///<summary>The delimiter split string.</summary>
  public const  string      DelimiterSplitString = " ";

  ///<summary>EntryNameNameServer</summary>
  public static String      EntryNameNameServer = @"NameServer";

  ///<summary>SYSTEM_CurrentControlSet_Services_Tcpip_Parameters</summary>
  public static String      SubKeySYSTEM_CurrentControlSet_Services_Tcpip_Parameters = @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters";
  
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Stub();
  }//public static void Main()

  /// <summary>Stub()</summary>
  public static void Stub()
  {
   string exceptionMessage = null;   
   GetListOfDirectoryProviders(null, out exceptionMessage);
   /*
   Service();
   Software();
   */
  }//public static void Stub()

  ///<summary>DNSServerList</summary>
  public static void DNSServerList()
  {
   String      DNSServerListValue      = null;
   String[]    DNSServerListCollection = null; 
 
   RegistryKey RegistryKeyDNSServer    = null;
   RegistryKey RegistryKeyLocalMachine = null;

   RegistryKeyLocalMachine = Registry.LocalMachine;
   RegistryKeyDNSServer    = RegistryKeyLocalMachine.OpenSubKey( SubKeySYSTEM_CurrentControlSet_Services_Tcpip_Parameters );
  	
   if ( RegistryKeyDNSServer == null )
   {
     System.Console.WriteLine("Unable to open DNS servers key");
     return;
   }
   
   System.Console.WriteLine( RegistryKeyDNSServer );
  
   DNSServerListValue = ( String ) RegistryKeyDNSServer.GetValue(EntryNameNameServer);
   System.Console.WriteLine("DNS Server: {0}", DNSServerListValue);
   
   RegistryKeyDNSServer.Close();
   RegistryKeyLocalMachine.Close();
   
   DNSServerListCollection = DNSServerListValue.Split( DelimiterSplitChar );
   
   foreach( String DNSServerListCollectionCurrent in DNSServerListCollection )
   {
    System.Console.WriteLine("DNS Server: {0}", DNSServerListCollectionCurrent );    	
   }
  }//public static void DNSServerList

  ///<summary>GetListOfDirectoryProviders</summary>
  ///<remarks>
  /// enterprise-minds.com Klaus Salchner
  /// string[] provider = GetListOfDirectoryProviders("localhost", out exceptionMessage);
  ///</remarks>
  public static string[] GetListOfDirectoryProviders
  (
   string computer,
   out string exceptionMessage
  )
  {
   string[] provider = null;
   exceptionMessage = null;
   if (computer == null) {computer = Environment.MachineName;}
   try
   {
    provider = RegistryKeyGetValues(computer, ADSIProvider, out exceptionMessage);
    for (int count = 0; count < provider.Length; ++count)
    {
     provider[count] = provider[count] + "://" + computer;
     System.Console.WriteLine( provider[count] );
    }
   }
   catch( Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   if ( exceptionMessage != null ) { System.Console.WriteLine(exceptionMessage); }
   // return the list of providers
   return (provider);
  }

  /// <summary>RegistryKeyGetValue</summary>
  /// <remarks>
  ///  Blogs.msdn.com/brada/archive/2003/12/10/50944.aspx
  ///   "System\CurrentControlSet\Control\Session Manager\Environment" 
  ///   windir
  /// </remarks> 
  public static string RegistryKeyGetValue
  (
   string computer,
   string subkey,
   string name,
   out string exceptionMessage
  )
  {
   RegistryKey registryKey;
   string value = null;
   exceptionMessage = null;
   if ( computer == null ) { computer = ""; }
   try
   {
    registryKey = RegistryKey.OpenRemoteBaseKey( RegistryHive.LocalMachine, computer ).OpenSubKey
    (
     subkey
    );
    if ( registryKey != null )
    {
     value = (string) registryKey.GetValue(name);
    }
   }
   catch (Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   return ( value );
  }

  ///<summary>RegistryKeyGetValues</summary>
  ///<remarks>
  /// enterprise-minds.com Klaus Salchner
  /// string[] value = RegistryKeyGetValues("localhost", subkey, out exceptionMessage);
  ///</remarks>
  public static string[] RegistryKeyGetValues
  (
   string computer,
   string subkey,
   out string exceptionMessage
  )
  {
   string[] value = null;

   exceptionMessage = null;
   if (computer == null) {computer = "";}
   try
   {
    value = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine,computer).OpenSubKey(subkey).GetSubKeyNames();
   }
   catch( Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   if ( exceptionMessage != null ) { System.Console.WriteLine(exceptionMessage); }
   return (value);
  }
  
  ///<summary>Service</summary>
  ///<remarks>
  /// Subject: Browsing host services  8/10/2005 7:47 AM PST By: Ignacio Machin ( .NET/ C# MVP ) In: microsoft.public.dotnet.languages.csharp
  /// ServiceController.GetServices
  ///</remarks>
  public static void Service()
  {
   string    exceptionMessage  =  null;
   string[]  services          =  null;
   
   object    imagePath         =  null;
   object    displayName       =  null;
   
   try
   {
    services  =  Microsoft.Win32.Registry.LocalMachine.OpenSubKey( @"System\CurrentControlSet\Services").GetSubKeyNames();
    foreach ( string service in services )
    {
     imagePath    =  Microsoft.Win32.Registry.LocalMachine.OpenSubKey( @"System\CurrentControlSet\Services\" + service ).GetValue( "ImagePath");
     if ( imagePath == null ) { continue; }
     displayName  =  Microsoft.Win32.Registry.LocalMachine.OpenSubKey( @"System\CurrentControlSet\Services\" + service ).GetValue( "DisplayName");
     System.Console.WriteLine
     ( 
      "Service Name: {0} | ImagePath: {1} | DisplayName: {2}",
      service,
      imagePath,
      displayName
     ); 
    }//foreach ( string service in services )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, "Exception", ref exceptionMessage ); }
   finally
   {
   }//finally   	
  }//Service()

  ///<summary>Software</summary>
  ///<remarks>
  /// Subject: Getting a list of installed programs on windows using C# 9/7/2005 6:35 AM PST By: Phil Williams In: microsoft.public.dotnet.languages.csharp 
  ///</remarks>
  public static void Software()
  {
   string                               displayName       =  null;
   string                               displayVersion    =  null;
   string                               fileDescription   =  null;
   string                               fileName          =  null;
   string                               exceptionMessage  =  null;
   string[]                             subKeyNames       =  null;

   Microsoft.Win32.RegistryKey          registryKey       =  null;
   Microsoft.Win32.RegistryKey          registrySubKey    =  null;
   Microsoft.Win32.RegistryKey          registrySubKey2   =  null;

   System.Diagnostics.FileVersionInfo   fileVersionInfo;

   try
   {
    registryKey       =  Microsoft.Win32.Registry.LocalMachine;
    registrySubKey    =  registryKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
    subKeyNames       =  registrySubKey.GetSubKeyNames();
    foreach ( string subKeyName in subKeyNames )
    {
     registrySubKey2  =  registrySubKey.OpenSubKey( subKeyName );
     /*
     if 
     ( 
      ValueNameExists( registrySubKey2.GetValueNames(), "DisplayName") && 
      ValueNameExists( registrySubKey2.GetValueNames(), "DisplayVersion")
     )
     */
     if 
     ( 
      registrySubKey2.GetValue("DisplayName") != null    && 
      registrySubKey2.GetValue("DisplayVersion") != null
     )
     {
      displayName      =  registrySubKey2.GetValue("DisplayName").ToString();
      displayVersion   =  registrySubKey2.GetValue("DisplayVersion").ToString();
      fileName         =  displayName;
      /*
      fileVersionInfo  =  System.Diagnostics.FileVersionInfo.GetVersionInfo( fileName );
      fileDescription  =  fileVersionInfo.FileDescription;
      */
      System.Console.WriteLine( "Software Name: {0} | Version: {1}", displayName, displayVersion );
     }//if ( ValueNameExists( registrySubKey2.GetValueNames(), "DisplayName") && ValueNameExists( registrySubKey2.GetValueNames(), "DisplayVersion" ) )
     registrySubKey2.Close();
    }//foreach ( string subKeyName in subKeyNames )
    registrySubKey.Close();
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, "Exception", ref exceptionMessage ); }
   finally
   {
    if ( registrySubKey != null )
    {
     registrySubKey.Close();
    }
    if ( registrySubKey2 != null )
    {
     registrySubKey2.Close();
    }
   }//finally   	
  }//Software()
  
  static UtilityRegistry()
  {
   DelimiterSplitChar = DelimiterSplitString.ToCharArray();  	
  }//static()

 }//public class UtilityRegistry
}//namespace WordEngineering

