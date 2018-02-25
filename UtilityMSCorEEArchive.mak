all: UtilityMSCorEE.exe 

UtilityMSCorEE.exe: UtilityMSCorEE.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityMSCorEEDocumentation.xml /main:WordEngineering.UtilityMSCorEE /out:UtilityMSCorEE.exe /target:exe UtilityMSCorEE.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityImpersonate.cs UtilityJavaScript.cs UtilityProcess.cs UtilityProperty.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation