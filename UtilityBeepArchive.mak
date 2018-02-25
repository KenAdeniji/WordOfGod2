all: bin\UtilityBeep.dll UtilityBeep.exe

bin\UtilityBeep.dll: UtilityBeep.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityBeepDocumentation.xml /out:bin\UtilityBeep.dll /r:Microsoft.VisualBasic.dll /target:library UtilityBeep.cs

UtilityBeep.exe: UtilityBeep.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityBeepDocumentation.xml /main:WordEngineering.UtilityBeep /out:UtilityBeep.exe /r:Microsoft.VisualBasic.dll /target:exe UtilityBeep.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation