all: UtilityWakeOnLan.exe

UtilityWakeOnLan.exe: UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityHex.cs UtilityWakeOnLan.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWakeOnLanDocumentation.xml /main:WordEngineering.UtilityWakeOnLan /out:UtilityWakeOnLan.exe /target:exe UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityHex.cs UtilityWakeOnLan.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
