using System;
using System.Globalization;

namespace WordEngineering
{
 ///<summary>UtilityGlobalization</summary>
 public class UtilityGlobalization
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
  }//public static void Stub()

  /// <summary>DayName</summary>
  /// <remarks>
  ///  DateTime.Now.ToString("dddd")
  ///  DateTime.Now.DayOfWeek.ToString()
  /// </remarks>
  public static string DayName
  (
   DateTime  dateTime
  )
  {
   return ( CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)dateTime.DayOfWeek] );
  }

  /// <summary>GlobalizationCultureInfo()</summary>
  public static void GlobalizationCultureInfo()
  {
   foreach( CultureInfo cultureInfo in CultureInfo.GetCultures( CultureTypes.AllCultures ) )
   {
    System.Console.WriteLine
    ( 
     "CultureInfo Name: {0} | Native Name: {1}",
     cultureInfo.Name,
     cultureInfo.NativeName
    );
   }//foreach( CultureInfo cultureInfo in CultureInfo.GetCultures( CultureTypes.AllCultures ) )
  }//public static void GlobalizationCultureInfo()

  /// <summary>MonthName</summary>
  /// <remarks>DateTime.Now.ToString("MMMM")</remarks>
  public static string MonthName
  (
   DateTime  dateTime
  )
  {
   return ( CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[dateTime.Month-1] );
  }
  
  static UtilityGlobalization()
  {

  }//static UtilityGlobalization()

 }//public class UtilityGlobalization
 
}//namespace WordEngineering