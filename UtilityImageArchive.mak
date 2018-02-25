all: UtilityImage.exe FileUploadImagePage.dll

UtilityImage.exe: UtilityImage.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityImage.xml /main:WordEngineering.UtilityImage /out:UtilityImage.exe /target:exe /unsafe UtilityImage.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

FileUploadImagePage.dll: FileUploadImagePage.aspx.cs UtilityImage.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\FileUploadImagePageDocumentation.xml /out:bin\FileUploadImagePage.aspx.cs.dll /target:library /unsafe FileUploadImagePage.aspx.cs UtilityImage.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation