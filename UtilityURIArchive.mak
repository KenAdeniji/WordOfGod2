all: UtilityURI.exe bin\URIMaintenancePage.aspx.cs.dll bin\URIPage.aspx.cs.dll bin\URIXMLPage.aspx.cs.dll

bin\URIMaintenancePage.aspx.cs.dll: URIMaintenancePage.aspx.cs UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDirectory.cs UtilityDatabase.cs UtilityDebug.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs bin\UtilityPostback.dll bin\UtilityWebControl.dll
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\URIMaintenancePageDocumentation.xml /out:bin\URIMaintenancePage.aspx.cs.dll /reference:bin\UtilityPostback.dll /reference:bin\UtilityWebControl.dll /target:library URIMaintenancePage.aspx.cs UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs

bin\URIPage.aspx.cs.dll: URIPage.aspx.cs UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDirectory.cs UtilityDatabase.cs UtilityDebug.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\URIPageDocumentation.xml /out:bin\URIPage.aspx.cs.dll /target:library URIPage.aspx.cs UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs

bin\URIXMLPage.aspx.cs.dll: URIXMLPage.aspx.cs UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDirectory.cs UtilityDatabase.cs UtilityDebug.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\URIXMLPageDocumentation.xml /out:bin\URIXMLPage.aspx.cs.dll /target:library URIXMLPage.aspx.cs UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs

UtilityURI.exe: UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityURI.xml /main:WordEngineering.UtilityURI /out:UtilityURI.exe /target:exe UtilityURI.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation