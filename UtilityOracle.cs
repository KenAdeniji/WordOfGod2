using System;
using System.Data;
using System.Data.OracleClient;

namespace WordEngineering
{
 public class UtilityOracle
 {
  public static void Main(string[] argv)
  {
   OracleConnection connection;
   OracleCommand command;
   try
   {
    //connection = new OracleConnection("Data Source=Comfort; User Id=system; Password=eTHan");
    connection = new OracleConnection("Data Source=KJV; User Id=system; Password=eTHan");
    command = new OracleCommand();
    command.Connection = connection;
    command.CommandText = "SELECT COUNT(*) FROM JOB_HISTORY";
    command.CommandText = "SELECT COUNT(*) FROM USER_USERS";
    command.CommandType = CommandType.Text;
    connection.Open();
    command.ExecuteScalar();
    connection.Close();
   }
   catch (Exception exception)
   {
    System.Console.WriteLine("Exception: {0}", exception.Message);
   }
  }
 }
}