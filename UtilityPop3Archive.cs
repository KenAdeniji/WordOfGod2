using System;
using System.Net.Sockets;
using System.Text;

using CommandLine;

namespace WordEngineering
{
	public class UtilityPop3Argument
	{
		private string server;
		private int port;
		private int timeout;
		private string username;
		private string password;

		///<summary>files</summary>
		[DefaultArgumentAttribute(ArgumentType.MultipleUnique)]
		public string[] files;
  
		public UtilityPop3Argument():this
		(
			UtilityPop3.Pop3Server,
			UtilityPop3.Pop3Port,
			UtilityPop3.Pop3Timeout,
			null,
			null
		)	
		{
		}
  
		public UtilityPop3Argument
		(
			string server,
			int port,
			int timeout,
			string username,
			string password
		)
		{
			this.server = server;
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

		public string Server 
		{
			get { return server; }
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

	public class UtilityPop3
	{
		public const string Pop3Server = "localhost";
		public const int Pop3Port = 110;
		public const int Pop3Timeout = 1000000; //Milliseconds

		public static string EndConditionMultiLine = System.Environment.NewLine + System.Environment.NewLine + ".";	
		public static string EndConditionSingleLine = System.Environment.NewLine;	
		
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
				utilityPop3Argument.Server,
				utilityPop3Argument.Port,
				out networkStream,
				utilityPop3Argument.Timeout
			);
		}
		
		public static TcpClient Connect (string server, int port, out NetworkStream networkStream, int timeout)
		{
			String response;
			TcpClient tcpClient;
			
			tcpClient = new TcpClient();
			tcpClient.ReceiveTimeout = timeout;
			tcpClient.Connect(server, port);
			networkStream = tcpClient.GetStream();
			networkStream.ReadTimeout = timeout;
			networkStream.WriteTimeout = timeout;
			response = GetResponse(tcpClient, networkStream);
			if (response.EndsWith("+OK") == false)
			{
				throw new Exception(String.Format("Connection Failed. Response: {0}", response));
			}
			return (tcpClient);
		}

		public static String GetResponse(TcpClient tcpClient, NetworkStream networkStream)
		{
			return(GetResponse(tcpClient, networkStream, false));
		}

		/// <summary>GetResponse wraps the work of waiting for a server response to complete Single-Line and Multi-Line responses end differently, so they need slightly different end conditions.</summary>
		public static String GetResponse(TcpClient tcpClient, NetworkStream networkStream, bool multiLine)
		{
			byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
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
	}	
}