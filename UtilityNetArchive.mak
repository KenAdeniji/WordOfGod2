all: UtilityNet.exe

UtilityNet.exe: UtilityNet.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityNetDocumentation.xml /main:WordEngineering.UtilityNet /out:UtilityNet.exe /target:exe UtilityNet.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation