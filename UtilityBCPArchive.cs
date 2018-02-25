using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Xml;

namespace WordEngineering
{

 /// <summary>UtilityBCPArgument</summary>
 public class UtilityBCPArgument
 {

  ///<summary>characterType</summary>  
  public bool     characterType           =  UtilityBCP.CharacterType;

  ///<summary>keepIdentityValue</summary>  
  public bool     keepIdentityValue       =  UtilityBCP.KeepIdentityValue;

  ///<summary>keepNullValue</summary>  
  public bool     keepNullValue           =  UtilityBCP.KeepNullValue;

  ///<summary>characterType</summary>  
  public bool     nativeType              =  UtilityBCP.NativeType;

  ///<summary>trustedConnection</summary>  
  public bool     trustedConnection       =  UtilityBCP.TrustedConnection;

  ///<summary>batchSize</summary>  
  public int      batchSize               =  UtilityBCP.BatchSize;

  ///<summary>maximumError</summary>  
  public int      maximumError            =  UtilityBCP.MaximumError;

  ///<summary>directoryDatafile</summary>
  public String   directoryDatafile       =  UtilityBCP.DirectoryDataFile;

  ///<summary>databaseName</summary>
  public String[] databaseName            =  null; //UtilityBCP.DatabaseName.Split( UtilityBCP.DelimiterCharArrayComma );

  ///<summary>password</summary>  
  public String   password                =  null;

  ///<summary>serverName</summary>  
  public String   serverName              =  null;

  ///<summary>tableName</summary>  
  public String[] tableName               =  null; //UtilityBCP.TableName.Split( UtilityBCP.DelimiterCharArrayComma );

  ///<summary>userName</summary>  
  public String   userName                =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files                   =  null;

  /// <summary>Constructor.</summary>
  public UtilityBCPArgument():this
  (
   UtilityBCP.BatchSize,
   UtilityBCP.CharacterType,      //Character type
   UtilityBCP.DatabaseName.Split( UtilityBCP.DelimiterCharArrayComma ),
   UtilityBCP.DirectoryDataFile,
   UtilityBCP.KeepIdentityValue,
   UtilityBCP.KeepNullValue,
   UtilityBCP.MaximumError,
   UtilityBCP.NativeType,         //Native type
   null,                          //Password
   null,                          //Server name
   UtilityBCP.TableName.Split( UtilityBCP.DelimiterCharArrayComma ),
   UtilityBCP.TrustedConnection,  //Trusted connection
   null                           //User name
  )
  {
  }//public UtilityBCPArgument()

  /// <summary>Constructor.</summary>
  public UtilityBCPArgument
  (
   int       batchSize,
   bool      characterType,
   String[]  databaseName,
   String    directoryDatafile,
   bool      keepIdentityValue,
   bool      keepNullValue,
   int       maximumError,
   bool      nativeType,   
   String    password,
   String    serverName,
   String[]  tableName,
   bool      trustedConnection,
   String    userName
  )
  {
   this.batchSize          =  batchSize;
   this.characterType      =  characterType;
   this.databaseName       =  databaseName;
   this.directoryDatafile  =  directoryDatafile;
   this.keepNullValue      =  keepNullValue;
   this.keepIdentityValue  =  keepIdentityValue;
   this.maximumError       =  maximumError;
   this.nativeType         =  nativeType;   
   this.password           =  password;
   this.serverName         =  serverName;
   this.tableName          =  tableName;
   this.trustedConnection  =  trustedConnection;
   this.userName           =  userName;   
  }//public UtilityBCPArgument()

  ///<summary>Property.</summary>
  ///<value>BatchSize.</value>
  public int BatchSize
  {
   get 
   {
    return ( batchSize );
   }//get
   set 
   {
    batchSize = value;
   }//set
  }//BatchSize

  ///<summary>Property.</summary>
  ///<value>CharacterType.</value>
  public bool CharacterType
  {
   get 
   {
    return ( characterType );
   }//get
   set 
   {
    characterType = value;
   }//set
  }//CharacterType

  ///<summary>Property.</summary>
  ///<value>DirectoryDataFile.</value>
  public String DirectoryDataFile
  {
   get 
   {
    return ( directoryDatafile );
   }//get
   set 
   {
    directoryDatafile = value.Trim();
    if ( directoryDatafile == null || directoryDatafile == String.Empty )
    {
     directoryDatafile = UtilityBCP.DirectoryDataFile;
    }//if ( directoryDatafile == null || directoryDatafile == String.Empty )
   }//set
  }//DirectoryDataFile

