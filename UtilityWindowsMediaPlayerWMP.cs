using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;

namespace WordEngineering
{

 /// <summary>UtilityWindowsMediaPlayerWMPArgument</summary>
 public class UtilityWindowsMediaPlayerWMPArgument
 {
  ///<summary>filenameMedia</summary>  
  public String[] filenameMedia      =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor.</summary>
  public UtilityWindowsMediaPlayerWMPArgument()
  {
  }//public UtilityWindowsMediaPlayerWMPArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityWindowsMediaPlayerWMPArgument
  (
   string[] filenameMedia
  )
  {
   this.filenameMedia  =  filenameMedia;
  }//public UtilityWindowsMediaPlayerWMPArgument()

 }//public class UtilityWindowsMediaPlayerWMPArgument

 ///<summary>UtilityWindowsMediaPlayerWMP</summary>
 public class UtilityWindowsMediaPlayerWMP
 {

  /// <summary>Main()</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Boolean                               booleanParseCommandLineArguments      =  false;
   string                                exceptionMessage                      =  null;     
   UtilityWindowsMediaPlayerWMPArgument  utilityWindowsMediaPlayerWMPArgument  =  null;
   
   utilityWindowsMediaPlayerWMPArgument = new UtilityWindowsMediaPlayerWMPArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityWindowsMediaPlayerWMPArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityWindowsMediaPlayerWMPArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

  }//public static void Main()

  static UtilityWindowsMediaPlayerWMP()
  {

  }//static UtilityWindowsMediaPlayerWMP()

 }//public class UtilityWindowsMediaPlayerWMP
 
}//namespace WordEngineering