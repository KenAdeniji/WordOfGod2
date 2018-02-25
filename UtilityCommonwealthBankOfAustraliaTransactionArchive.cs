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
 /// <summary>UtilityCommonwealthBankOfAustraliaTransactionArgument</summary>
 public class UtilityCommonwealthBankOfAustraliaTransactionArgument
 {
  ///<summary>firstRow</summary>
  public int     firstRow       = UtilityCommonwealthBankOfAustraliaTransaction.FirstRow;

  ///<summary>filenameSource</summary>
  public String[] filenameSource = null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilityCommonwealthBankOfAustraliaTransactionArgument():this
  (
   UtilityCommonwealthBankOfAustraliaTransaction.FirstRow,
   null //filenameSource
  )
  {
  }//public UtilityCommonwealthBankOfAustraliaTransactionArgument()
  
  /// <summary>Constructor.</summary>
  public UtilityCommonwealthBankOfAustraliaTransactionArgument
  (
   int     firstRow,
   string[] filenameSource
  )
  {
   this.filenameSource = filenameSource;
   this.firstRow       = firstRow;
  }//public UtilityCommonwealthBankOfAustraliaTransactionArgument()

 }//public class UtilityCommonwealthBankOfAustraliaTransactionArgument
	
 ///<summary>UtilityCommonwealthBankOfAustraliaTransaction</summary>
 public class UtilityCommonwealthBankOfAustraliaTransaction
 {
  ///<summary>DateFormat</summary>
  public const string DateFormat = "SET DATEFORMAT dmy";
  
  ///<summary>FirstRow</summary>
  public const int    FirstRow = 1;

  ///<summary>DatabaseConnectionString</summary>
  public const string DatabaseConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=WordEngineering;Integrated Security=SSPI;";

  ///<summary>SQLInsert</summary>
  public const string SQLInsert = "IF NOT EXISTS ( SELECT 1 FROM CommonwealthBankOfAustraliaTransaction WHERE Dated = '{0}' AND DebitCredit = {1} AND TransactionDescription = '{2}' AND Balance = {3} ) INSERT CommonwealthBankOfAustraliaTransaction( Dated,  DebitCredit, TransactionDescription, Balance ) " +
                                  "VALUES ( '{0}', {1}, '{2}', {3} )";
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   string[] argv
  )
  {
   Boolean          booleanParseCommandLineArguments  =  false;
   String           exceptionMessage                  =  null;
   UtilityCommonwealthBankOfAustraliaTransactionArgument  UtilityCommonwealthBankOfAustraliaTransactionArgument                   =  null;

   UtilityCommonwealthBankOfAustraliaTransactionArgument = new UtilityCommonwealthBankOfAustraliaTransactionArgument();
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    UtilityCommonwealthBankOfAustraliaTransactionArgument
   );
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityCommonwealthBankOfAustraliaTransactionArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   Import( ref UtilityCommonwealthBankOfAustraliaTransactionArgument, ref exceptionMessage );
  }

  ///<summary>Import</summary>
  public static void Import
  (
   ref UtilityCommonwealthBankOfAustraliaTransactionArgument  UtilityCommonwealthBankOfAustraliaTransactionArgument,
   ref string                                                 exceptionMessage
  )
  {
   double			     debitCredit	       =  0.0;	
   int                               rowCount                  =  -1;
   int                               rowAffect                 =  -1;
   string[]                          column                    =  null;
   string                            commandText               =  null;
   string                            line                      =  null;
   OleDbCommand                      oleDbCommand              =  null;
   OleDbConnection                   oleDbConnection           =  null;
   StreamReader                      streamReader              =  null;
   try
   {
    oleDbConnection  =  new OleDbConnection( DatabaseConnectionString );
    oleDbConnection.Open();
    oleDbCommand = new OleDbCommand( DateFormat, oleDbConnection );
    oleDbCommand.ExecuteNonQuery();
    foreach( string filenameSource in UtilityCommonwealthBankOfAustraliaTransactionArgument.filenameSource )
    {
     streamReader     =  new StreamReader( filenameSource );
     rowCount = 0;
     while ( streamReader != StreamReader.Null )
     {
      line = streamReader.ReadLine();
      if ( line == null ) { break; }
      ++rowCount;
      if ( rowCount < UtilityCommonwealthBankOfAustraliaTransactionArgument.firstRow )
      {
       continue;
      }
      column = line.Split(',');
      column[1] = column[1].Replace("\"", "");
      Double.TryParse(column[1], out debitCredit);	
      column[2] = column[2].Replace("\"", "");
      column[2] = column[2].Replace("'", "''");	
      commandText  = string.Format
      ( 
       SQLInsert, 
       column[0], 
       debitCredit,
       column[2],
       column[3] 
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