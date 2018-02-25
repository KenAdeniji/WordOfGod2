using System;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Text;

namespace WordEngineering
{
 ///<summary>UtilityUnicode</summary>
 public class UtilityUnicode
 {

  ///<summary>Welcome</summary>
  public static char[][] Welcome = 
  {
   //English
   new char[]
   {
   '\u0057',
   '\u0065',
   '\u006c',
   '\u0063',
   '\u006F',
   '\u006D',
   '\u0065',
   '\u0020',
   '\u0074',
   '\u006F',
   '\u0020'
   },

   //French
   new char[]
   {
   '\u0042',
   '\u0069',
   '\u0065',
   '\u006E',
   '\u0076',
   '\u0065',
   '\u006E',
   '\u0075',
   '\u0065',
   '\u0020',
   '\u0061',
   '\u0075',
   '\u0020'
   },

   //German
   new char[]
   {
   '\u0057',
   '\u0069',
   '\u006C',
   '\u006B',
   '\u006F',
   '\u006D',
   '\u006D',
   '\u0065',
   '\u006E',
   '\u0020',
   '\u0061',
   '\u007A',
   '\u0075',
   '\u0020'
   },

   //Japan
   new char[]
   {
   '\u3078',
   '\u3087',
   '\u3045',
   '\u3053',
   '\u305D',
   '\u0021'
   },

   //Portueguese
   new char[]
   {
   '\u0053',
   '\u0065',
   '\u006A',
   '\u0061',
   '\u0020',
   '\u0062',
   '\u0065',
   '\u006D',
   '\u0020',
   '\u0076',
   '\u0069',
   '\u006E',
   '\u0064',
   '\u006F',
   '\u0020',
   '\u0061',
   '\u0020',
   },
   
   //Russian
   new char[]
   {
   '\u0414',
   '\u043E',
   '\u0431',
   '\u0440',
   '\u043E',
   '\u0020',
   '\u043F',
   '\u043E',
   '\u0436',
   '\u0430',
   '\u043B',
   '\u043E',
   '\u0432',
   '\u0430',
   '\u0442',
   '\u044A',
   '\u0020',
   '\u0432',   
   '\u0020'
   },
   
   //Spanish
   new char[]
   {
   '\u0042',
   '\u0069',
   '\u0065',
   '\u006E',
   '\u0076',
   '\u0065',
   '\u006E',
   '\u0069',
   '\u0064',
   '\u006F',
   '\u0020',   
   '\u0061',
   '\u0020'      
   },

   //Simple Chinese
   new char[]
   {
   '\u6B22',
   '\u8FCE',
   '\u4F7F',
   '\u7528',
   '\u0020'
   }

  };
                  
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   for(int argc = 0; argc < argv.Length; ++argc)
   {
    System.Console.WriteLine("{0}", argv[argc]);
   }//for(int argc = 0; argc < argv.Length; ++argc)
  }//public static void Main()
  
  ///<summary>Treasure</summary>
  public static void Treasure
  (
   ref String exceptionMessage,
   ref String feedback
  )
  {
   String          treasure    = null;
   StringBuilder   sb          = null;
   HttpContext     httpContext = HttpContext.Current;
   
   if ( httpContext != null )
   {
    treasure = httpContext.Request.Params["request"];
    
    switch ( treasure )
    {
     case "Welcome":
      sb = new StringBuilder();
      foreach( char[] welcome in Welcome )
      {
       sb.Append( welcome ); 
       sb.Append( "<br />" );       
      }
      feedback = sb.ToString(); 
      break;
          	
    }//switch ( request )    	
    
   }//if ( httpContext == null )

  }//public static void TreasureRequest()

 }//public class UtilityUnicode 
}//namespace WordEngineering