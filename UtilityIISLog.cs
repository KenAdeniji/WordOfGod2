using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

using CommandLine;

namespace WordEngineering
{
 /// <summary>UtilityIISLogArgument</summary>
 public class UtilityIISLogArgument
 {
  ///<summary>computer</summary>
  public string computer;

  ///<summary>log</summary>
  public string log;
  
  ///<summary>site</summary>
  public string site;
  
  ///<summary>files</summary>
  [DefaultArgumentAttribute(ArgumentType.MultipleUnique)]
  public string[] files;
  
  /// <summary>Constructor Overloading</summary>
  public UtilityIISLogArgument():this
  (
   null, //computer
   null, //log
   null //site
  ) 
  {
  }
  
  /// <summary>Constructor</summary>
  public UtilityIISLogArgument
  (
   string computer,
   string log,
   string site
  )
  {
   this.computer = computer;
   this.log = log;
   this.site = site;
  }
 }
 
 /// <summary>UtilityIISLog</summary>
 /// <remarks>
 ///  Karl Seguin ASP.NET Spiced: AJAX http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspp/html/ASPNetSpicedAjax.asp
 ///  <system.web> <identity impersonate="true"/> </system.web>
 ///   IUSR_Computer Permission Allow List Folder Contents &amp; Read
 ///   C:\WINDOWS\system32\LogFiles
 ///   C:\WINDOWS\system32\LogFiles\W3SVC1 
 /// </remarks> 
 public class UtilityIISLog
 {
  ///<summary>DayOffset</summary>
  public const int DayOffset = -1;

  ///<summary>ColumnReplace</summary>
  public static string[] ColumnReplace = new string[] { "-", "(", ")" };
  
  ///<summary>WebSite</summary>
  public const string WebSite = "W3SVC1";
  
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(String[] argv)
  {
   bool parseCommandLineArguments;
   UtilityIISLogArgument  utilityIISLogArgument = new UtilityIISLogArgument();
   parseCommandLineArguments = Parser.ParseArgumentsWithUsage
   (
   	argv, 
   	utilityIISLogArgument
   );
   if ( parseCommandLineArguments == false )
   {
    return;
   }
   Stub( utilityIISLogArgument );
  }

  
  ///<summary>Stub</summary>
  public static void Stub
  (
   UtilityIISLogArgument utilityIISLogArgument
  )
  {
   List<string> log;
   List<string> site;
   DataTable dataTable;             
   LoadSite
   (
        utilityIISLogArgument,
    out site
   );
   if ( site != null )
   {
    foreach( string siteCurrent in site ) { System.Console.WriteLine( siteCurrent ); }
   }
   
   LoadLog
   (
        utilityIISLogArgument,
    out log
   );
   if ( log != null )
   {
    foreach( string logCurrent in log ) { System.Console.WriteLine( logCurrent ); }
   }
   
   ParseLog
   (
        utilityIISLogArgument,
   	out dataTable
   );
  }
  
  ///<summary>LoadLog</summary>
  public static void LoadLog
  (
       UtilityIISLogArgument utilityIISLogArgument,
   out List<string>          log
  )
  {
   string IISLogPath;
   string computer = utilityIISLogArgument.computer;
   string site = utilityIISLogArgument.site;
   string windir;
   DirectoryInfo directoryInfo;
   HttpContext httpContext = HttpContext.Current;
   if ( string.IsNullOrEmpty( computer ) )
   {
   	computer = Environment.MachineName;
   }
   if ( string.IsNullOrEmpty( site ) )
   {
   	site = WebSite;
   }
   //windir = Environment.GetEnvironmentVariable("windir");
   windir = WinDir( computer );
   if ( String.Compare( computer, Environment.MachineName, true ) == 0 )
   {
    IISLogPath = windir + @"\System32\LogFiles\" + site;
   }
   else
   {
    IISLogPath = @"\\" + computer + @"\" + windir + @"\System32\LogFiles\" + site;
    IISLogPath = IISLogPath.Replace(':','$');
   }
   /*
   if ( Directory.Exists( IISLogPath ) == false )
   {
   	return;
   }
   */
   directoryInfo = new DirectoryInfo( IISLogPath );
   log = new List<string>();
   foreach( FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos() )
   {
    if ( fileSystemInfo.Name.StartsWith("ex") &&  fileSystemInfo.Name.EndsWith(".log") )
    {
     log.Add( fileSystemInfo.Name );
    }
   }
  }
  
