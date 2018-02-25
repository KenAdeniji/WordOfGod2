using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace WordEngineering
{
 /// <summary>
 /// Patrik Löwendahl with the cshrp.net team.
 /// http://www.csharpsweden.com/content.aspx?showID=900
 /// SQL Enumerator class. A collection class which contains entries for all SQL servers on the network.
 /// </summary>
 public class UtilitySQLServerEnumerator : IEnumerable, IDisposable
 {		
  // Enumeration to hold type of servers
  [Flags]
  internal enum SvType
  {
   WORKSTATION   = 0x00000001,
   SERVER    = 0x00000002,
   SQLSERVER   = 0x00000004,
   DOMAIN_CTRL   = 0x00000008,
   DOMAIN_BAKCTRL  = 0x00000010,
   TIME_SOURCE   = 0x00000020,
   AFP     = 0x00000040,
   NOVELL    = 0x00000080,
   DOMAIN_MEMBER  = 0x00000100,
   PRINTQ_SERVER  = 0x00000200,
   DIALIN_SERVER  = 0x00000400,
   XENIX_SERVER  = 0x00000800,
   NT     = 0x00001000,
   WFW     = 0x00002000,
   SERVER_MFPN   = 0x00004000,
   SERVER_NT   = 0x00008000,
   POTENTIAL_BROWSER = 0x00010000,
   BACKUP_BROWSER  = 0x00020000,
   MASTER_BROWSER  = 0x00040000,
   DOMAIN_MASTER  = 0x00080000,
   SERVER_OSF   = 0x00100000,
   SERVER_VMS   = 0x00200000,
   WINDOWS    = 0x00400000,
   DFS     = 0x00800000,
   CLUSTER_NT   = 0x01000000,
   TERMINALSERVER  = 0x02000000,
   CLUSTER_VS_NT  = 0x04000000,
   DCE     = 0x10000000,
   ALTERNATE_XPORT  = 0x20000000,
   LOCAL_LIST_ONLY  = 0x40000000,
   DOMAIN_ENUM   = unchecked( (int) 0x80000000 ),
   ALL     = unchecked( (int) 0xFFFFFFFF )
  }

  /// <summary>Struct for the server information, will be marshaled from the unmanaged world of life.</summary>
  [StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
  internal struct SERVER_INFO_101
  {
   public Int32 Id;         
   public string Name;      
   public Int32 VerMaj;     
   public Int32 VerMin;     
   public SvType Typ;       
   public string Comment;   
  }

  /// <summary>NetServerEnum API call to enumerate servers on the network.</summary>
  [DllImport("Netapi32", CharSet=CharSet.Unicode, ExactSpelling=true)]
  private static extern int NetServerEnum
  ( 
       string servername, 
       Int32  level, 
   out IntPtr bufptr,
       Int32  prefmaxlen, 
   out Int32  entriesread, 
   out Int32  totalentries, 
       SvType servertype, 
       string domain, 
       IntPtr resume_handle 
  );

  /// <summary>Releases the buffer created on the Unmanaged heap.</summary>
  [DllImport("netapi32.dll", ExactSpelling=true)]
  private extern static int NetApiBufferFree( IntPtr bufptr );

  /// <summary>Constant to hold the error messge "more data" indicates that the enumeration was partially successful but more data could be retrieved.</summary>		
  private const int ERROR_MORE_DATA = 234;
		
  ///<summary>SQLServer</summary>
  public static SqlServer[] _servers;
		
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main
  (
   string[] argv
  )
  {
   foreach ( SqlServer sqlServer in _servers )
   {
    System.Console.WriteLine( sqlServer.Name );
   }
  }//public static void Main()
        	 
  /// <summary>Static servers currently broadcasting on the network.</summary>
  static UtilitySQLServerEnumerator()
  {			
   int size;
   int read;
   int tmpPtr;
   int total;
   int status;
			
   // Pointer to the unmanaged world.
   IntPtr ptr; 
						
   // Information about the struct.
   Type type = typeof( SERVER_INFO_101 );
   size = Marshal.SizeOf( type );
			
   SERVER_INFO_101 si;

   // Call the API and fill the unmanaged buffer with Server_Info_101 structs.
   // Asks only for servers broadcasting SQL Server instances(SvType.SQLSERVER)
   status = NetServerEnum( null, 101, out ptr, -1, out read, out total, SvType.SQLSERVER, null, IntPtr.Zero );
			
   // Make sure that the API call was successful.
   if((status != 0) && (status != ERROR_MORE_DATA)) 
   { 
    // Leave, somtehing blew up.
    return; 
   }
   else
   {
    _servers = new SqlServer[read];
    tmpPtr = (int)ptr;
   
    for(int i = 0; i < read; i++ )
    {
     // Marshal the data from the unmanaged world of life to an instance of
     // Server_Info_101 managed struct on the stack.
     si = (SERVER_INFO_101)Marshal.PtrToStructure((IntPtr)tmpPtr, type );
					
     // Create a new instance of the SqlServer information class.
     _servers[i] = new SqlServer(si);
				
     //Move to beginning of next struct in the buffer
     tmpPtr += size;
    }
			
    // Release the unmanaged buffer.
    NetApiBufferFree(ptr); 
    ptr = IntPtr.Zero;
   }
  }
		
  ///<summary> Number of servers in the collection.</summary>
  public static int Lenght
  {
   get
   { 
    if(_servers!=null)
    {
     return _servers.Length;
    }
    else
    {
     return 0;
    }
   }  
  }
		
  /// <summary>Returns a object with the IEnumerator interface attached to it.</summary>
  public IEnumerator GetEnumerator()
  {
   return new ServerEnumerator(_servers);		
  }
		
  ///<summary>Dispose this object.</summary>  
  public void Dispose()
  {
   _servers = null;	
  }

  // Indexer to access the SqlServer objects
  // directly.
  /*
  public SqlServer this[int item]
  {
   get 
   { 
    if(item < this.Lenght -1)
    {
     return _servers[item];
    }
    else
    {
     throw new IndexOutOfRangeException();
    }	
   }
  }
  */
 }//public class UtilitySQLServerEnumerator : IEnumerable, IDisposable
 
 ///<summary>public struct SqlServer.</summary>
 public struct SqlServer
 {
  UtilitySQLServerEnumerator.SERVER_INFO_101 _serverinfo;

  internal SqlServer(UtilitySQLServerEnumerator.SERVER_INFO_101 info)
  {
   _serverinfo = info;
  }
		
  ///<summary>Name of Sql Server.</summary>
  public string Name
  {
   get{ return _serverinfo.Name; }
  }
 }
	
 ///<summary>Enumerator class used when iterating through the collection. Hidden from intellisense.</summary>
 [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
 public class ServerEnumerator : IEnumerator
 {
  private SqlServer[] g_Servers;
  private int Index;
		
  internal ServerEnumerator(SqlServer[] Servers)
  {
   g_Servers = Servers;
   Index = -1;
  }

  ///<summary>Reset().</summary>		
  public void Reset()
  {
   Index = -1;
  }
		
  ///<summary>Current.</summary>
  public object Current
  {
   get
   {
    if ((Index < 0) || (Index == g_Servers.Length)) throw new InvalidOperationException();
    return g_Servers[Index];
   }
  }

  ///<summary>MoveNext().</summary>
  public bool MoveNext()
  {
   if(g_Servers == null) { return false; }
   if (Index < g_Servers.Length) Index++;
   return (!(Index == g_Servers.Length));
  }//public bool MoveNext()
 }//public class ServerEnumerator : IEnumerator
}//namespace WordEngineering
