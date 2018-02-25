using System;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Web;

namespace WordEngineering
{

 ///<summary>UtilityWindowsManagementInstrumentationWMI.</summary>
 ///<remarks>
 /// PerfMon.exe
 ///</remarks>
 public class UtilityWindowsManagementInstrumentationWMI
 {

  /// <summary>RankWMIPath.</summary>
  public const   int        RankWMIPath = 0;

  /// <summary>RankWMIPerformanceCounterCategoryName.</summary>
  public const   int        RankWMIPerformanceCounterCategoryName = 0;

  /// <summary>RankWMIPerformanceCounterCounterName.</summary>
  public const   int        RankWMIPerformanceCounterCounterName = 1;

  /// <summary>ThreadSleep.</summary>
  public static  int        ThreadSleep = 10000;

  /// <summary>TimerClock.</summary>
  public static  int        TimerClock  = 1000;

  /// <summary>DefaultProcess</summary>
  public static  String     DefaultProcess = "Calc.exe";
  
  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml   = @"WordEngineering.config";

  /// <summary>SNMPRequest.</summary>
  public static  String[][] WMIPath = new String[][]
                                          {
                                           new String[] { "Win32_Process" },
                                           new String[] { "Win32_Service" },
                                           new String[] { "Win32_Share" }
                                          };//                                            

  /// <summary>SNMPRequest.</summary>
  public static  String[][] WMIPerformanceCounter = new String[][]
                                          {
                                           new String[] { "Cache",                   "Data Map Hits %"  },
                                           new String[] { "LogicalDisk",             "% Free Space" },
                                           new String[] { "Memory",                  "Available Bytes" },
                                           new String[] { "Memory",                  "Cache Bytes" },
                                           new String[] { "Memory",                  "Committed Bytes" },                                           
                                           new String[] { "Memory",                  "Pages/sec" },
                                           new String[] { "Memory",                  "Page Reads/sec" },
                                           new String[] { "Memory",                  "Pool Paged Bytes" },
                                           new String[] { "Memory",                  "Pool Nonpaged Allocs" },
                                           new String[] { "Memory",                  "Pool Nonpaged Bytes" },
                                           new String[] { "Memory",                  "Transition Faults/sec" },
                                           new String[] { "Network Interface",       "Bytes total/sec" },
                                           new String[] { "Network Interface",       "Packets/sec" },
                                           new String[] { "Paging File",             "% Usage" },
                                           new String[] { "PhysicalDisk",            "% Disk Read Time"  },
                                           new String[] { "PhysicalDisk",            "% Disk Write Time" },
                                           new String[] { "PhysicalDisk",            "Avg. Disk Queue Length" },
                                           new String[] { "PhysicalDisk",            "Avg. Disk Bytes/Transfer" },
                                           new String[] { "PhysicalDisk",            "Avg. Disk sec/Transfer" },
                                           /*
                                           new String[] { "Processor",               "%Interrupt Time" },
                                           new String[] { "Processor",               "%Privileged Time" },
                                           new String[] { "Processor",               "%Processor Time" },
                                           new String[] { "Processor",               "%User Time" },
                                           new String[] { "Processor",               "Interrupts/sec" },
                                           */
                                           new String[] { "Server",                  "Bytes Received/sec" },
                                           new String[] { "Server",                  "Bytes Total/sec" },
                                           new String[] { "Server",                  "Bytes Transmitted/sec" },
                                           new String[] { "Server",                  "Pool Nonpaged Bytes" },
                                           new String[] { "Server",                  "Pool Paged Bytes" },
                                           new String[] { "Server",                  "Pool Paged Peak" },
                                           new String[] { "Server",                  "Work Item Shortages" },                                           
                                           new String[] { "Server Work Queues",      "Queue Length" },                                           
                                           new String[] { "System",                  "Context switches/sec" },
                                           new String[] { "System",                  "Processor Queue Length" },
                                          };
 
  /// <summary>PerformanceCounterCategoryInstanceNameTotal.</summary>
  public static  String     PerformanceCounterCategoryInstanceNameTotal = "_Total";
  
  /// <summary>Constructor.</summary>
  public UtilityWindowsManagementInstrumentationWMI()
  {

  }

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Stub();
  }//public static void Main( String[] argv )

  ///<summary>Stub.</summary>
  public static void Stub()
  {
   /*
   Win32_EnvironmentSynchronousInstanceEnumeration();
   Win32_EnvironmentAsynchronousInstanceEnumeration();
   */
   
   /*
   EventWatcherPolling();
   EventWatcherAynchronous();
   */

   /*
   InvokeMethodParameterObjectsSynchronous();
   InvokeMethodArgumentArraySynchronous();
   */
   
   /*
   InvokeMethodParameterObjectsAsynchronous();
   */
   
   /*
   Query();
   */
   
   QueryPerformanceCounter();
   
  }

  ///<summary>This example demonstrates how to perform an asynchronous instance enumeration.</summary>
  public static void Win32_EnvironmentAsynchronousInstanceEnumeration()
  {
  
   ManagementObjectSearcher     managementObjectSearcher     =  null;
   ManagementOperationObserver  managementOperationObserver  =  null;

   ObjectHandler                objectHandler                =  null;

   SelectQuery                  selectQuery                  =  null;
     	
   //Build a query for enumeration of Win32_Service instances
   selectQuery = new SelectQuery("Win32_Service");

   //Instantiate an object searcher with this query
   managementObjectSearcher = new ManagementObjectSearcher( selectQuery ); 
   
   //Create a results watcher object, and handler for results and completion
   managementOperationObserver  = new ManagementOperationObserver();
   objectHandler = new ObjectHandler();

   // Attach handler to events for results and completion
   managementOperationObserver.ObjectReady += new ObjectReadyEventHandler(objectHandler.NewObject);
   managementOperationObserver.Completed += new CompletedEventHandler(objectHandler.Done);

   //Call the asynchronous overload of Get() to start the enumeration
   managementObjectSearcher.Get( managementOperationObserver );
         
   //Do something else while results arrive asynchronously
   while (!objectHandler.IsCompleted) 
   {
    System.Threading.Thread.Sleep( ThreadSleep );
   }

   objectHandler.Reset();
     	
  }//public static void Win32_EnvironmentAsynchronousInstanceEnumeration()
  
  ///<summary>This example demonstrates how to perform a synchronous instance enumeration.</summary>
  public static void Win32_EnvironmentSynchronousInstanceEnumeration()
  {
   ManagementObjectSearcher  managementObjectSearcher  =  null;
   SelectQuery               selectQuery               =  null;
    	
   //Build a query for enumeration of Win32_Environment instances
   selectQuery = new SelectQuery("Win32_Environment");

   //Instantiate an object searcher with this query
   managementObjectSearcher = new ManagementObjectSearcher( selectQuery ); 

   //Call Get() to retrieve the collection of objects and loop through it
   foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
   {   
    System.Console.WriteLine
    (
     "Variable: {0}, Value = {1}", 
     managementBaseObject["Name"],
     managementBaseObject["VariableValue"]
    );
   }//foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
  }//public static void Win32_EnvironmentSynchronousInstanceEnumeration()

  ///<summary>
  /// This example shows synchronous consumption of events. The client
  /// is blocked while waiting for events. See additional example for
  /// asynchronous event handling.
  ///</summary>
  public static void EventWatcherPolling()
  {
   ManagementBaseObject     managementBaseObject    =  null;
   ManagementEventWatcher   managementEventWatcher  =  null;
   WqlEventQuery            wqlEventQuery           =  null;
   
   // Create event query to be notified within 1 second of 
   // a change in a service
   wqlEventQuery  = new WqlEventQuery
   (
    "__InstanceModificationEvent", 
    new TimeSpan(0,0,1), 
    "TargetInstance isa \"Win32_Service\""
   );

   // Initialize an event watcher and subscribe to events 
   // that match this query
   managementEventWatcher = new ManagementEventWatcher( wqlEventQuery );
      
   // Block until the next event occurs 
   // Note: this can be done in a loop if waiting for 
   //        more than one occurrence
   managementBaseObject = managementEventWatcher.WaitForNextEvent();

   //Display information from the event
   System.Console.WriteLine
   (
    "Service {0} has changed, State is {1}", 
    ((ManagementBaseObject)managementBaseObject["TargetInstance"])["Name"],
    ((ManagementBaseObject)managementBaseObject["TargetInstance"])["State"]
   );

   //Cancel the subscription
   managementEventWatcher.Stop();
  }//public static int EventWatcherPolling()

