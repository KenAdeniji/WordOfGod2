using System;
using System.Net;
using System.Net.NetworkInformation;

namespace WordEngineering
{
    public class UtilityIPGlobalProperties
    {
        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
            IPGlobalPropertiesGetActiveTcpListeners();
            IPGlobalPropertiesIsWinsProxy();
        }

        /// <summary>http://devdistrict.com/codedetails.aspx?A=412</summary>
        public static void IPGlobalPropertiesGetActiveTcpListeners()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveTcpListeners();

            foreach (IPEndPoint p in endPoints)
            {
                Console.WriteLine("{0} : {1}", p.Address, p.Port);
            }
        }

        /// <summary>http://devdistrict.com/codedetails.aspx?A=398</summary>
        public static void IPGlobalPropertiesIsWinsProxy()
        {
            System.Net.NetworkInformation.IPGlobalProperties prop = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("Windows internet name service proxy: {0}", prop.IsWinsProxy);
        }
    }
}
