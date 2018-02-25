all: ContentSimplePage.dll MasterSimplePage.dll

ContentSimplePage.dll: ContentSimplePage.aspx.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\ContentSimplePageDocumentation.xml /out:bin\ContentSimplePage.aspx.cs.dll /target:library /unsafe ContentSimplePage.aspx.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

MasterSimplePage.dll: MasterSimplePage.master.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\MasterSimplePageDocumentation.xml /out:bin\MasterSimplePage.master.cs.dll /target:library /unsafe MasterSimplePage.master.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation