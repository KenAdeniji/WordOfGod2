using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Web;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WordEngineering
{

 /// <summary>UtilityProperty</summary>
 public class UtilityProperty
 {

  ///<summary>FormatNamevalue.</summary>  
  public static   String   FormatNamevalue           = @"{0} LIKE '%{1}%'";

  ///<summary>TypeInformation.</summary>
  public static void TypeInformation
  (
       object        objectInstance,
   ref Type          typeClass,
   ref StringBuilder sbTypeNameValue,
   ref String[]      typeName
  )
  {
   TypeInformation
   (
        objectInstance,
    ref typeClass,
    ref sbTypeNameValue,
    ref typeName,
    ref FormatNamevalue
   );
  }//public static void TypeInformation()
  
  ///<summary>TypeInformation.</summary>
  public static void TypeInformation
  (
       object        objectInstance,
   ref Type          typeClass,
   ref StringBuilder sbTypeNameValue,
   ref String[]      typeName,
   ref String        formatNameValue
  )
  {
   int           indexTypeName         =  -1;
   int           getValueIndex         =  -1;
   
   String        fieldInfoCurrentName  =  null;

   FieldInfo[]   fieldInfo             =  null;
   
   object[]      getValue              =  null;

   sbTypeNameValue                     =  null;
   
   //Get the type and fields
   fieldInfo = typeClass.GetFields();
   
   foreach( FieldInfo fieldInfoCurrent in fieldInfo )
   {

    /*
    System.Console.WriteLine("\nName          : {0}", fieldInfoCurrent.Name);
    System.Console.WriteLine("Declaring Type  : {0}", fieldInfoCurrent.DeclaringType);
    System.Console.WriteLine("IsPublic        : {0}", fieldInfoCurrent.IsPublic);
    System.Console.WriteLine("MemberType      : {0}", fieldInfoCurrent.MemberType);
    System.Console.WriteLine("FieldType       : {0}", fieldInfoCurrent.FieldType);
    System.Console.WriteLine("IsFamily        : {0}", fieldInfoCurrent.IsFamily);
    System.Console.WriteLine("GetValue        : {0}", fieldInfoCurrent.GetValue(this));
    */
    
    fieldInfoCurrentName = fieldInfoCurrent.Name;
    
    indexTypeName = Array.IndexOf(typeName, fieldInfoCurrentName);
    
    if ( indexTypeName < 0 )
    {
     continue;
    }//if ( indexTypeName < 0 )     	

    //if ( fieldInfoCurrent.FieldType.IsArray )
    //typeof(Array).IsAssignableFrom(type)
    if ( typeof(Array).IsAssignableFrom(fieldInfoCurrent.FieldType) )
    {
     getValue = (object[]) fieldInfoCurrent.GetValue(objectInstance);
     for( getValueIndex = 0; getValueIndex < getValue.Length; ++getValueIndex )
     {
      UtilityDebug.Write
      (
       String.Format
       (
        "{0}[{1}]: {2}",
        fieldInfoCurrentName, 
        getValueIndex, 
        getValue[getValueIndex]
       )    
      );

      TypeInformationNameValue
      (
       ref sbTypeNameValue,
       ref fieldInfoCurrentName,
           getValue[getValueIndex],
       ref formatNameValue
      );
     }//for( getValueIndex = 0; getValueIndex < getValue.Length; ++getValueIndex )
    }//if ( typeof(Array).IsAssignableFrom(fieldInfoCurrent.FieldType) )
    else
    {
     TypeInformationNameValue
     (
      ref sbTypeNameValue,
      ref fieldInfoCurrentName,
          getValue[getValueIndex],
      ref formatNameValue
     );
    }    	
    	
   }//foreach( FieldInfo fieldInfoCurrent in fieldInfo )
   
   UtilityDebug.Write
   (
    String.Format
    (
     "TypeInformation: {0}",
     sbTypeNameValue
    )    
   );
  }//public static void TypeInformation()  	

  ///<summary>TypeInformationNameValue.</summary>
  public static void TypeInformationNameValue
  (
   ref StringBuilder   sbTypeNameValue,
   ref String          typeName,
       object          typeValue,
   ref String          formatNameValue    
  )
  {

   if ( typeValue.ToString() == String.Empty )
   {
    return;
   }      	

   if ( sbTypeNameValue == null )
   {
    sbTypeNameValue = new StringBuilder();       	
   }//if ( sbTypeNameValue.Length == 0 )
   else
   {
   	sbTypeNameValue.Append(" OR ");
   }
   
   sbTypeNameValue.AppendFormat
   (
    formatNameValue,
    typeName,
    typeValue
   );
    
  }//public void TypeInformationAppend()

 }//public class UtilityProperty

}//namespace WordEngineering