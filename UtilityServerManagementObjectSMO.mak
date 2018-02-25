all: SQLSCMTypeLib.dll UtilityServerManagementObjectSMO.exe 

SQLDMOTypeLib.dll: "C:\Program Files\Microsoft SQL Server\90\Tools\binn\Resources\1033\SQLDMO90.RLL"
 TLBIMP "C:\Program Files\Microsoft SQL Server\90\Tools\binn\Resources\1033\SQLDMO90.RLL" /Out:SQLDMOTypeLib.dll
 ILDASM SQLDMOTypeLib.dll
 Xcopy SQLDMOTypeLib.dll bin /C /D /E /I /R /S /Y /Z

SQLSCMTypeLib.dll: "D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Binn\sqlscm90.dll"
 TLBIMP "D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Binn\sqlscm90.dll" /Out:SQLSCMTypeLib.dll
 ILDASM SQLSCMTypeLib.dll
 Xcopy SQLSCMTypeLib.dll bin /C /D /E /I /R /S /Y /Z

UtilityServerManagementObjectSMO.exe:  UtilityServerManagementObjectSMO.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityServerManagementObjectSMODocumentation.xml /main:WordEngineering.UtilityServerManagementObjectSMO /out:UtilityServerManagementObjectSMO.exe /target:exe /reference:sqlscm90.dll UtilityServerManagementObjectSMO.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation