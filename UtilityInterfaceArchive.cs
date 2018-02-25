using System;
using System.Diagnostics;

namespace WordEngineering
{
 interface I1
 {
  void P();
 }

 interface I2 : I1
 {
  void Q();
 }

 ///<summary>UtilityInterface</summary>
 public class UtilityInterface: I1, I2
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   GetInterfaces( new UtilityInterface() );
  }

  ///<summary>P</summary>
  public void P() { }

  ///<summary>Q</summary>
  public void Q() { }

  ///<summary>GetInterfaces</summary>
  ///<remarks>
  /// Subject: Re: Iterate through all the interfaces supported by an object   12/16/2005 1:36 PM PST 
  /// By: Stefan Simek In: microsoft.public.dotnet.languages.csharp
  ///</remarks>
  public static void GetInterfaces(object obj)
  {
   foreach(Type type in obj.GetType().GetInterfaces())
   {
    System.Console.WriteLine(type.FullName);
   }
  
   /*
   Type typeObj = obj.GetType();
   foreach(Type type in typeObj.GetInterfaces())
   {
    System.Console.WriteLine(type.FullName);
   }
   */
  
  }
 }
}