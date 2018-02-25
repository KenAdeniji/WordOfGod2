using System;

namespace WordEngineering
{
 /// <summary>UtilityClass.</summary>   
 public class UtilityClass
 {
  /// <summary>Full Name.</summary>
  /// <param name="instance">An object, a class instance.</param>  
  public static string FullName
  (
   object instance
  )
  {
   return ( instance.GetType().ToString() );
  }//public static string Fullname

  /// <summary>Simple Name.</summary>
  /// <param name="instance">An object, a class instance.</param>  
  public static string SimpleName
  (
   object instance
  )
  {
   string fullName   = null;
   string simpleName = null;
   
   fullName = FullName ( instance );
   simpleName = fullName.Substring( fullName.LastIndexOf('.') + 1 );
   return simpleName;
  }//public static string SimpleName

  /// <summary>GetType.</summary>
  /// <param name="instance">An object, a class instance.</param>  
  public static Type GetType
  (
   object instance
  )
  {
   return instance.GetType();
  }//public static Type GetType
 
 }//public class UtilityClass
}//namespace WordEngineering. 
