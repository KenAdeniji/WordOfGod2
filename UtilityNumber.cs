using System;

namespace WordEngineering
{
	public class UtilityNumber
	{
		public const int Number = 100;
		public static string[] ArabicNums = new string[] {"1", "4", "5", "9", "10", "40", "50", "90", "100", "400", "500", "900", "1000"};    
		public static string[] RomanNums = new string[] {"I","IV","V","IX","X","XL","L","XC","C","CD","D","CM","M"};
		public static int ArabicUpper = ArabicNums.GetUpperBound(0); 
		public static int ArabicLower = ArabicNums.GetLowerBound(0);

        /// <summary>The entry point for the application.</summary>
        /// <param name="argv">A list of command line arguments</param>
        public static void Main(string[] argv)
        {
			int number = Number;	
			if (argv.Length > 0) {number = System.Convert.ToInt32(argv[0]); }
            System.Console.WriteLine("Arabic number: {0} | Roman number: {1}", number, GetRomanNumber(number));
        }

		///>summary>
		/// http://www.dotnettechnologies.com/dotnettechnologies/CategoryView,category,ASP.Net.aspx
		///</summary>
		public static string GetRomanNumber(int number)
		{    
			string output = "";
			for (int i = ArabicUpper; i >= ArabicLower; i--)
			{  
				while (number >= Convert.ToInt32(ArabicNums[i]))
				{
					number -= Convert.ToInt32(ArabicNums[i]);
					output += RomanNums[i]; 
				}
			}
			return output;
		}
	}
}