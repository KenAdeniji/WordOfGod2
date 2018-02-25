using System;
using System.Management;
using System.Runtime.InteropServices;

namespace WordEngineering
{
 ///<summary>UtilityMemory</summary>
 ///<remarks>
 /// http://blog.opennetcf.org/ayakhnin/PermaLink.aspx?guid=f1df04aa-99c0-4e2a-906d-6fcf0c4b0d59  Alex Yakhnin - How to free up memory programmatically.
 ///</remarks>
 public class UtilityMemory
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
   MemoryStatus();
   MemoryManagement();
   MemoryPerformanceCounter();
  }//public static void Stub()

  ///<summary>MemoryStatus()</summary>
  public static void MemoryStatus()
  {
   MEMORYSTATUS memoryStatus = new MEMORYSTATUS();
   GlobalMemoryStatus( ref memoryStatus );
   
   System.Console.WriteLine("dwLength: {0}", memoryStatus.dwLength);
   System.Console.WriteLine("dwMemoryLoad: {0}", memoryStatus.dwMemoryLoad);
   System.Console.WriteLine("dwTotalPhys: {0}", memoryStatus.dwTotalPhys);
   System.Console.WriteLine("dwAvailPhys: {0}", memoryStatus.dwAvailPhys);
   System.Console.WriteLine("dwTotalPageFile: {0}", memoryStatus.dwTotalPageFile);
   System.Console.WriteLine("dwAvailPageFile: {0}", memoryStatus.dwAvailPageFile);
   System.Console.WriteLine("dwTotalVirtual: {0}", memoryStatus.dwTotalVirtual);
   System.Console.WriteLine("dwAvailVirtual: {0}", memoryStatus.dwAvailVirtual);   
  }//MemoryStatus()	 

  ///<summary>FreeProgramMemory()</summary>
  public static bool FreeProgramMemory
  (
   int memSought
  )
  {                 
   bool result = true;
   MEMORYSTATUS memStatus = new MEMORYSTATUS();
   GlobalMemoryStatus( ref memStatus );
   //check if we're out of memory first
   if( memStatus.dwAvailPhys < memSought )
   {
    if (SHCloseApps(memSought)!= 0)
    {
     result = false;
    } 
   }//if( memStatus.dwAvailPhys < memSought )

   return result;
  }//public bool FreeProgramMemory( int memSought )

  //Required P/Invoke declarations
  ///<sumary>SHCloseApps</sumary>  
  [DllImport("aygshell.dll")]
  public static extern int SHCloseApps
  (
   int  dwMemSought
  );

  ///<sumary>GlobalMemoryStatus</sumary>
  ///<remarks>
  ///[DllImport("coredll.dll")]
  ///</remarks>
  [DllImport("kernel32.dll")]
  public static extern  void GlobalMemoryStatus
  ( 
   ref MEMORYSTATUS lpBuffer 
  );

  ///<summary>MEMORYSTATUS</summary>
  public struct MEMORYSTATUS 
  { 
   
   ///<summary>dwLength</summary>
   public int dwLength;
   
   ///<summary>dwMemoryLoad</summary>
   public int dwMemoryLoad; 
   
   ///<summary>dwTotalPhys</summary>
   public int dwTotalPhys; 
   
   ///<summary>dwAvailPhys</summary>
   public int dwAvailPhys; 
   
   ///<summary>dwTotalPageFile</summary>
   public int dwTotalPageFile;
   
   ///<summary>dwAvailPageFile</summary>
   public int dwAvailPageFile;
    
   ///<summary>dwTotalVirtual</summary>
   public int dwTotalVirtual; 
   
   ///<summary>dwAvailVirtual</summary>   
   public int dwAvailVirtual; 
   
  }//public struct MEMORYSTATUS

  ///<summary>MemoryManagement()</summary>
  ///<remarks>Wessel Troost</remarks>
  public static void MemoryManagement()
  {
   ManagementScope scope = new ManagementScope("\\root\\cimv2");
   scope.Connect();

   ObjectQuery query = new ObjectQuery("SELECT * FROM  Win32_OperatingSystem");
   ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

   ManagementObjectCollection queryCollection = searcher.Get();
   
   foreach ( ManagementObject m in queryCollection )
   {
    System.Console.WriteLine
    (
     "Available memory: {0}",
     m["FreePhysicalMemory"]
    );
   }//foreach ( ManagementObject m in queryCollection )

   query = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
   searcher = new ManagementObjectSearcher(scope, query);
   queryCollection = searcher.Get();
   foreach (ManagementObject m in queryCollection)
   {
    System.Console.WriteLine
    (
     "Total memory: {0}",
     m["TotalPhysicalMemory"]
    );
   }
  }//public static void MemoryManagement()

  ///<summary>MemoryPerformanceCounter()</summary>
  public static void MemoryPerformanceCounter()
  {
   System.Diagnostics.PerformanceCounter  performanceCounter;
   
   try
   {
    performanceCounter = new System.Diagnostics.PerformanceCounter();
    performanceCounter.CategoryName = "Memory";
    performanceCounter.CounterName = "System Code Total Bytes";
    System.Console.WriteLine( "Memory System Code Total Bytes: {0}", performanceCounter.RawValue.ToString() );
    performanceCounter.CounterName = "Available MBytes";
    System.Console.WriteLine( "Memory Available MBytes: {0}", performanceCounter.RawValue.ToString() );
   }//try
   catch ( Exception exception )
   {
   	System.Console.WriteLine("Exception: {0}", exception.Message );
   }//catch ( Exception exception )
  }//public static void MemoryPerformanceCounter()	
 }//public class UtilityMemory
 
}//namespace WordEngineering