using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;


namespace WordEngineering
{
 /// <summary>UtilityLanguage.</summary>
 /// <remarks>http://www.funducode.com/csharp/assemblies/assembly3/assembly3.htm To install support for Arabic or any other Script. Open Control Panel --> Regional Options --> Language Setting for the System and check your desired Script(Arabic in our case). After checking Arabic click on OK and Arabic Script support is installed on our computer and you can have input locale in Arabic, Farsi and Urdu.</remarks>
 public class UtilityLanguage : System.Windows.Forms.Form
 {

  /// <summary>Aleph</summary>
  public static char    Aleph  =  '\u0627';
  
  /// <summary>Ra</summary>
  public static char    Ra     =  '\u0631';

  /// <summary>Dal</summary>
  public static char    Dal    =  '\u062F';
  
  /// <summary>Wao</summary>  
  public static char    Wao    =  '\u0648';
  
  /// <summary>Urdu</summary>  
  public static String  Urdu   =  Aleph.ToString() + Ra.ToString() + Dal.ToString() + Wao.ToString();

  /// <summary>components</summary>
  private System.ComponentModel.Container components = null;

  /// <summary>UtilityLanguage</summary>
  public UtilityLanguage( )
  {
   InitializeComponent ( ) ;
  }

  /// <summary>Dispose</summary>
  protected override void Dispose( bool disposing )
  {
   if ( disposing )
   {
    if ( components != null ) 
    {
     components.Dispose ( ) ;
    }
   }
   base.Dispose( disposing ) ;
  }

  #region Windows Form Designer generated code
   /// <summary>InitializeComponent</summary>
   private void InitializeComponent ( )
   {
    this.AutoScaleBaseSize = new System.Drawing.Size ( 5, 13 ) ;
    this.ClientSize = new System.Drawing.Size ( 160, 85 ) ;
    this.Name = "myform" ;
    this.Text = "Hello World" ;//change the form title
    this.Text = Urdu;
   }
  #endregion
 
  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  [STAThread]
  public static void Main
  (
    String[] argv
  )
  {
   Application.Run ( new UtilityLanguage ( ) ) ;
  }//public static void Main()

 }//public class UtilityLanguage
}//namespace WordEngineering