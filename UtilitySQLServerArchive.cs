using System.Data;
using System.Data.Sql;

namespace WordEngineering
{
 ///<summary>UtilitySQLServer</summary>
 public class UtilitySQLServer
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   SQLServerInstance();
  }

  ///<summary>SQLServerInstance</summary>
  public static DataTable SQLServerInstance()
  {
   System.Data.Sql.SqlDataSourceEnumerator instance = 
   System.Data.Sql.SqlDataSourceEnumerator.Instance;
   System.Data.DataTable dataTable = instance.GetDataSources();

   /*
   #if (DEBUG)
    foreach( DataRow dataRow in dataTable.Rows )
    {
     foreach (System.Data.DataColumn dataColumn in dataTable.Columns)
     {
      System.Console.WriteLine("{0} = {1}", dataColumn.ColumnName, dataRow[dataColumn]);
     }
    }
   #endif
   */

   return( dataTable );
  }
 }
}