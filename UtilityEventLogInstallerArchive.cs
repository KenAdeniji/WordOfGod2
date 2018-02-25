using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Configuration.Install;

namespace WordEngineering
{

 /// <summary>UtilityEventLogInstallerArgument</summary>
 public class UtilityEventLogInstallerArgument
 {
  ///<summary>log</summary>
  public  string  log      =  "Application";

  ///<summary>source</summary>
  public  string  source   =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor.</summary>
  public UtilityEventLogInstallerArgument():this
  {
   "Application",
   null //source  	
  }//public UtilityEventLogInstallerArgument()

  /// <summary>Constructor.</summary>
  public UtilityEventLogInstallerArgument
  (
   string log,
   string source
  )
  {
   this.log     =  log;
   this.source  =  source;
  }//public UtilityEventLogInstallerArgument()

 }//public class UtilityEventLogInstallerArgument

 ///<summary>UtilityEventLogInstaller<summary>
 [RunInstaller(true)]
 public class UtilityEventLogInstaller : Installer
 {
  ///<summary>eventLogInstaller<summary>
  public  EventLogInstaller  eventLogInstaller;

  ///<summary>UtilityEventLogInstaller()<summary>
  public UtilityEventLogInstaller()
  {
   Boolean                               booleanParseCommandLineArguments      =  false;
   string                                exceptionMessage                      =  null;     
   UtilityEventLogInstallerArgument      utilityEventLogInstallerArgument      =  null;
   
   utilityEventLogInstallerArgument  =  new UtilityEventLogInstallerArgument();
   
   booleanParseCommandLineArguments  =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityEventLogInstallerArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityEventLogInstallerArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   //Create Instance of EventLogInstaller
   eventLogInstaller = new EventLogInstaller();

   // Set the Source of Event Log, to be created.
   eventLogInstaller.Source = "TEST";

   // Set the Log that source is created in
   eventLogInstaller.Log = "Application";

   // Add eventLogInstaller to the Installers Collection.
   Installers.Add( eventLogInstaller );
  }
 }
}