using System;

namespace WordEngineering
{
 ///<summary>Factorial( 0 ) = 1; Factorial( 1 ) = 1; number * Factorial( number - 1 ); 0!=1;1!=1;2!=2;3!=6;4!=24;5!=120;6!=720;7!=5040;8!=40320;9!=362880;10!=3628800</summary>
 public class UtilityFactorial
 {
  public static void Main(string[] argv)
  {
   Factorial(argv);
  }

  public static void Factorial(string[] argv)
  {
   long[] factorialIterate = new long[argv.Length];
   long[] factorialRecursion = new long[argv.Length];
   for(int index = 0; index < argv.Length; ++index)
   {
    bool parse;
    long number;
    parse = Int64.TryParse( argv[index], out number);
    if ( parse == false ) { continue; }
    factorialIterate[index] = FactorialIterate(number);
    factorialRecursion[index] = FactorialRecursion(number);
    System.Console.WriteLine
    (
     "{0} = Recursion: {1} | Iterate: {2}",
     number,
     factorialRecursion[index],
     factorialIterate[index]
    );
   }
  }

  public static long FactorialRecursion( long number )
  {
   if ( number <= 1 ) { return 1; }
   else
   { 
    return ( number * FactorialRecursion( number - 1 ) );
   }
  }

  public static long FactorialIterate(long number)
  {
   long factorial;
   if (number < 0 ) { factorial = 0; }
   else if (number == 0 || number == 1) { factorial = 1; }
   else
   {
    factorial = 1;
    for ( int loop = 2; loop <= number; ++loop )
    {
     factorial *= loop;
    }
   }
   return ( factorial );
  }
 }
}