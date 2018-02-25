using System;
using System.Runtime.InteropServices;

namespace WordEngineering
{

 /// <summary>UtilityWavArgument</summary>
 public class UtilityWavArgument
 {
  ///<summary>filenameWav</summary>  
  public string   filenameWav        =  null;

  ///<summary>flagWav</summary>  
  public string   flagWav            =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor.</summary>
  public UtilityWavArgument()
  {
  }//public UtilityWavArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityWavArgument
  (
   string   filenameWav,
   string   flagWav
  )
  {

   this.filenameWav   =  filenameWav;
   this.flagWav       =  flagWav;
   
  }//public UtilityWavArgument()

 }//public class UtilityWavArgument

 ///<summary>UtilityWav</summary>
 ///<remarks>/// http://www.publicjoe.f9.co.uk/csharp/snip/snip036.html</remarks>
 public class UtilityWav
 {

  /// <summary>SND_SYNC       =  0x00000000</summary>
  public const int SND_SYNC       =  0x00000000;      // Wait until finished playing
  
  /// <summary>SND_ASYNC      =  0x00000001</summary>
  public const int SND_ASYNC      =  0x00000001;      // Continue in program
  
  /// <summary>SND_NODEFAULT  =  0x00000002</summary>
  public const int SND_NODEFAULT  =  0x00000002;      // No default sound if not found
  
  /// <summary>SND_NOSTOP     =  0x00000010</summary>
  public const int SND_NOSTOP     =  0x00000010;      // Play only if not already busy
  
  /// <summary>SND_LOOP       =  0x00000008</summary>  
  public const int SND_LOOP       =  0x00000008;      // Play it again 

  /// <summary>PlaySound</summary>
  [DllImport("winmm.dll")]
  public static extern bool PlaySound
  ( 
   string  lpszName, 
   int     hModule, 
   int     dwFlags
  );

  /// <summary>PlaySoundW</summary>
  [DllImport("winmm.dll", EntryPoint="sndPlaySoundA")]
  public extern static bool PlaySoundW
  ( 
   string  lpszName, 
   int     dwFlags 
  );

  /// <summary>Constructor.</summary>
  public UtilityWav()
  {

  }
  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Boolean                       booleanParseCommandLineArguments  =  false;
   string                        exceptionMessage                  =  null;     
   UtilityWavArgument            utilityWavArgument               =  null;
   
   utilityWavArgument = new UtilityWavArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityWavArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityWavArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   Stub
   (
    ref utilityWavArgument,
    ref exceptionMessage
   );
   
  }//static void Main( String[] argv ) 

  ///<summary>Stub.</summary>
  public static void Stub
  (
   ref UtilityWavArgument  utilityWavArgument,
   ref string              exceptionMessage
  )
  {
   
   PlaySound
   (
    ref utilityWavArgument,
    ref exceptionMessage
   );

   PlaySoundW
   (
    ref utilityWavArgument,
    ref exceptionMessage
   );
 
  }//public static void Stub()

  ///<summary>PlaySound()</summary>
  public static bool PlaySound
  (
   ref UtilityWavArgument  utilityWavArgument,
   ref string              exceptionMessage
  )
  {
   
   bool  playSound  =  false;
   
   
   try
   {
    playSound  =  PlaySound( utilityWavArgument.filenameWav, 0, 1 );
   }//try
   catch ( Exception exception )
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }//catch ( Exception exception )

   return ( playSound );

  }//public static bool PlaySound( ref UtilityWavArgument utilityWavArgument, ref string exceptionMessage )

  ///<summary>PlaySoundW()</summary>
  public static bool PlaySoundW
  (
   ref UtilityWavArgument  utilityWavArgument,
   ref string              exceptionMessage
  )
  {
   bool  playSound  =  false;
   int   flagWav    =  0;
   
   try
   {
    if ( !String.IsNullOrEmpty( utilityWavArgument.flagWav ) )
    {
     flagWav = System.Convert.ToInt32( utilityWavArgument.flagWav, 16 );
    } 
   
    playSound  =  PlaySoundW( utilityWavArgument.filenameWav, flagWav );
    
   }    
   catch ( Exception exception )
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }//catch ( Exception exception )

   return ( playSound );

  }//public static bool PlaySoundW( ref UtilityWavArgument utilityWavArgument, ref string exceptionMessage )

  static UtilityWav()
  {

  }//static UtilityWav()
  
 }//public class UtilityWav
 
}//namespace WordEngineering