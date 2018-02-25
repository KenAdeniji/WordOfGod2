using System;

namespace WordEngineering
{
    delegate void Del(string statement);

    public class MulticastDelegate
    {
        public static void Hello(string statement)
        {
            System.Console.WriteLine("{0} Hello", statement);
        }
        public static void Goodbye(string statement)
        {
            System.Console.WriteLine("{0} Goodbye", statement);
        }
        public static void Main(string[] argv)
        {
            Del hello, goodbye, helloGoodbye, helloNoGoodbye;
            hello = Hello;
            goodbye = Goodbye;
            helloGoodbye = hello + goodbye;
            helloNoGoodbye = helloGoodbye - goodbye;
            hello("Calling the hello delegate");
            goodbye("Calling the goodbye delegate");
            helloGoodbye("Calling the helloGoodbye delegate");
            helloNoGoodbye("Calling the helloNoGoodbye delegate");
        }
    }
}