using System;
using System.Threading;

namespace WordEngineering
{
 public class UtilityThread
 {

  public static int threadIterator = 0;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main(string[] argv)
  {
   ThreadJoin();
   ThreadCoupling();
  }

  ///<summary>Divide</summary>
  public static void Divide()
  {
   for (int divisor = 1; divisor <= 12; divisor++)
   {
    ThreadIterator();
    System.Console.WriteLine("[{0}] 100 / {1} = {2}", threadIterator, divisor, 100.0 / divisor);
   }
  }

  ///<summary>Multiply</summary>
  public static void Multiply()
  {
   for (int multiplier = 1; multiplier <= 12; multiplier++)
   {
    ThreadIterator();
    System.Console.WriteLine("[{0}] 5 * {1} = {2}", threadIterator, multiplier, 5 * multiplier);
   }
  }

  ///<summary>ThreadIterator</summary>
  public static void ThreadIterator()
  {
   lock (typeof(UtilityThread)) { ++threadIterator; }
  }

  ///<summary>ThreadCoupling</summary>
  public static void ThreadCoupling()
  {
   Thread[] thread = new Thread[2];
   thread[0] = new Thread(new ThreadStart(UtilityThread.Multiply));
   thread[1] = new Thread(new ThreadStart(UtilityThread.Divide));
   for ( int threadIndex = 0; threadIndex < thread.Length; ++threadIndex )
   {
    thread[threadIndex].Start();
   }
  }

  ///<summary>ThreadJoin</summary>
  public static void ThreadJoin()
  {
   Thread thread = new Thread(new ThreadStart(UtilityThread.Multiply));
   System.Console.WriteLine("IsBackground: {0}", thread.IsBackground);
   System.Console.WriteLine("IsAlive: {0}", thread.IsAlive);
   Thread.Sleep(100);
   thread.Start();
   UtilityThread.Divide();
   thread.Join();
  }
 }
}