  ///<summary>LoadSite</summary>
  public static void LoadSite
  (
       UtilityIISLogArgument utilityIISLogArgument,
   out List<string>          site
  )
  {
   string IISLogPath;
   string computer = utilityIISLogArgument.computer;
   string windir;
   DirectoryInfo directoryInfo;
   HttpContext httpContext = HttpContext.Current;
   site = null;
   if ( string.IsNullOrEmpty( computer ) )
   {
   	computer = Environment.MachineName;
   }
   //windir = Environment.GetEnvironmentVariable("windir");
   windir = WinDir( computer );
   IISLogPath = @"\\" + computer + @"\" + windir + @"\System32\LogFiles\";
   IISLogPath = IISLogPath.Replace(':','$');
   /*
   if ( Directory.Exists( IISLogPath ) == false )
   {
   	return;
   }
   */
   directoryInfo = new DirectoryInfo( IISLogPath );
   site = new List<string>();
   foreach( FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos() )
   {
   	if ( fileSystemInfo.Name.StartsWith("W3") )
   	{
     site.Add( fileSystemInfo.Name );
   	}
   }
  }

  ///<summary>ParseLog</summary>
  public static void ParseLog
  (
       UtilityIISLogArgument utilityIISLogArgument,
   out DataTable             dataTable
  )
  {
   string[] column;
   string columnName;
   string computer = utilityIISLogArgument.computer;
   string content;
   string[] line;
   string log = utilityIISLogArgument.log;
   string logFile;   
   string site = utilityIISLogArgument.site;
   string[] value;
   string windir;
   DateTime date;
   HttpContext httpContext = HttpContext.Current;
   FileStream fileStream;
   StreamReader streamReader;
   
   dataTable = null;
   if ( string.IsNullOrEmpty( computer ) )
   {
   	computer = Environment.MachineName;
   }
   if ( string.IsNullOrEmpty( log ) )
   {
   	log = String.Format("ex{0:yyMMdd}.log",DateTime.Now.AddDays(DayOffset));
   }
   if ( string.IsNullOrEmpty( site ) )
   {
   	site = WebSite;
   }
   //windir = Environment.GetEnvironmentVariable("windir");
   windir = WinDir( computer );
   if ( String.Compare( computer, Environment.MachineName, true ) == 0 )
   {
    logFile = windir + @"\System32\LogFiles\" + site + @"\" + log;
   }
   else
   {
    logFile = @"\\" + computer + @"\" + windir + @"\System32\LogFiles\" + site + @"\" + log;
    logFile = logFile.Replace(':','$');
   }
   /*
   if ( File.Exists( logFile ) == false )
   {
   	return;
   }
   */
   fileStream = new FileStream
   (
   	logFile,
   	FileMode.Open,
   	FileAccess.Read,
	FileShare.Read
   );
   streamReader = new StreamReader(fileStream);
   content = streamReader.ReadToEnd();
   streamReader.Close();
   fileStream.Close();
   streamReader=null;
   fileStream=null;
   line = content.Split('\n');
   //line = File.ReadAllLines(logFile);
   dataTable = new DataTable("log");
   columnName=line[3].Replace("#Fields: ","");
   column = columnName.Split(' ');
   for ( int columnIndex = 0; columnIndex < column.Length -1; ++columnIndex )
   {
   	foreach( string columnReplace in ColumnReplace )
   	{
   	 column[columnIndex] = column[columnIndex].Replace(columnReplace, "");
   	}
   	dataTable.Columns.Add( column[columnIndex] );
   }
   for ( int lineIndex = line.Length - 2; lineIndex > 3; --lineIndex )
   {
   	if ( string.IsNullOrEmpty( line[lineIndex].Trim() ) ) { continue; }
   	value = line[lineIndex].Split(' ');
   	if ( DateTime.TryParse( value[0], out date ) )
   	{
   	 dataTable.Rows.Add( value );
   	 /*
     DataRow dataRow;
   	 dataRow = dataTable.NewRow();
   	 for ( int columnIndex = 0; columnIndex < column.Length -1; ++columnIndex )
   	 {
   	  dataRow[columnIndex] = value[columnIndex];
   	 }
     dataTable.Rows.Add( dataRow );
     */
   	}
   }
  }
  
  /// <summary>WinDir</summary>
  /// <remarks>Blogs.msdn.com/brada/archive/2003/12/10/50944.aspx</remarks> 
  public static string WinDir
  (
   string computer
  )
  {
   RegistryKey registryKey;
   string      value = null;
   if ( computer == null ) { computer = ""; }
   registryKey = RegistryKey.OpenRemoteBaseKey( RegistryHive.LocalMachine, computer ).OpenSubKey
   (
    @"System\CurrentControlSet\Control\Session Manager\Environment" 
   );
   if ( registryKey != null )
   {
    value = (string) registryKey.GetValue("windir");
   }
   return ( value );
  }
  
 }
}