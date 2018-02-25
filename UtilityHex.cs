using System;

namespace WordEngineering
{
    /// <summary>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystembyteclasstopic.asp
    /// </summary>
    public class UtilityHex
    {
        ///<summary>HexDigits</summary>
        public static char[] HexDigits =
        {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        ///<summary>The entry point for the application.</summary>
        ///<param name="argv">Command-line parameters.</param>
        public static void Main(string[] argv)
        {

        }

        ///<summary>
        /// Alexander Duggleby MCC.in.tum.de/support/development/LanWaker/ Microsoft Competence Center am Lehrstuhl Informatik IV: Software &amp; Systems Engineering
        ///</summary>
        public static byte[] ToByteArray(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int hexStringIndex = 0; hexStringIndex < hexString.Length; hexStringIndex += 2)
            {
                bytes[hexStringIndex / 2] = byte.Parse
                                            (
                                                hexString.Substring(hexStringIndex, 2),
                                                System.Globalization.NumberStyles.HexNumber
                                            );
            }
            return (bytes);
        }

        public static string ToHexString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = HexDigits[b >> 4];
                chars[i * 2 + 1] = HexDigits[b & 0xF];
            }
            return new string(chars);
        }

        public static string ToHexString(int number)
        {
            return (number.ToString("X"));
        }

        /// <summary>
        /// http://devdistrict.com/codedetails.aspx?A=63
        /// </summary>
        public static Int32 ToInt32(string hexString)
        {
            return (int.Parse(hexString, System.Globalization.NumberStyles.HexNumber));
        }

        /// <summary>
        /// http://devdistrict.com/codedetails.aspx?A=63
        /// </summary>
        public static uint ToUInt32(string hexString)
        {
            return (System.Convert.ToUInt32(hexString, 16));
        }
    }
}