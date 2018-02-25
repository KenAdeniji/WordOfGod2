using System;
using System.Runtime.InteropServices;

namespace WordEngineering
{

 /// <summary>UtilityBeep.</summary>
 /// <remarks>http://blogs.msdn.com/brada/archive/2004/06/03/148142.aspx Brad Abrams  : MessageBeep in the .NET Framework</remarks>
 public class UtilityBeep
 {
  /// <summary>MessageBeepType</summary>
  public enum MessageBeepType
  {
   /// <summary>Default</summary>
   Default = -1,
   /// <summary>OK</summary>   
   Ok = 0x00000000,
   /// <summary>Error</summary>   
   Error = 0x00000010,
   /// <summary>Question</summary>   
   Question = 0x00000020,
   /// <summary>Warning</summary>   
   Warning = 0x00000030,
   /// <summary>Information</summary>   
   Information = 0x00000040,
  }

  /// <summary>MessageBeep</summary>
  [DllImport("user32.dll", SetLastError=true)]
  public static extern bool MessageBeep
  (
    MessageBeepType type
  );

  /// <summary>MessageBeep</summary>
  [DllImport("kernel32.dll")]
  public static extern bool Beep
  (
   int freq,
   int duration
  );

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {
   System.Console.Beep();
   //Microsoft.VisualBasic.Interaction.Beep();
   //MessageBeep( MessageBeepType.Information ); 
   //Beep( 800, 200 );
   //System.Console.WriteLine("\a");
  }//public static void Main()

  static UtilityBeep()
  {
  }//static UtilityBeep()

 }//public class UtilityBeep
}//namespace WordEngineering