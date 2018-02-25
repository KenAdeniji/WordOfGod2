using System;

namespace WordEngineering
{
 delegate void DelegateSignature();
 public class WorkDelegate
 {
  public static void DelegateFunction()
  {
   System.Console.WriteLine("DelegateFunction()");
  }
  public static void DelegateFunction2()
  {
   System.Console.WriteLine("DelegateFunction2()");
  }
  public static void Main(string[] argv)
  {
   DelegateSignature delegateSignature = new DelegateSignature(DelegateFunction);
   delegateSignature();
  }
 }
}