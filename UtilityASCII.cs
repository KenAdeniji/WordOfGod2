using System;

namespace WordEngineering
{
 ///<summary>UtilityASCII</summary>
 public class UtilityASCII
 {

  /// <summary>Main()</summary>
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
   System.Console.WriteLine( Asc( 'A' ) );
   System.Console.WriteLine( Microsoft.VisualBasic.Strings.Asc( 'A' ) );
   System.Console.WriteLine( Chr( 97 ) );
   System.Console.WriteLine( Microsoft.VisualBasic.Strings.Chr( 97 ) );
  }//public static void Stub()

  /// <summary>Asc</summary>
  /// <remarks>Microsoft.VisualBasic.Strings.Asc</remarks>
  public static int Asc( char charCurrent )
  {
   return Convert.ToInt32( charCurrent );
  }

  /// <summary>Chr</summary>
  /// <remarks>Microsoft.VisualBasic.Strings.Chr</remarks>  
  public static char Chr( int ascii )
  {
   return Convert.ToChar( ascii );
  }
 
  static UtilityASCII()
  {

  }//static UtilityASCII()

 }//public class UtilityASCII
 
}//namespace WordEngineering