  ///<summary>
  /// This example shows synchronous consumption of events. The client
  /// is blocked while waiting for events. See additional example for
  /// asynchronous event handling.
  ///</summary>
  public static void EventWatcherAynchronous()
  {
   ManagementClass         managementClass         =  null;
   ManagementEventWatcher  managementEventWatcher  =  null;
   ManagementObject        managementObject        =  null;
   
   WqlEventQuery           wqlEventQuery           =  null;
   
   // Set up a timer to raise events every ...
   managementClass   =  new ManagementClass("__IntervalTimerInstruction");
   managementObject  =  managementClass.CreateInstance();
   
   managementObject["TimerId"] = "Timer1";
   managementObject["IntervalBetweenEvents"] = TimerClock;
   managementObject.Put();

   // Set up the event consumer Create event query to receive timer events
   wqlEventQuery  = new WqlEventQuery
   (
    "__TimerEvent",
    "TimerId=\"Timer1\""
   );

   // Initialize an event watcher and subscribe to 
   // events that match this query
   managementEventWatcher = new ManagementEventWatcher( wqlEventQuery );

   // Set up a listener for events
   managementEventWatcher.EventArrived += new EventArrivedEventHandler((new EventHandler()).HandleEvent);

   // Start listening
   managementEventWatcher.Start();

   // Do something in the meantime
   System.Threading.Thread.Sleep(ThreadSleep);
      
   // Stop listening
   managementEventWatcher.Stop();
  }//public static void EventWatcherAynchronous()

  ///<summary>RemoteConnect</summary>
  public static void RemoteConnect()
  {

   //String  domainName        =  null;
   String  exceptionMessage  =  null;
   String  userName          =  null;
   String  password          =  null;
   String  serverName        =  null;

   /*
   UtilityImpersonate.GetUsernamePasswordDomainName
   (
    ref  FilenameConfigurationXml,
    ref  userName,
    ref  domainName,
    ref  password,
    ref  exceptionMessage
   );
   
   serverName = Environment.MachineName;
   */
   
   RemoteConnect
   (
    ref  userName,
    ref  password,
    ref  serverName,
    ref  exceptionMessage
   );
      
  }//public static void RemoteConnect() 

  ///<summary>RemoteConnect</summary>
  ///<param name="userName">userName</param>
  ///<param name="password">password</param>
  ///<param name="serverName">serverName</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void RemoteConnect
  (
   ref  String  userName,
   ref  String  password,
   ref  String  serverName,
   ref  String  exceptionMessage
  ) 
  {
   //Build an options object for the connection
   ConnectionOptions  connectionOptions  =  null;
   
   ManagementScope    managementScope    =  null;
   
   try
   {
    connectionOptions = new ConnectionOptions();
  
    if ( userName != null && userName != String.Empty )
    {
     connectionOptions.Username  = userName;
    }
    
    if ( password != null && password != String.Empty )
    {
     connectionOptions.Password  = password;
    }
     
    if ( serverName == null || serverName == String.Empty )
    {
     serverName = Environment.MachineName;
    } 

    //Make a connection to a remote computer using these options
    managementScope = new ManagementScope
    (
     "\\\\" + serverName + "\\root\\cimv2", 
     connectionOptions
    );
   
    managementScope.Connect();
   }//try
   catch ( System.Management.ManagementException  managementException )
   {
    exceptionMessage = "System.Management.ManagementException " + managementException.Message;
    System.Console.WriteLine
    (
     "System.Management.ManagementException: {0}",
     managementException.Message
    ); 
   }//catch ( System.Management.ManagementException  managementException )
   catch ( Exception  exception )
   {
    exceptionMessage = "Exception " + exception.Message;
    System.Console.WriteLine
    (
     "Exception: {0}",
     exception.Message
    ); 
   }//catch ( System.Management.ManagementException  managementException )

  }//public static void RemoteConnect()

  ///<summary>InvokeMethodParameterObjectsSynchronous</summary>
  public static void InvokeMethodParameterObjectsSynchronous()
  {
   String  processParameter  =  null;
   String  exceptionMessage  =  null;
   
   processParameter = DefaultProcess;

   InvokeMethodParameterObjectsSynchronous
   (
    ref processParameter,
    ref exceptionMessage
   );
    
  }//public static void InvokeMethodParameterObjectsSynchronous()
    	  	
  ///<summary>InvokeMethodParameterObjectsSynchronous</summary>
  ///<param name="processParameter">processParameter</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void InvokeMethodParameterObjectsSynchronous
  (
   ref  String  processParameter,
   ref  String  exceptionMessage
  )
  { 
  
   ManagementClass       managementClass               =  null;
   ManagementBaseObject  managementBaseObjectParamIn   =  null;
   ManagementBaseObject  managementBaseObjectParamOut  =  null;   
   
   //Get the object on which the method will be invoked
   managementClass = new ManagementClass("Win32_Process");

   //Get an input parameters object for this method
   managementBaseObjectParamIn = managementClass.GetMethodParameters("Create");

   //Fill in input parameter values
   managementBaseObjectParamIn["CommandLine"] = processParameter;

   //Execute the method
   managementBaseObjectParamOut = managementClass.InvokeMethod
   (
    "Create",
    managementBaseObjectParamIn,
    null
   );

   //Display results
   //Note: The return code of the method is provided in the "returnValue" property of the outParams object
   System.Console.WriteLine
   (
    "Process Name: {0} | Return: {1} | Process Id: {2}",
    processParameter,    
    managementBaseObjectParamOut["returnValue"],
    managementBaseObjectParamOut["processId"]
   );
  }//public static void InvokeMethodParameterObjectsSynchronous

  ///<summary>InvokeMethodArgumentArraySynchronous</summary>
  public static void InvokeMethodArgumentArraySynchronous()
  {
   String  processParameter  =  null;
   String  exceptionMessage  =  null;
   
   processParameter = DefaultProcess;

   InvokeMethodArgumentArraySynchronous
   (
    ref processParameter,
    ref exceptionMessage
   );
    
  }//public static void InvokeMethodArgumentArraySynchronous()

  ///<summary>InvokeMethodArgumentArraySynchronous</summary>
  ///<param name="processParameter">processParameter</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void InvokeMethodArgumentArraySynchronous
  (
   ref  String  processParameter,
   ref  String  exceptionMessage
  )
  { 
  
   ManagementClass       managementClass               =  null;
   object[]              processArgument               =  null;
   object                processResult                 =  null;
   
   //Get the object on which the method will be invoked
   managementClass = new ManagementClass("Win32_Process");

   //Create an array containing all arguments for the method
   processArgument = new object[] { processParameter, null, null, 0 };

   //Execute the method
   processResult = managementClass.InvokeMethod
   (
    "Create",
    processArgument
   );

   //Display results
   //Note: The return code of the method is provided in the "returnValue" property of the outParams object
   System.Console.WriteLine
   (
    "Process Name: {0} | Return: {1} | Process Id: {2}",
    processParameter,    
    processResult,
    processArgument[3]
   );
  }//public static void InvokeMethodArgumentArraySynchronous()

  ///<summary>InvokeMethodParameterObjectsAsynchronous</summary>
  public static void InvokeMethodParameterObjectsAsynchronous()
  {
   String  processParameter  =  null;
   String  exceptionMessage  =  null;
   
   processParameter = DefaultProcess;

   InvokeMethodParameterObjectsAsynchronous
   (
    ref processParameter,
    ref exceptionMessage
   );
    
  }//public static void InvokeMethodParameterObjectsAsynchronous()
    	  	
