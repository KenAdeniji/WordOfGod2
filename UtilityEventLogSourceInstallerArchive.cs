using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Configuration.Install;

namespace WordEngineering
{

 /// <summary>UtilityEventLogSourceInstallerArgument</summary>
 /// <remarks>
 ///  PRB: "Requested Registry Access Is Not Allowed" Error Message When ASP.NET Application Tries to Write New EventSource in the EventLog  http://support.microsoft.com/default.aspx?scid=kb;en-us;329291
 ///  InstallUtil bin\UtilityEventLogSourceInstaller.dll
 /// </remarks> 
 public class UtilityEventLogSourceInstallerArgument
 {
  ///<summary>log</summary>
  public  string    log      =  "Application";

  ///<summary>source</summary>
  public  string[]  source   =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor.</summary>
  public UtilityEventLogSourceInstallerArgument():this
  (
   "Application",
   null //source  	
  )
  {
  }//public UtilityEventLogSourceInstallerArgument():this
  
  /// <summary>Constructor.</summary>
  public UtilityEventLogSourceInstallerArgument
  (
   string   log,
   string[] source
  )
  {
   this.log     =  log;
   this.source  =  source;
  }//public UtilityEventLogSourceInstallerArgument()

 }//public class UtilityEventLogSourceInstallerArgument

 ///<summary>UtilityEventLogSourceInstaller</summary>
 [RunInstaller(true)]
 public class UtilityEventLogSourceInstaller : Installer
 {

  ///<summary>SourceName</summary>
  public  string[]           SourceName = new string[]
                                          {
                                           @"file:///D:/WordOfGod/bin/ContactMaintenancePage.aspx.cs.dll",
                                           @"file:///D:/WordOfGod/bin/URIMaintenancePage.aspx.cs.DLL"
                                          };                                       	

  ///<summary>eventLogInstaller</summary>
  public  EventLogInstaller  eventLogInstaller;

  ///<summary>UtilityEventLogSourceInstaller()</summary>
  public UtilityEventLogSourceInstaller()
  {
   //Create Instance of EventLogInstaller
   eventLogInstaller = new EventLogInstaller();

   // Set the Log that source is created in
   eventLogInstaller.Log = "Application";

   foreach ( string source in SourceName )
   {
    // Set the Source of Event Log, to be created.
    eventLogInstaller.Source = source;

    // Add eventLogInstaller to the Installers Collection.
    Installers.Add( eventLogInstaller );
   }//foreach ( string source in SourceName ) 
  }//public UtilityEventLogSourceInstaller()
 }//public class UtilityEventLogSourceInstaller : Installer
}//namespace WordEngineering