all: UtilityImpersonate.exe

UtilityImpersonate.exe: UtilityImpersonate.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityException.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityImpersonateDocumentation.xml /main:WordEngineering.UtilityImpersonate /out:UtilityImpersonate.exe /target:exe UtilityImpersonate.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityException.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation