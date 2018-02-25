using System;
using System.Globalization;

namespace WordEngineering
{
 ///<summary>UtilityMath</summary>
 public class UtilityMath
 {

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  ( 
   string[] argv
  )
  {
   Stub();
  }//public static void Main()
  
  /// <summary>Stub()</summary>
  public static void Stub()
  {
   System.Console.WriteLine( HexToInt( "FFFF" ) );
   System.Console.WriteLine( IntToHex( 65535 ) );
   System.Console.WriteLine( Factorial( 10 ) );
  }//public static void Stub()

  ///<summary>Factorial</summary>
  public static long Factorial( long number )
  {
   long result;
   if ( number == 1 )
   {
    result = number;
   }
   else
   {
    result = number * Factorial( number - 1 );
   }
   return ( result );
  }

  /// <summary>HexToInt</summary>
  /// <remarks>José Alarcón JASoft.org</remarks>
  public static long HexToInt( string hex )
  {
   long  num  =  -1;
   Int64.TryParse( hex, System.Globalization.NumberStyles.HexNumber, null, out num );
   return ( num );
  }
  
  /// <summary>IntToHex</summary>
  /// <remarks>José Alarcón JASoft.org</remarks>
  public static string IntToHex( long num )
  {
   return String.Format( "{0:x}", num ).ToUpper();
  }
   
  static UtilityMath()
  {

  }//static UtilityMath()

 }//public class UtilityMath
 
}//namespace WordEngineering