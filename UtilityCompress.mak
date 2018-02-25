all: UtilityCompress.exe

UtilityCompress.exe: UtilityCommandLineArgument.cs UtilityCompress.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityCompressDocumentation.xml /main:WordEngineering.UtilityCompress /out:UtilityCompress.exe /target:exe UtilityCommandLineArgument.cs UtilityCompress.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
