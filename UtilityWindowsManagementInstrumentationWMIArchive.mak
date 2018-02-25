all: UtilityWindowsManagementInstrumentationWMI.exe

UtilityWindowsManagementInstrumentationWMI.exe: UtilityWindowsManagementInstrumentationWMI.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDirectory.cs UtilityDebug.cs UtilityException.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWindowsManagementInstrumentationWMIDocumentation.xml /main:WordEngineering.UtilityWindowsManagementInstrumentationWMI /out:UtilityWindowsManagementInstrumentationWMI.exe /target:exe UtilityWindowsManagementInstrumentationWMI.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDirectory.cs UtilityDebug.cs UtilityException.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation