using System;
using System.Diagnostics;

namespace WordEngineering
{
 ///<summary>Dx21.com</summary>
 public class UtilityRunDLL32
 {
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(string[] argv)
  {

  }
  
  ///<summary>RunDLL32</summary>
  public static void RunDLL32(string argument)
  {
   ProcessStartInfo processStartInfo = new ProcessStartInfo("RunDLL32");
   processStartInfo.Arguments = argument;
   Process.Start(processStartInfo);
  }

  ///<summary>AddOrRemovePrograms</summary>
  public static void AddOrRemovePrograms()
  {
   RunDLL32("Shell32.dll,Control_RunDLL appwiz.cpl,,0");
  }

  ///<summary>ControlPanel</summary>
  public static void ControlPanel()
  {
   RunDLL32("Shell32.dll,Control_RunDLL");
  }

  ///<summary>DeviceManager</summary>
  public static void DeviceManager()
  {
   RunDLL32("DevMgr.dll DeviceManager_Execute");
  }
 }
}