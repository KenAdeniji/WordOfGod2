using System;
using System.Web;

namespace WordEngineering
{

 ///<summary>UtilityString</summary>
 public class UtilityString
 {
  ///<summary>ArrayCopy</summary>
  public static void ArrayCopy
  (
       string[][] source,
   ref string[]   target,
       int        rank,
   ref string     exceptionMessage
  )
  {
   try
   {
    target = new string[source.Length];
    for ( int index = 0; index < source.Length; ++index )
    {
     target[index] = source[index][rank];
    }  
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
  }//ArrayCopy
  	
  static UtilityString()
  {

  }//static UtilityString()

 }//public class UtilityString
 
}//namespace WordEngineering