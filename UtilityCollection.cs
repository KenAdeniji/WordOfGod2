/*
 200303210923 Created.
 REFERENCE
 C# Programmer's ArrayList.IndexOf Method 
  ms-help://MS.NETFrameworkSDKv1.1/cpref/html/frlrfSystemCollectionsArrayListClassIndexOfTopic.htm
*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace WordEngineering
{
 /// <summary>UtilityCollection will offer collection services.</summary>
 /// <remarks>UtilityCollection will offer collection services.</remarks>
 public class UtilityCollection
 {
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
  }//public static void Main
 	
  /// <summary>Concatenate index and values</summary>
  /// <param name="list">Supports a simple iteration over a collection.</param>   
  /// <code>
  ///  <example>
  ///   ArrayList arrayListBibleBook = new ArrayList();
  ///   arrayListBibleBook.Add("Genesis");
  ///   arrayListBibleBook.Add("Exodus");
  ///   arrayListBibleBook.Add("Leveticus");
  ///   arrayListBibleBook.Add("Numbers");
  ///   arrayListBibleBook.Add("Deuteronomy");   
  ///   System.Console.WriteLine("Bible Books Collection: {0}", UtilityCollection.ToString(arrayListBibleBook));
  ///  </example>  
  /// </code>  
  public static string ToString( IEnumerable list )
  {
   StringBuilder                  sb         = new StringBuilder();
   System.Collections.IEnumerator enumerator = list.GetEnumerator();    
   while ( enumerator.MoveNext() )
   {
    sb.Append(enumerator.Current);
    sb.Append(ElementSeparator);     
   }//while ( enumerator.MoveNext() )
   return sb.ToString();
  }//public override string ToString( IEnumerable list ) 

  /// <summary>Returns the index of the first occurrence of a value in a two-dimensional Array.</summary>
  /// <param name="array">The array.</param>
  /// <param name="key">The key.</param>  
  public static string IndexOf
  (
   string[][] array,
   string     key
  )
  {
   return IndexOf( array, key, 0, 1 );
  }
   
  /// <summary>Returns the index of the first occurrence of a value in a two-dimensional Array.</summary>
  /// <param name="array">The array.</param>
  /// <param name="key">The key.</param>  
  /// <param name="rankKey">The array dimension's key.</param>    
  /// <param name="rankValue">The array dimension's value.</param>      
  public static string IndexOf
  (
   string[][] array,
   string     key,
   int        rankKey,
   int        rankValue
  )
  {
   int    index  = -1;
   int    length = array.Length;
   string value  = null;
   for ( index = 0; index < length; ++index )
   {
    if ( string.Compare( array[index][rankKey], key, true ) == 0 )
    {
     value = array[index][rankValue];
     break;
    }//if ( string.Compare( array[index][rankKey], rankValue, true ) == 0 )
   }//for ( index = 0; index < length; ++index )
   return ( value );
  }//public static string IndexOf( string[][] array, int rankKey, int rankValue )

  /// <summary>NameValueCollection: Extract the key and value.</summary>
  /// <param name="nameValueCollection">The name value collection.</param>
  /// <param name="nameValueCollectionKey">The name value collection key.</param>
  /// <param name="nameValueCollectionValue">The name value collection value.</param>  
  public static void NameValueCollectionExtract
  (
       NameValueCollection nameValueCollection,
   ref String[]            nameValueCollectionKey,
   ref String[][]          nameValueCollectionValue
  )
  {

   int          nameValueCollectionKeyIndex   = 0;
   int          nameValueCollectionKeyTotal   = 0;

   int          nameValueCollectionValueIndex = 0;
   int          nameValueCollectionValueTotal = 0;

   HttpContext  httpContext                   = HttpContext.Current;

   nameValueCollectionKeyTotal = nameValueCollection.Count;
   nameValueCollectionKey      = new String[nameValueCollectionKeyTotal];
   nameValueCollectionKey      = nameValueCollection.AllKeys;

   nameValueCollectionValue    = new String[nameValueCollectionKeyTotal][];
   	   
   for 
   ( 
    nameValueCollectionKeyIndex = 0;
    nameValueCollectionKeyIndex < nameValueCollectionKeyTotal;
    ++nameValueCollectionKeyIndex
   )
   {
   	nameValueCollectionValue[ nameValueCollectionKeyIndex ] = new String[nameValueCollectionKeyIndex];
   	nameValueCollectionValue[ nameValueCollectionKeyIndex ] = 
   	nameValueCollection.GetValues(nameValueCollectionKey[nameValueCollectionKeyIndex]);
   	
    if ( httpContext != null )
    {
   	 nameValueCollectionKey[nameValueCollectionKeyIndex] = httpContext.Server.HtmlEncode( nameValueCollectionKey[nameValueCollectionKeyIndex] );
     #if (DEBUG)
      httpContext.Response.Write("Key: " + nameValueCollectionKey[nameValueCollectionKeyIndex] + "<br/>");
     #endif
    }//if ( httpContext != null )      

   	nameValueCollectionValueTotal = nameValueCollectionValue[ nameValueCollectionKeyIndex ].Length;
    for 
    ( 
     nameValueCollectionValueIndex = 0;
     nameValueCollectionValueIndex < nameValueCollectionValueTotal;
     ++nameValueCollectionValueIndex
    )
    {
     if ( httpContext != null )
     {
      nameValueCollectionValue[nameValueCollectionKeyIndex][nameValueCollectionValueIndex] = httpContext.Server.HtmlEncode( nameValueCollectionValue[nameValueCollectionKeyIndex][nameValueCollectionValueIndex] );
      #if (DEBUG)
       httpContext.Response.Write("Value: " + nameValueCollectionValue[nameValueCollectionKeyIndex][nameValueCollectionValueIndex] + "<br/>");
      #endif
     }//if ( httpContext != null )      
    }//for ( nameValueCollectionValueIndex = 0; nameValueCollectionValueIndex < nameValueCollectionValueTotal; ++nameValueCollectionValueIndex )
   }//for ( nameValueCollectionKeyIndex = 0; nameValueCollectionKeyIndex < nameValueCollectionKeyTotal; ++nameValueCollectionKeyIndex )
  }//public static void NameValueCollectionExtract( NameValueCollection nameValueCollection, ref String[] nameValueCollectionKey, ref String[][] nameValueCollectionValue )

  ///<summary>PrintKeysAndValues()</summary>
  public static void PrintKeysAndValues
  (
   Hashtable  hashtable
  )
  {
   HttpContext            httpContext            =  HttpContext.Current;
   IDictionaryEnumerator  iDictionaryEnumerator;
   
   iDictionaryEnumerator  =  hashtable.GetEnumerator();
  
   while (  iDictionaryEnumerator.MoveNext() )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine
     (
      "{0} | {1}",
      iDictionaryEnumerator.Key, 
      iDictionaryEnumerator.Value
     );
    }
    else
    {
     httpContext.Response.Write( "Key: " + iDictionaryEnumerator.Key + " | " + " Value: " + iDictionaryEnumerator.Value + "<br/>" );
    }	 
   }//while (  iDictionaryEnumerator.MoveNext() )
  }//public static void PrintKeysAndValues() 
   
  /// <summary>The element separator.</summary>
  public const char ElementSeparator = '|';

 }//public class UtilityCollection
}//namespace WordEngineering