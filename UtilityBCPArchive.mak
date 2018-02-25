all: UtilityBCP.exe BCPPage.aspx.cs.dll

UtilityBCP.exe: UtilityBCP.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityBCPDocumentation.xml /main:WordEngineering.UtilityBCP /out:UtilityBCP.exe /target:exe UtilityBCP.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityXml.cs

BCPPage.aspx.cs.dll: BCPPage.aspx.cs UtilityBCP.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\BCPPageDocumentation.xml /out:bin\BCPPage.aspx.cs.dll /target:library BCPPage.aspx.cs UtilityBCP.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation