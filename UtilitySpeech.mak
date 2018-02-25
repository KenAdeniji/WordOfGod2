all: bin\SpeechPage.aspx.cs.dll UtilitySpeech.exe 

bin\SpeechTypeLib.dll: "%SystemDrive%\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll"
 REGSVR32  "%SystemDrive%\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll"
 TLBIMP    "%SystemDrive%\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll" /Out:bin\SpeechTypeLib.dll
 ILDASM    bin\SpeechTypeLib.dll

bin\SpeechPage.aspx.cs.dll: SpeechTypeLib.dll SpeechPage.aspx.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilitySpeech.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SpeechPageDocumentation.xml /out:bin\SpeechPage.aspx.cs.dll /reference:bin\SpeechTypeLib.dll /target:library /unsafe SpeechPage.aspx.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilitySpeech.cs UtilityXml.cs

UtilitySpeech.exe: SpeechTypeLib.dll UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilitySpeech.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySpeechDocumentation.xml /main:WordEngineering.UtilitySpeech /out:UtilitySpeech.exe /reference:SpeechTypeLib.dll /target:exe /unsafe UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilitySpeech.cs UtilityXml.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation