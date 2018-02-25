using System;
using System.Collections;
using System.Security;
using System.Security.Policy;

namespace WordEngineering
{
    public class UtilitySecurityManager
    {
        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
            DisplaySecurityPolicy();
        }

        private static void DisplaySecurityPolicy()
        {
            IEnumerator e = SecurityManager.PolicyHierarchy();

            while (e.MoveNext())
            {
                PolicyLevel currentLevel = (PolicyLevel)e.Current;
                Console.WriteLine(currentLevel.Label);

                IList perms = currentLevel.NamedPermissionSets;
                IEnumerator p = perms.GetEnumerator();

                while (p.MoveNext())
                {
                    Console.WriteLine("\t" + ((NamedPermissionSet)p.Current).Name);
                }
            }
        }
    }
}
