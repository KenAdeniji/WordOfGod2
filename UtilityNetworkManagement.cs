using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace WordEngineering
{
   	/// <summary>
    /// http://www.csharphelp.com/archives2/archive439.html Using the Network Functions in C# (Part I - User Functions) By Michael Bright mailto:mike.bright@brightweb.co.uk
    /// http://www.csharphelp.com/archives2/archive440.html Using Network Functions in Visual C#.NET (Part II - Group Functions) By Michael Bright mailto:mike.bright@brightweb.co.uk
    /// http://mlei0.spaces.live.com/blog/
    /// http://msdn2.microsoft.com/en-us/library/ac7ay120.aspx Platform Invoke Data Types
    /// http://uchukamen.com/Programming/Workgroup/index.htm NetApiBufferFree(bufptr);
    /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/Q187/5/35.asp&NoWebContent=1 How To Change Passwords Programmatically in Windows NT
    /// http://www.pinvoke.net/default.aspx/netapi32/DsGetDcName.html?diff=y 
    /// http://msdn2.microsoft.com/en-us/library/aa370674.aspx Network Management Error Codes
    /// http://uchukamen.com/Programming/EnumServer/index.htm
    /// http://support.microsoft.com/kb/316318 How To Use NetQueryDisplayInformation() in Visual Basic
    ///</summary>
    public class UtilityNetworkManagement
    {
        /// <summary>
        /// C:\Program Files\Microsoft Visual Studio 8\VC\PlatformSDK\Include\WinError.h
        /// </summary>
        public const int ERROR_MORE_DATA = 234;

        /// <summary>
        /// C:\Program Files\Microsoft Visual Studio 8\VC\PlatformSDK\Include\LMCons.h
        /// Value to be used with APIs which have a "preferred maximum length"
        /// parameter.  This value indicates that the API should just allocate
        /// "as much as it takes."
        /// http://uchukamen.com/Programming/EnumServer/index.htm
        /// </summary>
        public const uint MAX_PREFERRED_LENGTH = unchecked((uint)-1);

        /// <summary>
        /// C:\Program Files\Microsoft Visual Studio 8\VC\PlatformSDK\Include\LMErr.h
        /// </summary>
        public const int NERR_Success = 0;

        public static string HomePathParentDirectory = Environment.GetEnvironmentVariable("homepath").Substring(0, Environment.GetEnvironmentVariable("homepath").LastIndexOf(Path.DirectorySeparatorChar) - 1);
        public static Random randomString = new System.Random((int)System.DateTime.Now.Ticks % 26);

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/ms675912.aspx
        /// typedef struct _DOMAIN_CONTROLLER_INFO 
        /// {  
        /// LPTSTR DomainControllerName;
        /// LPTSTR DomainControllerAddress;
        /// ULONG DomainControllerAddressType;
        /// GUID DomainGuid;
        /// LPTSTR DomainName;
        /// LPTSTR DnsForestName;
        /// ULONG Flags;
        /// LPTSTR DcSiteName;
        /// LPTSTR ClientSiteName;
        /// } 
        /// DOMAIN_CONTROLLER_INFO,  *PDOMAIN_CONTROLLER_INFO;
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DOMAIN_CONTROLLER_INFO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DomainControllerName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DomainControllerAddress;
            public uint DomainControllerAddressType;
            public Guid DomainGuid;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DomainName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DnsForestName;
            public uint Flags;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DcSiteName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string ClientSiteName;
        }

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370684.aspx
        /// typedef struct _NET_DISPLAY_USER 
        /// {  
        ///     LPWSTR usri1_name;
        ///     LPWSTR usri1_comment;
        ///     DWORD usri1_flags;
        ///     LPWSTR usri1_full_name;
        ///     DWORD usri1_user_id;
        ///     DWORD usri1_next_index;
        /// } NET_DISPLAY_USER,  *PNET_DISPLAY_USER;
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DISPLAY_USER 
        {
            public static readonly int SIZEOF_NET_DISPLAY_USER = Marshal.SizeOf(typeof(NET_DISPLAY_USER));
            public System.IntPtr usri1_name;  
            public System.IntPtr usri1_comment;  
            public uint usri1_flags;  
            public System.IntPtr usri1_full_name;  
            public uint usri1_user_id;  
            public uint usri1_next_index;
        }

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370682.aspx
        /// typedef struct _NET_DISPLAY_GROUP
        /// {  
        ///     LPWSTR grpi3_name;
        ///     LPWSTR grpi3_comment;
        ///     DWORD grpi3_group_id;
        ///     DWORD grpi3_attributes;
        ///     DWORD grpi3_next_index;
        ///} NET_DISPLAY_GROUP,  *PNET_DISPLAY_GROUP;
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DISPLAY_GROUP
        {
            public static readonly int SIZEOF_NET_DISPLAY_GROUP = Marshal.SizeOf(typeof(NET_DISPLAY_GROUP));
            public System.IntPtr grpi3_name;
            public System.IntPtr grpi3_comment;
            public uint grpi3_group_id;
            public uint grpi3_attributes;
            public uint grpi3_next_index;
        }

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370683.aspx
        /// typedef struct _NET_DISPLAY_MACHINE
        /// {  
        ///     LPWSTR usri2_name;
        ///     LPWSTR usri2_comment;
        ///     DWORD usri2_flags;
        ///     DWORD usri2_user_id;
        ///     DWORD usri2_next_index;
        /// } NET_DISPLAY_MACHINE,  *PNET_DISPLAY_MACHINE;
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DISPLAY_MACHINE 
        {
            public static readonly int SIZEOF_NET_DISPLAY_MACHINE = Marshal.SizeOf(typeof(NET_DISPLAY_MACHINE));
            public System.IntPtr usri2_name;
            public System.IntPtr usri2_comment;
            public uint  usri2_flags;
            public uint  usri2_user_id;
            public uint  usri2_next_index;
        } 

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa371360.aspx
        /// typedef struct _USER_INFO_1 
        /// {  
        ///     LPWSTR usri1_name; 
        ///     LPWSTR usri1_password;
        ///     DWORD usri1_password_age;
        ///     DWORD usri1_priv;
        ///     LPWSTR usri1_home_dir;
        ///     LPWSTR usri1_comment;
        ///     DWORD usri1_flags; 
        ///     LPWSTR usri1_script_path;
        /// } 
        /// USER_INFO_1,  *PUSER_INFO_1,  *LPUSER_INFO_1;
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USER_INFO_1
        {
            public string usri1_name;
            public string usri1_password;
            public System.UInt32 usri1_password_age;
            public System.UInt32 usri1_priv;
            public string usri1_home_dir;
            public string comment;
            public System.UInt32 usri1_flags;
            public string usri1_script_path;
        };

        ///<summary>
        /// typedef struct _USER_INFO_1003 
        /// {  
        ///     LPWSTR usri1003_password;
        /// } USER_INFO_1003,  *PUSER_INFO_1003,  *LPUSER_INFO_1003;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USER_INFO_1003
        {
            public string usri1003_password;
        };

        /// </summary>
        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370968.aspx
        /// typedef struct _USER_INFO_1008 
        /// {  
        ///     DWORD usri1008_flags;
        /// } 
        /// USER_INFO_1008,  *PUSER_INFO_1008,  *LPUSER_INFO_1008;
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USER_INFO_1008
        {
            public System.UInt32 usri1008_flags;
        }

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/ms675983.aspx
        /// DWORD DsGetDcName
        /// (
        ///     LPCTSTR ComputerName,
        ///     LPCTSTR DomainName,
        ///     GUID* DomainGuid,
        ///     LPCTSTR SiteName,
        ///     ULONG Flags,
        ///     PDOMAIN_CONTROLLER_INFO* DomainControllerInfo
        /// );
        /// </summary>
        [DllImport("netapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int DsGetDcName
        (
            [MarshalAs(UnmanagedType.LPTStr)]
            string ComputerName,
            [MarshalAs(UnmanagedType.LPTStr)]
            string DomainName,
            [In] int DomainGuid,
            [MarshalAs(UnmanagedType.LPTStr)]
            string SiteName,
            uint Flags,
            out IntPtr DomainControllerInfo
        );

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370304.aspx
        /// NET_API_STATUS NetApiBufferFree
        /// (
        ///     LPVOID Buffer
        /// );
        /// </summary>
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NetApiBufferFree(IntPtr bufptr);

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370420.aspx
        /// NET_API_STATUS NetGetDCName
        /// (
        ///     LPCWSTR servername,
        ///     LPCWSTR domainname,
        ///     LPBYTE* bufptr
        /// );
        /// </summary>
        /// <remarks
        /// The NetGetDCName function returns the name of the primary domain controller (PDC).
        /// It does not return the name of the backup domain controller (BDC) for the specified domain.
        /// Also, you cannot remote this function to a non-PDC server.
        /// Applications that support DNS-style names should call the DsGetDcName function.
        /// Domain controllers in this type of environment have a multi-master directory replication relationship.
        /// Therefore, it may be advantageous for your application to use a DC that is not the PDC.
        /// You can call the DsGetDcName function to locate any DC in the domain; 
        /// NetGetDCName returns only the name of the PDC.
        /// </remarks>
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NetGetDCName
        (
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            [MarshalAs(UnmanagedType.LPWStr)] string domainname,
            out System.IntPtr bufptr
        );

        ///<summary>
        ///http://msdn2.microsoft.com/en-us/library/aa370610.aspx
        ///NET_API_STATUS NetQueryDisplayInformation
        ///(
        ///     LPCWSTR ServerName,
        ///     DWORD Level,
        ///     DWORD Index,
        ///     DWORD EntriesRequested,
        ///     DWORD PreferredMaximumLength,
        ///     LPDWORD ReturnedEntryCount,
        ///     PVOID* SortedBuffer
        ///);
        ///</summary>
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NetQueryDisplayInformation
        (
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            System.UInt32 level,
            System.UInt32 index,
            System.UInt32 entriesRequested,
            System.UInt32 preferredMaximumLength,
            out System.UInt32 returnedEntryCount,
            out System.IntPtr sortedBuffer
        );

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370645.aspx
        /// NET_API_STATUS NetUseAdd
        /// (
        ///     LMSTR UncServerName,
        ///     DWORD Level,
        ///     LPBYTE Buf,
        ///     LPDWORD ParmError
        /// );
        /// </summary>
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int NetUserAdd
        (
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            System.UInt32 level,
            System.IntPtr bufptr,
            out System.UInt32 parm_err
        );

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370650.aspx
        /// NET_API_STATUS NetUserChangePassword
        /// (
        ///     LPCWSTR domainname,
        ///     LPCWSTR username,
        ///     LPCWSTR oldpassword,
        ///     LPCWSTR newpassword
        /// );
        /// </summary>
        [DllImport("netapi32.dll")]
        public extern static int NetUserChangePassword
        (
            [MarshalAs(UnmanagedType.LPWStr)] string domainname,
            [MarshalAs(UnmanagedType.LPWStr)] string username,
            [MarshalAs(UnmanagedType.LPWStr)] string oldpassword,
            [MarshalAs(UnmanagedType.LPWStr)] string newpassword
        );

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370651.aspx
        /// NET_API_STATUS NetUserDel
        /// (
        ///     LPCWSTR servername,
        ///     LPCWSTR username
        /// );
        /// </summary>
        [DllImport("netapi32.dll")]
        public extern static int NetUserDel
        (
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            [MarshalAs(UnmanagedType.LPWStr)] string username
        );

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370654.aspx
        /// NET_API_STATUS NetUserGetInfo
        /// (
        ///     LPCWSTR servername,
        ///     LPCWSTR username,
        ///     DWORD level,
        ///     LPBYTE* bufptr
        /// );
        /// </summary>
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int NetUserGetInfo
        (
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            [MarshalAs(UnmanagedType.LPWStr)] string username,
            System.UInt32 level,
            out IntPtr bufptr
        );

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370659.aspx
        /// NET_API_STATUS NetUserSetInfo
        /// (
        ///     LPCWSTR servername,
        ///     LPCWSTR username,
        ///     DWORD level,
        ///     LPBYTE buf,
        ///     LPDWORD parm_err
        /// );
        /// </summary>
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int NetUserSetInfo
        (
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            [MarshalAs(UnmanagedType.LPWStr)] string username,
            System.UInt32 level,
            IntPtr buf,
            out System.UInt32 parm_err
        );

        [FlagsAttribute]
        public enum DS : uint
        {
            DS_FORCE_REDISCOVERY = 0x00000001,
            DS_DIRECTORY_SERVICE_REQUIRED = 0x00000010,
            DS_DIRECTORY_SERVICE_PREFERRED = 0x00000020,
            DS_GC_SERVER_REQUIRED = 0x00000040,
            DS_PDC_REQUIRED = 0x00000080,
            DS_BACKGROUND_ONLY = 0x00000100,
            DS_IP_REQUIRED = 0x00000200,
            DS_KDC_REQUIRED = 0x00000400,
            DS_TIMESERV_REQUIRED = 0x00000800,
            DS_WRITABLE_REQUIRED = 0x00001000,
            DS_GOOD_TIMESERV_PREFERRED = 0x00002000,
            DS_AVOID_SELF = 0x00004000,
            DS_ONLY_LDAP_NEEDED = 0x00008000,
            DS_IS_FLAT_NAME = 0x00010000,
            DS_IS_DNS_NAME = 0x00020000,
            DS_RETURN_DNS_NAME = 0x40000000,
            DS_RETURN_FLAT_NAME = 0x80000000
        };

        /// <summary>
        /// http://msdn2.microsoft.com/en-us/library/aa370610.aspx
        /// [in] Specifies the information level of the data. This parameter can be one of the following values. Value Meaning
        /// </summary>
        public enum NET_DISPLAY : uint
        {
            [Description("Return user account information. The SortedBuffer parameter points to an array of NET_DISPLAY_USER structures.")] 
            NET_DISPLAY_USER = 1,
            [Description("Return individual computer information. The SortedBuffer parameter points to an array of NET_DISPLAY_MACHINE structures.")] 
            NET_DISPLAY_MACHINE,
            [Description("Return group account information. The SortedBuffer parameter points to an array of NET_DISPLAY_GROUP structures.")] 
            NET_DISPLAY_GROUP
        };

        /// <summary>
        /// C:\Program Files\Microsoft Visual Studio 8\VC\PlatformSDK\Include\LMaccess.h
        /// </summary>
        [FlagsAttribute]
        public enum UF : uint
        {
            [Description("The logon script executed. This value must be set.")] 
            UF_SCRIPT = 0x0001,
            [Description("The user's account is disabled.")]
            UF_ACCOUNTDISABLE = 0x0002,
            [Description("The home directory is required. This value is ignored.")]
            UF_HOMEDIR_REQUIRED = 0x0008,
            [Description("No password is required.")]
            UF_PASSWD_NOTREQD = 0x0020,
            [Description("The user cannot change the password.")]
            UF_PASSWD_CANT_CHANGE = 0x0040,
            [Description("The account is currently locked out. You can call the NetUserSetInfo function and clear this value to unlock a previously locked account. You cannot use this value to lock a previously unlocked account.")]
            UF_LOCKOUT = 0x0010,
            [Description("The password should never expire on the account.")]
            UF_DONT_EXPIRE_PASSWD = 0x10000,
            [Description("The user's password is stored under reversible encryption in the Active Directory.")]
            UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x0080,
            [Description("Marks the account as \"sensitive\"; other users cannot act as delegates of this user account.")]
            UF_NOT_DELEGATED = 0x100000,
            [Description("Requires the user to log on to the user account with a smart card.")]
            UF_SMARTCARD_REQUIRED = 0x40000,
            UF_USE_DES_KEY_ONLY = 0x200000,
            [Description("This account does not require Kerberos preauthentication for logon.")]
            UF_DONT_REQUIRE_PREAUTH = 0x400000,
            [Description("The account is enabled for delegation. This is a security-sensitive setting; accounts with this option enabled should be tightly controlled. This setting allows a service running under the account to assume a client's identity and authenticate as that user to other remote servers on the network.")]
            UF_TRUSTED_FOR_DELEGATION = 0x80000,
            [Description("The user's password has expired.")]
            UF_PASSWORD_EXPIRED = 0x800000,
            UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0x1000000
        };

        public enum USER_INFO
        {
            USER_INFO_0 = 0,
            USER_INFO_1,
            USER_INFO_2,
            USER_INFO_3,
            USER_INFO_4,
            USER_INFO_21 = 21,
            USER_INFO_22,
            USER_INFO_1003 = 1003,
            USER_INFO_1005 = 1005,
            USER_INFO_1006,
            USER_INFO_1007,
            USER_INFO_1008,
            USER_INFO_1009,
            USER_INFO_1010,
            USER_INFO_1011,
            USER_INFO_1012,
            USER_INFO_1014 = 1014,
            USER_INFO_1017 = 1017,
            USER_INFO_1020 = 1020,
            USER_INFO_1024 = 1024, 
            USER_INFO_1051 = 1051, 
            USER_INFO_1052,
            USER_INFO_1053
        };

        public enum USER_PRIV
        {
            USER_PRIV_GUEST = 0,
            USER_PRIV_USER = 1,
            USER_PRIV_ADMIN = 2
        };

        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
            Stub();
        }

        /// <summary>http://www.pinvoke.net/default.aspx/netapi32/DsGetDcName.html?diff=y</summary>
        public static int DsGetDcName(out DOMAIN_CONTROLLER_INFO domainControllerInfo)
        {
            int nStatus = 0;
            IntPtr domainControllerInfoPtr = IntPtr.Zero;
            try
            {
                nStatus = DsGetDcName
                (
                    "", 
                    "", 
                    0, 
                    "",
                    Convert.ToUInt32
                    (
                        DS.DS_DIRECTORY_SERVICE_REQUIRED |
                        DS.DS_RETURN_DNS_NAME |
                        DS.DS_IP_REQUIRED
                    ),
                    out domainControllerInfoPtr
                 );
                //check return value for error
                if (NERR_Success == nStatus)
                {
                    domainControllerInfo = (DOMAIN_CONTROLLER_INFO)Marshal.PtrToStructure(domainControllerInfoPtr, typeof(DOMAIN_CONTROLLER_INFO));
                    System.Console.WriteLine
                    (
                        "DnsForestName: {0} | DomainControllerName: {1} | ClientSiteName: {2}",
                        domainControllerInfo.DnsForestName,
                        domainControllerInfo.DomainControllerName,
                        domainControllerInfo.ClientSiteName
                    );
                }
                else
                {
                    throw new Win32Exception(nStatus);
                }
            }
            finally
            {
                NetApiBufferFree(domainControllerInfoPtr);
            }
            return (nStatus);
        }

        /// <summary>
        /// http://www.microsoft.com/mspress/books/sampchap/6436.aspx Microsoft® Visual Basic .NET Programmer's Cookbook Matthew MacDonald
        /// http://blogs.msdn.com/abhinaba/archive/2005/10/20/483000.aspx I know the answer (it's 42) Abhinaba's blog on C#, Team System, and all other things. 42
        /// </summary>
        public static string EnumToString(Type enumType, uint enumFlag)
        {
            uint enumValue;
            string[] enumName;
            string enumDescription;
            StringBuilder sb = new StringBuilder();

            enumName = Enum.GetNames(enumType);
            for (int i = 0; i <= enumName.GetUpperBound(0); i++)
            {
                enumDescription = EnumDescription((Enum)Enum.Parse(enumType, enumName[i]));
                enumValue = (uint) (Enum.Parse(enumType, enumName[i]));
                if ((enumFlag & enumValue) == enumValue) { sb.Append(enumDescription + ' '); }
            } 
            return (sb.ToString());
        }

        public static string EnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes
                                                ( typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        } 

        public static int GetPrimaryDCName(string domainname, out string primaryDomainController)
        {
            int nStatus;
            System.IntPtr userInfoPtr = IntPtr.Zero;
            primaryDomainController = null;

            if (String.IsNullOrEmpty(domainname))
            {
                domainname = Environment.GetEnvironmentVariable("USERDNSDOMAIN"); //Environment.GetEnvironmentVariable("USERDOMAIN");
            }
            nStatus = NetGetDCName(null, domainname, out userInfoPtr);
            if (nStatus == NERR_Success)
            {
                if (userInfoPtr != null)
                {
                    primaryDomainController = (string)Marshal.PtrToStructure(userInfoPtr, typeof(String));
                    System.Console.WriteLine("Primary Domain Controller: {0}", primaryDomainController);
                }
            }
            else
            {
                System.Console.WriteLine("A system error has occurred: {0} {1}", nStatus, MethodBase.GetCurrentMethod());
            }
            NetApiBufferFree(userInfoPtr);
            return (nStatus);
        }

        /// <summary>
        /// http://archives.neohapsis.com/archives/microsoft/various/dcom/2001-q1/0361.html
        ///     The docs don't explain it, but NetQueryDisplayInformation() only retrieves 
        ///     Domain (global) groups and then only if you point it at a DC/BDC. 
        ///     If you want to get local and builtin groups, either look at 
        ///     NetLocalGroupEnum() or go with ADSI which is more efficient then the Net* 
        ///     family when run against AD. It also works with NT4 too! 
        ///     Suggest you start off by downloading the ADSI SDK and checking out "WinNT" 
        ///     group enumeration samples: 
        /// </summary>
        public static int NetQueryDisplayInformationGroup
        (
            string servername,
            uint index,
            uint entriesRequested,
            uint maxPreferredLength,
            out uint returnedEntryCount,
            out System.Collections.Generic.List<NET_DISPLAY_GROUP> netDisplayGroups
        )
        {
            int nStatus;
            System.IntPtr netDisplayGroupPtr = IntPtr.Zero;
            System.IntPtr netDisplayGroupCurrentPtr = IntPtr.Zero;
            NET_DISPLAY_GROUP netDisplayGroup;
            netDisplayGroups = new System.Collections.Generic.List<NET_DISPLAY_GROUP>();
            do
            {
                nStatus = NetQueryDisplayInformation
                (
                    servername,
                    (uint)NET_DISPLAY.NET_DISPLAY_GROUP,
                    index,
                    entriesRequested,
                    maxPreferredLength,
                    out returnedEntryCount,
                    out netDisplayGroupPtr
                );
                if ((nStatus == NERR_Success) || (nStatus == ERROR_MORE_DATA))
                {
                    netDisplayGroupCurrentPtr = netDisplayGroupPtr;
                    for (; returnedEntryCount > 0; returnedEntryCount--)
                    {
                        netDisplayGroup = (NET_DISPLAY_GROUP)Marshal.PtrToStructure(netDisplayGroupCurrentPtr, typeof(NET_DISPLAY_GROUP));
                        netDisplayGroups.Add(netDisplayGroup);
                        System.Console.WriteLine
                        (
                            "Group name: {0} | Comment: {1} | Relative Identifier (RID): {2} | Attributes: {3}",
                            Marshal.PtrToStringUni(netDisplayGroup.grpi3_name),
                            Marshal.PtrToStringUni(netDisplayGroup.grpi3_comment),
                            netDisplayGroup.grpi3_group_id,
                            netDisplayGroup.grpi3_attributes
                        );
                        //
                        // If there is more data, set the index.
                        //
                        index = netDisplayGroup.grpi3_next_index;
                        netDisplayGroupCurrentPtr = (IntPtr)((int)netDisplayGroupCurrentPtr + NET_DISPLAY_GROUP.SIZEOF_NET_DISPLAY_GROUP);
                    }
                    NetApiBufferFree(netDisplayGroupPtr);
                }
                else
                {
                    System.Console.WriteLine("Status: {0}", nStatus);
                }
            } while (nStatus == ERROR_MORE_DATA);
            return (nStatus);
        }

        /// <summary>
        /// http://archives.neohapsis.com/archives/microsoft/various/dcom/2001-q1/0361.html
        ///     The docs don't explain it, but NetQueryDisplayInformation() only retrieves 
        ///     Domain (global) groups and then only if you point it at a DC/BDC. 
        ///     If you want to get local and builtin groups, either look at 
        ///     NetLocalGroupEnum() or go with ADSI which is more efficient then the Net* 
        ///     family when run against AD. It also works with NT4 too! 
        ///     Suggest you start off by downloading the ADSI SDK and checking out "WinNT" 
        ///     group enumeration samples: 
        /// </summary>
        public static int NetQueryDisplayInformationMachine
        (
            string servername,
            uint index,
            uint entriesRequested,
            uint maxPreferredLength,
            out uint returnedEntryCount,
            out System.Collections.Generic.List<NET_DISPLAY_MACHINE> netDisplayMachines
        )       
        {
            int nStatus;
            System.IntPtr netDisplayMachinePtr = IntPtr.Zero;
            System.IntPtr netDisplayMachineCurrentPtr = IntPtr.Zero;
            NET_DISPLAY_MACHINE netDisplayMachine;
            netDisplayMachines = new System.Collections.Generic.List<NET_DISPLAY_MACHINE>();
            do
            {
                nStatus = NetQueryDisplayInformation
                (
                    servername,
                    (uint)NET_DISPLAY.NET_DISPLAY_MACHINE,
                    index,
                    entriesRequested,
                    maxPreferredLength,
                    out returnedEntryCount,
                    out netDisplayMachinePtr
                );
                if ((nStatus == NERR_Success) || (nStatus == ERROR_MORE_DATA))
                {
                    netDisplayMachineCurrentPtr = netDisplayMachinePtr;
                    for (; returnedEntryCount > 0; returnedEntryCount--)
                    {
                        netDisplayMachine = (NET_DISPLAY_MACHINE)Marshal.PtrToStructure(netDisplayMachineCurrentPtr, typeof(NET_DISPLAY_MACHINE));
                        netDisplayMachines.Add(netDisplayMachine);
                        System.Console.WriteLine
                        (
                            "Machine name: {0} | Comment: {1} | Flags: {2} | Relative Identifier (RID): {3}",
                            Marshal.PtrToStringUni(netDisplayMachine.usri2_name),
                            Marshal.PtrToStringUni(netDisplayMachine.usri2_comment),
                            netDisplayMachine.usri2_user_id,
                            EnumToString(typeof(UF), netDisplayMachine.usri2_flags)
                        );
                        //
                        // If there is more data, set the index.
                        //
                        index = netDisplayMachine.usri2_next_index;
                        netDisplayMachineCurrentPtr = (IntPtr)((int)netDisplayMachineCurrentPtr + NET_DISPLAY_MACHINE.SIZEOF_NET_DISPLAY_MACHINE);
                    }
                    NetApiBufferFree(netDisplayMachinePtr);
                }
                else
                {
                    System.Console.WriteLine("Status: {0}", nStatus);
                }
            } while (nStatus == ERROR_MORE_DATA);
            return (nStatus);
        }

        public static int NetQueryDisplayInformationUser
        (
            string servername,
            uint index,
            uint entriesRequested,
            uint maxPreferredLength,
            out uint returnedEntryCount,
            out System.Collections.Generic.List<NET_DISPLAY_USER> netDisplayUsers
        )
        {
            int nStatus;
            System.IntPtr netDisplayUserPtr = IntPtr.Zero;
            System.IntPtr netDisplayUserCurrentPtr = IntPtr.Zero;
            NET_DISPLAY_USER netDisplayUser;
            netDisplayUsers = new System.Collections.Generic.List<NET_DISPLAY_USER>();
            do
            {
                nStatus = NetQueryDisplayInformation
                (
                    servername,
                    (uint) NET_DISPLAY.NET_DISPLAY_USER,
                    index,
                    entriesRequested,
                    maxPreferredLength,
                    out returnedEntryCount,
                    out netDisplayUserPtr
                );
                if ((nStatus == NERR_Success) || (nStatus == ERROR_MORE_DATA))
                {
                    netDisplayUserCurrentPtr = netDisplayUserPtr;
                    for (; returnedEntryCount > 0; returnedEntryCount--)
                    {
                        netDisplayUser = (NET_DISPLAY_USER)Marshal.PtrToStructure(netDisplayUserCurrentPtr, typeof(NET_DISPLAY_USER));
                        netDisplayUsers.Add(netDisplayUser);
                        System.Console.WriteLine
                        (
                            "User Full name: {0} | Comment: {1} | Id: {2} | Flags: {3}",
                            Marshal.PtrToStringUni(netDisplayUser.usri1_full_name),
                            Marshal.PtrToStringUni(netDisplayUser.usri1_comment),
                            netDisplayUser.usri1_user_id,
                            EnumToString(typeof(UF), netDisplayUser.usri1_flags)
                        );
                        //
                        // If there is more data, set the index.
                        //
                        index = netDisplayUser.usri1_next_index;
                        netDisplayUserCurrentPtr = (IntPtr)((int)netDisplayUserCurrentPtr + NET_DISPLAY_USER.SIZEOF_NET_DISPLAY_USER);
                    }
                    NetApiBufferFree(netDisplayUserPtr);
                }
                else
                {
                    System.Console.WriteLine("Status: {0}", nStatus);
                }
            } while (nStatus == ERROR_MORE_DATA);
            return (nStatus);
        }

        public static string RandomString(int size, char min, char max)
        {
            return(RandomString(size, Convert.ToInt16(min), Convert.ToInt16(max)));
        }

        public static string RandomString(int size, int min, int max)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(randomString.Next(min, max));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static void Stub()
        {
            int nStatus;
            uint returnedEntryCount;
            string newpassword;
            string password;
            string servername = null;
            string username;
            System.Collections.Generic.List<NET_DISPLAY_GROUP> netDisplayGroups;
            System.Collections.Generic.List<NET_DISPLAY_MACHINE> netDisplayMachines;
            System.Collections.Generic.List<NET_DISPLAY_USER> netDisplayUsers;
            DOMAIN_CONTROLLER_INFO domainControllerInfo;

            /*
            nStatus = DsGetDcName(out domainControllerInfo);
            servername = domainControllerInfo.DomainControllerName;
            */ 



            nStatus = NetQueryDisplayInformationUser
            (
               servername,
               0,
               100,
               MAX_PREFERRED_LENGTH,
               out returnedEntryCount,
               out netDisplayUsers
            );

            nStatus = NetQueryDisplayInformationMachine
            (
               servername,
               0,
               100,
               MAX_PREFERRED_LENGTH,
               out returnedEntryCount,
               out netDisplayMachines
            );

            nStatus = NetQueryDisplayInformationGroup
            (
               servername,
               0,
               100,
               MAX_PREFERRED_LENGTH,
               out returnedEntryCount,
               out netDisplayGroups
            );

            newpassword = RandomString(4, 'A', 'Z') + RandomString(4, 'a', 'z') + RandomString(4, '0', '9');
            password = RandomString(4, 'A', 'Z') + RandomString(4, 'a', 'z') + RandomString(4, '0', '9');
            username = RandomString(20, 'A', 'Z');

            nStatus = NetUserAdd
            (
                servername,
                username, 
                password,
                USER_PRIV.USER_PRIV_USER, 
                Path.Combine(HomePathParentDirectory, username),
                "Comment",
                null
            );
            nStatus = NetUserChangePasswordStub
            (
                servername != null ? servername : @"\\" + System.Environment.MachineName,
                username,
                null, //password
                newpassword
            );
            nStatus = NetUserGetInfo(servername, username);
            nStatus = NetUserSetInfoAccountDisabled(servername, username);
            nStatus = NetUserGetInfo(servername, username);
            nStatus = NetUserDelStub(servername, username);
        }

        /// <summary>
        /// net user [UserName {Password | *} /add [Options] [/domain]]
        ///     Options Specifies a command-line option. The following table lists valid command-line options that you can use.
        ///         /homedir:Path
        ///             Sets the path for the user's home directory. The path must exist.
        ///         /comment:"Text"
        ///         /scriptpath:Path 
        ///             Sets a path for the user's logon script. Path cannot be an absolute path. Path is relative to %systemroot%\System32\Repl\Import\Scripts.  
        /// </summary>
        public static int NetUserAdd
        (
            string servername,
            string name,
            string password,
            USER_PRIV priv,
            string home_dir,
            string comment,
            string script_path
        )
        {
            int nStatus;
            IntPtr userInfoPtr = IntPtr.Zero;;
            System.UInt32 parm_err = 0;
            USER_INFO_1 userInfo = new USER_INFO_1();;

            userInfo.usri1_name = name;
            userInfo.usri1_password = password;
            userInfo.usri1_priv = (System.UInt32) priv;
            userInfo.usri1_home_dir = home_dir;
            userInfo.comment = comment;
            userInfo.usri1_flags = (System.UInt32) UF.UF_SCRIPT;
            userInfo.usri1_script_path = script_path;

            userInfoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(userInfo));
            Marshal.StructureToPtr(userInfo, userInfoPtr, false);

            nStatus = NetUserAdd(servername, (System.UInt32) USER_INFO.USER_INFO_1, userInfoPtr, out parm_err);

            if (nStatus != NERR_Success)
            {
                System.Console.WriteLine("Status: {0} | parm_err: {1}", nStatus, parm_err);
            }
            NetApiBufferFree(userInfoPtr);
            return (nStatus);
        }

        public static int NetUserChangePasswordStub
        (
            string domainname,
            string username,
            string oldpassword,
            string newpassword
        )
        {
            int nStatus;
            string servername = null;
            DOMAIN_CONTROLLER_INFO domainControllerInfo;
            if (domainname != null && domainname.StartsWith(@"\\"))
            {
                servername = domainname;
            }
            else
            {
                //nStatus = GetPrimaryDCName(domainname, out servername);
                nStatus = DsGetDcName(out domainControllerInfo);
                if (nStatus != NERR_Success) { return (nStatus); }
                servername = domainControllerInfo.DomainControllerName;
            }    
            if (string.IsNullOrEmpty(oldpassword))
            {
                nStatus = NetUserSetInfoPassword(servername, username, newpassword);
            }
            else
            {
                nStatus = NetUserChangePassword(servername, username, oldpassword, newpassword);
                if (nStatus == NERR_Success)
                {
                    System.Console.WriteLine("Password has been changed successfully");
                }
                else
                {
                    System.Console.WriteLine("A system error has occurred: {0} {1}", nStatus, MethodBase.GetCurrentMethod());
                }
            }
            return (nStatus);
        }

        ///<summary>
        /// Net user user_name /delete [/domain]
        ///</summary>
        public static int NetUserDelStub(string servername, string username)
        {
            int nStatus;
            nStatus = NetUserDel(servername, username);
            if (nStatus == NERR_Success)
            {
                System.Console.WriteLine
                (
                    "User {0} has been successfully deleted on {1}",
                    username,
                    servername == null ? System.Environment.MachineName : servername
                );
            }
            else
            {
                System.Console.WriteLine("A system error has occurred: {0} {1}", nStatus, MethodBase.GetCurrentMethod());
            }
            return(nStatus);
        }

        /// <summary>
        /// Net user user_name [/domain]
        /// </summary>
        public static int NetUserGetInfo(string servername, string name)
        {
            int nStatus;
            USER_INFO level = USER_INFO.USER_INFO_1;
            System.IntPtr userInfoPtr = IntPtr.Zero;
            USER_INFO_1 userInfo = new USER_INFO_1();

            nStatus = NetUserGetInfo(servername, name, (System.UInt32)level, out userInfoPtr);
            if (nStatus == NERR_Success)
            {
                if (userInfoPtr != null)
                {
                    userInfo = (USER_INFO_1)Marshal.PtrToStructure(userInfoPtr, typeof(USER_INFO_1));
                    System.Console.WriteLine
                    (
                        "User Name: {0} | Priviledge: {1} | Home Directory: {2} | Comment: {3} | Flags: {4} | Script Path: {5}",
                        userInfo.usri1_name,
                        userInfo.usri1_priv,
                        userInfo.usri1_home_dir,
                        userInfo.comment,
                        EnumToString(typeof(UF), userInfo.usri1_flags),
                        userInfo.usri1_script_path
                    );
                }
            }
            else
            {
                System.Console.WriteLine("A system error has occurred: {0} {1}", nStatus, MethodBase.GetCurrentMethod());
            }
            NetApiBufferFree(userInfoPtr);
            return (nStatus);
        }

        /// <summary>
        /// Net user user_name /active:{no | yes} [/domain] 
        /// </summary>
        public static int NetUserSetInfoAccountDisabled(string servername, string username)
        {
            int nStatus;
            USER_INFO level = USER_INFO.USER_INFO_1008;
            System.UInt32 parm_err;
            System.IntPtr userInfoPtr = IntPtr.Zero;
            USER_INFO_1008 userInfo = new USER_INFO_1008();

            userInfo.usri1008_flags =(uint) (UF.UF_SCRIPT | UF.UF_ACCOUNTDISABLE);

            userInfoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(userInfo));
            Marshal.StructureToPtr(userInfo, userInfoPtr, false);

            nStatus = NetUserSetInfo(servername, username, (System.UInt32)level, userInfoPtr, out parm_err);

            if (nStatus == NERR_Success)
            {
                System.Console.WriteLine("User account {0} has been disabled", username);
            }
            else
            {
                System.Console.WriteLine("A system error has occurred: {0} {1}", nStatus, MethodBase.GetCurrentMethod());
            }
            NetApiBufferFree(userInfoPtr);
            return (nStatus);
        }

        /// <summary>
        /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/Q187/5/35.asp&NoWebContent=1 How To Change Passwords Programmatically in Windows NT
        /// http://support.microsoft.com/kb/149427 How to Change User Password at Command Prompt
        ///     net user user_name * /domain
        ///         When you are prompted to type a password for the user, type the new password, not the existing password. After you type the new password, the system prompts you to retype the password to confirm. The password is now changed. 
        ///     net user user_name new_password
        /// </summary>
        public static int NetUserSetInfoPassword(string servername, string username, string newpassword)
        {
            int nStatus;
            USER_INFO level = USER_INFO.USER_INFO_1003;
            System.UInt32 parm_err;
            System.IntPtr userInfoPtr = IntPtr.Zero;
            USER_INFO_1003 userInfo = new USER_INFO_1003();

            userInfo.usri1003_password = newpassword;

            userInfoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(userInfo));
            Marshal.StructureToPtr(userInfo, userInfoPtr, false);

            nStatus = NetUserSetInfo(servername, username, (System.UInt32)level, userInfoPtr, out parm_err);
            NetApiBufferFree(userInfoPtr);
            if (nStatus == NERR_Success)
            {
                System.Console.WriteLine("Password has been changed successfully");
            }
            else
            {
                System.Console.WriteLine("A system error has occurred: {0} {1}", nStatus, MethodBase.GetCurrentMethod());
            }
            return (nStatus);
        }
    }
}