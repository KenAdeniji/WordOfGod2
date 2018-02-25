using System;
using System.Net.Sockets;
using System.Text;

using CommandLine;

namespace WordEngineering
{
	public class UtilityPop3Argument
	{
		public string hostname;
		public int port;
		public int timeout;
		public string username;
		public string password;

		[DefaultArgumentAttribute(ArgumentType.MultipleUnique)]
		public string[] files;
  
		public UtilityPop3Argument():this
		(
			UtilityPop3.Pop3Hostname,
			UtilityPop3.Pop3Port,
			UtilityPop3.Pop3Timeout,
			null,
			null
		)	
		{
		}
  
		public UtilityPop3Argument
		(
			string hostname,
			int port,
			int timeout,
			string username,
			string password
		)
		{
			this.hostname = hostname;
			this.port = port;
			this.timeout = timeout; 
			this.username = username;
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

		public string Hostname 
		{
			get { return hostname; }
		}

		public int Timeout
		{
			get { return timeout; }
		}
		
		public string Username
		{
			get { return username; }
		}
	}

	/// <summary>
    ///  Yahoo Incoming Mail Server (POP3) - pop.mail.yahoo.com (port 110)
    ///  Yahoo Outgoing Mail Server (SMTP) - smtp.mail.yahoo.com (port 25)
    ///  Google Gmail Incoming Mail Server (POP3) - pop.gmail.com (SSL enabled, port 995)
    ///  Outgoing Mail Server - use the SMTP mail server address provided by your local ISP
	///    UtilityPop3.exe /hostname:POP3.HOTMAIL.COM /port:110
    ///    UtilityPop3.exe /hostname:pop3.mail.hotmail.com /port:110
    ///    UtilityPop3.exe /hostname:services.msn.com /port:110
    ///    UtilityPop3.exe /hostname:mail.hotmail.com /port:110
    ///    UtilityPop3.exe /hostname:localhost /port:110
	///</summary>
	public class UtilityPop3
	{
		public const string Pop3Hostname = "localhost";
		public const int Pop3Port = 110;
		public const int Pop3Timeout = 1000000; //Milliseconds

		public static string EndConditionMultiLine = System.Environment.NewLine + System.Environment.NewLine + ".";	
		public static string EndConditionSingleLine = System.Environment.NewLine;	

		public static string POP3CommandList = "LIST" + System.Environment.NewLine;
		public static string POP3CommandPass = "PASS {0}" + System.Environment.NewLine;
		public static string POP3CommandStat = "STAT" + System.Environment.NewLine;
		public static string POP3CommandQuit = "QUIT" + System.Environment.NewLine;
		public static string POP3CommandTop = "TOP {0}" + System.Environment.NewLine;
		public static string POP3CommandUser = "USER {0}" + System.Environment.NewLine;
		public const string PositiveResponse = "+OK";
		
		/// <summary>The entry point for the application.</summary>
		/// <param name="argv">A list of command line arguments</param>
		public static void Main(String[] argv)
		{
			bool parseCommandLineArguments;
			UtilityPop3Argument  utilityPop3Argument = new UtilityPop3Argument();
			parseCommandLineArguments = Parser.ParseArgumentsWithUsage(argv, utilityPop3Argument);
			if ( parseCommandLineArguments == false ) {	return;  }
			Stub( utilityPop3Argument );
		}
		
		public static void Stub(UtilityPop3Argument utilityPop3Argument)
		{
			NetworkStream networkStream;	
			TcpClient tcpClient = Connect
			(
				utilityPop3Argument.Hostname,
				utilityPop3Argument.Port,
				out networkStream,
				utilityPop3Argument.Timeout
			);
		}
		
		public static TcpClient Connect (string hostname, int port, out NetworkStream networkStream, int timeout)
		{
			String response;
			TcpClient tcpClient;
			
			tcpClient = new TcpClient();
			tcpClient.ReceiveTimeout = timeout;
			tcpClient.Connect(hostname, port);
			networkStream = tcpClient.GetStream();
			networkStream.ReadTimeout = timeout;
			networkStream.WriteTimeout = timeout;
			response = GetResponse(networkStream, tcpClient.ReceiveBufferSize);
			if (response.StartsWith(PositiveResponse) == false)
			{
				throw new Exception(String.Format("Connection failed. Response: {0}", response));
			}
			response = SendCommand(networkStream, String.Format(POP3CommandUser, "mailpop3110"), tcpClient.ReceiveBufferSize);
			if (response.StartsWith(PositiveResponse) == false)
			{
				throw new Exception(String.Format("User command failed. Response: {0}", response));
			}
			/*
			response = SendCommand(networkStream, String.Format(POP3CommandPass, "transit4201"), tcpClient.ReceiveBufferSize);
			if (response.StartsWith(PositiveResponse) == false)
			{
				throw new Exception(String.Format("Pass command failed. Response: {0}", response));
			}
			*/
			return (tcpClient);
		}

		/*
		public static string POP3CommandList = "LIST" + System.Environment.NewLine;
		public static string POP3CommandPass = "PASS {0}" + System.Environment.NewLine;
		public static string POP3CommandStat = "STAT" + System.Environment.NewLine;
		public static string POP3CommandQuit = "QUIT" + System.Environment.NewLine;
		public static string POP3CommandTop = "TOP {0}" + System.Environment.NewLine;
		public static string POP3CommandUser = "USER {0}" + System.Environment.NewLine;
		public const string PositiveResponse = "+OK";
		*/

		public static String GetResponse(NetworkStream networkStream, int byteSize)
		{
			return(GetResponse(networkStream, byteSize, false));
		}

		/// <summary>GetResponse wraps the work of waiting for a hostname response to complete Single-Line and Multi-Line responses end differently, so they need slightly different end conditions.</summary>
		public static String GetResponse(NetworkStream networkStream, int byteSize, bool multiLine)
		{
			byte[] bytes = new byte[byteSize];
			int bytesRead;
			string endCondition = multiLine == true ? EndConditionMultiLine : EndConditionSingleLine;
			string response;
			DateTime startTime;
			StringBuilder sb = new StringBuilder();

			do
			{
				startTime = DateTime.Now;
				bytesRead = networkStream.Read(bytes, 0, bytes.Length);
				response = Encoding.ASCII.GetString(bytes, 0, bytesRead);
				sb.Append(response);
			}
			while
			(
				networkStream.DataAvailable &&
				response.EndsWith(endCondition) == false &&
				DateTime.Now.Subtract(startTime).TotalMilliseconds < networkStream.ReadTimeout
			);
			
			return (sb.ToString());
		}

		public static String SendCommand(NetworkStream networkStream, string command, int byteSize)
		{
			return(SendCommand(networkStream, command, byteSize, false));
		}

		/// <summary>SendCommand abstracts sending a string and receiving a response</summary>
		public static String SendCommand(NetworkStream networkStream, string command, int byteSize, bool multiLine)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(command);
			networkStream.Write(bytes, 0, bytes.GetLength(0));
			return(GetResponse(networkStream, byteSize, multiLine));
		}
	}	
}