  ///<summary>InvokeMethodParameterObjectsAsynchronous</summary>
  ///<param name="processParameter">processParameter</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void InvokeMethodParameterObjectsAsynchronous
  (
   ref  String  processParameter,
   ref  String  exceptionMessage
  )
  { 
  
   ManagementClass              managementClass               =  null;
   ManagementBaseObject         managementBaseObjectParamIn   =  null;
   ManagementOperationObserver  managementOperationObserver   =  null;
   ObjectReadyHandler           objectReadyHandler            =  null;
   
   //Get the object on which the method will be invoked
   managementClass = new ManagementClass("Win32_Process");

   // Create a results and completion handler
   managementOperationObserver  =  new ManagementOperationObserver();
   objectReadyHandler           =  new ObjectReadyHandler();
   managementOperationObserver.ObjectReady += new ObjectReadyEventHandler
   (
    objectReadyHandler.NewObject
   );

   //Get an input parameters object for this method
   managementBaseObjectParamIn = managementClass.GetMethodParameters("Create");

   //Fill in input parameter values
   managementBaseObjectParamIn["CommandLine"] = processParameter;

   //Execute the method
   managementClass.InvokeMethod
   (
    managementOperationObserver,
    "Create",
    managementBaseObjectParamIn,
    null
   );

   //Do something while method is executing
   while( !objectReadyHandler.IsComplete ) 
   {
    System.Threading.Thread.Sleep( ThreadSleep );
   }

   //After execution is completed, display results
   //Note: The return code of the method is provided in the "returnValue" property of the outParams object
   System.Console.WriteLine
   (
    "Process Name: {0} | Return: {1} | Process Id: {2}",
    processParameter,    
    objectReadyHandler.ManagementBaseObjectResult["returnValue"],
    objectReadyHandler.ManagementBaseObjectResult["processId"]
   );
  }//public static void InvokeMethodParameterObjectsAsynchronous

  ///<summary>Query</summary>
  public static void Query()
  {
   String  exceptionMessage  =  null;
   
   foreach( String[] WMIPathCurrent in WMIPath )
   {
   	Query
   	(
   	 ref WMIPathCurrent[RankWMIPath],
   	 ref exceptionMessage
   	); 
   }//foreach( String[] WMIPathCurrent in WMIPath )   	
  }//public static void Query()

  ///<summary>Query</summary>
  ///<param name="WMIPath">WMIPath</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void Query
  (
   ref  String  WMIPath,
   ref  String  exceptionMessage
  )
  {
   ManagementClass             managementClass             =  null;
   ManagementObjectCollection  managementObjectCollection  =  null;
   ManagementObjectSearcher    managementObjectSearcher    =  null;
   QualifierDataCollection     qualifierDataCollection     =  null;
   PropertyDataCollection      propertyDataCollection      =  null;
   SelectQuery                 selectQuery                 =  null;   

   try
   {
    managementClass = new ManagementClass( WMIPath );
    managementClass.Options.UseAmendedQualifiers = true;
     
    qualifierDataCollection = managementClass.Qualifiers;
     
    foreach ( QualifierData qualifierData in qualifierDataCollection )
    {
     System.Console.WriteLine
     (
      qualifierData.Name + " = " + qualifierData.Value
     );
    }//foreach ( QualifierData qualifierData in qualifierDataCollection )
    
    //Build a query for enumeration of instances
    selectQuery = new SelectQuery( WMIPath );

    //Instantiate an object searcher with this query
    managementObjectSearcher = new ManagementObjectSearcher( selectQuery ); 
    
    //Call Get() to retrieve the collection of objects and loop through it
    managementObjectCollection = managementObjectSearcher.Get();

    foreach (ManagementBaseObject managementBaseObject in managementObjectCollection )
    {   
     propertyDataCollection = managementBaseObject.Properties;
     
     foreach ( PropertyData propertyData in propertyDataCollection )
     {
      System.Console.WriteLine
      (
       "{0}: {1}",
       propertyData.Name,
       managementBaseObject[ propertyData.Name ]
      );
     }//foreach ( PropertyData propertyData in propertyDataCollection )
         	
    }//foreach (ManagementBaseObject managementBaseObject in managementObjectCollection )
    
   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }//catch ( Exception exception ) 
  }//public static void Query()  	

