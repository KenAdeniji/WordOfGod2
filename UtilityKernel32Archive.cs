using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WordEngineering
{

 /// <summary>SystemTime</summary>
 [StructLayout( LayoutKind.Sequential)]
 public class SystemTime 
 {
  ///<summary>year</summary>
  public ushort year; 

  ///<summary>month</summary>
  public ushort month;

  ///<summary>dayOfWeek</summary>
  public ushort dayOfWeek; 

  ///<summary>day</summary>
  public ushort day; 

  ///<summary>hour</summary>
  public ushort hour; 

  ///<summary>minute</summary>
  public ushort minute; 

  ///<summary>second</summary>
  public ushort second; 

  ///<summary>milliseconds</summary>
  public ushort milliseconds; 
 }

 ///<summary>OSVERSIONINFO</summary>
 [StructLayout(LayoutKind.Sequential)]
 public struct OSVERSIONINFO
 {
  ///<summary>dwOSVersionInfoSize</summary>
  public int dwOSVersionInfoSize;

  ///<summary>dwMajorVersion</summary>
  public int dwMajorVersion;

  ///<summary>dwMinorVersion</summary>
  public int dwMinorVersion;

  ///<summary>dwBuildNumber</summary>
  public int dwBuildNumber;

  ///<summary>dwPlatformId</summary>
  public int dwPlatformId;

  ///<summary>szCSDVersion</summary>
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
  public string szCSDVersion;
  }

 /// <summary>UtilityKernel32</summary>
 public class UtilityKernel32
 {
  /// <summary>Beep</summary>
  [DllImport("kernel32")]
  public static extern bool Beep
  (
   uint dwFreq, 
   uint dwDuration
  );

  [DllImport("kernel32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool CreateDirectory(string lpPathName, IntPtr lpSecurityAttributes);

  /// <summary>GetComputerName</summary>
  [DllImport("kernel32", CharSet=CharSet.Unicode)]
  public static extern bool GetComputerName(StringBuilder name, ref int len);

  /// <summary>GetDiskFreeSpace</summary>
  /// <remarks>http://www.pinvoke.net/default.aspx/kernel32.GetDiskFreeSpace</remarks>
  [DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
  public static extern bool GetDiskFreeSpace
  (
   string lpRootPathName, 
   out uint lpSectorsPerCluster, 
   out uint lpBytesPerSector, 
   out uint lpNumberOfFreeClusters, 
   out uint lpTotalNumberOfClusters
  );

  /// <summary>GetDriveType</summary>
  /// <remarks>
  ///  http://msdn.microsoft.com/library/default.asp?url=/library/en-us/fileio/fs/getdrivetype.asp
  ///   Value             Meaning 
  ///   DRIVE_UNKNOWN     The drive type cannot be determined. 
  ///   DRIVE_NO_ROOT_DIR The root path is invalid, for example, no volume is mounted at the path. 
  ///   DRIVE_REMOVABLE   The drive is a type that has removable media, for example, a floppy drive or removable hard disk. 
  ///   DRIVE_FIXED       The drive is a type that cannot be removed, for example, a fixed hard drive. 
  ///   DRIVE_REMOTE      The drive is a remote (network) drive. 
  ///   DRIVE_CDROM       The drive is a CD-ROM drive. 
  ///   DRIVE_RAMDISK     The drive is a RAM disk. 
  /// </remarks>
  [DllImport("kernel32.dll")]
  public static extern uint GetDriveType(string lpRootPathName);

  /// <summary>GetLogicalDrives</summary>
  /// <remarks>
  ///  http://msdn.microsoft.com/library/default.asp?url=/library/en-us/fileio/fs/getdrivetype.asp
  ///  If the function succeeds, the return value is a bitmask representing the currently available disk drives. 
  ///  Bit position 0 (the least-significant bit) is drive A, bit position 1 is drive B, bit position 2 is drive C, 
  ///  and so on.
  /// </remarks>
  [DllImport("kernel32.dll")]
  public static extern uint GetLogicalDrives();

  /// <summary>GetLogicalDriveStrings</summary>
  [DllImport("kernel32.dll")]
  public static extern uint GetLogicalDriveStrings
  (
   uint nBufferLength,   
   string lpBuffer
  );

  /// <summary>GetLogicalDriveStrings</summary>
  [DllImport("kernel32.dll")]
  public static extern uint GetLogicalDriveStrings
  (
   uint nBufferLength,   
   StringBuilder lpBuffer
  );

  ///<summary>GetLongPathName</summary>
  [DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
  public static extern uint GetLongPathName
  (
   string lpszShortPath,
   [Out] StringBuilder lpszLongPath,
   uint cchBuffer
  );

  ///<summary>GetSystemTime</summary>
  [ DllImport("kernel32.dll" )]
  public static extern void GetSystemTime( SystemTime systemTime );

  ///<summary>GetVersionEx</summary>
  [DllImport("kernel32.Dll")] public static extern short GetVersionEx(ref OSVERSIONINFO o);

  [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public extern static bool GetVolumeInformation
  (
   string RootPathName,
   StringBuilder VolumeNameBuffer,
   int VolumeNameSize,
   out uint VolumeSerialNumber,
   out uint MaximumComponentLength,
   out uint FileSystemFlags,
   StringBuilder FileSystemNameBuffer,
   int nFileSystemNameSize
  );

  /// <summary>GetVolumeInformationA</summary>
  [DllImport("kernel32.dll")]
  public static extern long GetVolumeInformationA
  (
   string rootPathName,
   StringBuilder volumeNameBuffer,
   int volumeNameSize,
   out long volumeSerialNumber,
   out long maximumComponentLength,
   out long fileSystemFlags,
   StringBuilder fileSystemNameBuffer,
   int fileSystemNameSize
  );

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  [STAThread]
  public static void Main(string[] argv)
  {

  }

  /// <summary>BeepStub</summary>
  public static void BeepStub()
  {
   Beep(1000, 1000);
  }

  /// <summary>CD</summary>
  /// <remarks>System.Console.WriteLine(CD("E:"));</remarks>
  public static bool CD(string path)
  {
   bool cd = false;
   if ( GetDriveType(path) == 5 ) { cd = true; }
   return(cd);
  }

  /// <summary>CreateDirectoryStub</summary>
  /// <remarks>http://www.pinvoke.net/default.aspx/kernel32.CreateDirectory</remarks>
  public static bool CreateDirectoryStub(string path)
  {
   return(CreateDirectory(path, IntPtr.Zero));
  }

  /// <summary>GetComputerNameStub</summary>
  /// <remarks>System.Console.WriteLine("Computer Name: {0}", GetComputerNameStub() );</remarks>
  public static StringBuilder GetComputerNameStub()
  {
   int len = 256;
   StringBuilder computerName = new StringBuilder(256);
   GetComputerName(computerName, ref len);
   return(computerName);
  }

  /// <summary>GetDiskFreeSpaceStub</summary>
  public static long GetDiskFreeSpaceStub(string rootPathName)
  {
   uint SectorsPerCluster;
   uint BytesPerSector;
   uint NumberOfFreeClusters;
   uint TotalNumberOfClusters;

   GetDiskFreeSpace
   (
    rootPathName, 
    out SectorsPerCluster, 
    out BytesPerSector,
    out NumberOfFreeClusters, 
    out TotalNumberOfClusters
   );

   System.Console.WriteLine("Sectors Per Cluster: {0}", SectorsPerCluster);
   System.Console.WriteLine("Bytes Per Sector: {0}", BytesPerSector);
   System.Console.WriteLine("Number Of Free Clusters: {0}", NumberOfFreeClusters);
   System.Console.WriteLine("Total Number Of Clusters: {0}", TotalNumberOfClusters);

   // SectorsPerCluster * BytesPerSection will give you how many bytes are available per sector
   // And by multiplying the result with NumberOfFreeClusters gives you the free space in bytes.
   long Bytes = (long)NumberOfFreeClusters * SectorsPerCluster * BytesPerSector;
   System.Console.WriteLine("Total Free Space in bytes: {0} ", Bytes);

   return(Bytes);
  
  }

  /// <summary>GetLogicalDrivesStub</summary>
  public static StringBuilder GetLogicalDrivesStub()
  {
   uint getLogicalDrives = GetLogicalDrives();
   return( DriveLetter(getLogicalDrives) );
  }

  /// <summary>GetLogicalDriveStringsStub</summary>
  public static StringBuilder GetLogicalDriveStringsStub()
  {
   uint getLogicalDriveStrings;
   StringBuilder logicalDriveStrings = new StringBuilder(255);
   getLogicalDriveStrings = GetLogicalDriveStrings(255,logicalDriveStrings);
   for (int n = 0; n < 255; ++n)
   {
    System.Console.WriteLine( logicalDriveStrings[n] );
   }
   return(logicalDriveStrings);
  }

  /*
  /// <summary>GetLogicalDriveStringsStub</summary>
  public static String GetLogicalDriveStringsStub()
  {
   uint getLogicalDriveStrings;
   string logicalDriveStrings = new String(' ',255);
   getLogicalDriveStrings = GetLogicalDriveStrings(255,logicalDriveStrings);
   System.Console.WriteLine( getLogicalDriveStrings );
   for (int n = 0; n < 255; ++n)
   {
    System.Console.WriteLine( logicalDriveStrings[n] );
   }
   return(logicalDriveStrings);
  }
  */

  /// <summary>GetLongPathNameStub</summary>
  public static StringBuilder GetLongPathNameStub(string shortName)
  {
   StringBuilder longNameBuffer = new StringBuilder(256);
   uint bufferSize = (uint)longNameBuffer.Capacity;
   GetLongPathName(shortName, longNameBuffer, bufferSize);
   return(longNameBuffer);
  }

  /// <summary>GetServicePack</summary>
  /// <remarks>http://support.microsoft.com/?kbid=304721</remarks>
  public static string GetServicePack()
  {
   string servicePack = "No Service Pack Installed";
   OSVERSIONINFO os = new OSVERSIONINFO();
   os.dwOSVersionInfoSize=Marshal.SizeOf(typeof(OSVERSIONINFO));
   GetVersionEx(ref os);
   if (os.szCSDVersion!="") { servicePack = os.szCSDVersion; }
   return (servicePack);
  }

  /// <summary>GetSystemTimeStub</summary>
  /// <remarks>http://support.microsoft.com/?kbid=304721</remarks>
  public static string GetSystemTimeStub()
  {
   SystemTime systemTime = new SystemTime();
   GetSystemTime( systemTime );
   return
   ( 
    String.Format
    (
     "{0}-{1:00}-{2:00}T{3:00}:{4:00}:{5}-08:00",
     systemTime.year,
     systemTime.month,
     systemTime.day,
     systemTime.hour,
     systemTime.minute,
     systemTime.day
    )  
   );
  }

  /// <summary>GetVolumeInformationAStub</summary>
  /// <remarks>GetVolumeInformationAStub("c:");</remarks>
  public static void GetVolumeInformationAStub( string rootPathName )
  {
   StringBuilder volname = new StringBuilder(256);
   StringBuilder fsname = new StringBuilder(256);
   long getVolumeInformationA;
   long sernum, maxlen, flags;
   getVolumeInformationA = GetVolumeInformationA(rootPathName, volname, volname.Capacity, out sernum, out maxlen, out flags, fsname, fsname.Capacity);
   string volnamestr = volname.ToString();
   string fsnamestr = fsname.ToString();
   System.Console.WriteLine
   (
    "volname: {0} | sernum: {1} | fsname: {2}",
    volname,
    sernum,
    fsname 
   );
  }

  /// <summary>GetVolumeInformationStub</summary>
  /// <remarks>GetVolumeInformationStub("c:");</remarks>
  public static void GetVolumeInformationStub( string rootPathName )
  {
   StringBuilder volname = new StringBuilder(256);
   StringBuilder fsname = new StringBuilder(256);
   uint sernum, maxlen, flags;
   if(!GetVolumeInformation(rootPathName, volname, volname.Capacity, out sernum, out maxlen, out flags, fsname, fsname.Capacity))
   {
    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
   }
   string volnamestr = volname.ToString();
   string fsnamestr = fsname.ToString();
   System.Console.WriteLine
   (
    "volname: {0} | sernum: {1} | fsname: {2}",
    volname,
    sernum,
    fsname 
   );
  }

  /// <summary>DriveLetter</summary>
  public static StringBuilder DriveLetter(uint drive)
  {
   int mod;
   StringBuilder sb = new StringBuilder();
   for ( int index = 0; drive > 0 && index < 26; ++index )
   {
    mod = (int) drive % 2;
    if ( mod == 1 ) { sb.Append( (char) ('A' + index) ); }
    drive /= 2;
   }
   return (sb);
  }
  
 }
}