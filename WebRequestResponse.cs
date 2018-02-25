using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace WordEngineering
{
    public class WebRequestResponse
    {
        public const string WebAddress = "http://www.microsoft.com";

        public static void Main(string[] argv)
        {
            string uri = WebAddress;
            if (argv.Length >= 1) uri = argv[0];
            WebRequestResponseStub(uri);
            System.Console.WriteLine("ResponseContentLength: {0}", ResponseContentLength(uri));
			System.Console.WriteLine("HttpStatus: {0}", (int)HttpStatus(uri));
        }

		///<summary>
		///Mads Kristensen Get the HTTP status code from a URL by  http://www.madskristensen.dk/blog/Get+The+HTTP+Status+Code+From+A+URL.aspx 
		///http://msdn2.microsoft.com/en-gb/library/system.net.httpstatuscode.aspx
		///</summary>
		public static HttpStatusCode HttpStatus(string url)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					return response.StatusCode;
				}
			}
			catch (System.Net.WebException)
			{
				return HttpStatusCode.NotFound;
			}
		}
		
        public static void WebRequestResponseStub(string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
            System.Console.WriteLine(reader.ReadToEnd());
        }

        /// <summary>
        /// http://devdistrict.com/codedetails.aspx?A=387 
        /// Get the Size of a File over the Internet
        /// </summary>
        public static long ResponseContentLength(string uri)
        {
            long responseContentLength;
            WebRequest request = WebRequest.Create(uri);            
            request.Method = "HEAD";
            using (WebResponse response = request.GetResponse())
            {              
                responseContentLength = response.ContentLength;
            }
            return(responseContentLength);
        }
     }
}