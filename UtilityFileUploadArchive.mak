all: UtilityFileUpload.exe FileUploadPage.dll

UtilityFileUpload.exe: UtilityFileUpload.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityFileUpload.xml /main:WordEngineering.UtilityFileUpload /out:UtilityFileUpload.exe /target:exe /unsafe UtilityFileUpload.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

FileUploadPage.dll: FileUploadPage.aspx.cs UtilityFileUpload.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\FileUploadPageDocumentation.xml /out:bin\FileUploadPage.aspx.cs.dll /target:library /unsafe FileUploadPage.aspx.cs UtilityFileUpload.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation