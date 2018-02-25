using System;
using System.Collections;

public class UtilityQueue
{
 public static void Main()
 {
  Queue queue = new Queue();
  queue.Enqueue("The");
  queue.Enqueue("First");
  queue.Enqueue("Shall");
  queue.Enqueue("Be");
  queue.Enqueue("A");
  queue.Enqueue("Child");
  foreach ( Object obj in queue )
  {
   System.Console.WriteLine(obj);
  }
 }
}