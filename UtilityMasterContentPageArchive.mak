all: ContentSimplePage.dll MasterSimplePage.master.cs

ContentSimplePage.dll: ContentSimplePage.aspx.csUtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\ContentSimplePageDocumentation.xml /out:bin\ContentSimplePage.aspx.cs.dll /target:library /unsafe ContentSimplePage.aspx.csUtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

MasterSimplePage.dll: MasterSimplePage.aspx.csUtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\MasterSimplePageDocumentation.xml /out:bin\MasterSimplePage.aspx.cs.dll /target:library /unsafe MasterSimplePage.aspx.csUtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation