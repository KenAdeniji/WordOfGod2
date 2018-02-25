using System;
using System.Collections.Generic;

using CommandLine;

namespace WordEngineering
{
 /// <summary>UtilityTemperatureArgument</summary>
 public class UtilityTemperatureArgument
 {
  ///<summary>celsuis</summary>  
  public double[]  celsuis           =  null;

  ///<summary>fahrenheit</summary>  
  public double[]  fahrenheit        =  null;

  ///<summary>kelvin</summary>  
  public double[]  kelvin            =  null;
  
  ///<summary>files</summary>
  [DefaultArgumentAttribute(ArgumentType.MultipleUnique)]
  public string[] files;
  
  /// <summary>Constructor Overloading</summary>
  public UtilityTemperatureArgument()
  :this
  (
   null,  //celsuis
   null,  //fahrenheit
   null   //kelvin
  ) 
  {
  }//public UtilityTemperatureArgument()

  /// <summary>Constructor.</summary>
  public UtilityTemperatureArgument
  (
   double[]  celsuis,
   double[]  fahrenheit,
   double[]  kelvin
  )
  {
   this.celsuis     =  celsuis;
   this.fahrenheit  =  fahrenheit;
   this.kelvin      =  kelvin;
  }//public UtilityTemperatureArgument()

 }//public class UtilityTemperatureArgument

 ///<summary>UtilityTemperature</summary>
 ///<remarks>
 ///</remarks>
 public class UtilityTemperature
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   bool parseCommandLineArguments;
   UtilityTemperatureArgument utilityTemperatureArgument;
   
   utilityTemperatureArgument = new UtilityTemperatureArgument();
   parseCommandLineArguments =  Parser.ParseArgumentsWithUsage
   ( 
    argv, 
    utilityTemperatureArgument
   );
   if ( parseCommandLineArguments == false )
   {
    return;
   }
  }

  ///<summary>CelsuisToFahrenheit</summary>
  public static double CelsuisToFahrenheit
  (
   double celsuis
  )
  {
   double fahrenheit = ( ( celsuis * 9 ) / 5 ) + 32;
   return ( fahrenheit );
  }

  ///<summary>CelsuisToKelvin</summary>
  public static double CelsuisToKelvin
  (
   double celsuis
  )
  {
   double kelvin = celsuis + 273.15;
   return ( kelvin );
  }

  ///<summary>FahrenheitToCelsuis</summary>
  public static double FahrenheitToCelsuis
  (
   double fahrenheit
  )
  {
   double celsuis = ( ( fahrenheit - 32 ) * 5 ) / 9;
   return ( celsuis );
  }

  ///<summary>FahrenheitToKelvin</summary>
  public static double FahrenheitToKelvin
  (
   double fahrenheit
  )
  {
   double kelvin = ( ( ( fahrenheit - 32 ) * 5 ) / 9 ) + 273.15;
   return ( kelvin );
  }

  ///<summary>KelvinToCelsuis</summary>
  public static double KelvinToCelsuis
  (
   double kelvin
  )
  {
   double celsuis = kelvin - 273.15;
   return ( celsuis );
  }

  ///<summary>KelvinToFahrenheit</summary>
  public static double KelvinToFahrenheit
  (
   double kelvin
  )
  {
   double fahrenheit = ((kelvin - 273.15) * 9 / 5) + 32;
   return ( fahrenheit );
  }

  ///<summary>Metric</summary>
  public static List<string> Metric()
  {
   List<string> metric = new List<string>();
   metric.Add("Celsuis (Centigrade)");
   metric.Add("Fahrenheit");
   metric.Add("Kelvin");
   return (metric);
  }

 }
}