  ///<summary>QueryPerformanceCounter</summary>
  public static void QueryPerformanceCounter()
  {
   String[][]  WMIPerformanceCounterMonitoringConfiguration  =  null;
   String      exceptionMessage                              =  null;
  
   WMIPerformanceCounterMonitoringConfiguration = WMIPerformanceCounter; 
  
   QueryPerformanceCounter
   (
    ref  WMIPerformanceCounterMonitoringConfiguration,
    ref  exceptionMessage
   );	
  }//public static void QueryPerformanceCounter()
  
  ///<summary>QueryPerformanceCounter</summary>
  ///<param name="WMIPerformanceCounterMonitoringConfiguration">WMIPath</param>
  ///<param name="exceptionMessage">exceptionMessage</param>
  public static void QueryPerformanceCounter
  (
   ref  String[][]  WMIPerformanceCounterMonitoringConfiguration,
   ref  String      exceptionMessage
  )
  {
   float                       performanceCounterNextValue             =  0;
   
   String                      performanceCounterCategoryNameCurrent   =  null;
   String                      performanceCounterCounterNameCurrent    =  null;   
   String                      performanceCounterInstanceNameCurrent   =  null;   
   
   String[]                    performanceCounterCategoryInstanceName  =  null;
   
   PerformanceCounter          performanceCounter                      =  null;
   PerformanceCounterCategory  performanceCounterCategory              =  null; 
   
   try
   {
    foreach ( String[] WMIPerformanceCounterMonitoringConfigurationCurrent in WMIPerformanceCounterMonitoringConfiguration )
    {

     if ( !PerformanceCounterCategory.Exists(WMIPerformanceCounterMonitoringConfigurationCurrent[ RankWMIPerformanceCounterCategoryName ])) 
     {
      continue;	
     }     	

     performanceCounter          = new PerformanceCounter();
     performanceCounterCategory  = new PerformanceCounterCategory();
     
     performanceCounterCategoryNameCurrent    =  WMIPerformanceCounterMonitoringConfigurationCurrent[ RankWMIPerformanceCounterCategoryName ];
     performanceCounter.CategoryName          =  performanceCounterCategoryNameCurrent;
     
     performanceCounterCategory.CategoryName  =  performanceCounterCategoryNameCurrent;

     performanceCounterNextValue              =  -1;
     performanceCounterInstanceNameCurrent    =  null;

     performanceCounterCategoryInstanceName   =  performanceCounterCategory.GetInstanceNames();
     
     if ( performanceCounterCategoryInstanceName.Length > 1 )
     {
      /*
      foreach ( String performanceCounterCategoryInstanceNameCurrent in performanceCounterCategoryInstanceName )
      {
       System.Console.WriteLine("Instance: {0}", performanceCounterCategoryInstanceNameCurrent );	
      }//foreach ( String performanceCounterCategoryInstanceNameCurrent in performanceCounterCategoryInstanceName )
      */
      if ( performanceCounterCategory.InstanceExists( PerformanceCounterCategoryInstanceNameTotal ) )
      {
       performanceCounterInstanceNameCurrent  =  PerformanceCounterCategoryInstanceNameTotal;  	
      }//if ( performanceCounterCategory.InstanceExists( PerformanceCounterCategoryInstanceNameTotal ) )
      else
      {
       performanceCounterInstanceNameCurrent  =  performanceCounterCategoryInstanceName[0];
      }//else ( performanceCounterCategory.InstanceExists( PerformanceCounterCategoryInstanceNameTotal ) ) 
     }//if ( performanceCounterCategoryInstanceName.Length > 1 )
     
     performanceCounterCounterNameCurrent = WMIPerformanceCounterMonitoringConfigurationCurrent[ RankWMIPerformanceCounterCounterName ];
     performanceCounter.CounterName       = performanceCounterCounterNameCurrent;
      
     if ( performanceCounterInstanceNameCurrent != null )
     {
      performanceCounter.InstanceName      = performanceCounterInstanceNameCurrent;
     } 
      
     performanceCounter.InstanceName      = performanceCounterInstanceNameCurrent;
     performanceCounterNextValue = performanceCounter.NextValue();
      
     System.Console.WriteLine
     (
      "Performance Counter Category: {0} | Counter: {1} | Instance: {2} | Value: {3}",
      performanceCounterCategoryNameCurrent,
      performanceCounterCounterNameCurrent,       
      performanceCounterInstanceNameCurrent,
      performanceCounterNextValue
     );
   	
    }//for ( String[] WMIPerformanceCounterMonitoringConfigurationCurrent in WMIPerformanceCounterMonitoringConfiguration )
   }//try
   catch ( Exception exception )
   {
   	exceptionMessage = "Exception: " + exception.Message;
   	System.Console.WriteLine("Exception: {0}", exception.Message);
   }    	
  }//public static void QueryPerformanceCounter()
  
