using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace WordEngineering
{

 /// <summary>UtilityThoughtWorksSalesTaxArgument</summary>
 public class UtilityThoughtWorksSalesTaxArgument
 {
  ///<summary>shoppingBasket</summary>  
  public string[] shoppingBasket        =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityThoughtWorksSalesTaxArgument():this
  (
   null
  ) 
  {
  }//public UtilityThoughtWorksSalesTaxArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityThoughtWorksSalesTaxArgument
  (
   string[]  shoppingBasket
  )
  {
   this.shoppingBasket  =  shoppingBasket;
  }//public UtilityThoughtWorksSalesTaxArgument()

 }//public class UtilityThoughtWorksSalesTaxArgument
 
 /// <summary>UtilityThoughtWorksSalesTax</summary>
 /// <remarks>
 /// Basic sales tax is applicable at a rate of 10% on all goods, except books,
 /// food, and medical products that are exempt. Import duty is an additional
 /// sales tax applicable on all imported goods at a rate of 5%, with no
 /// exemptions.
 ///
 /// When I purchase items I receive a receipt which lists the name of all the
 /// items and their price (including tax), finishing with the total cost of the
 /// items, and the total amounts of sales taxes paid.  The rounding rules for
 /// sales tax are that for a tax rate of n%, a shelf price of p contains
 /// (np/100 rounded up to the nearest 0.05) amount of sales tax.
 /// Write an application that prints out the receipt details for these shopping
 /// baskets...
 /// INPUT:
 /// Input 1:
 /// 1 book at 12.49
 /// 1 music CD at 14.99
 /// 1 chocolate bar at 0.85
 /// Input 2:
 /// 1 imported box of chocolates at 10.00
 /// 1 imported bottle of perfume at 47.50
 /// Input 3:
 /// 1 imported bottle of perfume at 27.99
 /// 1 bottle of perfume at 18.99
 /// 1 packet of headache pills at 9.75
 /// 1 box of imported chocolates at 11.25
 /// OUTPUT
 /// Output 1:
 /// 1 book : 12.49
 /// 1 music CD: 16.49
 /// 1 chocolate bar: 0.85
 /// Sales Taxes: 1.50
 /// Total: 29.83
 /// Output 2:
 /// 1 imported box of chocolates: 10.50
 /// 1 imported bottle of perfume: 54.65
 /// Sales Taxes: 7.65
 /// Total: 65.15
 /// Output 3:
 /// 1 imported bottle of perfume: 32.19
 /// 1 bottle of perfume: 20.89
 /// 1 packet of headache pills: 9.75
 /// 1 imported box of chocolates: 11.85
 /// Sales Taxes: 6.70
 /// Total: 74.68
 /// </remarks>
 public class UtilityThoughtWorksSalesTax
 {  

  /// <summary>ImportDutyRate</summary>
  public static  double     ImportDutyRate                     = 0.05;

  /// <summary>ItemSalesTaxRate</summary>
  public static  double     ItemSalesTaxRate                   = 0.10;

  /// <summary>Round5Cent</summary>
  public static  double     Round5Cent                         = 0.05;
  
  /// <summary>The database connection string.</summary>
  public static  String     DatabaseConnectionString           = "Provider=SQLOLEDB;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=WordEngineering;";

  /// <summary>Imported</summary>
  public static  string     Imported                           = @" imported ";

  /// <summary>The configuration XML filename.</summary>
  public static  String     FilenameConfigurationXml           = @"WordEngineering.config";
  
  /// <summary>SeparatorItemPriceAt</summary>
  public static  string     SeparatorItemPriceAt               = @" at ";

  /// <summary>ShoppingBasket</summary>
  public static  string[][] ShoppingBasket                     = new string[][]
                                                                 {
                                                                  new String[]
                                                                  {
                                                                   "1 book at 12.49",
                                                                   "1 music CD at 14.99",
                                                                   "1 chocolate bar at 0.85"
	                                                              },
                                                                  new String[]
                                                                  {
                                                                   "1 imported box of chocolates at 10.00",
                                                                   "1 imported bottle of perfume at 47.50"
	                                                              },
                                                                  new String[]
                                                                  {
                                                                   "1 imported bottle of perfume at 27.99",
                                                                   "1 bottle of perfume at 18.99",
                                                                   "1 packet of headache pills at 9.75",
                                                                   "1 box of imported chocolates at 11.25"                                                                       
	                                                              }
                                                                 };	

  /// <summary>The XPath database connection String.</summary>
  public static  String     XPathDatabaseConnectionString      = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>Main</summary>
  public static void Main
  ( 
   string[] argv
  )
  {
   Boolean                                 booleanParseCommandLineArguments  =  false;
   string                                  exceptionMessage                  =  null;     
   string                                  filenameApplication               =  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
   UtilityThoughtWorksSalesTaxArgument     utilityThoughtWorksSalesTaxArgument         =  null;
   
   utilityThoughtWorksSalesTaxArgument = new UtilityThoughtWorksSalesTaxArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityThoughtWorksSalesTaxArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityThoughtWorksSalesTaxArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   /*
   #if (DEBUG)
    System.Console.WriteLine
    (
     "Filename Application: {0}",
     filenameApplication
    );
   #endif

   #if (DEBUG)
    foreach( string  shoppingBasketCurrent in  utilityThoughtWorksSalesTaxArgument.shoppingBasket )
    {
     System.Console.WriteLine
     (
      "Argument ShoppingBasket: {0}",
      shoppingBasketCurrent
     );
    }//foreach( string  shoppingBasketCurrent in  utilityThoughtWorksSalesTaxArgument.shoppingBasket ) 
   #endif
   */
   
   ReceiptDetail
   (
    ref  utilityThoughtWorksSalesTaxArgument,
    ref  exceptionMessage
   );

  }//Main  

  /// <summary>ReceiptDetail</summary>
  public static void ReceiptDetail
  (
   ref UtilityThoughtWorksSalesTaxArgument  utilityThoughtWorksSalesTaxArgument,
   ref string                               exceptionMessage
  )
  {
   HttpContext       httpContext  =  HttpContext.Current;
   
   double            itemImportDuty                                    =  0.0;
   double            itemImportDutyRound5Cents                         =  0.0;
   double            itemMarkPrice                                     =  0.0;
   double            itemShelfPrice                                    =  0.0;
   double            itemSalesTax                                      =  0.0;
   double            itemSalesTaxRound5Cents                           =  0.0;
   double            itemSalesTaxRate                                  =  0.0;
   
   double            totalCost                                         =  0.0;
   double            totalSalesTax                                     =  0.0;   

   int               indexOfItemImport                                 =  0;
   int               indexOfItemTitle                                  =  0;
   int               indexOfSeparatorItemPriceAt                       =  0;
   
   string            itemCategory                                      =  null;
   string            itemDescription                                   =  null;
   string            itemPrice                                         =  null;

   // Creates CompareInfo for the InvariantCulture.
   CompareInfo       compareInfo                                       = CultureInfo.InvariantCulture.CompareInfo;

   UtilityThoughtWorksItem[]          utilityThoughtWorksItem          =  null;
   UtilityThoughtWorksItemCategory[]  utilityThoughtWorksItemCategory  =  null;   
   
   try
   {

    utilityThoughtWorksItem = ( UtilityThoughtWorksItem[] ) UtilityThoughtWorksItem.arrayListUtilityThoughtWorksItem.ToArray( typeof( UtilityThoughtWorksItem ) );

    utilityThoughtWorksItemCategory = ( UtilityThoughtWorksItemCategory[] ) UtilityThoughtWorksItemCategory.arrayListUtilityThoughtWorksItemCategory.ToArray( typeof( UtilityThoughtWorksItemCategory ) );

    foreach( string  shoppingBasketCurrent in  utilityThoughtWorksSalesTaxArgument.shoppingBasket )
    {
     itemImportDuty               =  0;
     itemImportDutyRound5Cents    =  0;
     itemSalesTax                 =  0;
     itemSalesTaxRate             =  ItemSalesTaxRate;
     itemSalesTaxRound5Cents      =  0;
     
     indexOfSeparatorItemPriceAt  =  compareInfo.IndexOf
                                     ( 
                                      shoppingBasketCurrent,
                                      SeparatorItemPriceAt,
                                      CompareOptions.IgnoreCase
                                     );

     itemDescription              =  shoppingBasketCurrent.Substring( 0, indexOfSeparatorItemPriceAt ).Trim();
     itemPrice                    =  shoppingBasketCurrent.Substring( indexOfSeparatorItemPriceAt + 4  ).Trim();
     itemMarkPrice                =  double.Parse( itemPrice );
     itemMarkPrice                =  Convert.ToDouble( itemPrice );

     foreach( UtilityThoughtWorksItem utilityThoughtWorksItemCurrent in utilityThoughtWorksItem )
     {
      indexOfItemTitle =  compareInfo.IndexOf
                          ( 
                           itemDescription,
                           utilityThoughtWorksItemCurrent.itemTitle,
                           CompareOptions.IgnoreCase
                          );

      if ( indexOfItemTitle >= 0 )
      {
       itemCategory = utilityThoughtWorksItemCurrent.itemCategory;

       if ( utilityThoughtWorksItemCurrent.itemCategory != null )
       {
        foreach( UtilityThoughtWorksItemCategory utilityThoughtWorksItemCategoryCurrent in utilityThoughtWorksItemCategory )
        {
         if ( string.Compare( utilityThoughtWorksItemCategoryCurrent.itemCategory, itemCategory, true ) == 0 )
         {
          itemSalesTaxRate = utilityThoughtWorksItemCategoryCurrent.itemSalesTaxRate;
          break;
         }//if ( string.Compare( utilityThoughtWorksItemCategoryCurrent.itemCategory, itemCategory ) == 0 )
        }//foreach( UtilityThoughtWorksItemCategory utilityThoughtWorksItemCategoryCurrent in utilityThoughtWorksItemCategory )
       }//if ( utilityThoughtWorksItemCurrent.itemCategory != null ) 
       
       break;
       
      }//if ( indexOfItemTitle >= 0 )

     }//foreach( UtilityThoughtWorksItem utilityThoughtWorksItemCurrent in utilityThoughtWorksItem )
     
     if ( itemSalesTaxRate != 0 )
     {
      itemSalesTax = itemMarkPrice * itemSalesTaxRate;
      itemSalesTaxRound5Cents = MonetaryRound( itemSalesTax );
     }//if ( itemSalesTaxRate != 0 ) 

     indexOfItemImport = shoppingBasketCurrent.IndexOf( Imported );
     if ( indexOfItemImport >= 0 )
     {
      itemImportDuty = itemMarkPrice * ImportDutyRate;
      itemImportDutyRound5Cents = MonetaryRound( itemImportDuty );
     }//if ( indexOfItemImport >= 0 )
      
     itemShelfPrice = itemMarkPrice + itemSalesTaxRound5Cents + itemImportDutyRound5Cents;
     	
     totalSalesTax  += itemSalesTax + itemImportDutyRound5Cents;
     totalCost      += itemShelfPrice;
     
     System.Console.WriteLine
     (
      "{0}: {1}",
      itemDescription,
      itemShelfPrice.ToString("#,##0.00;(#,##0.00);Zero")
     );
     
     /*
     #if (DEBUG)
      System.Console.WriteLine
      (
       "itemMarkPrice: {0} | itemSalesTax: {1} | itemSalesTaxRound5Cents: {2} | itemImportDuty: {3}",
       itemMarkPrice,
       itemSalesTax,
       itemSalesTaxRound5Cents,
       itemImportDuty
      );
     #endif
     */

    }//foreach( string  shoppingBasketCurrent in  utilityThoughtWorksSalesTaxArgument.shoppingBasket ) 

    System.Console.WriteLine("Sales Taxes: {0}", totalSalesTax.ToString("#,##0.00;(#,##0.00);Zero"));
    System.Console.WriteLine("Total: {0}", totalCost.ToString("#,##0.00;(#,##0.00);Zero"));

   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )
     
  }//public static void ReceiptDetail()

  /// <summary>MonetaryRound</summary>
  public static double MonetaryRound
  (
   double  valueMonetary
  )
  {
   return 
   (
   	MonetaryRound
   	(
   	 valueMonetary,
   	 Round5Cent
   	) 
   );	
  }//public static double MonetaryRound

  /// <summary>MonetaryRound</summary>
  public static double MonetaryRound
  (
   double  valueMonetary,
   double  valueRound
  )
  {
   HttpContext       httpContext         =  HttpContext.Current;
   double            valueMonetaryRound  =  0.0;
   string            exceptionMessage    =  null;

   try
   {
    //No division by zero
    if ( valueRound == 0 ) 
    { 
     return 0; 
    }

    valueMonetaryRound = Math.Round( valueMonetary / valueRound ) * valueRound;

   }//try
   catch ( Exception exception )
   {
    exceptionMessage = "Exception: " + exception.Message;
   }//catch ( Exception exception )

   if ( exceptionMessage != null )
   {
    if ( httpContext == null )
    {
     System.Console.WriteLine( exceptionMessage );
    }//if ( httpContext == null )
    else
    {
     //httpContext.Response.Write( exceptionMessage );
    }//else 
   }//if ( exceptionMessage != null )

   return( valueMonetaryRound );

  }//public static double MonetaryRound
      
 }//public class UtilityThoughtWorksSalesTax

 /// <summary>UtilityThoughtWorksItem</summary>
 public class UtilityThoughtWorksItem
 {
  /// <summary>itemTitle</summary>
  public  string  itemTitle         =  null;

  /// <summary>itemCategory</summary>
  public  string  itemCategory      =  null;

  /// <summary>Constructor.</summary>
  public UtilityThoughtWorksItem
  (
   string  itemTitle
  )
  :this
  (
   itemTitle,
   null
  )
  {
  }//public UtilityThoughtWorksItem()
  
  /// <summary>Constructor.</summary>
  public UtilityThoughtWorksItem
  (
   string  itemTitle,
   string  itemCategory
  )
  {
   
   this.itemTitle     =  itemTitle;
   this.itemCategory  =  itemCategory;
   
   arrayListUtilityThoughtWorksItem.Add( this );
   
  }//public UtilityThoughtWorksItem()

  /// <summary>The static method.</summary>
  static UtilityThoughtWorksItem()
  {
   Book = new UtilityThoughtWorksItem( "Book", "Book" );
   MusicCD = new UtilityThoughtWorksItem( "Music CD" );
   Chocolate = new UtilityThoughtWorksItem( "Chocolate", "Food" );
   Perfume = new UtilityThoughtWorksItem( "Perfume" );
   HeadachePills = new UtilityThoughtWorksItem( "Headache Pills", "Medicine" );
  }//static UtilityThoughtWorksItemCategory()
  
  /// <summary>UtilityThoughtWorksItem</summary>      
  public static readonly UtilityThoughtWorksItem
   Book,
   MusicCD,
   Chocolate,
   Perfume,
   HeadachePills;

  /// <summary>arrayListUtilityThoughtWorksItem</summary>
  public static ArrayList arrayListUtilityThoughtWorksItem = new ArrayList();

 }//UtilityThoughtWorksItem

 /// <summary>UtilityThoughtWorksItemCategory</summary>
 public class UtilityThoughtWorksItemCategory
 {
  /// <summary>itemCategory</summary>
  public  string  itemCategory      =  null;

  /// <summary>itemSalesTaxRate</summary>
  public  double  itemSalesTaxRate  =  0.0;

  /// <summary>Constructor.</summary>
  public UtilityThoughtWorksItemCategory
  (
   string  itemCategory
  )
  :this
  (
   itemCategory,
   0.0
  )
  {
  }//public UtilityThoughtWorksItemCategory()

  /// <summary>Constructor.</summary>
  public UtilityThoughtWorksItemCategory
  (
   string  itemCategory,
   double  itemSalesTaxRate
  )
  {
   
   this.itemCategory      =  itemCategory;
   this.itemSalesTaxRate  =  itemSalesTaxRate;   

   arrayListUtilityThoughtWorksItemCategory.Add( this );
   
  }//public UtilityThoughtWorksItemCategory()

  /// <summary>The static method.</summary>
  static UtilityThoughtWorksItemCategory()
  {
   Book     =  new UtilityThoughtWorksItemCategory( "Book" );
   Food     =  new UtilityThoughtWorksItemCategory( "Food" );
   Medicine  =  new UtilityThoughtWorksItemCategory( "Medicine" );
  }//static UtilityThoughtWorksItemCategory()
  
  /// <summary>UtilityThoughtWorksItemCategory</summary>
  public static readonly UtilityThoughtWorksItemCategory
   Book,
   Food,
   Medicine;
  
  /// <summary>arrayListUtilityThoughtWorksItemCategory</summary>
  public static ArrayList arrayListUtilityThoughtWorksItemCategory = new ArrayList();
  
 }//UtilityThoughtWorksItemCategory

}//namespace WordEngineering