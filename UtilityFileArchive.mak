all: UtilityFile.exe

UtilityFile.exe: UtilityFile.cs
 csc /define:DEBUG /debug:full /doc:UtilityFileDocumentation.xml /main:WordEngineering.UtilityFile /out:UtilityFile.exe /target:exe UtilityFile.cs
 
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 