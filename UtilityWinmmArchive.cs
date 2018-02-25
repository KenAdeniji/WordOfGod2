using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WordEngineering
{

 [Flags]
 public enum SoundFlags : int
 {
  SND_SYNC = 0x0000,         // play synchronously (default)
  SND_ASYNC = 0x0001,        // play asynchronously
  SND_NODEFAULT = 0x0002,    // silence (!default) if sound not found
  SND_MEMORY = 0x0004,       // pszSound points to a memory file
  SND_LOOP = 0x0008,         // loop the sound until next sndPlaySound
  SND_NOSTOP = 0x0010,       // don't stop any currently playing sound
  SND_NOWAIT = 0x00002000,   // don't wait if the driver is busy
  SND_ALIAS = 0x00010000,    // name is a registry alias
  SND_ALIAS_ID = 0x00110000, // alias is a predefined id
  SND_FILENAME = 0x00020000, // name is file name
  SND_RESOURCE = 0x00040004  // name is resource name or atom
 }

 /// <summary>UtilityWinmm</summary>
 public class UtilityWinmm
 {
  /// <summary>PlaySound</summary>
  [DllImport("winmm.dll", SetLastError=true)]
  public static extern bool PlaySound
  (
   string pszSound,
   System.UIntPtr hmod, 
   uint fdwSound
  );

  /// <summary>waveOutGetNumDevs</summary>
  /// <remarks>
  ///  Subject: Re: How can I check for sound card?   12/1/2005 3:45 AM PST 
  ///  By:    Vitaly Zayko  In:    microsoft.public.dotnet.languages.csharp 
  ///  That's right - you need to import a winmm.dll function.
  ///  If you just want to make sure that this PC does have a sound card you 
  ///  may use this:
  ///  [DllImport("winmm.dll", SetLastError = true)]
  ///  public static extern uint waveOutGetNumDevs();
  ///  If result is non-zero then you can expect a device.
  /// </remarks>
  [DllImport("winmm.dll", SetLastError = true)]
  public static extern uint waveOutGetNumDevs();

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  [STAThread]
  public static void Main(string[] argv)
  {
  }

  /// <summary>PlaySoundStub</summary>
  /// <remarks>System.Console.WriteLine("PlaySound: {0}", PlaySoundStub(@"C:\WINDOWS\Media\start.wav"));</remarks>
  public static bool PlaySoundStub(string filename)
  {
   bool playSound = PlaySound
   (
    filename, 
    UIntPtr.Zero,
    (uint) (SoundFlags.SND_FILENAME)
   );
   return(playSound);
  }

  /// <summary>WaveOutGetNumDevsStub</summary>
  /// <remarks>System.Console.WriteLine("Number of Devices: {0}", WaveOutGetNumDevsStub());</remarks>
  public static uint WaveOutGetNumDevsStub()
  {
   uint numDevs = waveOutGetNumDevs();
   return(numDevs);
  }
  
 }
}