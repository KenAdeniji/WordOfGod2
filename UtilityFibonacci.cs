using System;

namespace WordEngineering
{
 ///<summary>Fibonacci( 0 ) = 0; Fibonacci( 1 ) = 1; Fibonacci( n ) = Fibonacci( n – 1 ) + Fibonacci( n – 2 ); 0, 1, 1, 2, 3, 5, 8, 13, 21</summary>
 public class UtilityFibonacci
 {
  public static void Main(string[] argv)
  {
   Fibonacci(argv);
  }

  public static void Fibonacci(string[] argv)
  {
   long[] fibonacciIterate = new long[argv.Length];
   long[] fibonacciRecursion = new long[argv.Length];
   for(int index = 0; index < argv.Length; ++index)
   {
    bool parse;
    long number;
    parse = Int64.TryParse( argv[index], out number);
    if ( parse == false ) { continue; }
    fibonacciIterate[index] = FibonacciIterate(number);
    fibonacciRecursion[index] = FibonacciRecursion(number);
    System.Console.WriteLine
    (
     "{0} = Recursion: {1} | Iterate: {2}",
     number,
     fibonacciRecursion[index],
     fibonacciIterate[index]
    );
   }
  }

  public static long FibonacciRecursion( long number )
  {
   if ( number == 0 || number == 1 ) { return number; }
   else
   { 
    return FibonacciRecursion( number - 1 ) + FibonacciRecursion( number - 2 );
   }
  }

  public static long FibonacciIterate(long number)
  {
   long fibonacci;
   long[] series;
   if (number < 0 ) { fibonacci = 0; }
   else if (number == 0) { fibonacci = 0; }
   else if (number == 1) { fibonacci = 1; }
   else
   {
    series = new long[number+1];
    series[0] = 0; series[1] = 1;
    for ( int loop = 2; loop <= number; ++loop )
    {
     series[loop] = series[loop-1] + series[loop-2];
    }
    fibonacci = series[number];
   }
   return ( fibonacci );
  }

 }
}