all: bin\WMPTypeLib.dll UtilityWindowsMediaPlayerWMP.exe

bin\WMPTypeLib.dll: %SystemRoot%\System32\wmp.dll
 TLBIMP %SystemRoot%\System32\wmp.dll /Out:bin\WMPTypeLib.dll
 ILDASM bin\WMPTypeLib.dll

UtilityWindowsMediaPlayerWMP.exe: UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityWindowsMediaPlayerWMP.cs WMPTypeLib.dll
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWindowsMediaPlayerWMPDocumentation.xml /main:WordEngineering.UtilityWindowsMediaPlayerWMP /out:UtilityWindowsMediaPlayerWMP.exe /reference:bin\WMPTypeLib.dll /target:exe

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation