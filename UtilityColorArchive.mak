all: bin\UtilityColor.dll UtilityColor.exe
 
bin\UtilityColor.dll: UtilityColor.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityColorDocumentation.xml /out:bin\UtilityColor.dll /target:library UtilityColor.cs

UtilityColor.exe: UtilityColor.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityColorDocumentation.xml /main:WordEngineering.UtilityColor /out:UtilityColor.exe /target:exe UtilityColor.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 