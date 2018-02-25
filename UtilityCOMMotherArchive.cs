using System;
using System.Reflection;
using System.IO;

/*
You could use sn.exe and the Assembly Linker tool (al.exe) to add the key file to your assembly after you 
have compiled it, but a much easier way is to use the AssemblyKeyFile attribute in your code to compile it 
at compile time. It must be only declared once, and be at assembly scope (i.e., outside the namespace 
declaration for your code). 
For our example, 
 Add the following two lines to the top of the createtxt.cs file, 
 immediately beneath all the "using" namespace statements and above the nscreatetxt namespace declaration. 
 The AssemblyVersion attribute will be added also to control our versioning process. 
Save the file.
*/
[assembly: System.Reflection.AssemblyKeyFile("CreateTextKey.snk")] 
[assembly: AssemblyVersion("1.0.0")]

namespace WordEngineering
{ 
 public class UtilityCOMMother
 {
  /*
  Note the use of a public no-arguments’ constructor. 
  For this to be called from COM it has to have a default public no-arguments constructor.
  */
  public UtilityCOMMother()
  {
  }
  
  public void HelloWorld() 
  {
   StreamWriter streamWriter;
   streamWriter=File.CreateText(@"c:\HelloWorld.txt");
   streamWriter.WriteLine("Hello Mom");
   streamWriter.WriteLine("Hello GrandMom");
   streamWriter.Close();
  } 
 }
}