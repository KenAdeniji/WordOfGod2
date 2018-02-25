using System;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Web;

namespace WordEngineering
{
 ///<summary>UtilityManagement</summary>
 public class UtilityManagement
 {

  /// <summary>Main()</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Stub();
  }//public static void Main

  /// <summary>Stub()</summary>
  public static void Stub()
  {
  }//public static void Stub()

  /// <summary>ManagementSelectQuery()</summary>
  /// <remarks>Subject: Distinguish between server os and workstation 7/27/2005 12:56 AM PST By: Mattias Sjögren In: microsoft.public.dotnet.languages.csharp Mattias Sjögren [MVP]  mattias @ mvps.org http://www.msjogren.net/dotnet/ | http://www.dotnetinterop.com</remarks>
  public static void ManagementSelectQuery()
  {
   string      methodCalling      =  null;
   string      methodCurrentName  =  null;

   MethodBase  methodCurrent      =  null;
   StackTrace  stackTrace         =  new StackTrace();
   Type        typeBase           =  null;
   Type        typeCurrent        =  null;
      
   methodCurrent      =  MethodBase.GetCurrentMethod();
   methodCurrentName  =  MethodBase.GetCurrentMethod().Name;
   typeCurrent        =  MethodBase.GetCurrentMethod().DeclaringType;
   typeBase           =  typeCurrent.BaseType;

   methodCalling      =  stackTrace.GetFrame(1).GetMethod().Name;

   ManagementSelectQuery( methodCalling );

  }

  /// <summary>ManagementSelectQuery()</summary>
  public static void ManagementSelectQuery
  (
   string query
  )
  {
   string                    exceptionMessage          =  null;
   ManagementObjectSearcher  managementObjectSearcher  =  null;
   SelectQuery               selectQuery               =  null;
   try
   {
  	selectQuery               =  new SelectQuery( query );
    managementObjectSearcher  =  new ManagementObjectSearcher( selectQuery );
    foreach( ManagementObject managementObject in managementObjectSearcher.Get() )
    {
     foreach ( PropertyData propertyData in managementObject.Properties )
     {
      System.Console.WriteLine
      (
       "Property: {0}, Value: {1}",
       propertyData.Name, 
       propertyData.Value
      );
     }//foreach ( PropertyData propertyData in managementObject.Properties )
    }//foreach( ManagementObject managementObject in managementObjectSearcher.Get() )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
  }//ManagementSelectQuery
  
  ///<summary>LocalRootNamespacesClasses</summary>
  ///<remarks>Willy Denoyette [MVP] http://www.msdn.microsoft.com/newsgroups/default.aspx?pg=184&amp;guid=&amp;sloc=en-us&amp;dg=microsoft.public.dotnet.languages.csharp&amp;fltr=</remarks> 
  public static void LocalRootNamespacesClasses()
  {
   HttpContext                 httpContext                 =  HttpContext.Current;
   string                      cimRoot = "root\\";
   string                      exceptionMessage            =  null;
   EnumerationOptions          enumerationOptions;
   ManagementClass             managementClass;
   ManagementClass             managementClassSub;
   ManagementObjectCollection  managementObjectCollection;

   try
   {
    managementClass = new ManagementClass
    ( 
     new ManagementScope(@"root"),
     new ManagementPath("__namespace"),
     null
    );
 
    enumerationOptions = new EnumerationOptions();
    enumerationOptions.EnumerateDeep = true; // set to false if only the root classes are needed

    foreach ( ManagementObject managementObject in managementClass.GetInstances() )
    {
     System.Console.WriteLine(cimRoot + managementObject["Name"].ToString());
     managementClassSub = new ManagementClass
     (
      cimRoot + managementObject["Name"].ToString()
     );
     managementObjectCollection = managementClassSub.GetSubclasses( enumerationOptions );

     foreach( ManagementObject managementObjectSub in managementObjectCollection )
     {
      if ( managementObjectSub["__SuperClass"] == null )
      {
       System.Console.WriteLine( managementObjectSub["__Class"] ) ;
      }//if ( managementObjectSub["__SuperClass"] == null )
      else
      {
       System.Console.WriteLine( "\t" + managementObjectSub["__Class"] );
      }
     }//foreach( ManagementObject managementObjectSub in managementObjectCollection )
    }//foreach ( ManagementObject managementObject in managementClass.GetInstances() )
   }//try
   catch( System.Exception exception )
   {
    exceptionMessage = "System.Exception" + exception.Message;
   }
   #if ( DEBUG )
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     httpContext.Response.Write( exceptionMessage );
    }//else if ( httpContext == null )
   #endif
 
  }//public static void LocalRootNamespacesClasses()

  /// <summary>Win32_ComputerSystem()</summary>
  public static void Win32_ComputerSystem() { ManagementSelectQuery(); }

  /// <summary>Win32_DiskDrive()</summary>
  public static void Win32_DiskDrive() { ManagementSelectQuery(); }

  /// <summary>Win32_LogicalDisk()</summary>
  public static void Win32_LogicalDisk() { ManagementSelectQuery(); }

  /// <summary>Win32_NetworkAdapter()</summary>
  public static void Win32_NetworkAdapter() { ManagementSelectQuery(); }

  /// <summary>Win32_OperatingSystem()</summary>
  public static void Win32_OperatingSystem() { ManagementSelectQuery(); }

  /// <summary>Win32_PhysicalMemory()</summary>
  public static void Win32_PhysicalMemory() { ManagementSelectQuery(); }

  /// <summary>Win32_Processor()</summary>
  public static void Win32_Processor() { ManagementSelectQuery(); }

  /// <summary>Win32_Process()</summary>
  public static void Win32_Process() { ManagementSelectQuery(); }

  /// <summary>Win32_Product()</summary>
  public static void Win32_Product() { ManagementSelectQuery(); }

  /// <summary>Win32_Service()</summary>
  public static void Win32_Service() { ManagementSelectQuery(); }

  /// <summary>Win32_UserAccount()</summary>
  public static void Win32_UserAccount() { ManagementSelectQuery(); }

  /// <summary>Win32_VideoController()</summary>
  /// <remarks>Subject: Reading Video Card String Description 7/18/2005 10:14 AM PST By: Nicholas Paldino [.NET/C# MVP] In: microsoft.public.dotnet.languages.csharp</remarks>
  public static void Win32_VideoController() { ManagementSelectQuery(); }

 }//public class UtilityManagement
 
}//namespace WordEngineering