  static UtilityWindowsManagementInstrumentationWMI()
  {

  }//static UtilityWindowsManagementInstrumentationWMI()
  
 }//public class UtilityWindowsManagementInstrumentationWMI
 
 ///<summary>ObjectHandler Handler for asynchronous results</summary>
 ///<remarks>ObjectHandler Handler for asynchronous results</remarks>
 public class ObjectHandler 
 {

  /// <summary>isCompleted.</summary>
  public bool isCompleted = false;

  ///<summary>NewObject</summary>
  ///<param name="sender">sender</param>
  ///<param name="objectReadyEventArgs">objectReadyEventArgs</param>  
  public void NewObject
  (
   object                sender, 
   ObjectReadyEventArgs  objectReadyEventArgs
  ) 
  {
   System.Console.WriteLine
   (
    "Service : {0}, State = {1}", 
    objectReadyEventArgs.NewObject["Name"],
    objectReadyEventArgs.NewObject["State"]
   );
  }

  ///<summary>Property IsCompleted</summary>
  public bool IsCompleted
  {
   get 
   { 
    return isCompleted;
   }
  }
      
  ///<summary>Reset</summary>
  public void Reset()   
  {
   isCompleted = false;
  }

  ///<summary>Done</summary>
  ///<param name="sender">sender</param>
  ///<param name="completedEventArgs">completedEventArgs</param>  
  public void Done
  (
   object              sender, 
   CompletedEventArgs  completedEventArgs
  ) 
  {
   isCompleted = true;
  }
 }//public class ObjectHandler

 ///<summary>EventHandler</summary>
 ///<remarks>EventHandler</remarks>
 public class EventHandler
 {
  ///<summary>HandleEvent</summary>
  ///<param name="sender">sender</param>
  ///<param name="eventArrivedEventArgs">eventArrivedEventArgs</param>  
  public void HandleEvent
  (
   object                 sender,
   EventArrivedEventArgs  eventArrivedEventArgs
  )   
  {
   System.Console.WriteLine("Event arrived !");
  }//public void HandleEvent( object sender, EventArrivedEventArgs eventArrivedEventArgs )   

 }//public class EventHandler

 ///<summary>ObjectReadyHandler</summary>
 ///<remarks>ObjectReadyHandler</remarks>
 public class ObjectReadyHandler
 {

  /// <summary>isCompleted</summary>
  public  bool                  isComplete                  =  false;

  /// <summary>managementBaseObjectResult</summary>
  public  ManagementBaseObject  managementBaseObjectResult  =  null;

  ///<summary>NewObject Delegate called when the method completes and results are available</summary>
  ///<param name="sender">sender</param>
  ///<param name="objectReadyEventArgs">objectReadyEventArgs</param>
  public void NewObject
  (
   object                sender, 
   ObjectReadyEventArgs  objectReadyEventArgs
  ) 
  {
   System.Console.WriteLine("New Object arrived!");
   managementBaseObjectResult = objectReadyEventArgs.NewObject;
   isComplete = true;
  }

  ///<summary>Property allows accessing the result object in the main function</summary>
  public  ManagementBaseObject  ManagementBaseObjectResult
  {
   get 
   {
    return managementBaseObjectResult;
   }
  }

  ///<summary>Property IsCompleted</summary>  
  public bool IsComplete
  {
   get 
   {
    return isComplete;
   }
  }//public bool IsComplete
 }//public class ObjectReadyHandler
 
}//namespace WordEngineering