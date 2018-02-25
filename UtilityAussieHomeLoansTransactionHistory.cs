using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading;

namespace WordEngineering
{
 /// <summary>UtilityAussieHomeLoansTransactionHistoryArgument</summary>
 public class UtilityAussieHomeLoansTransactionHistoryArgument
 {
  ///<summary>firstRow</summary>
  public int     firstRow       = UtilityAussieHomeLoansTransactionHistory.FirstRow;

  ///<summary>filenameSource</summary>
  public String[] filenameSource = null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityAussieHomeLoansTransactionHistoryArgument():this
  (
   UtilityAussieHomeLoansTransactionHistory.FirstRow,
   null //filenameSource
  )
  {
  }//public UtilityAussieHomeLoansTransactionHistoryArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityAussieHomeLoansTransactionHistoryArgument
  (
   int     firstRow,
   string[] filenameSource
  )
  {
   this.filenameSource = filenameSource;
   this.firstRow       = firstRow;
  }//public UtilityAussieHomeLoansTransactionHistoryArgument()

 }//public class UtilityAussieHomeLoansTransactionHistoryArgument
	
 ///<summary>UtilityAussieHomeLoansTransactionHistory</summary>
 public class UtilityAussieHomeLoansTransactionHistory
 {
  ///<summary>DateFormat</summary>
  public const string DateFormat = "SET DATEFORMAT dmy";
  
  ///<summary>FirstRow</summary>
  public const int    FirstRow = 2;

  ///<summary>DatabaseConnectionString</summary>
  public const string DatabaseConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;Integrated Security=SSPI;";

  ///<summary>SQLInsert</summary>
  public const string SQLInsert = "IF NOT EXISTS ( SELECT 1 FROM AussieHomeLoansTransactionHistory WHERE LoanNumber = {0} AND TransactionDate = '{1}' AND TransactionEffectiveDate = '{2}' AND TransactionAmount = {3} AND TransactionDRCR = '{4}' AND Description = '{5}' ) INSERT AussieHomeLoansTransactionHistory( LoanNumber, TransactionDate, TransactionEffectiveDate, TransactionAmount, TransactionDRCR, Description ) " +
                                  "VALUES ( {0}, '{1}', '{2}', {3}, '{4}', '{5}' )";

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
   Boolean          booleanParseCommandLineArguments  =  false;
   String           exceptionMessage                  =  null;
   UtilityAussieHomeLoansTransactionHistoryArgument  utilityAussieHomeLoansTransactionHistoryArgument                   =  null;

   utilityAussieHomeLoansTransactionHistoryArgument = new UtilityAussieHomeLoansTransactionHistoryArgument();
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityAussieHomeLoansTransactionHistoryArgument
   );
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityAussieHomeLoansTransactionHistoryArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   Import( ref utilityAussieHomeLoansTransactionHistoryArgument, ref exceptionMessage );
  }

  ///<summary>Import</summary>
  public static void Import
  (
   ref UtilityAussieHomeLoansTransactionHistoryArgument  utilityAussieHomeLoansTransactionHistoryArgument,
   ref string                                            exceptionMessage
  )
  {
   int                               rowCount                  =  -1;
   int                               rowAffect                 =  -1;
   string[]                          column                    =  null;
   string                            commandText               =  null;
   string                            line                      =  null;
   DateTime                          transactionDate;
   DateTime                          transactionEffectiveDate;
   OleDbCommand                      oleDbCommand              =  null;
   OleDbConnection                   oleDbConnection           =  null;
   StreamReader                      streamReader              =  null;
   System.Globalization.CultureInfo  cultureInfoAU;
   System.Globalization.CultureInfo  cultureInfoUS;
   try
   {
    cultureInfoAU = new System.Globalization.CultureInfo("en-AU");
    cultureInfoUS = new System.Globalization.CultureInfo("en-US");
    System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfoAU;
    System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfoAU;
    System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
    oleDbConnection  =  new OleDbConnection( DatabaseConnectionString );
    oleDbConnection.Open();
    oleDbCommand = new OleDbCommand( DateFormat, oleDbConnection );
    oleDbCommand.ExecuteNonQuery();
    foreach( string filenameSource in utilityAussieHomeLoansTransactionHistoryArgument.filenameSource )
    {
     streamReader     =  new StreamReader( filenameSource );
     rowCount = 0;
     while ( streamReader != StreamReader.Null )
     {
      line = streamReader.ReadLine();
      if ( line == null ) { break; }
      ++rowCount;
      if ( rowCount < utilityAussieHomeLoansTransactionHistoryArgument.firstRow )
      {
       continue;
      }
      column = line.Split(',');
      column[0] = column[0].Trim();
      DateTime.TryParse( column[1], out transactionDate );
      DateTime.TryParse( column[2], out transactionEffectiveDate );
      /*
      commandText  = string.Format
      ( 
       SQLInsert, 
       column[0], 
       transactionDate.ToString("d", cultureInfoUS),
       transactionEffectiveDate.ToString("d", cultureInfoUS),
       column[3], 
       column[4], 
       column[5], 
       column[6] 
      );
      */      
      commandText  = string.Format
      ( 
       SQLInsert, 
       column[0], 
       column[1],
       column[2],
       column[3], 
       column[4], 
       column[5], 
       column[6] 
      );
      oleDbCommand = new OleDbCommand( commandText, oleDbConnection );
      rowAffect = oleDbCommand.ExecuteNonQuery();
     }
     if ( streamReader != null ) { streamReader.Close(); }
    }
   }
   catch ( SqlException exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   catch ( InvalidOperationException exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   if ( oleDbConnection != null ) { oleDbConnection.Close(); }
  }
 }
}
