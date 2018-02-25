using System;
using System.Net;
using System.Net.NetworkInformation;

namespace WordEngineering
{
    public class UtilitySecureString
    {
        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
            SecureStringStub();
        }

        /// <summary>http://devdistrict.com/codedetails.aspx?A=405</summary>
        public static void SecureStringStub()
        {
            /*
             * Building the SecureString
               To put data into the secure string you need to feed it one character at a time. Alternatively you can give the constructor a pointer to an array of characters to load the data in. 
            */ 
            System.Security.SecureString secureData = new System.Security.SecureString();
            secureData.AppendChar('M');
            secureData.AppendChar('y');
            secureData.AppendChar('P');
            secureData.AppendChar('a');
            secureData.AppendChar('s');
            secureData.AppendChar('s');
            secureData.AppendChar('w');
            secureData.AppendChar('o');
            secureData.AppendChar('r');
            secureData.AppendChar('d');

            /*
             * Pinning the SecureString
                Once you are done feeding data into the SecureString you need to call the MakeReadOnly() method. This makes the string immutable, and pins the location of the string in memory so the .NET Framework doesn't move it around. 
                Make it immutable
            */
            secureData.MakeReadOnly();

            /*
            Reading from a SecureString
            Reading the data from the SecureString is a bit of a pain, having to use Interop. 
            NOTE: This code is for instruction only and is not secure since we read the value of our SecureString into a normal string thus defeating our own security. 
            */ 
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureData);
            string unsecureData = System.Runtime.InteropServices.Marshal.PtrToStringUni(ptr);

            /*
            Clean up when your done!
            Always clean up when your done with your SecureString. You can easily do this by calling the Dispose method. This deletes the data from memory. 
            */ 
            secureData.Dispose();
        }
    }
}