  ///<summary>Property.</summary>
  ///<value>DatabaseName.</value>
  public String[] DatabaseName
  {
   get 
   {
    return ( databaseName );
   }//get
   set 
   {
    if ( value != null )
    {
     for ( int valueIndex = 0; valueIndex < value.Length; ++valueIndex )
     {
      value[valueIndex] = value[valueIndex].Trim();
     }//for ( int valueIndex = 0; valueIndex <= value.Length; ++valueIndex )
    }//if ( value != null )
    this.databaseName = value;
   }//set
  }//DatabaseName

  ///<summary>Property.</summary>
  ///<value>KeepIdentityValue.</value>
  public bool KeepIdentityValue
  {
   get 
   {
    return ( keepIdentityValue );
   }//get
   set 
   {
    keepIdentityValue = value;
   }//set
  }//KeepIdentityValue

  ///<summary>Property.</summary>
  ///<value>KeepNullValue.</value>
  public bool KeepNullValue
  {
   get 
   {
    return ( keepNullValue );
   }//get
   set 
   {
    keepNullValue = value;
   }//set
  }//KeepNullValue

  ///<summary>Property.</summary>
  ///<value>MaximumError.</value>
  public int MaximumError
  {
   get 
   {
    return ( maximumError );
   }//get
   set 
   {
    maximumError = value;
   }//set
  }//MaximumError

  ///<summary>Property.</summary>
  ///<value>NativeType.</value>
  public bool NativeType
  {
   get 
   {
    return ( nativeType );
   }//get
   set 
   {
    nativeType = value;
   }//set
  }//NativeType

  ///<summary>Property.</summary>
  ///<value>Password.</value>
  public String Password
  {
   get 
   {
    return ( password );
   }//get
   set 
   {
    password = value.Trim();
   }//set
  }//Password

  ///<summary>Property.</summary>
  ///<value>ServerName.</value>
  public String ServerName
  {
   get 
   {
    return ( serverName );
   }//get
   set 
   {
    serverName = value.Trim();
   }//set
  }//ServerName

  ///<summary>Property.</summary>
  ///<value>TableName.</value>
  public String[] TableName
  {
   get 
   {
    return ( tableName );
   }//get
   set 
   {
    if ( value != null )
    {
     for ( int valueIndex = 0; valueIndex < value.Length; ++valueIndex )
     {
      value[valueIndex] = value[valueIndex].Trim();
     }//for ( int valueIndex = 0; valueIndex <= value.Length; ++valueIndex )
    }//if ( value != null )
    this.tableName = value;
   }//set
  }//tableName

  ///<summary>Property.</summary>
  ///<value>TrustedConnection.</value>
  public bool TrustedConnection
  {
   get 
   {
    return ( trustedConnection );
   }//get
   set 
   {
    trustedConnection = value;
   }//set
  }//TrustedConnection

  ///<summary>Property.</summary>
  ///<value>UserName.</value>
  public String UserName
  {
   get 
   {
    return ( userName );
   }//get
   set 
   {
    userName = value.Trim();
   }//set
  }//UserName

  ///<summary>CapitalLetters.</summary>
  public StringBuilder CapitalLetters()
  {
   StringBuilder sb = new StringBuilder();
   
   if ( BatchSize >= 1 )
   {
    sb.Append( " -b " );
    sb.Append( BatchSize );
   }//if ( BatchSize >= 1 ) 

   if ( CharacterType )
   {
    sb.Append( " -c " );
   }//if ( CharacterType ) 

   if ( KeepIdentityValue )
   {
    sb.Append( " -E " );
   }//if ( keepIdentityValue ) 

   if ( KeepNullValue )
   {
    sb.Append( " -k " );
   }//if ( keepNullValue ) 

   if ( MaximumError >= 1 )
   {
    sb.Append( " -m " );
    sb.Append( MaximumError );
   }//if ( MaximumError >= 1 ) 

   if ( NativeType )
   {
    sb.Append( " -n " );
   }//if ( NativeType )

   if ( Password != null )
   {
    sb.Append( " -P " );
    sb.Append( Password );
   }//if ( Password != null )

   if ( ServerName != null )
   {
    sb.Append( " -S " );
    sb.Append( ServerName );
   }//if ( ServerName != null )

   if ( TrustedConnection )
   {
    sb.Append( " -T " );
   }//if ( TrustedConnection )

   if ( UserName != null )
   {
    sb.Append( " -U " );
    sb.Append( UserName );
   }//if ( UserName != null )

   UtilityDebug.Write
   (
    String.Format
    (
     "Capital Letters: {0}", 
     sb
    )
   );  
   
   return ( sb );

  }//public StringBuilder CapitalLetters()  	  

  /*
  /// <summary>ToString().</summary>
  public override string ToString()
  {
   StringBuilder sb = new StringBuilder();
   sb.Append( serverName );
   return sb.ToString();
  }//public override string ToString()
  */
  
 }//public class UtilityBCPArgument

 /// <summary>UtilityBCP.</summary>
 public class UtilityBCP
 {

  ///<summary>CharacterType.</summary>
  public static bool            CharacterType                               = true;

  ///<summary>NativeType.</summary>
  public static bool            NativeType                                  = false;

  ///<summary>RankDatabaseNamingConventionDatabaseName.</summary>
  public static int             RankDatabaseNamingConventionDatabaseName    = 0;

  ///<summary>RankDatabaseNamingConventionOwnerName.</summary>
  public static int             RankDatabaseNamingConventionOwnerName       = 1;

  ///<summary>RankDatabaseNamingConventionTableName.</summary>
  public static int             RankDatabaseNamingConventionTableName       = 2;
  
  ///<summary>TrustedConnection.</summary>
  public static bool            TrustedConnection                           = true;
  
  ///<summary>BatchSize.</summary>
  public static int             BatchSize                                   = 10;

  ///<summary>BCP.</summary>
  public static String[]        CommandBCP                                  = new String[] { "BCP" };

  ///<summary>The connection string database.</summary>
  public static String          ConnectionStringDatabase                    = @"Provider=SQLOLEDB; Data Source=localhost; Integrated Security=SSPI; Initial Catalog=WordEngineering";

  /// <summary>DatabaseName</summary>
  public static String          DatabaseName                                = null;

  /// <summary>DelimiterStringComma</summary>
  public static String          DelimiterStringComma                        = ",";

  /// <summary>DelimiterString</summary>
  public static String          DelimiterStringFullStop                     = ".";
  
  /// <summary>DelimiterCharArrayComma</summary>
  public static char[]          DelimiterCharArrayComma                     = DelimiterStringComma.ToCharArray();

  /// <summary>DelimiterCharArrayFullStop</summary>
  public static char[]          DelimiterCharArrayFullStop                  = DelimiterStringFullStop.ToCharArray();

  /// <summary>DirectoryDataFile</summary>
  public static String          DirectoryDataFile                           = @"\BCP";

  /// <summary>DomainName.</summary>
  public static String          DomainName                                  = null;

  /// <summary>The configuration XML filename.</summary>
  public static String          FilenameConfigurationXml                    = @"WordEngineering.config";

  /// <summary>FormatBCPCommandOut</summary>
  public static String          FormatBCPCommandOut                         = @"{0} OUT {1} -e {2} {3}";

  /// <summary>FormatBCPDirectory</summary>
  public static String          FormatBCPDirectory                         = @"{0}\{1}\{2}";

  /// <summary>FormatBCPDatafile</summary>
  public static String          FormatBCPDataFile                          = @"{0}\{1}{2}.txt";

  /// <summary>FormatBCPErrorfile</summary>
  public static String          FormatBCPErrorFile                         = @"{0}\{1}{2}.err";

  /// <summary>FormatDate</summary>
  public static String          FormatDate                                 = "yyyyMMddHHmm";

  ///<summary>KeepIdentityValue.</summary>
  public static bool            KeepIdentityValue                           = true;

  ///<summary>KeepNullValue.</summary>
  public static bool            KeepNullValue                               = true;

  ///<summary>MaximumError.</summary>
  public static int             MaximumError                                = 1000;

  /// <summary>Password.</summary>
  public static String          Password                                    = null;

  /// <summary>TableName</summary>
  public static String          TableName                                   = null;

  /// <summary>UserName.</summary>
  public static String          UserName                                    = null;

