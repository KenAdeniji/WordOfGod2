all: bin\SpeechPage.aspx.cs.dll UtilitySpeech.exe 

bin\SpeechTypeLib.dll: "%SystemDrive%\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll"
 REGSVR32  "%SystemDrive%\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll"
 TLBIMP    "%SystemDrive%\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll" /Out:bin\SpeechTypeLib.dll
 ILDASM    bin\SpeechTypeLib.dll

bin\SpeechPage.aspx.cs.dll: SpeechPage.aspx.cs SpeechTypeLib.dll UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilitySpeech.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SpeechPageDocumentation.xml /out:bin\SpeechPage.aspx.cs.dll /reference:bin\SpeechTypeLib.dll /target:library SpeechPage.aspx.cs UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilitySpeech.cs

UtilitySpeech.exe: SpeechTypeLib.dll UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilitySpeech.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySpeechDocumentation.xml /main:WordEngineering.UtilitySpeech /out:UtilitySpeech.exe /reference:bin\SpeechTypeLib.dll /target:exe UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilitySpeech.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation