using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;

namespace WordEngineering
{
    /// <summary>UtilityProcess.</summary>
    /// <remarks>The Support Tools are available on the installation CD in the \Support\Tools folder.</remarks>
    public class UtilityProcess
    {
        /// <summary>Win32 error code for file not found.</summary>
        public const int ERROR_FILE_NOT_FOUND = 2;

        /// <summary>Win32 error code for access denied.</summary>
        public const int ERROR_ACCESS_DENIED = 5;

        /// <summary>Port POP3 110.</summary>
        public const int PORT_POP3 = 110;

        /// <summary>Port SMTP 25.</summary>
        public static int PORT_SMTP = 25;

        /// <summary>Double quote</summary>
        public static String DoubleQuote = "\"";

        /// <summary>Single quote</summary>
        public static String SingleQuote = "'";

        /// <summary>Question mark</summary>
        public static String QuestionMark = "?";

        /// <summary>VerbPrint</summary>
        public static String VerbPrint = "print";

        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main
        (
          String[] argv
        )
        {

            Stub();
        }

        /// <summary>Stub.</summary>
        public static void Stub()
        {
            //Environment.FailFast("A castrophic failure has occured.");
            //Environment.Exit(0);
			NetUse();
        }

        /// <summary>ProcessStart</summary>
        public static void ProcessStart()
        {
            ProcessStart(null);
        }

        /// <summary>ProcessStart</summary>
        public static void ProcessStart(string extension)
        {
            string filename;
            string methodCalling = null;
            string methodCurrentName = null;

            MethodBase methodCurrent = null;
            StackTrace stackTrace = new StackTrace();
            Type typeBase = null;
            Type typeCurrent = null;

            methodCurrent = MethodBase.GetCurrentMethod();
            methodCurrentName = MethodBase.GetCurrentMethod().Name;
            typeCurrent = MethodBase.GetCurrentMethod().DeclaringType;
            typeBase = typeCurrent.BaseType;

            methodCalling = stackTrace.GetFrame(1).GetMethod().Name;

            if (methodCalling == "ProcessStart")
            {
                methodCalling = stackTrace.GetFrame(2).GetMethod().Name;
            }

            filename = methodCalling;

            if (!string.IsNullOrEmpty(extension))
            {
                filename += "." + extension;
            }
            Process.Start(filename);
        }

        /// <summary>File start.</summary>
        public static void FileStart(string filename)
        {
            FileStart(filename, null, null);
        }
        /// <summary>File start.</summary>
        public static void FileStart
        (  
            string filename,
            string argument
        )
        {
            FileStart
            (
                filename,
                argument,
                null
            );
        }

        /// <summary>File start.</summary>
        public static void FileStart
        (
         String filename,
         String argument,
         String verb
        )
        {
            Boolean redirectStandardOutputError = false;

            String redirectStandardError = null;
            String redirectStandardOutput = null;

            FileStart
            (
                filename,
                argument,
                verb,
                redirectStandardOutputError,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>File start.</summary>
        ///<param name="filename">Filename.</param>
        ///<param name="argument">Argument</param>
        ///<param name="verb">Verb, for example, open, print.</param>
        ///<param name="redirectStandardOutputError">Flag redirect standard output, error.</param>
        ///<param name="redirectStandardOutput">Redirect standard output</param>
        ///<param name="redirectStandardError">Redirect standard error</param>
        public static void FileStart
        (
            string filename,
            string argument,
            string verb,
            bool redirectStandardOutputError,
            ref string redirectStandardOutput,
            ref string redirectStandardError
        )
        {
            Process process = null;
            ProcessStartInfo processStartInfo = null;

            process = new Process();
            processStartInfo = new ProcessStartInfo();

            try
            {

                process.StartInfo.Arguments = argument;
                processStartInfo.Arguments = argument;
                processStartInfo.CreateNoWindow = false;
                processStartInfo.Verb = verb;
                if (redirectStandardOutputError)
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                }
                process.StartInfo.FileName = filename;
                process.Start();
                if (redirectStandardOutputError)
                {
                    process.WaitForExit();
                    redirectStandardOutput = process.StandardOutput.ReadToEnd();
                    UtilityDebug.Write(redirectStandardOutputError);
                }
            }
            catch (Win32Exception e)
            {
                if (e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
                {
                    System.Console.WriteLine(e.Message + ". Check the path.");
                }
                else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
                {
                    // Note that if your word processor might generate exceptions
                    // such as this, which are handled first.
                    System.Console.WriteLine(e.Message + ". You do not have permission.");
                }
            }
        }

        /// <summary>GetCurrentProcess()</summary>
        public static void GetCurrentProcess()
        {
            long mainMemoryUsage = -1;
            long virtualMemory = -1;
            Process process = null;

            //Get the applications process
            process = System.Diagnostics.Process.GetCurrentProcess();

            //There are a number of properties, the two shown below
            //return the number of bytes consumed by the process
            mainMemoryUsage = process.WorkingSet64;
            virtualMemory = process.VirtualMemorySize64;
            System.Console.WriteLine
            (
             "Main Memory Usage: {0} | Virtual Memory: {1}",
             mainMemoryUsage,
             virtualMemory
            );
        }

        
        ///<summary>Access. Accessiblity.</summary>
        public static void Access() { ProcessStart("cpl"); }

        ///<summary>Add or Remove Programs</summary>
        public static void Appwiz() { ProcessStart("cpl"); }

        /// <summary>Address Resolution Protocol (ARP).</summary>
        public static void ARP()
        {
            String filename = "ARP";
            String argument = "-a, -g";
            String verb = null;
            Boolean redirectStandardOutputError = true;
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                argument,
                verb,
                redirectStandardOutputError,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>At: Schedule service.</summary>
        public static void At()
        {
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                "At",
                null,
                null,
                true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Cacls</summary>
        public static void Cacls()
        {
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                "Cacls",
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>CasPol Code Access Security Policy</summary>
        public static void CasPol()
        {
            String filename = "CasPol";
            String argument = "-security off";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                argument,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );

            argument = "-security on";

            FileStart
            (
                filename,
                argument,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

		/// <summary>Certificates</summary>
		public static void CertMgr() { ProcessStart(); }

        ///<summary>Character Map</summary>
        public static void CharMap() { ProcessStart(); }
		
        /// <summary>ChkDsk</summary>
        public static void ChkDsk()
        {
            String filename = "ChkDsk";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>CHMOfficeWebComponents</summary>
        public static void CHMOfficeWebComponents()
        {
            string filename = Path.Combine
            (
             Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
             @"Common Files\Microsoft Shared\Web Components\11\1033\OWCDCH11.chm"
            );
            FileStart(filename);
        }

        /// <summary>Indexing Service</summary>
        public static void Ciadv()
        {
            ProcessStart("msc");
        }

        /// <summary>Cipher</summary>
        public static void Cipher()
        {
            String filename = "Cipher";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>Disk Cleanup Utility</summary>
        public static void CleanMgr()
        {
            ProcessStart();
        }

        ///<summary>SQL Server Client Network Utility</summary>
        public static void CliConfg() { ProcessStart(); }

        ///<summary>ClipBook Viewer Clipboard</summary>
        public static void ClipBrd() { ProcessStart(); }

        /// <summary>ClrVer</summary>
        public static void ClrVer()
        {
            String filename = "ClrVer";
            String argument = "-all";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                argument,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>Compact</summary>
        public static void Compact()
        {
            String filename = "Compact";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>Computer Management</summary>
        public static void CompMgmt()
        {
            ProcessStart("msc");
        }

        ///<summary>SQL Server Integration Services. c:\program files\microsoft SQL Server\100\DTS\binn\DataProfileViewer</summary>
        public static void DataProfileViewer()
        {
            ProcessStart();
        }

        ///<summary>Component Services</summary>
        public static void DComCnfg()
        {
            ProcessStart();
        }

        /// <summary>DCDiag</summary>
        public static void DCDiag()
        {
            String filename = "DCDiag";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>Defrag</summary>
        public static void Defrag()
        {
            String filename = "Defrag";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>Depend</summary>
        public static void Depend()
        {
            String filename = "Depend";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>Display Properties.</summary>
        public static void Desk()
        {
            ProcessStart("cpl");
        }            

        ///<summary>Device Manager</summary>
        public static void DevMgmt()
        {
            ProcessStart("msc");
        }

        /// <summary>DFSUtil</summary>
        public static void DFSUtil()
        {
            String filename = "DFSUtil";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>Disk Defragmenter</summary>
        public static void Dfrg()
        {
            ProcessStart("msc");
        }

        ///<summary>Dir</summary>
        public static void Dir()
        {
            String filename = "Cmd.exe";
            String argument = "/C Dir";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                argument,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>Disk Management</summary>
        public static void DiskMgmt()
        {
            ProcessStart("msc");
        }

        ///<summary>DiskPart</summary>
        public static void DiskPart() { ProcessStart(); }

        ///<summary>DiskPerf</summary>
        public static void DiskPerf()
        {
            String filename = "DiskPerf";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>Disks</summary>
        public static void Disks()
        {
            FileStart("Disks");
        }

        /// <summary>DNSCmd</summary>
        public static void DNSCmd()
        {
            String filename = "DNSCmd";
            String argument = "/Statistics";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                argument,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        /// <summary>DNSUtil</summary>
        public static void DNSUtil()
        {
            String filename = "DNSUtil";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>DriversEtcServices</summary>
        public static void DriversEtcServices()
        {

            String filename = "NotePad";
            String argument = null;

            String systemDirectory = null;

            systemDirectory = Environment.SystemDirectory;
            argument = systemDirectory + @"\drivers\etc\Services";

            FileStart
            (
                filename,
                argument
            );
        }

        ///<summary>DriverQuery.</summary>
        public static void DriverQuery()
        {
            String filename = "DriverQuery";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Dr. Watson</summary>
        public static void DrWtsn32()
        {
            ProcessStart();
        }

        /// <summary>Disk Probe</summary>
        public static void DSKProbe()
        {
            ProcessStart();
        }

        /// <summary>Data Transformation Service (Dts) Wizard</summary>
        public static void DtsWizard()
        {
            ProcessStart();
        }

        ///<summary>DxDiag.exe DirectX Diagnostic Tool</summary>
        public static void DxDiag() { ProcessStart(); }

        /// <summary>Displays information of encrypted files on NTFS partitions.</summary>
        public static void EFSInfo()
        {
            String filename = "EFSInfo";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
                filename,
                null,
                null,
                true,
                ref redirectStandardOutput,
                ref redirectStandardError
            );
        }

        ///<summary>Email client</summary>
        public static void EmailClient()
        {
            Process.Start("mailto:user@domain.com?subject=subject text");
        }

        ///<summary>EventCombMt</summary>
        public static void EventCombMt()
        {
            FileStart("EventCombMt");
        }

        ///<summary>
        /// The EVENTQUERY.vbs script enables an administrator to list
        /// the events and event properties from one or more event logs.
        ///</summary>
        public static void EventQuery()
        {
            FileStart("EventQuery");
        }

        ///<summary>EventVwr: Event viewer EventVwr.msc</summary>
        public static void EventVwr() { ProcessStart(); }

        ///<summary>Exctrlst: Extensible Performance Counter List.</summary>
        public static void Exctrlst()
        {
            ProcessStart();
        }

        ///<summary>Explorer %SystemRoot%\Assembly</summary>
        public static void Explorer()
        {
            String filename = "Explorer";
            String argument = SystemRoot() + @"\Assembly";

            FileStart
            (
                filename,
                argument
            );
        }

        ///<summary>FileVer</summary>
        public static void FileVer()
        {
            String filename = "FileVer";
            String argument = "/V";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Firewall</summary>
        public static void Firewall()
        {
            ProcessStart("cpl");
        }

        ///<summary>ForFiles</summary>
        public static void ForFiles()
        {
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             "ForFiles",
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Shared Folders</summary>
        public static void FSMgmt()
        {
            ProcessStart("msc");
        }

        ///<summary>FSUtil</summary>
        public static void FSUtil()
        {
            String filename = "FSUtil";
            String argument = "fsInfo volumeInfo C:\\";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Ftp</summary>
        public static void Ftp()
        {
            String filename = "Ftp";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Media Access Control (MAC)</summary>
        public static void GetMAC()
        {
            String filename = "GetMAC";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>GetProcessIdentity()</summary>
        ///<remarks>Subject: UserName of a process 7/8/2005 1:30 PM PST By: Willy Denoyette [MVP] In: microsoft.public.dotnet.languages.csharp</remarks>
        public static void GetProcessIdentity()
        {
            int processId = -1;
            Process process = null;

            process = Process.GetCurrentProcess();
            processId = process.Id;

            GetProcessIdentity(processId);

        }

        ///<summary>GetProcessIdentity()</summary>
        ///<remarks>Subject: UserName of a process 7/8/2005 1:30 PM PST By: Willy Denoyette [MVP] In: microsoft.public.dotnet.languages.csharp</remarks>
        public static void GetProcessIdentity
        (
         int processId
        )
        {
            ManagementObject managementObjectProcess = null;
            PropertyDataCollection propertyDataCollection = null;

            managementObjectProcess = new ManagementObject("win32_process.handle=" + processId);

            foreach (ManagementObject managementObjectLogonSession in managementObjectProcess.GetRelated("win32_logonSession"))
            {
                foreach (ManagementBaseObject managementBaseObject in managementObjectLogonSession.GetRelated("win32_UserAccount"))
                {
                    propertyDataCollection = managementBaseObject.Properties;
                    System.Console.WriteLine
                    (
                     "Name: {0} | Domain: {1}  | Fullname: {2} | SID: {3}",
                     propertyDataCollection["Name"].Value,
                     propertyDataCollection["Domain"].Value,
                     propertyDataCollection["FullName"].Value,
                     propertyDataCollection["SID"].Value
                    );
                }
            }
        }

        ///<summary>GetType</summary>
        public static new void GetType()
        {
            String filename = "GetType";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Global Flags</summary>
        public static void GFlags()
        {
            ProcessStart();
        }

        ///<summary>GPEdit.msc Group Policy object editor</summary>
        public static void GPEdit()
        {
            ProcessStart("msc");
        }

        ///<summary>GPUpdate: Refreshes local Group Policy settings and Group Policy settings that are stored in Active Directory, including security settings. This command supersedes the now obsolete /refreshpolicy option for the secedit command.</summary>
        public static void GPUpdate()
        {
            String filename = "GPUpdate";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Add Hardware Wizard</summary>
        public static void Hdwwiz() { ProcessStart("cpl"); }

        /// <summary>Hosts</summary>
        public static void Hosts()
        {
            string filename = "NotePad";
            String argument = null;

            String systemDirectory = null;

            systemDirectory = Environment.SystemDirectory;
            argument = systemDirectory + @"\drivers\etc\hosts";

            FileStart
            (
             filename,
             argument
            );
        }

        /// <summary>HyperTerminal</summary>
        public static void HyperTerminal()
        {
            string filename = Path.Combine
            (
             Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
             @"Windows NT\HyperTrm.exe"
            );
            FileStart(filename);
        }

        /// <summary>Internet Properties</summary>
        public static void InetCpl()
        {
            ProcessStart("cpl");
        }

        /// <summary>Regional and Language Options</summary>
        public static void Intl()
        {
            ProcessStart("cpl");
        }

        /// <summary>IPConfig</summary>
        public static void IPConfig()
        {
            String filename = "IPConfig";
            String argument = "/all";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Internet Information Services Manager INetMgr</summary>
        public static void INetMgr()
        {
            string filename = Path.Combine
            (
             Environment.GetFolderPath(Environment.SpecialFolder.System),
             @"INetSrv\INetMgr.exe"
            );
            FileStart(filename);
        }

        /// <summary>IPXRoute</summary>
        public static void IPXRoute()
        {
            String filename = "IPXRoute";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>IISReset()</summary>
        public static void IISReset() { ProcessStart(); }

        /// <summary>LDP Lightweight Directory Access Protocol (LDAP) client.</summary>
        public static void LDP() { ProcessStart(); }

        /// <summary>LodCtr Registers new Performance counter names and Explain text for a service or device driver, and saves and restores counter settings and Explain text.</summary>
        public static void LodCtr()
        {
            String filename = "LodCtr";
            String argument = "/s:LodCtr.txt";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>
        ///  LogMan Update Counter Daily_Perf_Log -v mmddhhmm -c "\Processor(_Total)\% Processor Time" "\Memory\Available bytes" -si 00:15 -o "LogManDailyPerformanceLog" -a -m start
        ///  String argumentUpdate = "Update Counter Daily_Perf_Log -v mmddhhmm -c \"\Processor(_Total)\% Processor Time\" \"\Memory\Available bytes\" -si 00:15 -o \"LogManDailyPerformanceLog\" -a -m start";
        ///  In Visual C# and Visual C++, insert the escape sequence \" as an embedded quotation mark.
        /// </summary>
        public static void LogMan()
        {
            String filename = "LogMan";
            String argumentUpdate = @"Update Counter Daily_Perf_Log -v mmddhhmm -c '\Processor(_Total)\% Processor Time' '\Memory\Available bytes' -si 00:15 -o 'LogManDailyPerformanceLog' -a -m start";
            String argumentStart = "Start Daily_Perf_Log";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            argumentUpdate.Replace(SingleQuote, DoubleQuote);
            FileStart
            (
             filename,
             argumentUpdate,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );

            FileStart
            (
             filename,
             argumentStart,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );

        }

        /// <summary>Logoff</summary>
        public static void LogOff()
        {
            ProcessStart();
        }

        /// <summary>LPQ print queue status, Line Printer Daemon (LPD) service.</summary>
        public static void LPQ()
        {
            String filename = "LPQ";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>LPr Line Printer Daemon (LPD) service.</summary>
        public static void LPr()
        {
            String filename = "LPr";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Local Users and Groups</summary>
        public static void LUsrMgr()
        {
            ProcessStart("msc");
        }

        /// <summary>Magnifier. Accessibility.</summary>
        public static void Magnify()
        {
            ProcessStart();
        }

        /// <summary>MemSnap</summary>
        public static void MemSnap() { ProcessStart(); }

        /// <summary>Sounds And Audio Devices Properties</summary>
        public static void MMSys() { ProcessStart("cpl"); }

        /// <summary>Mode</summary>
        public static void Mode()
        {
            String filename = "Mode";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>C:\WINDOWS\PCHEALTH\HELPCTR\Binaries\MSConfig.exe System Configuration Utility. Services. Startup.</summary>
        public static void MSConfig()
        {
            string filename = Path.Combine(Environment.GetEnvironmentVariable("windir"), @"PCHEALTH\HELPCTR\Binaries\MSConfig.exe");
            FileStart(filename);
        }

        /// <summary>MSInfo32 System Information</summary>
        public static void MSInfo32()
        {
            string filename = Path.Combine
            (
             Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
             @"Common Files\Microsoft Shared\MSInfo\MSInfo32.exe"
            );
            FileStart(filename);
        }

        /// <summary>Remote Desktop Connection</summary>
        public static void Mstsc()
        {
            ProcessStart();
        }

        /// <summary>NBTStat.</summary>
        public static void NBTStat()
        {
            String filename = "NBTStat";
            String argument = "-n -r";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Network Connections</summary>
        public static void Ncpa()
        {
            ProcessStart("cpl");
        }

        /// <summary>Microsoft Network Monitor capture utility</summary>
        public static void NetCap()
        {
            String filename = "NetCap";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetDiag.</summary>
        public static void NetDiag()
        {
            String filename = "NetDiag";
            String argument = "/debug /fix /v";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Microsoft Network Monitor</summary>
        public static void NetMon()
        {
            String filename = @"C:\Windows\System32\NetMon\NetMon.exe";
            String argument = "/autostart";
            FileStart
            (
                filename,
                argument
            );
        }

        /// <summary>Displays protocol statistics and current TCP/IP network connections.</summary>
        public static void NetStat()
        {
            String filename = "NetStat";
            String argument = "-a -o";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetName</summary>
        public static void NetName()
        {
            String filename = "Net";
            String argument = "Name";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetSend: Must be running the Messenger service.</summary>
        public static void NetSend()
        {
            String filename = "Net";
            String argument = "Send *";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>Netsh</summary>
        public static void Netsh()
        {
            String filename = "Netsh";
            String argument = "Show Helper";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetStatisticsServer</summary>
        public static void NetStatisticsServer()
        {
            String filename = "Net";
            String argument = "Statistics Server";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetStatisticsWorkstation</summary>
        public static void NetStatisticsWorkstation()
        {
            String filename = "Net";
            String argument = "Statistics Workstation";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetTime: Time synchronization, is similar to W32tm.exe.</summary>
        public static void NetTime()
        {
            String filename = "Net";
            String argument = "Time";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NetUse. Mapping network drives.</summary>
        public static void NetUse()
        {
            String filename = "Net";
            String argument = @"Use Z: \\Server\C$";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }
		
        /// <summary>NLTest</summary>
        public static void NLTest()
        {
            String filename = "NLTest";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>NSLookup</summary>
        public static void NSLookup()
        {
            String filename = "NSLookup";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>ODBC Data Source Administrator</summary>
        public static void OdbcCp32()
        {
            ProcessStart("cpl");
        }

        ///<summary>
        /// Enables an administrator to list or disconnect files and folders
        /// that have been opened on a system.
        ///</summary>
        public static void OpenFiles()
        {
            String filename = "OpenFiles";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>On-Screen Keyboard</summary>
        public static void Osk()
        {
            ProcessStart();
        }

        ///<summary>PathPing</summary>
        public static void PathPing()
        {
            String filename = "PathPing";
            String argument = "127.0.0.1";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>PerfMon.exe </summary>
        public static void PerfMon() { ProcessStart(); }

        ///<summary>Packet InterNet Groper (Ping)</summary>
        public static void Ping()
        {
            String filename = "Ping";
            String argument = "127.0.0.1";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Pmon.exe is included in the Windows Server 2003 Resource Kit Tools. Superceded by PViewer.exe.</summary>
        public static void PMon() { ProcessStart(); }

        /// <summary>PortQry</summary>
        public static void PortQry()
        {
            String filename = "PortQry";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Power Options Properties</summary>
        public static void PowerCfg()
        {
            ProcessStart("cpl");
        }

        /// <summary>http://devdistrict.com/codedetails.aspx?A=414</summary>
        public static void Print(string filename)
        {
            PrintDialog dlg = new PrintDialog();
            System.Drawing.Printing.PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            dlg.PrinterSettings = ps;
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                p.StartInfo.FileName = filename;
                p.StartInfo.Verb = "PrintTo";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = "" + dlg.PrinterSettings.PrinterName.ToString() + "\"";
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }
        }

        /// <summary>ProcessInfo</summary>
        public static void ProcessInfo()
        {
            Process process = System.Diagnostics.Process.GetCurrentProcess();
            long mainMemoryUsage = process.WorkingSet64;
            long virtualMemorySize = process.VirtualMemorySize64;

            StackTrace stackTrace = new StackTrace();

            System.Console.WriteLine("StackTrace: {0}", Environment.StackTrace);
            System.Console.WriteLine("Calling Method: {0}", stackTrace.GetFrame(1).GetMethod().Name);
            System.Console.WriteLine("Current Method: {0}", stackTrace.GetFrame(0).GetMethod().Name);
            System.Console.WriteLine("Current Method: {0}", MethodBase.GetCurrentMethod().Name);
            System.Console.WriteLine("Executing Assembly: {0}", Assembly.GetExecutingAssembly().CodeBase);
            System.Console.WriteLine("Executing Assembly: {0}", Assembly.GetEntryAssembly().Location);

        }

        ///<summary>PStat</summary>
        public static void PStat() { ProcessStart(); }

        ///<summary>PTree</summary>
        public static void PTree() { ProcessStart(); }

        ///<summary>PViewer</summary>
        public static void PViewer() { ProcessStart(); }

        /// <summary>Quotes</summary>
        public static void Quotes()
        {

            String filename = "NotePad";
            String argument = null;

            String systemDirectory = null;

            systemDirectory = Environment.SystemDirectory;
            argument = systemDirectory + @"\drivers\etc\Quotes";

            FileStart
            (
             filename,
             argument
            );

        }

        ///<summary>Rcp: The destination must be running rshd, the remote shell service (daemon).</summary>
        public static void Rcp()
        {
            String filename = "Rcp";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>
        ///  ReLog LogManDailyPerformanceLog*.* -c "\Processor(_Total)\% Processor Time" "\Memory\Available bytes" -o "ReLogDailyPerformanceLog" -y
        ///  String argumentUpdate = "Update Counter Daily_Perf_Log -v mmddhhmm -c \"\Processor(_Total)\% Processor Time\" \"\Memory\Available bytes\" -si 00:15 -o \"LogManDailyPerformanceLog\" -a -m start";
        ///  In Visual C# and Visual C++, insert the escape sequence \" as an embedded quotation mark.
        /// </summary>
        public static void ReLog()
        {
            String filename = "ReLog";
            String argument = @"LogManDailyPerformanceLog*.* -c '\Processor(_Total)\% Processor Time' '\Memory\Available bytes' -o 'ReLogDailyPerformanceLog' -y";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            argument.Replace(SingleQuote, DoubleQuote);

            System.Console.WriteLine("Argument: {0}", argument);

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );

        }

        ///<summary>RSDiag</summary>
        public static void RSDiag()
        {
            String filename = "RSDiag";
            String argument = "/I";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Rsh: The destination must be running Rshsvc.exe, that is provided with the Windows 2000 Server Resource Kit.</summary>
        public static void Rsh()
        {
            String filename = "Rsh";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Route Print</summary>
        public static void RoutePrint()
        {
            String filename = "Route";
            String argument = "Print";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>RunAs</summary>
        public static void RunAs()
        {
            FileStart("RunAs");
        }

        ///<summary>SC Communicates with the Service Controller and installed services</summary>
        public static void SC()
        {
            String filename = "SC";
            String argument = "QueryEx";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Schtasks supercedes At %WINDIR%\Tasks</summary>
        public static void Schtasks()
        {
            String filename = "Schtasks";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>SecEdit</summary>
        public static void SecEdit()
        {
            String filename = "SecEdit";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>%SystemRoot%\system32\SecPol.msc</summary>
        public static void SecPol()
        {
            ProcessStart("Msc");
        }

        ///<summary>Services</summary>
        public static void Services()
        {
            ProcessStart("Msc");
        }

        ///<summary>
        /// SFC Scans all protected system files and replaces incorrect versions with correct Microsoft versions.
        ///     /SCANNOW Scans all protected system files immediately.
        ///</summary>    
        public static void Sfc()
        {
            String filename = "Sfc";
            String argument = "/SCANNOW";

            FileStart(filename, argument);
        }

        /// <summary>Shutdown</summary>
        public static void Shutdown()
        {
            ProcessStart();
        }

        /// <summary>File Signature Verification</summary>
        public static void SigVerif()
        {
            ProcessStart();
        }

        ///<summary>SNMPUtilG</summary>
        public static void SNMPUtilG()
        {
            String filename = "SNMPUtilG";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }//public static void SNMPUtilG()

	///<summary>Database Mapping Generator</summary>
	public static void SqlMetal()
	{
		ProcessStart();
	}
	
        ///<summary>Start</summary>
        public static void Start()
        {
            String filename = "Start";
            String argument = "/High /AboveNormal";
            FileStart(filename, argument);
        }

        ///<summary>Associates a path with a drive letter.</summary>
        public static void Subst() { ProcessStart(); }
		
        ///<summary>System Properties</summary>
        public static void Sysdm()
        {
            ProcessStart("cpl");
        }

        /// <summary>
        /// System Configuration Editor
        /// C:\Windows\System.ini
        /// C:\Windows\Win.ini
        /// C:\Config.sys
        /// C:\Autoexec.bat
        /// </summary>
        public static void SysEdit()
        {
            ProcessStart();
        }

        ///<summary>
	/// SystemInfo.
	///	Find out when you installed Windows.
	///</summary>
        public static void SystemInfo()
        {
            String filename = "SystemInfo";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>%SystemRoot%</summary>
        public static String SystemRoot()
        {
            int indexOfSystemDirectory = -1;
            String systemDirectory = null;
            String systemRoot = null;

            systemDirectory = Environment.SystemDirectory;
            indexOfSystemDirectory = systemDirectory.IndexOf("\\", 3);
            systemRoot = systemDirectory.Substring(0, indexOfSystemDirectory);
            return (systemRoot);
        }

        ///<summary>TaskList</summary>
        public static void TaskList()
        {
            String filename = "TaskList";
            String argument = "/svc";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>TaskMgr</summary>
        public static void TaskMgr()
        { 
            ProcessStart(); 
        }

        ///<summary>Phone and Modem Options</summary>
        public static void Telephon()
        {
            ProcessStart("cpl");
        }

        ///<summary>Telnet: C:\WINDOWS\system32\tlntsvr.exe service.</summary>
        public static void Telnet() { ProcessStart(); }

        ///<summary>Telnet: C:\WINDOWS\system32\tlntsvr.exe service.</summary>
        public static void Telnet
        (
         String serverName,
         int portNo
        )
        {
            String filename = "Telnet";
            String argument = serverName + " " + portNo;

            FileStart(filename, argument);
        }

        ///<summary>Telnet pop3_name port_number TelNet localhost 110.</summary>
        public static void TelnetPOP3()
        {
            Telnet("localhost", PORT_POP3);
        }

        ///<summary>Telnet smtp_name port_number TelNet localhost 25.</summary>
        public static void TelnetSMTP()
        {
            Telnet("localhost", PORT_SMTP);
        }

        ///<summary>Date and Time Properties</summary>
        public static void TimeDate()
        {
            ProcessStart("cpl");
        }

	///<summary>Type Library to Assembly Converter</summary>
	public static void TlbImp()
	{
		ProcessStart();
	}		

        /// <summary>
        ///  TraceRpt LogManDailyPerformanceLog*.* -o TraceRptDailyPerformanceLog.csv -report TraceRptDailyPerformanceLog.xml -f xml -y
        ///  String argumentUpdate = "Update Counter Daily_Perf_Log -v mmddhhmm -c \"\Processor(_Total)\% Processor Time\" \"\Memory\Available bytes\" -si 00:15 -o \"LogManDailyPerformanceLog\" -a -m start";
        ///  In Visual C# and Visual C++, insert the escape sequence \" as an embedded quotation mark.
        /// </summary>
        public static void TraceRpt()
        {
            String filename = "TraceRpt";
            String argument = @"LogManDailyPerformanceLog*.* -o TraceRptDailyPerformanceLog.csv -report TraceRptDailyPerformanceLog.xml -f xml -y";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );

        }

        ///<summary>TraceRt</summary>
        public static void TraceRt()
        {
            String filename = "TraceRt";
            String argument = "127.0.0.1";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>TypePerf</summary>
        public static void TypePerf()
        {
            String filename = "TypePerf";
            String argument = "\\Processor(_Total)\\% Processor Time";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Tip based on a newsgroup posting by Stoitcho Goutsev</summary>
        public static TimeSpan UpTime()
        { 
            PerformanceCounter upTime = new PerformanceCounter("System", "System Up Time");
            //You've got to call this twice. First time it returns 0 and the second time it returns the real info.
            upTime.NextValue();
            TimeSpan timeSpan = TimeSpan.FromSeconds(upTime.NextValue());
            return timeSpan;
            //ProcessStart();
        }

        ///<summary>Utility Manager. Accessibility.</summary>
        public static void UtilMan() { ProcessStart(); }

        ///<summary>Driver Verifier Manager</summary>
        public static void Verifier() { ProcessStart(); }

        ///<summary>Windows Management Instrumentation Tester</summary>
        ///<remarks>Connect to the root\cimv2 namespace</remarks>
        /* <see cref="UtilityManagement" /> */
        public static void WBEMTest() { ProcessStart(); }

        /// <summary>W32tm.exe: Time synchronization.</summary>
        public static void W32tm()
        {
            String filename = "W32tm";
            String argument = "/tz";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>Windows Communication Foundation Service Host.
	    /// C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\
	    ///</summary>
        public static void WcfSvcHost() { ProcessStart(); }

        public static void WcfTestClient() { ProcessStart(); }

        ///<summary>WinChat:Network DDE DSDM C:\WINDOWS\system32\netdde.exe. Network DDE.</summary>
        public static void WinChat() { ProcessStart(); }

        ///<summary>WhoAmI</summary>
        public static void WhoAmI()
        {
            String filename = "WhoAmI";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             null,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>WinPop.exe: POP3 Service administration utility.</summary>
        public static void WinPop()
        {
            String filename = "WinPop";
            String argument = "List";
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>WinCV Class Viewer</summary>
        public static void WinCV() { ProcessStart(); }

        /// <summary>WinPop.exe: POP3 Service administration utility. 
	///  WinPop Add domain, for example, WinPop Add localhost.
	/// </summary>
        public static void WinPopAddDomain()
        {
            String filename = "WinPop";
            String argument = "Add" + " " + Environment.UserDomainName;
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>WinPop.exe: POP3 Service administration utility.
	///  WinPop Add domain, for example, WinPop Add localhost.
	/// </summary>
        public static void WinPopAddDomain
        (
         String domainName
        )
        {
            String filename = "WinPop";
            String argument = "Add" + " " + domainName;
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>WinPop.exe: POP3 Service administration utility.
	///  WinPop Add mailbox@domain, for example, WinPop Add administrator@localhost.
	/// </summary>
        public static void WinPopAddMailbox()
        {
            String filename = "WinPop";
            String argument = "Add" + " " + Environment.UserName + "@" + Environment.UserDomainName;
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        /// <summary>WinPop.exe: POP3 Service administration utility.
	///  WinPop Add mailbox@domain, for example, WinPop Add administrator@localhost.
	/// </summary>
        public static void WinPopAddMailbox
        (
         String domainName,
         String mailbox
        )
        {
            String filename = "WinPop";
            String argument = "Add" + " " + mailbox + "@" + domainName;
            String redirectStandardOutput = null;
            String redirectStandardError = null;

            FileStart
            (
             filename,
             argument,
             null,
             true,
             ref redirectStandardOutput,
             ref redirectStandardError
            );
        }

        ///<summary>WinVer</summary>
        public static void WinVer()
        {
            ProcessStart("msc");
        }

        ///<summary>WMIC</summary>
        public static void WMIC()
        {
            ProcessStart();
        }

        ///<summary>Windows Management Infrastructure</summary>
        public static void WmiMgmt()
        {
            ProcessStart("msc");
        }

        ///<summary>WordPad</summary>
        public static void Write()
        {
            ProcessStart();
        }

        ///<summary>XamlPad</summary>
        public static void XamlPad()
        {
            ProcessStart();
        }

        ///<summary>XML Schema Definition</summary>
        public static void XSD() { ProcessStart(); }

    }
}