using System;
using System.Collections;   
using System.Text;
using System.Windows.Forms;

namespace WordEngineering
{
    /// <summary>
    /// http://devdistrict.com/codedetails.aspx?A=412
    /// http://msdn2.microsoft.com/en-us/library/system.windows.forms.clipboard.aspx
    /// http://msdn2.microsoft.com/en-us/library/system.windows.forms.dataformats.aspx DataFormats Class
    /// </summary>
    public class UtilityClipboard
    {
        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        [STAThread]
        public static void Main(string[] argv)
        {
            // Creates a new data format.
            DataFormats.Format environmentVariableFormat = DataFormats.GetFormat("EnvironmentVariable");

            ClipboardSetDataObject(environmentVariableFormat);
            ClipboardGetDataObject(environmentVariableFormat);
        }

        public static void ClipboardGetDataObject(DataFormats.Format environmentVariableFormat)
        {
            // Retrieves the data from the clipboard.
            IDataObject retrievedObject = Clipboard.GetDataObject();

            // Converts the IDataObject type to EnvironmentVariable type. 
            EnvironmentVariable environmentVariable = (EnvironmentVariable)retrievedObject.GetData(environmentVariableFormat.Name);

            // Prints the value of the Object.
            System.Console.WriteLine(environmentVariable);
        }

         public static void ClipboardSetDataObject(DataFormats.Format environmentVariableFormat)
        {
            /* Creates a new object and stores it in a DataObject using myFormat 
             * as the type of format. */
            EnvironmentVariable environmentVariable = new EnvironmentVariable();
            DataObject environmentVariableDataObject = new DataObject(environmentVariableFormat.Name, environmentVariable);

            // Copy into the clipboard.
            Clipboard.SetDataObject(environmentVariableDataObject);
        }
    }

    [Serializable]
    public class EnvironmentVariable : Object
    {
        private IDictionary environmentValue;

        // Creates a default constructor for the class.
        public EnvironmentVariable()
        {
            environmentValue = Environment.GetEnvironmentVariables();
        }

        // Creates a property to retrieve or set the value.
        public IDictionary EnvironmentValue
        {
            get
            {
                return environmentValue;
            }
            set
            {
                environmentValue = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry de in EnvironmentValue)
            {
                sb.AppendFormat("  {0} = {1}{2}", de.Key, de.Value, Environment.NewLine);
            }
            return (sb.ToString());
        }
    }
}