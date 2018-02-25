using System;

namespace WordEngineering
{
 ///<summary>UtilityAppDomain<summary>
 public class UtilityAppDomain
 {
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static int Main(string[] argv)
  {
   //Create domain
   AppDomain appDomain = AppDomain.CreateDomain("appDomain");
   int r = appDomain.ExecuteAssembly("DarkBrownCarpet.exe",null,argv);
   //Unload domain
   AppDomain.Unload(appDomain);
   return r;
  } 
 }
}