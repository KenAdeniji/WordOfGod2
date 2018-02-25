all: RegularExpressionPage.aspx.cs.dll RegularExpressionMaintenancePage.aspx.cs.dll UtilityRegularExpression.exe

UtilityRegularExpression.exe: UtilityRegularExpression.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityRegularExpressionDocumentation.xml /main:WordEngineering.UtilityRegularExpression /out:UtilityRegularExpression.exe /target:exe /unsafe UtilityRegularExpression.cs  UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityProcess.cs UtilityXml.cs

RegularExpressionPage.aspx.cs.dll: RegularExpressionPage.aspx.cs UtilityRegularExpression.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RegularExpressionPageDocumentation.xml /out:bin\RegularExpressionPage.aspx.cs.dll /target:library /unsafe RegularExpressionPage.aspx.cs UtilityRegularExpression.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityXml.cs

RegularExpressionMaintenancePage.aspx.cs.dll: RegularExpressionMaintenancePage.aspx.cs UtilityRegularExpression.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityXml.cs bin\UtilityPostback.dll
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RegularExpressionMaintenancePageDocumentation.xml /out:bin\RegularExpressionMaintenancePage.aspx.cs.dll /reference:bin\UtilityPostback.dll /target:library /unsafe RegularExpressionMaintenancePage.aspx.cs UtilityRegularExpression.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityProcess.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation