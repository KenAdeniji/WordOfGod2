using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web;

namespace WordEngineering
{
 /// <summary>Utility graph chart.</summary>
 /// <remarks>Utility graph chart.</remarks>
 public class UtilityGraphChart
 {
 	
  /// <summary>BitMap pixels height.</summary>
  public const int BitMapPixelsHeight   = 1;

  /// <summary>BitMap pixels width.</summary>
  public const int BitMapPixelsWidth    = 1;

  /// <summary>XAxis space between bars.</summary>
  public const int XAxisSpaceBetweenBars = 35;
  
  /// <summary>Y-Axis scale.</summary>  
  public const int YAxisScale            = 10;

  /// <summary>The Graph chart type include bar, pie.</summary>
  public static readonly string[] GraphChartType = new string []
                                   {
                                    "Bar",
                                    "Pie",
                                   };                                    	
  
  /// <summary>Argument values for example, filenameXml, chart graph type, x-axis, y-axis.</summary>
  public static readonly string[] ArgumentValues = new string []
                                   {
                                    "filenameXml",
                                    "chartGraphType",
                                    "xAxis",
                                    "yAxis"
                                   };                                    	

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
  }//public static void Main
 
  /// <summary>GraphChart().</summary>
  public static void GraphChart
  (
   ref DataSet dataSet,
   ref String  chartGraphType,
   ref String  xAxis,
   ref String  yAxis
  )
  {
   NameValueCollection  nameValueCollectionRequest = null;
   
   int                  nameValueCollectionKeyIndexChartGraphType = -1;
   int                  nameValueCollectionKeyIndexFilenameXml    = -1;
   int                  nameValueCollectionKeyIndexXAxis          = -1;
   int                  nameValueCollectionKeyIndexYAxis          = -1;
   
   String               exceptionMessage           = null;
   String[]             nameValueCollectionKey     = null;
   String[][]           nameValueCollectionValue   = null;

   HttpContext          httpContext                = HttpContext.Current;

   dataSet = new DataSet();
   
   // Load NameValueCollection object.
   nameValueCollectionRequest = httpContext.Request.QueryString;

   UtilityCollection.NameValueCollectionExtract
   (
        nameValueCollectionRequest,
    ref nameValueCollectionKey,
    ref nameValueCollectionValue
   );
   
   nameValueCollectionKeyIndexFilenameXml    = Array.IndexOf( nameValueCollectionKey, ArgumentValues[0] );
   nameValueCollectionKeyIndexChartGraphType = Array.IndexOf( nameValueCollectionKey, ArgumentValues[1] );
   nameValueCollectionKeyIndexXAxis          = Array.IndexOf( nameValueCollectionKey, ArgumentValues[2] );
   nameValueCollectionKeyIndexYAxis          = Array.IndexOf( nameValueCollectionKey, ArgumentValues[3] );
   
   if ( nameValueCollectionKeyIndexChartGraphType >= 0 )
   {
   	chartGraphType = nameValueCollectionValue[nameValueCollectionKeyIndexChartGraphType][0];
   }//if ( nameValueCollectionKeyIndexChartGraphType >= 0 )

   if ( nameValueCollectionKeyIndexXAxis >= 0 )
   {
   	xAxis = nameValueCollectionValue[nameValueCollectionKeyIndexXAxis][0];
   }//if ( nameValueCollectionKeyIndexXAxis >= 0 )

   if ( nameValueCollectionKeyIndexYAxis >= 0 )
   {
   	yAxis = nameValueCollectionValue[nameValueCollectionKeyIndexYAxis][0];
   }//if ( nameValueCollectionKeyIndexYAxis >= 0 )
      
   for 
   ( 
    int nameValueCollectionValueIndex = 0;
    nameValueCollectionValueIndex < nameValueCollectionValue[nameValueCollectionKeyIndexFilenameXml].Length;
    ++nameValueCollectionValueIndex
   ) 
   {
    #if (DEBUG)
     if ( httpContext == null )
     {
      System.Console.WriteLine( nameValueCollectionValue[nameValueCollectionKeyIndexFilenameXml][nameValueCollectionValueIndex] );
     } 	
     else
     {
      httpContext.Response.Write( nameValueCollectionValue[nameValueCollectionKeyIndexFilenameXml][nameValueCollectionValueIndex] );
     } 	
    #endif
    UtilityXml.ReadXml
    (
      ref dataSet,
      ref exceptionMessage,
      ref nameValueCollectionValue[nameValueCollectionKeyIndexFilenameXml][nameValueCollectionValueIndex]
    );
   }//foreach ( String filenameXml in nameValueCollectionValue )

   UtilityDatabase.PrintValues
   (
    dataSet
   );

  }//public static void GraphChart()
  
  /// <summary>GetColor.</summary>
  public static Color GetColor
  (
   int indexColor 
  )
  {
   Color color = Color.Blue;
   
   switch( indexColor )
   {
   	case 0:
   	 color = Color.Blue;
   	 break;

   	case 1:
   	 color = Color.Red;
   	 break;

   	case 2:
   	 color = Color.Yellow;
   	 break;

   	case 3:
   	 color = Color.Peru;
   	 break;

   	case 4:
   	 color = Color.Orange;
   	 break;

   	case 5:
   	 color = Color.Coral;
   	 break;

   	case 6:
   	 color = Color.Gray;
   	 break;

   	case 7:
   	 color = Color.Maroon;
   	 break;

   	case 8:
   	 color = Color.Green;
   	 break;
   	 
   }//switch( indexColor )
   
   return ( color );
   
  }//public Color GetColor()	 

 }//public class UtilityGraphChart
}//namespace WordEngineering 