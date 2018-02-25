all: UtilityCommandLineArgument.exe

UtilityCommandLineArgument.exe: UtilityCommandLineArgument.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityCommandLineArgument.xml /main:WordEngineering.UtilityCommandLineArgument /out:UtilityCommandLineArgument.exe /target:exe /unsafe UtilityCommandLineArgument.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation