using System;
using System.Text;

namespace WordEngineering
{
 public class UtilityMilitaryDate
 {

  public static DateTime Convert
  ( 
   string militaryDate
  )
  {

   int year;
   int month;
   int day;
   int hour;
   int minute;
   int second;

   DateTime datedMilitary;

   try
   {
    datedMilitary = new DateTime
    ( 	
     Convert.ToInt32( militaryDate.Substring(0,3)  ),
     Convert.ToInt32( militaryDate.Substring(4,2)  ),
     Convert.ToInt32( militaryDate.Substring(6,2)  ),
     Convert.ToInt32( militaryDate.Substring(8,2)  ),
     Convert.ToInt32( militaryDate.Substring(10,2) ),
     Convert.ToInt32( militaryDate.Substring(12,2) )
    ); 
   }//try
   catch ( Exception exception)
   {
    System.Console.WriteLine("Exception: {0}", exception.Message);
   } 
   
   return datedMilitary;
   
  }//public static DateTime Convert()
  
  /*
  public override string ToString()
  {
   try
   { 	
    StringBuilder militaryDate = null;
    militaryDate = new StringBuilder(13);
    militaryDate.Append("{0}{1}{2}{3}{4}{5}", new object[] {year, month, day, hour, minute, second } );
   }//try
   catch ( Exception exception )
   {
    System.Console.WriteLine("Exception: {0}", exceptionMessage); 	 
   }//catch ( Exception exception ) 	
   return militaryDate.ToString();
  }//ToString()	
  */
  
 }//public class UtilityMilitaryDate	
}//namespace WordEngineering