using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace WordEngineering
{

 /// <summary>UtilityWhoIsArgument</summary>
 public class UtilityWhoIsArgument
 {
  ///<summary>registryDomainSuffixOnly</summary>
  public bool     registryDomainSuffixOnly  =  UtilityWhoIs.RegistryDomainSuffixOnly;
  
  ///<summary>domainName</summary>
  public String[] domainName                =  null;

  ///<summary>port</summary>
  public int      port                      =  UtilityWhoIs.PortWhoIs;

  ///<summary>registry</summary>
  public String[] registry                  =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityWhoIsArgument():this
  (
   UtilityWhoIs.RegistryDomainSuffixOnly,
   UtilityWhoIs.PortWhoIs,
   null, //domainName
   null  //registry
  )
  {
  }//public UtilityWhoIsArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityWhoIsArgument
  (
   bool     registryDomainSuffixOnly,
   int      port,
   string[] domainName,
   string[] registry
  )
  {
   this.registryDomainSuffixOnly  =  registryDomainSuffixOnly;
   this.port                      =  port;
   this.domainName                =  domainName;
   this.registry                  =  registry;
  }//public UtilityWhoIsArgument()

 }//public class UtilityWhoIsArgument

 ///<summary>UtilityWhoIs</summary>
 ///<remarks>
 /// Written by: Christoph Wille Translated by: Bernhard Spuida http://www.aspheute.com/english/20000825.asp Displaying Event Log Entries the ASP.NET Way
 /// auDA.org.au AUSRegistry.com.au whois-check.ausregistry.net.au
 ///</remarks>
 public class UtilityWhoIs
 {
  /// <summary>RegistryDomainSuffixOnly</summary>
  public const bool RegistryDomainSuffixOnly = true;
  
  /// <summary>PortWhoIs</summary>
  public const int  PortWhoIs = 43;

  /// <summary>RankRegistryWhoIsName</summary>
  public const int  RankRegistryWhoIsName          = 0;

  /// <summary>PortWhoIs</summary>
  public static readonly string[][] RegistryWhoIs = new string[][]
                                                  {
                                                   new string[] { "whois.networksolutions.com" },
                                                   new string[] { "whois.ripe.net" },
                                                   new string[] { "whois.nic.at" },
                                                   new string[] { "whois.denic.de" },
                                                   new string[] { "whois.dns.be" },
                                                   new string[] { "whois.nic.gov" },
                                                   new string[] { "whois.nic.mil" },
                                                   new string[] { "whois-check.ausregistry.net.au" }, 
                                                  };
                                                    
  /// <summary>Main()</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Boolean               booleanParseCommandLineArguments      =  false;
   string                exceptionMessage                      =  null;     
   StringBuilder[][]     sb                                    =  null;
   UtilityWhoIsArgument  utilityWhoIsArgument                  =  null;
   
   utilityWhoIsArgument = new UtilityWhoIsArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityWhoIsArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityWhoIsArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   WhoisLookup
   (
    ref utilityWhoIsArgument,
    ref sb,
    ref exceptionMessage
   );
  }//public static void Main()

  ///<summary>WhoisLookup</summary>
  public static void WhoisLookup
  (
   ref UtilityWhoIsArgument utilityWhoIsArgument,
   ref StringBuilder[][]    sb,
   ref string               exceptionMessage
  )
  {
   byte[]        domainNameByte            =  null;
   bool          registryDomainSuffixOnly  =  false;
   int           domainIndex               =  -1;
   int           registryIndex             =  -1;
   string        domainName                =  null;
   string        domainNameNewLine         =  null;
   string[]      domainNameSplit           =  null;
   string        readLine                  =  null;
   string[]      registry                  =  null;
   string        registryCurrent           =  null;
   HttpContext   httpContext               =  HttpContext.Current;
   Stream        stream                    =  null;
   StreamReader  streamReader              =  null;
   TcpClient     tcpClient                 =  null;
   try
   {
   	if ( utilityWhoIsArgument.registry.Length > 0 )
   	{
     registry = utilityWhoIsArgument.registry;
    }
    else if ( utilityWhoIsArgument.registryDomainSuffixOnly == false )
    {
     UtilityString.ArrayCopy( RegistryWhoIs, ref registry, RankRegistryWhoIsName, ref exceptionMessage );
     if ( exceptionMessage != null ) { return; }    	
    }
    else
    {
     registryDomainSuffixOnly = true;
    }
    sb = new StringBuilder[utilityWhoIsArgument.domainName.Length][];
    for ( domainIndex = 0; domainIndex < utilityWhoIsArgument.domainName.Length; ++domainIndex )
    {
     domainName         =  utilityWhoIsArgument.domainName[domainIndex];
     domainNameNewLine  =  domainName + Environment.NewLine;
     domainNameByte     =  Encoding.ASCII.GetBytes( domainNameNewLine.ToCharArray() );
     if ( registryDomainSuffixOnly )
     {
      domainNameSplit  =  domainName.Split('.');
      registry         =  new string[1];
      registry[0]      =  RegistryWhoIs[0][RankRegistryWhoIsName];
      for ( int index = 0; index < RegistryWhoIs.Length; ++index )
      {
       if ( RegistryWhoIs[index][RankRegistryWhoIsName].EndsWith( domainNameSplit[domainNameSplit.Length-1].ToLower() ) ) 
       {
       	registry[0] = RegistryWhoIs[index][RankRegistryWhoIsName];
       	break;
       }//if ( RegistryWhoIs[index][RankRegistryWhoIsName].EndsWith( domainNameSplit[domainNameSplit.Length-1].ToLower() );
      }//for ( int index = 0; index < RegistryWhoIs.Length; ++index )
     }//if ( registryDomainSuffixOnly == true )
     sb[domainIndex] = new StringBuilder[registry.Length];
     for ( registryIndex = 0; registryIndex < registry.Length; ++registryIndex )
     {
      registryCurrent                 =  registry[registryIndex]; 
      tcpClient                       =  new TcpClient( registryCurrent, utilityWhoIsArgument.port );
      stream                          =  tcpClient.GetStream();
      stream.Write( domainNameByte, 0, domainNameNewLine.Length );
      streamReader                    =  new StreamReader( tcpClient.GetStream(), Encoding.ASCII );
      sb[domainIndex][registryIndex]  =  new StringBuilder( registryCurrent);
      if ( httpContext == null ) 
      { sb[domainIndex][registryIndex].Append( Environment.NewLine ); }
      else
      { sb[domainIndex][registryIndex].Append( "<br/>" ); }      
      while ( true )
      {
	   readLine  =  streamReader.ReadLine();
	   if ( readLine == null ) { break; }
       if ( httpContext == null ) { System.Console.WriteLine( readLine ); }
	   sb[domainIndex][registryIndex].Append( readLine );
	   if ( httpContext != null ) { sb[domainIndex][registryIndex].Append("<br/>"); }
	  }//while ( true )
	  tcpClient.Close();
     }//for ( registryIndex = 0; registryIndex < registry.Length; ++registryIndex ) 
    }//foreach ( string domainName in utilityWhoIsArgument.domainName )    	
   }//try
   catch ( SocketException exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   finally 
   {
   	if ( tcpClient != null ) { tcpClient.Close(); }
   }//finally	
  }//WhoisLookup

  static UtilityWhoIs()
  {

  }//static UtilityWhoIs()

 }//public class UtilityWhoIs
 
}//namespace WordEngineering