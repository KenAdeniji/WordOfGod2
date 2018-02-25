using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityActiveDirectory</summary>
 ///<remarks>
 /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemenumclasstopic.asp
 /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/vbcon/html/vbtsksearchingactivedirectoryhierarchy.asp
 /// enterprise-minds.com Klaus Salchner
 /// For a list of all WinNT ADSI objects, methods, properties, required properties and schema see following URLs
 ///
 /// http://msdn.microsoft.com/library/en-us/adsi/adsi/adsi_objects_of_winnt.asp?frame=true
 /// http://msdn.microsoft.com/library/en-us/adsi/adsi/winnt_object_class_hierarchy.asp?frame=true
 /// http://msdn.microsoft.com/library/en-us/adsi/adsi/winnt_schemaampaposs_mandatory_and_optional_properties.asp?frame=true
 ///
 /// For a list of all IIS ADSI objects, methods, properties, etc. see following URL
 ///
 /// http://msdn.microsoft.com/library/en-us/iissdk/iis/ref_prog_iaoref.asp?frame=true
 /// Provider
 /// IIS://LocalHost
 /// LDAP://LocalHost
 /// NDS://LocalHost
 /// NWCOMPAT://LocalHost
 /// WinNT://LocalHost
 ///</remarks>
 public class UtilityActiveDirectory
 {
  ///<summary>ADSIProvider</summary>
  public const string ADSIProvider = @"SOFTWARE\Microsoft\ADs\Providers";

  ///<summary>FormatPath</summary>
  public const string FormatPath = "LDAP://{0}.com"; /* LDAP://DomainName.GenericTop-LevelDomains */

  ///<summary>FormatPropertyName</summary>
  public const string FormatPropertyName = "{0}";

  ///<summary>FormatResultProperty</summary>
  public const string FormatResultProperty = "    {0}<br/>";

  ///<summary>FormatSearchResultPath</summary>
  public const string FormatSearchResultPath = "Path: {0}<br/>";

  ///<summary>FullNameSchemaName</summary>
  public const string FullNameSchemaName = "FullName";

  ///<summary>DirectoryListServicesAuthenticationTypes</summary>
  public static List<string> ListDirectoryServicesAuthenticationTypes;

  ///<summary>SetPasswordMethodName</summary>
  public const string SetPasswordMethodName = "SetPassword";

  ///<summary>TypeDirectoryServicesAuthentication</summary>
  public static readonly Type TypeDirectoryServicesAuthentication = typeof(System.DirectoryServices.AuthenticationTypes);

  ///<summary>UserSchemaClassName</summary>
  public const string UserSchemaClassName = "User";

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   string exceptionMessage;
   string filter = null;
   string path = String.Format(FormatPath, Environment.UserDomainName);
   StringBuilder sb;
   if ( argv.Length > 0 ) { path = argv[0]; }
   if ( argv.Length > 1 ) { filter = argv[1]; }
   /*
   DirectoryEntrySearcher( path, "guest", out sb, out exceptionMessage );
   DirectoryEntrySearcher( path, null, out sb, out exceptionMessage );
   string[] provider = GetListOfDirectoryProviders(null, out exceptionMessage);
   */
   //AddUser("LDAP://EphraimTech.com","John Doe","John","J0hnD0e77aa6#@",out exceptionMessage);
   /*
   AddUser("WinNT://LocalHost","John Doe","JohnDoe","J0hnD0e",out exceptionMessage);
   DeleteUser("WinNT://LocalHost", "JohnDoe", out exceptionMessage);
   */
   DeleteUser("LDAP://EphraimTech.com", "JohnDoe", out exceptionMessage);
  }

  ///<summary>AddDirectoryObject</summary>
  public static void AddDirectoryObject
  (
   string path,
   string objectName,
   string objectSchemaClassName,
   object[,] property,
   object[,] method,
   out string exceptionMessage
  )
  {
   DirectoryEntry directoryEntryAdd = null;
   DirectoryEntry directoryEntryPath = null;
   exceptionMessage = null;
   try
   {
    directoryEntryPath = new DirectoryEntry( path );
    #if (DEBUG)
     System.Console.WriteLine("path: {0}", path);
    #endif
    // creates the new Adsi object
    directoryEntryAdd = directoryEntryPath.Children.Add(objectName, objectSchemaClassName);
    // now loop through all the properties and set them
    if ( property != null )
    {
     for ( int index = 0; index < property.GetLength(0); ++index )
     {
      directoryEntryAdd.Properties[Convert.ToString(property[index,0])].Value = property[index,1];
      #if (DEBUG)
       System.Console.WriteLine
       ( 
        "index: {0} | Convert.ToString(property[{0},0]): {1} | property[{0},1]: {2}",
        index,
        Convert.ToString(property[index,0]),
        property[index,1] 
       );
      #endif
     }
    }
    if ( method != null )
    {
     for ( int index = 0; index < method.GetLength(0); ++index )
     {
      directoryEntryAdd.Invoke(Convert.ToString(method[index,0]), method[index,1]);
      #if (DEBUG)
       System.Console.WriteLine
       ( 
        "index: {0} | Convert.ToString(method[{0},0]): {1} | method[{0},1]: {2}",
        index,
        Convert.ToString(method[index,0]),
        method[index,1]
       );
      #endif
     }
    }
    //commit the changes
    directoryEntryAdd.CommitChanges();
   }
   catch( Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   finally
   {
    if ( directoryEntryAdd != null ) { directoryEntryAdd.Close(); }
    if ( directoryEntryPath != null ) { directoryEntryPath.Close(); }
   }
   if ( exceptionMessage != null ) { System.Console.WriteLine( exceptionMessage ); }
  }

  ///<summary>AddUser</summary>
  ///<remarks>AddUser("WinNT://LocalHost","JohnDoe","John","J0hnD0e",out exceptionMessage);</remarks>
  public static void AddUser
  (
   string path,
   string fullname,
   string username,
   string password,
   out string exceptionMessage
  )
  {
   object[,] property = new object[,] { { FullNameSchemaName, fullname } };
   object[,] method = new object[,] { { SetPasswordMethodName, password } };
   AddDirectoryObject( path, username, UserSchemaClassName, property, method, out exceptionMessage );
  }

  ///<summary>DeleteDirectoryObject</summary>
  public static void DeleteDirectoryObject
  (
   string path,
   string objectName,
   string objectSchemaClassName,
   out string exceptionMessage
  )
  {
   DirectoryEntry directoryEntryPath = null;
   DirectoryEntry directoryEntryDelete = null;
   exceptionMessage = null;
   System.Console.WriteLine
   (
    "path: {0} | objectName: {1} | objectSchemaClassName: {2}",
    path,
    objectName,
    objectSchemaClassName
   );
   try
   {
    directoryEntryPath = new DirectoryEntry(path);
    if ( objectSchemaClassName != null )
    {
     directoryEntryDelete = directoryEntryPath.Children.Find(objectName, objectSchemaClassName);
    }
    else
    {
     directoryEntryDelete = directoryEntryPath.Children.Find(objectName);
    }
    if ( directoryEntryDelete != null )
    {
     directoryEntryDelete.Children.Remove(directoryEntryDelete);
    }
   }
   catch( Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   finally
   {
    if ( directoryEntryDelete != null ) { directoryEntryDelete.Close(); }
    if ( directoryEntryPath != null ) { directoryEntryPath.Close(); }
   }
   if ( exceptionMessage != null ) { System.Console.WriteLine( exceptionMessage ); }
  }

  ///<summary>DeleteUser</summary>
  public static void DeleteUser(string path, string username, out string exceptionMessage)
  {
   DeleteDirectoryObject(path, username, UserSchemaClassName, out exceptionMessage);
  }

  ///<summary>DirectoryEntrySearcher</summary>
  ///<remarks>
  /// DirectoryEntrySearcher( ActiveDirectoryPath, "Comfort" );
  /// DirectoryEntrySearcher( ActiveDirectoryPath, "guest" );
  ///</remarks>
  public static SearchResultCollection DirectoryEntrySearcher
  (
   string path,
   string filter,
   out StringBuilder sb,
   out String exceptionMessage
  )
  {
   SearchResultCollection searchResultCollection;
   searchResultCollection = DirectoryEntrySearcher
   (
    path,
    null, //username
    null, //password
    filter,
    out sb,
    out exceptionMessage
   );
   return( searchResultCollection );
  }

  ///<summary>DirectoryEntrySearcher</summary>
  ///<remarks>
  /// DirectoryEntrySearcher( "LDAP://localhost", "host" );
  /// DirectoryEntrySearcher( "LDAP://localhost", "Guest" );
  /// DirectoryEntrySearcher( "IIS://localhost", null );
  /// DirectoryEntrySearcher( "IIS://localhost/W3SVC", null );
  /// DirectoryEntrySearcher( "WinNT://localhost", null );
  ///</remarks>
  public static SearchResultCollection DirectoryEntrySearcher
  (
       string path,
       string username,
       string password, 
       string filter,
   out StringBuilder sb,
   out String exceptionMessage
  )
  {
   DirectoryEntry directoryEntry = null;
   DirectorySearcher directorySearcher = null;
   ResultPropertyCollection resultPropertyCollection;
   SearchResultCollection searchResultCollection = null;
   sb = null;
   exceptionMessage = null;
   try
   {
    directoryEntry = new DirectoryEntry(path);
    if ( string.IsNullOrEmpty(username) == false )
    {
     directoryEntry.Password = password;
     directoryEntry.Username = username;
    }
    directorySearcher = new DirectorySearcher( directoryEntry );
    if ( string.IsNullOrEmpty(filter) == false )
    {
     directorySearcher.Filter = (String.Format("(anr={0})", filter));
    }
    searchResultCollection = directorySearcher.FindAll();
    sb = new StringBuilder();
    foreach( SearchResult searchResult in searchResultCollection ) 
    {
     System.Console.WriteLine("Path: {0}", searchResult.GetDirectoryEntry().Path);
     sb.AppendFormat( FormatSearchResultPath, searchResult.GetDirectoryEntry().Path );
     resultPropertyCollection = searchResult.Properties;
     foreach( string propertyName in resultPropertyCollection.PropertyNames )
     {
      System.Console.WriteLine("Property Name: {0}", propertyName);
      sb.AppendFormat( FormatPropertyName, propertyName );
      foreach( Object obj in resultPropertyCollection[propertyName] )
      {
       System.Console.WriteLine("\t {0}",obj);
       sb.AppendFormat( FormatResultProperty, obj );
      }
     }
    }
   }
   catch( Exception ex ) { exceptionMessage = ex.Message; }
   finally 
   {
    if (directorySearcher != null) {directorySearcher.Dispose();};
    if (directoryEntry != null) {directoryEntry.Close();}
   }
   return( searchResultCollection );
  }

  ///<summary>DirectoryEntrySearcherUser</summary>
  public static SearchResultCollection DirectoryEntrySearcherUser()
  {
	Domain domain = Domain.GetCurrentDomain();
	string domainName = domain.Name;
	SearchResultCollection searchResultCollection = null;
	using(	DirectoryEntry root = new DirectoryEntry("LDAP://" + domainName)	)
	{
		using(	DirectorySearcher directorySearcher = new DirectorySearcher(root, "CN=Users"))
		{
			searchResultCollection = directorySearcher.FindAll();
		}	
	}
	return searchResultCollection;
  }
  
  ///<summary>DirectoryEntryPropertySet</summary>
  public static void DirectoryEntryPropertySet
  (
   string path,
   string username,
   string password,
   out string exceptionMessage
  )
  {
   System.DirectoryServices.DirectoryEntry  directoryEntry;
   exceptionMessage = null;
   try
   {
    directoryEntry = new System.DirectoryServices.DirectoryEntry(path);
    if ( string.IsNullOrEmpty(username) == false )
    {
     directoryEntry.Password = password;
     directoryEntry.Username = username;
    }
    System.Console.WriteLine(directoryEntry.Properties["homenumber"].Count);
    //directoryEntry.Properties["homenumber"][0]= "(425) 555-1212";
    directoryEntry.CommitChanges();
   }
   catch ( Exception ex )
   {
    exceptionMessage = ex.Message;
   }
   if (exceptionMessage != null ) { System.Console.WriteLine(exceptionMessage); }
  }

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
   string[] subKey;

   exceptionMessage = null;
   if (computer == null) {computer = Environment.MachineName;}
   try
   {
    subKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine,computer).OpenSubKey(ADSIProvider).GetSubKeyNames();

    // create the string array which will hold the provider list
    provider = new string[subKey.Length];
      
    // now add all providers to the array; all providers are 
    // pointed to the local machine
    for (int count = 0; count < subKey.Length; ++count)
    {
     provider[count] = subKey[count] + "://" + computer;
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
 
  static UtilityActiveDirectory()
  {
   ListDirectoryServicesAuthenticationTypes = new List<string>();
   foreach( string obj in Enum.GetNames( TypeDirectoryServicesAuthentication ) )
   {
    ListDirectoryServicesAuthenticationTypes.Add(obj);
   }

   /*
   foreach( string listDirectoryServicesAuthenticationTypes in ListDirectoryServicesAuthenticationTypes )
   {
    System.Console.WriteLine( listDirectoryServicesAuthenticationTypes );
   }
   */

  }

 }
}