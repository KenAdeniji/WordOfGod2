all: UtilityCom.dll UtilityCom.exe

UtilityCom.dll: UtilityCom.cs WordEngineering.snk
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityComDocumentation.xml /keyFile:WordEngineering.snk /out:UtilityCom.dll /target:library UtilityCom.cs

UtilityCom.exe: UtilityCom.cs WordEngineering.snk
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityComDocumentation.xml /keyFile:WordEngineering.snk /main:WordEngineering.UtilityCom /out:UtilityCom.exe /target:exe UtilityCom.cs 

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation