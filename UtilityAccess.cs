using System;
using System.Data;
using System.Data.OleDb;

namespace WordEngineering
{
 ///UtilityAccess
 public class UtilityAccess
 {
  ///Access Database FileName
  public const string AccessDatabaseFileName = @"c:\windows\system32\inetsrv\SanFranciscoGeneralHospitalSFGHSurgery.mdb";
  
  ///Table CoreData
  public const string TableCoreData = "CREATE TABLE CoreData ( MedicalRecordPatientNo NVARCHAR(30) NOT NULL, AccountNo NVARCHAR(30) NULL, AltMRN NVARCHAR(30) NULL, NameLast NVARCHAR(30) NULL, NameFirst NVARCHAR(30) NULL )";  

  ///Primary Key CoreData
  public const string PrimaryKeyCoreData = "ALTER TABLE CoreData ADD CONSTRAINT PrimaryKeyCoreData PRIMARY KEY ( MedicalRecordPatientNo, AccountNo )";  

  ///Main
  public static void Main
  (
   string[] argv
  )
  {
   CreateAccessDatabase
   (
    AccessDatabaseFileName
   );
  }
  
  ///CreateAccessDatabase
  public static void CreateAccessDatabase
  (
   string accessDatabaseFileName
  )
  {

   Type             objectClassType          =  null;
   object           objectClassTypeInstance  =  null;  

   OleDbCommand     oleDbCommand             =  null;
   OleDbConnection  oleDbConnection          =  null;


   try
   {
	objectClassType = Type.GetTypeFromProgID("ADOX.Catalog");
 
	if ( objectClassType == null )
	{
     UtilityDebug.Write(string.Format("Exception: {0}", "Type.GetTypeFromProgID(ADOX.Catalog"));
     return;
    }//if ( objClassType == null ) 		
	
	// Delete the mdb file if it already exists
	if (System.IO.File.Exists( accessDatabaseFileName ) )
	{
	 System.IO.File.Delete( accessDatabaseFileName );
	}//if (System.IO.File.Exists( accessDatabaseFileName ) )

    objectClassTypeInstance = Activator.CreateInstance( objectClassType );

	objectClassTypeInstance.GetType().InvokeMember
	(
	 "Create",
     System.Reflection.BindingFlags.InvokeMethod, 
     null, 
     objectClassTypeInstance, 
     new object[]
     {
      "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + accessDatabaseFileName + ";" 
     }//new object[]
	);//objectClassTypeInstance.GetType().InvokeMember

    if (System.IO.File.Exists( accessDatabaseFileName ) == false )
	{
	 return;
	}//if (System.IO.File.Exists( accessDatabaseFileName ) == false )
	
	oleDbConnection  =  new OleDbConnection
	(
	 "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
	 accessDatabaseFileName +
	 ";Persist Security Info=False"
	);
	
	oleDbConnection.Open();
	
    oleDbCommand = new OleDbCommand( TableCoreData, oleDbConnection);
    oleDbCommand.ExecuteNonQuery();
    
    oleDbCommand = new OleDbCommand( PrimaryKeyCoreData, oleDbConnection);
    oleDbCommand.ExecuteNonQuery();

   }//try
   catch ( Exception exception )
   {
    UtilityDebug.Write(string.Format("Exception: {0}", exception.Message));
   }//catch 
   finally
   {
    if (oleDbConnection.State == System.Data.ConnectionState.Open)
    {
     oleDbConnection.Close();
    }//if (oleDbConnection.State == System.Data.ConnectionState.Open)
   }//finally
  }//public static void CreateAccessDatabase
 }//public class UtilityAccess 
}//WordEngineering  