all: bin\UtilityMemory.dll UtilityMemory.exe
 
bin\UtilityMemory.dll: UtilityMemory.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityMemoryDocumentation.xml /out:bin\UtilityMemory.dll /target:library UtilityMemory.cs

UtilityMemory.exe: UtilityMemory.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityMemoryDocumentation.xml /main:WordEngineering.UtilityMemory /out:UtilityMemory.exe /target:exe UtilityMemory.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 