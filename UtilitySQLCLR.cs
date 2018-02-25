using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.XPath;

//using System.Data.SqlServer;

namespace WordEngineering
{
 ///<summary>UtilitySQLCLR</summary>
 ///<remarks>
 /// http://download.microsoft.com/download/3/3/f/33fb3703-6e6e-4715-9318-c52853bd55bf/SQLServer2005NewFeatures_Ch_4.pdf
 ///  Copyright © 2004 by The McGraw-Hill Companies, Inc. (Publisher). All rights reserved.
 ///  www.osborne.com
 ///  SQL Server 2005 New Features
 ///  By Michael Otey
 ///  ISBN 0-007-222776-1
 /// http://download.microsoft.com/download/f/8/5/f8520d64-f109-4111-b0b0-51f1f6d2d220/SQLServer2005_chapters1and6_SamsPublishing.pdf
 ///  Microsoft SQL Server 2005: Changing the Paradigm (SQL Server 2005 Public Beta Edition)
 ///  0-672-32778-3; Sams Publishing
 /// http://msdn2.microsoft.com/en-us/library/ms176063
 ///  CLR Stored Procedures
 /// DROP PROCEDURE CaseBasedReasoningExpenseSum;
 /// DROP PROCEDURE GetContactCount;
 /// DROP PROCEDURE GetDateAsString;
 /// DROP PROCEDURE HelloWorld;
 /// DROP PROCEDURE RSS;
 /// DROP PROCEDURE SendBibleBookResultSet;
 /// DROP PROCEDURE SendTelephoneTypeResultSet;
 /// DROP PROCEDURE SendTransientResultSet;
 /// DROP PROCEDURE SendContactResultSet
 /// DROP ASSEMBLY UtilitySQLCLR;
 /// GO
 /// CREATE ASSEMBLY UtilitySQLCLR 
 /// FROM 'D:\WordOfGod\bin\UtilitySQLCLR.dll'
 /// WITH PERMISSION_SET = SAFE; --EXTERNAL_ACCESS, SAFE, UNSAFE
 /// GO
 ///</remarks>
 public class UtilitySQLCLR
 {
  /*
  ///<summary>SqlDefinitionBibleBook</summary>
  public static readonly SqlDefinition SqlDefinitionBibleBook = new SqlDefinition("SELECT bookId, bookTitle, chapters, verseIdSequenceFirst, verseIdSequenceLast FROM BibleBook ORDER BY bookId", null, null);
  */
 	
  ///<summary>Main</summary>
  public static void Main( string[] argv )
  {
   XPathDocument doc = new XPathDocument("http://msdn.microsoft.com/rss.xml");
  }
    
  ///<summary>CaseBasedReasoningExpenseSum</summary>
  ///<remarks>
  /// SQL SELECT supports the sum clause.
  /// CREATE PROCEDURE CaseBasedReasoningExpenseSum
  /// (	
  ///  @expenseSum Float OUTPUT
  /// )
  /// AS EXTERNAL NAME UtilitySQLCLR.UtilitySQLCLR.CaseBasedReasoningExpenseSum;
  /// GO
  /// DECLARE @expenseSum Float
  /// EXECUTE CaseBasedReasoningExpenseSum @expenseSum OUT
  /// PRINT @expenseSum
  /// GO
  /// </remarks>
  public static void CaseBasedReasoningExpenseSum( out SqlDouble expenseSum )
  {
   using(SqlConnection sqlConnection = new SqlConnection("context connection=true"))
   {
    expenseSum = 0;
    sqlConnection.Open();
    SqlCommand sqlCommand = new SqlCommand("SELECT ISNULL(expense,0) FROM WordEngineering..CaseBasedReasoning", sqlConnection);
    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
    using ( sqlDataReader )
    {
     while( sqlDataReader.Read() )
     {
      expenseSum += sqlDataReader.GetSqlDouble(0);
     }
    }         
   }
  }
 	
  ///<summary>GetDateAsString</summary>
  public static string GetDateAsString()
  {
   return DateTime.Now.ToString();
  }

  ///<summary>GetContactCount</summary>
  public static System.Int32 GetContactCount()
  {
   int rowCount = -1;
   SqlCommand  sqlCommand;
   SqlConnection  sqlConnection = new SqlConnection("context connection=true");
   sqlConnection.Open();
   sqlCommand = new SqlCommand( "SELECT Count(*) FROM WordEngineering..Contact", sqlConnection );
   rowCount = (int) sqlCommand.ExecuteScalar();
   return rowCount;
  }

  ///<summary>HelloWorld</summary>
  public static void HelloWorld()
  {
   SqlContext.Pipe.Send("Hello world!\n");
  }

  ///<summary>RSS</summary>
  public static void RSS()
  {
   using (SqlConnection conn = new SqlConnection("context connection = true"))
   {
    // Retrieve the RSS feed
    //XPathDocument doc = new PathDocument("http://msdn.microsoft.com/sql/rss.xml");
    //XPathDocument doc = new XPathDocument("http://msdn.microsoft.com/sql/rss.xml");
    XPathDocument doc = new XPathDocument("http://msdn.microsoft.com/rss.xml");
    XPathNavigator nav = doc.CreateNavigator();
    XPathNodeIterator i = nav.Select("//item");

    // create metadata for four columns
    // three of them are string types and one of them is a datetime
    SqlMetaData[] rss_results = new SqlMetaData[4];
    rss_results[0] = new SqlMetaData("Title", SqlDbType.NVarChar, 250);
    rss_results[1] = new SqlMetaData("Publication Date", 
        SqlDbType.DateTime);
    rss_results[2] = new SqlMetaData("Description", 
        SqlDbType.NVarChar, 2000);
    rss_results[3] = new SqlMetaData("Link", SqlDbType.NVarChar, 1000);

    // construct the record which holds metadata and data buffers
    SqlDataRecord record = new SqlDataRecord(rss_results);

    // cache a SqlPipe instance to avoid repeated calls to  
    // SqlContext.GetPipe()
    SqlPipe sqlpipe = SqlContext.Pipe;
 
    // send the metadata, do not send the values in the data record
    sqlpipe.SendResultsStart(record);

    // for each xml node returned, extract four pieces 
    // of information and send back each item as a row
    while (i.MoveNext())
    {

        record.SetString(0, (string)
                            
        i.Current.Evaluate("string(title[1]/text())"));
            record.SetDateTime(1, DateTime.Parse((string)
                                             
        i.Current.Evaluate("string(pubDate[1]/text())")));
            record.SetString(2, (string)
                                    
        i.Current.Evaluate("string(description[1]/text())"));
            record.SetString(3, (string)
                                     
        i.Current.Evaluate("string(link[1]/text())"));
        sqlpipe.SendResultsRow(record);
    }
    // signal end of results
    sqlpipe.SendResultsEnd();
   }
  }

  /*
  ///<summary>SendBibleBookResultSet</summary>
  public static void SendBibleBookResultSet()
  {
   SqlExecutionContext sqlExecutionContext = SqlContext.GetConnection().CreateExecutionContext(SqlDefinitionBibleBook);
   SqlContext.GetPipe().Execute(sqlExecutionContext);
  }
  */
  
  ///<summary>SendTelephoneTypeResultSet</summary>
  public static void SendTelephoneTypeResultSet()
  {
   //SqlContext.Pipe.Send("Hello world! It's now " + System.DateTime.Now.ToString()+"\n");
   using(SqlConnection sqlConnection = new SqlConnection("context connection=true")) 
   {
    sqlConnection.Open();
    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM WordEngineering..TelephoneType", sqlConnection);
    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
    SqlContext.Pipe.Send(sqlDataReader);
   }
  }
  
  ///<summary>SendTransientResultSet</summary>
  public static void SendTransientResultSet()
  {
   // Create a record object that represents an individual row, including it's metadata.
   SqlDataRecord sqlDataRecord = new SqlDataRecord(new SqlMetaData("Greeting", SqlDbType.VarChar, 128));
   // Populate the record.
   sqlDataRecord.SetSqlString(0, "Welcome");
   // Send the record to the client.
   SqlContext.Pipe.Send(sqlDataRecord);
  }

  ///<summary>SendContactResultSet</summary>
  public static void SendContactResultSet()
  {
   using(SqlConnection sqlConnection = new SqlConnection("context connection=true")) 
   {
    sqlConnection.Open();
    SqlCommand sqlCommand = new SqlCommand("Select * FROM WordEngineering..ViewContactSet", sqlConnection);
    SqlContext.Pipe.ExecuteAndSend(sqlCommand);
   }
  }
 }
}