  /// <summary>The XPath BCPDatabase.</summary>
  public static String          XPathBCPDatabase                            = @"/word/bcp/database";

  /// <summary>The XPath BCPDirectoryDataFile.</summary>
  public static String          XPathBCPDirectoryDataFile                   = @"/word/bcp/directoryDatafile";

  /// <summary>The XPath BCPTable.</summary>
  public static String          XPathBCPTable                               = @"/word/bcp/table";

  /// <summary>The XPath Connection String Database Format.</summary>
  public static String          XPathConnectionStringDatabase               = @"/word/database/sqlServer/wordEngineering/databaseConnectionString";

  /// <summary>The XPath Connection String Database Format.</summary>
  public static String          XPathConnectionStringDatabaseFormat         = @"/word/database/sqlServer/{0}/databaseConnectionString";

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main
  (
    String[] argv
  )
  {
   Boolean                         booleanParseCommandLineArguments  =  false;
   String                          exceptionMessage                  =  null;

   UtilityBCPArgument  utilityBCPArgument    =  null;
   
   utilityBCPArgument = new UtilityBCPArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilityBCPArgument
   );
   
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    UtilityDebug.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilityBCPArgument ) )    
    );  
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   BCP
   (
    ref utilityBCPArgument,
    ref exceptionMessage
   );
  
  }//public static void Main()

  /// <summary>BCP.</summary>
  public static void BCP
  (
   ref UtilityBCPArgument utilityBCPArgument,
   ref String             exceptionMessage
  )
  {
   XmlNodeList         xmlNodeListDatabase  =  null;
   ArrayList           arrayListTableName   =  null;

   BCP
   (
    ref FilenameConfigurationXml,
    ref xmlNodeListDatabase,
    ref utilityBCPArgument,
    ref arrayListTableName,
    ref exceptionMessage
   );
   
  }//public static void BCP()
  
  /// <summary>BCP.</summary>
  public static void BCP
  (
   ref String              filenameConfigurationXml,
   ref XmlNodeList         xmlNodeListDatabase,
   ref UtilityBCPArgument  utilityBCPArgument,
   ref ArrayList           arrayListTableName,
   ref String              exceptionMessage
  )
  {

   Boolean                redirectStandardOutputError = true;
   
   String                 BCPCommandOut;
   String                 BCPDataFile;
   String                 BCPDirectory;
   String                 BCPErrorFile;   
   String[]               databaseOwnerTableName;
   String                 dateTimeNow                 =  DateTime.Now.ToString(FormatDate);
   
   String                 redirectStandardOutput      = null;
   String                 redirectStandardError       = null;
   String                 verb                        = null;
   
   StringBuilder          capitalLetters;
   
   HttpContext            httpContext                 = HttpContext.Current;
   
   arrayListTableName = new ArrayList();
   
   if ( utilityBCPArgument.DatabaseName == null || utilityBCPArgument.DatabaseName.Length == 0 )
   {
   	utilityBCPArgument.DatabaseName  =  DatabaseName.Split( DelimiterCharArrayComma );
   }//if ( utilityBCPArgument.DatabaseName == null || utilityBCPArgument.DatabaseName.Length == 0 )	

   if ( utilityBCPArgument.TableName == null || utilityBCPArgument.TableName.Length == 0 )
   {
   	utilityBCPArgument.TableName = UtilityBCP.TableName.Split( UtilityBCP.DelimiterCharArrayComma );
   }//if ( utilityBCPArgument.TableName == null || utilityBCPArgument.TableName.Length == 0 )	

   if ( utilityBCPArgument.TableName != null && utilityBCPArgument.TableName.Length != 0 )
   {
    foreach ( String tableNameCurrent in utilityBCPArgument.TableName )
    {
   	 if ( tableNameCurrent.Trim() == String.Empty )
   	 {
      continue;   	 	
     }//if ( tableNameCurrent.Trim() == String.Empty )   	 	
   	 arrayListTableName.Add( tableNameCurrent );
    }//foreach ( String tableNameCurrent in utilityBCPArgument.tableName )   	
   }//if ( utilityBCPArgument.TableName != null && utilityBCPArgument.TableName.Length != 0 )
      
   UtilityDatabase.PopulateDatabaseTable
   (
    ref filenameConfigurationXml,
    ref xmlNodeListDatabase,
    ref utilityBCPArgument.databaseName,
    ref arrayListTableName,
    ref exceptionMessage
   ); 

   capitalLetters = utilityBCPArgument.CapitalLetters();
   
   foreach (object databaseOwnerTableNameCurrent in arrayListTableName)
   {
    databaseOwnerTableName = ((String) (databaseOwnerTableNameCurrent)).Split( DelimiterCharArrayFullStop );

    BCPDirectory           = String.Format
    (
     FormatBCPDirectory,
     utilityBCPArgument.DirectoryDataFile.ToString(),
     databaseOwnerTableName[RankDatabaseNamingConventionDatabaseName],
     databaseOwnerTableName[RankDatabaseNamingConventionTableName]
    ); 

    BCPDataFile            = String.Format
    (
     FormatBCPDataFile,
     BCPDirectory,
     databaseOwnerTableName[RankDatabaseNamingConventionTableName],
     dateTimeNow
    ); 

    BCPErrorFile            = String.Format
    (
     FormatBCPErrorFile,
     BCPDirectory,
     databaseOwnerTableName[RankDatabaseNamingConventionTableName],
     dateTimeNow
    ); 
    
    BCPCommandOut = String.Format
    (
     FormatBCPCommandOut,
     databaseOwnerTableNameCurrent,
     BCPDataFile,
     BCPErrorFile,
     capitalLetters
    ); 

    UtilityDebug.Write
    (
     String.Format
     (
      "BCP Directory: {0} | BCPDataFile: {1} | BCPErrorFile: {2} | BCPCommandOut: {3}",
      BCPDirectory,
      BCPDataFile,
      BCPErrorFile,
      BCPCommandOut
     )
    );

    UtilityDirectory.CreateDirectory
    (
     ref BCPDirectory,
     ref exceptionMessage
    );

    UtilityProcess.FileStart
    (
     ref CommandBCP,
     ref BCPCommandOut,
     ref verb,
     ref redirectStandardOutputError,
     ref redirectStandardOutput,
     ref redirectStandardError
    );
    
   }//foreach (object databaseOwnerTableNameCurrent in arrayListTableName)
  }//public static void BCP()
  
  /// <summary>ConfigurationXml.</summary>
  public static void ConfigurationXml
  (
   ref String        filenameConfigurationXml
  ) 
  {
   String        exceptionMessage                 =  null;
   
   ConfigurationXml
   (
    ref filenameConfigurationXml,
    ref exceptionMessage,
    ref ConnectionStringDatabase,
    ref DatabaseName,
    ref DirectoryDataFile,
    ref TableName,
    ref UserName,
    ref DomainName,
    ref Password
   );
  }//public static void ConfigurationXml()

  /// <summary>ConfigurationXml.</summary>
  public static void ConfigurationXml
  (
   ref String         filenameConfigurationXml,
   ref String         exceptionMessage,
   ref String         connectionStringDatabase,
   ref String         databaseName,
   ref String         directoryDatafile,
   ref String         tableName,
   ref String         userName,
   ref String         domainName,
   ref String         password
  )
  {

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,         
         XPathConnectionStringDatabase,
     ref connectionStringDatabase
   );

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,         
         XPathBCPDatabase,
     ref databaseName
   );

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,         
         XPathBCPDirectoryDataFile,
     ref directoryDatafile
   );

   UtilityXml.XmlDocumentNodeInnerText
   (
         filenameConfigurationXml,
     ref exceptionMessage,         
         XPathBCPTable,
     ref tableName
   );

   UtilityImpersonate.GetUsernamePasswordDomainName
   (
    ref filenameConfigurationXml,
    ref userName,
    ref domainName,
    ref password,
    ref exceptionMessage
   );

   UtilityDebug.Write
   (
    String.Format
    (
     "filenameConfigurationXml: {0} | connectionStringDatabase: {1} | databaseName: {2} | directoryDatafile: {3} | tableName: {4}", 
     filenameConfigurationXml,
     connectionStringDatabase,
     databaseName,
     directoryDatafile,
     tableName
    )
   );
      
  }//public static void ConfigurationXml()

  static UtilityBCP()
  {
   ConfigurationXml
   (
    ref FilenameConfigurationXml
   );
  }//static UtilityBCP()

 }//public class UtilityBCP
}//namespace WordEngineering