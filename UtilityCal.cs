using System;

using CommandLine;

namespace WordEngineering
{
 /// <summary>UtilityCalArgument</summary>
 public class UtilityCalArgument
 {
  ///<summary>Julian dates (1..365)</summary>
  public bool j;

  ///<summary>Current year</summary>
  public bool y;
  
  ///<summary>files</summary>
  [DefaultArgumentAttribute(ArgumentType.MultipleUnique)]
  public string[] files;
  
  /// <summary>Constructor Overloading</summary>
  public UtilityCalArgument():this
  (
   false, //Julian
   false //y
  ) 
  {
  }
  
  /// <summary>Constructor</summary>
  public UtilityCalArgument
  (
   bool j, //Julian
   bool y //year
  )
  {
   this.j = j;
   this.y = y;
  }
 }

 /// <summary>UtilityCal</summary>
 /// <remarks>Jessica Perry Hekman Linux in a Nutshell ISBN 1-56592-167-4</remarks>
 public class UtilityCal
 {
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(String[] argv)
  {
   bool parseCommandLineArguments;
   int month;
   int year;
   UtilityCalArgument utilityCalArgument = new UtilityCalArgument();
   parseCommandLineArguments = Parser.ParseArgumentsWithUsage
   (
    argv, 
    utilityCalArgument
   );
   if ( parseCommandLineArguments == false )
   {
    return;
   }
   month = DateTime.Today.Month;
   year = DateTime.Today.Year;
   if ( utilityCalArgument.files.Length == 1 ) { Int32.TryParse(utilityCalArgument.files[0], out year); }
   if ( utilityCalArgument.files.Length == 2 ) 
   { 
    Int32.TryParse(utilityCalArgument.files[0], out month); 
    Int32.TryParse(utilityCalArgument.files[1], out year);
   }
   if ( utilityCalArgument.j ) { Julian(); }
   else if ( utilityCalArgument.y ) { for(int index = 1; index <= 12; ++index) { Calendar(index, year); } }
   else if ( utilityCalArgument.files.Length == 1 ) { for(int index = 1; index <= 12; ++index) { Calendar(index, year); } }
   else if ( utilityCalArgument.files.Length == 2 ) { { Calendar(month, year); } }
   else Calendar();
  }

  ///<summary>Calendar</summary>
  public static void Calendar()
  {
   Calendar( DateTime.Today.Month, DateTime.Today.Year );
  }

  ///<summary>Calendar</summary>
  public static void Calendar(int month, int year)
  {
   DateTime dateTime = new DateTime(year, month, 1);
   System.Console.WriteLine("Su Mo Tu We Th Fr Sa");
   System.Console.Write( new String(' ', (int) dateTime.DayOfWeek * 3) );
   for
   (
    ;   	 
    dateTime < new DateTime(year, month, 1).AddMonths(1);
    dateTime = dateTime.AddDays(1)
   )
   {
    if (dateTime.Day < 10) { System.Console.Write(' '); }
    System.Console.Write("{0} ", dateTime.Day);
    if ( (int) dateTime.DayOfWeek == 6 ) { System.Console.WriteLine(); }
   }
   System.Console.WriteLine();
  }

  ///<summary>Julian</summary>
  public static void Julian()
  {
   for 
   (
    DateTime dateTime = new DateTime(DateTime.Today.Year, 1, 1); 
    dateTime <= new DateTime(DateTime.Today.Year, 12, 31);
    dateTime = dateTime.AddDays(1)
   )
   {
    System.Console.WriteLine("{0:d} {1}", dateTime, dateTime.DayOfYear); 
   }
  }
 }
}