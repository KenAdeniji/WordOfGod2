using System;
using System.Collections;
using System.Collections.Generic;

public class UtilityStack
{
 public static void Main()
 {
  Stack stack = new Stack();
  stack.Push("Talent");
  stack.Push("Tale");
  foreach ( Object obj in stack )
  {
   System.Console.WriteLine(obj);
  }
  Stack<int> stackGeneric = new Stack<int>();
  stackGeneric.Push(3);
  int x = stackGeneric.Pop();
 }
}