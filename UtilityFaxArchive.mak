all: FaxComTypeLib.dll UtilityFax.exe FaxPage.aspx.cs.dll

FaxComTypeLib.dll: %SystemRoot%\System32\FaxCom.dll
 TLBIMP %SystemRoot%\System32\FaxCom.dll /Out:FaxComTypeLib.dll
 ILDASM FaxComTypeLib.dll
 Xcopy FaxComTypeLib.dll bin /C /D /E /I /R /S /Y /Z

UtilityFax.exe: FaxComTypeLib.dll UtilityFax.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs  UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityFaxDocumentation.xml /main:WordEngineering.UtilityFax /out:UtilityFax.exe /reference:FaxComTypeLib.dll /target:exe UtilityFax.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs

FaxPage.aspx.cs.dll: FaxPage.aspx.cs UtilityFax.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs
 csc /doc:XmlDocumentation\FaxWebFormDocumentation.xml /out:bin\FaxPage.aspx.cs.dll /reference:FaxComTypeLib.dll /target:library FaxPage.aspx.cs UtilityFax.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation