using System;
using System.IO;
using System.Net;

using CommandLine;

namespace WordEngineering
{
    public class UtilityFtpArgument
    {
        public string host;
        public int port;
        public string user;
        public string password;

        [DefaultArgumentAttribute(ArgumentType.MultipleUnique)]
        public string[] files;

        public UtilityFtpArgument() : this
        (
            UtilityFtp.Host,
            UtilityFtp.Port,
            UtilityFtp.User,
            UtilityFtp.Password
        )
        {
        }

        public UtilityFtpArgument
        (
            string host,
            int port,
            string user,
            string password
        )
        {
            this.host = host;
            this.port = port;
            this.user = user;
            this.password = password;
        }

        public int Port
        {
            get { return port; }
        }

        public string Password
        {
            get { return password; }
        }

        public string Host
        {
            get { return host; }
        }

        public string User
        {
            get { return user; }
        }
    }

    /// <summary>
    /// http://windowsdevcenter.com/pub/a/windows/2006/12/12/building-ftp-services-using-net-20.html Building FTP Services Using .NET 2.0 by Wei-Meng Lee
    /// http://www.rfc-editor.org 
    /// http://www.ietf.org/rfc/rfc959.txt 
    /// CALL UtilityFtp /host:"ftp://e-comfort.ephraimtech.com/%2fFtp" /user:FtpUser@ephraimtech.com /password:FtpPassword
    /// </summary>
    public static class UtilityFtp
    {
        public const int Port = 21;
        public const string Host = "ftp://127.0.0.1";
        public const string Password = null;
        public const string User = "anonymous";
        
        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
            bool parseCommandLineArguments;
            UtilityFtpArgument utilityFtpArgument = new UtilityFtpArgument();
            parseCommandLineArguments = Parser.ParseArgumentsWithUsage(argv, utilityFtpArgument);
            if (parseCommandLineArguments == false) { return; }
            Stub(utilityFtpArgument);
        }

        public static void Stub(UtilityFtpArgument utilityFtpArgument)
        {
            FtpWebRequest ftpWebRequest = null;
            ftpWebRequest = FtpWebRequestCreate(utilityFtpArgument);

            ListDirectory(ftpWebRequest);

            //DownloadFileText();
            //MakeDirectory(new Uri("ftp://127.0.0.1/MakeDirectory"));
        }

        public static void DownloadFileText()
        {
            string filename = System.IO.Path.Combine(Host, "text.txt");
            FtpWebRequest ftpWebRequest = null;
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            StreamReader streamReader = null;

            try
            {
                ftpWebRequest = (FtpWebRequest) WebRequest.Create(filename);
                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpWebRequest.Credentials = new NetworkCredential("anonymous", "password");

                ftpWebResponse = (FtpWebResponse) ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
                streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
                System.Console.WriteLine(streamReader.ReadToEnd());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public static String ListDirectory(FtpWebRequest ftpWebRequest)
        {
            string listDirectory = null;
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            StreamReader streamReader = null;

            try
            {
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
                streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
                listDirectory = streamReader.ReadToEnd();
                System.Console.WriteLine(listDirectory);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return (listDirectory);
        }
            
        public static bool MakeDirectory(Uri serverUri)
        {
            // The serverUri should start with the ftp:// scheme.
            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return false;
            }

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.KeepAlive = true;
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Status: {0}", response.StatusDescription);
            return true;
        }

        ///<remarks>
        ///http://msdn2.microsoft.com/en-us/library/system.net.ftpwebrequest.aspx
        /// To obtain an instance of FtpWebRequest, use the Create method. You can also use the WebClient class to upload and download information from an FTP server. Using either of these approaches, when you specify a network resource that uses the FTP scheme (for example, "ftp://contoso.com") the FtpWebRequest class provides the ability to programmatically interact with FTP servers.
        /// The URI may be relative or absolute. If the URI is of the form "ftp://contoso.com/%2fpath" (%2f is an escaped '/'), then the URI is absolute, and the current directory is /path. If, however, the URI is of the form "ftp://contoso.com/path", first the .NET Framework logs into the FTP server (using the user name and password set by the Credentials property), then the current directory is set to <UserLoginDirectory>/path.
        /// You must have a valid user name and password for the server or the server must allow anonymous logon. You can specify the credentials used to connect to the server by setting the Credentials property or you can include them in the UserInfo portion of the URI passed to the Create method. If you include UserInfo information in the URI, the Credentials property is set to a new network credential with the specified user name and password information. 
        ///http://msdn2.microsoft.com/en-us/library/system.uri.userinfo.aspx 
        /// Uri uriAddress = new Uri ("http://user:password@www.contoso.com/index.htm"); 
        /// 
        /// ftp://FtpUser@ephraimtech.com:FtpPassword123*@e-comfort.ephraimtech.com:21/fFtp
        ///     Invalid URI: A port was expected because of there is a colon (':') present but the port could not be parsed.
        ///</remarks>
        public static FtpWebRequest FtpWebRequestCreate(UtilityFtpArgument utilityFtpArgument)
        {
            FtpWebRequest ftpWebRequest = null;
            Uri uri = null;

            try
            {
                /*
                UriBuilder uriBuilder = null;
                uriBuilder = new UriBuilder(utilityFtpArgument.Host);

                if (utilityFtpArgument.User != UtilityFtp.User)
                {
                    uriBuilder.UserName = utilityFtpArgument.User;
                }
                if (utilityFtpArgument.Password != UtilityFtp.Password)
                {
                    uriBuilder.Password = utilityFtpArgument.Password;
                }
                if (utilityFtpArgument.Port != UtilityFtp.Port)
                {
                    uriBuilder.Port = utilityFtpArgument.Port;
                }
                System.Console.WriteLine(uriBuilder.ToString());
                uri = new Uri(uriBuilder.ToString());
                 
                ftpWebRequest = (FtpWebRequest)WebRequest.Create(uri);
                */

                uri = new Uri(utilityFtpArgument.Host);
                ftpWebRequest = (FtpWebRequest)WebRequest.Create(uri);
                ftpWebRequest.Credentials = new NetworkCredential(utilityFtpArgument.User, utilityFtpArgument.Password);

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return (ftpWebRequest);
        }
    }
}

/*
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): anonymous
331 Anonymous access allowed, send identity (e-mail name) as password.
Password:
230 Anonymous user logged in.
ftp> disconnect
221
ftp> open e-comfort.ephraimtech.com/WordOfGod
Unknown host e-comfort.ephraimtech.com/WordOfGod.
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> open e-comfort.ephraimtech.com
Already connected to e-comfort.ephraimtech.com, use disconnect first.
ftp> disconnect
221
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> user kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> quit
221

C:\Documents and Settings\KAdeniji>ftp /?
Unknown host /?.
ftp> open ftp://e-comfort.ephraimtech.com/WordOfGod
Unknown host ftp://e-comfort.ephraimtech.com/WordOfGod.
ftp> open e-comfort.ephraimtech.com/WordOfGod
Unknown host e-comfort.ephraimtech.com/WordOfGod.
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> open e-comfort.ephraimtech.com
Already connected to e-comfort.ephraimtech.com, use disconnect first.
ftp> user kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> help
Commands may be abbreviated.  Commands are:

!               delete          literal         prompt          send
?               debug           ls              put             status
append          dir             mdelete         pwd             trace
ascii           disconnect      mdir            quit            type
bell            get             mget            quote           user
binary          glob            mkdir           recv            verbose
bye             hash            mls             remotehelp
cd              help            mput            rename
close           lcd             open            rmdir
ftp> dir
Connection closed by remote host.
ftp> dir
Not connected.
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> disconnect
421 Timeout (120 seconds): closing control connection.
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
421 Terminating connection.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): anonymous
331 Anonymous access allowed, send identity (e-mail name) as password.
Password:
230 Anonymous user logged in.
ftp> help
Commands may be abbreviated.  Commands are:

!               delete          literal         prompt          send
?               debug           ls              put             status
append          dir             mdelete         pwd             trace
ascii           disconnect      mdir            quit            type
bell            get             mget            quote           user
binary          glob            mkdir           recv            verbose
bye             hash            mls             remotehelp
cd              help            mput            rename
close           lcd             open            rmdir
ftp> ls
200 PORT command successful.
150 Opening ASCII mode data connection for file list.
text.txt
226 Transfer complete.
ftp: 10 bytes received in 0.02Seconds 0.63Kbytes/sec.
ftp> mdir public
Local file public
output to local-file: public?
200 PORT command successful.
150 Opening ASCII mode data connection for /bin/ls.
550 public: The system cannot find the file specified.
ftp> ls
200 PORT command successful.
150 Opening ASCII mode data connection for file list.
text.txt
226 Transfer complete.
ftp: 10 bytes received in 0.02Seconds 0.63Kbytes/sec.
ftp> help mkdir
mkdir           Make directory on the remote machine
ftp> mkdir
Directory name public
550 public: Access is denied.
ftp> mkdir
Directory name public
Connection closed by remote host.
ftp> ls
Not connected.
ftp> open e-comfort.ephraimtech.com
Connected to e-comfort.ephraimtech.com.
220 Microsoft FTP Service
User (e-comfort.ephraimtech.com:(none)): anonymous
331 Anonymous access allowed, send identity (e-mail name) as password.
Password:
230 Anonymous user logged in.
ftp> mkdir public
550 public: Access is denied.
ftp> quit

C:\Documents and Settings\KAdeniji>ftp e-comfort.ephraimtech,com
Unknown host e-comfort.ephraimtech,com.
ftp> quit

C:\Documents and Settings\KAdeniji>cd\

C:\>ftp localhost
Connected to comfort.ephraimtech.com.
220 Microsoft FTP Service
User (comfort.ephraimtech.com:(none)): kadeniji
331 Password required for kadeniji.
Password:
530 User kadeniji cannot log in.
Login failed.
ftp> user kadeniji@ephraimtech.com
331 Password required for kadeniji@ephraimtech.com.
Password:
230 User kadeniji@ephraimtech.com logged in.
ftp> dir
200 PORT command successful.
150 Opening ASCII mode data connection for /bin/ls.
01-27-07  11:48AM       <DIR>          MakeDirectory
226 Transfer complete.
ftp: 54 bytes received in 0.13Seconds 0.43Kbytes/sec.
ftp> pwd
257 "/" is current directory.
ftp> cd /Baynet.com
250 CWD command successful.
ftp> ls
200 PORT command successful.
150 Opening ASCII mode data connection for file list.
EAgents
EAWebSites
ListingProducer
logs
Program Files
226 Transfer complete.
ftp: 59 bytes received in 0.00Seconds 59000.00Kbytes/sec.
ftp> cd\
Invalid command.
ftp> cd /
250 CWD command successful.
ftp> cd EAgents
250 CWD command successful.
ftp> ls
200 PORT command successful.
150 Opening ASCII mode data connection for file list.
Agents
App_Code
App_Data
app_offline.htm.save
App_Themes
Bin
BinArchive
BusinessLogic
DataAccess
Default.aspx
Default.aspx.cs
Documentation
EAgents.sln
EAgents.vssscc
Global.asax
Images
JavaScript
licenses.licx
Log4net.config
MasterPages
Member
menuStyleBasic.css
mssccprj.scc
Presentation
Session.aspx
SQL
SystemFramework
UC
UML
vssver.scc
web.config
226 Transfer complete.
ftp: 383 bytes received in 0.11Seconds 3.51Kbytes/sec.
ftp> disconnect
221
ftp>
*/