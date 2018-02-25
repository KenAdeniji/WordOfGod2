using System;
using System.IO;

using SHDocVw;
using AxSHDocVw;

namespace WordEngineering
{
    public class UtilityInternetExplorer
    {
        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
            OpeningInternetExplorer();
        }

        /// <summary>
        /// http://devdistrict.com/codedetails.aspx?A=416
        /// </summary>
        public static void OpeningInternetExplorer()
        {
            //Create a reference to IE
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();

            //Make it visible so we can see it
            ie.Visible = true;

            object o = null;

            //Navigate to a web page
            ie.Navigate("http://devdistrict.com", ref o, ref o, ref o, ref o);
        }

        /// <summary>
        /// http://weblogs.asp.net/stevencohn/archive/2004/01/23/62117.aspx Automating IE Remotely Using C#
        /// </summary>
        public static void AutomatingIERemotely()
        {
            string filename;
            SHDocVw.InternetExplorer browser = null;
            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();
            foreach (SHDocVw.InternetExplorer ie in shellWindows)
            {
                filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
                if (filename.Equals("iexplore"))
                {
                    browser = ie;
                    break;
                }
            }
        }
    }
}
