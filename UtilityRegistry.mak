all: UtilityRegistry.exe

UtilityRegistry.exe: UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityRegistry.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityRegistry.xml /main:WordEngineering.UtilityRegistry /out:UtilityRegistry.exe /target:exe /unsafe UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityRegistry.cs UtilityXml.cs

Clean:
 DEL RegistryDocumentation /F /S /Q
 RD RegistryDocumentation