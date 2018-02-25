using System;
using System.ComponentModel;  
using System.Media;

namespace WordEngineering
{
 ///<summary>UtilitySoundPlayer</summary>
 ///<remarks>
 /// http://msdn2.microsoft.com/en-us/library/system.media.soundplayer
 /// DIR C:\*.wav /s C:\WINDOWS\Media\chimes.wav
 ///</remarks>
 public static class UtilitySoundPlayer
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   /*
   System.Media.SystemSounds.Exclamation.Play();
   */

   SoundPlayer soundPlayer = SoundPlayerInitialize();
   soundPlayer.SoundLocation = @"C:\WINDOWS\Media\chimes.wav";
   soundPlayer.LoadAsync();

   /*
   soundPlayer.PlaySync();
   */

   /*
   soundPlayer.PlayLooping();
   System.Console.ReadLine();
   soundPlayer.Stop();
   */

  }

  ///<summary>SoundPlayerInitialize</summary>
  public static SoundPlayer SoundPlayerInitialize()
  {
   SoundPlayer soundPlayer;
   soundPlayer = new SoundPlayer();
   soundPlayer.LoadCompleted += new AsyncCompletedEventHandler(SoundPlayer_LoadCompleted);
   soundPlayer.SoundLocationChanged += new EventHandler(SoundPlayer_LocationChanged);
   return(soundPlayer);
  }

  ///<summary>SoundPlayer_LoadCompleted</summary>
  public static void SoundPlayer_LoadCompleted(object sender, AsyncCompletedEventArgs e)
  {   
   SoundPlayer soundPlayer = (SoundPlayer) sender;
   System.Console.WriteLine("SoundPlayer_LoadCompleted: {0}", soundPlayer.SoundLocation); 
  }

  ///<summary>SoundPlayer_LocationChanged</summary>
  public static void SoundPlayer_LocationChanged(object sender, EventArgs e)
  {   
   SoundPlayer soundPlayer = (SoundPlayer) sender;
   System.Console.WriteLine("SoundPlayer_LoadChanged: {0}", soundPlayer.SoundLocation); 
  }

 }
}