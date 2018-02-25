all: bin\UtilityIO.dll UtilityIO.exe
 
bin\UtilityIO.dll: UtilityIO.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityIODocumentation.xml /out:bin\UtilityIO.dll /target:library UtilityIO.cs

UtilityIO.exe: UtilityIO.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityIODocumentation.xml /main:WordEngineering.UtilityIO /out:UtilityIO.exe /target:exe UtilityIO.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 