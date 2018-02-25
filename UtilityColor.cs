using System;
using System.Drawing;

namespace WordEngineering
{

 /// <summary>UtilityColor.</summary>
 /// <remarks>http://codebetter.com/blogs/sahil.malik/archive/2005/08/24/131110.aspx</remarks>
 public class UtilityColor
 {
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {
   string  exceptionMessage  =  null;
   string  htmlColor         =  null;
   Color   colorRed          =  Color.Red;

   try
   {
    htmlColor = ColorTranslator.ToHtml( colorRed );
    System.Console.WriteLine
    (
     "{0}: {1}",
     colorRed,
     htmlColor
    );
   }
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }
   if ( exceptionMessage != null )
   {
    System.Console.WriteLine( exceptionMessage );
   }
  }//public static void Main()

  static UtilityColor()
  {
  }//static UtilityColor()

 }//public class UtilityColor
}//namespace WordEngineering