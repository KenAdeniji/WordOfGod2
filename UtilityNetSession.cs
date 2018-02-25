using System;
using System.Runtime.InteropServices;

namespace WordEngineering
{
 ///<summary>SessUsrFlgs</summary>
 [Flags]
 public enum SessUsrFlgs
 {
  ///<summary>Guest</summary>
  Guest   = 0x00000001, // session is logged on as a guest
  ///<summary>NoEncryption</summary>
  NoEncryption = 0x00000002 // session is not using encryption
 }

 ///<summary>SESSION_INFO_502</summary>
 [StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
 public struct SESSION_INFO_502
 {
  ///<summary>Computer</summary>
  public string  Computer;
  ///<summary>User</summary>
  public string  User;
  ///<summary>NumOpens</summary>
  public int   NumOpens;
  ///<summary>Seconds</summary>
  public int   Seconds;
  ///<summary>IdleSeconds</summary>
  public int   IdleSeconds;
  ///<summary>UsrFlags</summary>  
  public SessUsrFlgs UsrFlags;
  ///<summary>LmType</summary>
  public string  LmType;
  ///<summary>Transport</summary>
  public string  Transport;
 }

 ///<summary>UtilityNetSession</summary>
 ///<remarks>Lowendahl.net with Patrik Löwendahl</remarks>
 public class UtilityNetSession
 {
  ///<summary>NetSessionEnum</summary>
  [DllImport("netapi32.dll", CharSet=CharSet.Unicode)]
  public static extern int NetSessionEnum
  ( 
   string srvName, 
   string cltName, 
   string usrName, 
   int level,
   out IntPtr bufPtr, 
   int prefmaxlen, 
   out int entriesread, 
   out int totalentries, 
   ref IntPtr resume_handle 
  );

  ///<summary>NetApiBufferFree</summary>
  [DllImport("netapi32.dll", ExactSpelling=true)]
  public extern static int NetApiBufferFree( IntPtr bufptr );

  ///<summary>ERROR_MORE_DATA</summary>
  public const int ERROR_MORE_DATA = 234;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of arguments</param>
  public static void Main(string[] argv)
  {
   int read, total, s;
   IntPtr ptr, rhandle = IntPtr.Zero;
   Type typ = typeof( SESSION_INFO_502 );
   int size = Marshal.SizeOf( typ );
   SESSION_INFO_502 si;
   s = NetSessionEnum( null, null, null, 502, out ptr, -1, out read, out total, ref rhandle );
   if( (s != 0) && (s != ERROR_MORE_DATA) ) { return; }
   System.Console.WriteLine("Computer:        User:                Opens:     Seconds:" );
   int run = (int) ptr;
   for( int i = 0; i < read; i++ )
   {
    si = (SESSION_INFO_502) Marshal.PtrToStructure( (IntPtr) run, typ );
    System.Console.WriteLine
    ( 
     " {0,-16} {1,-20} {2,-10} {3,-10}", 
     si.Computer,
     si.User, 
     si.NumOpens.ToString(), 
     si.Seconds.ToString() 
    );
    run += size;
   }
   NetApiBufferFree( ptr ); 
   ptr = IntPtr.Zero;
  }			
 }
}
