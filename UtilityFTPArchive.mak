all: UtilityFTP.exe

UtilityFTP.exe: UtilityFTP.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityFTP.xml /main:WordEngineering.UtilityFTP /out:UtilityFTP.exe /target:exe /unsafe UtilityFTP.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation