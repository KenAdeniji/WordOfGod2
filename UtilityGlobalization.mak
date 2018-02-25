all: UtilityGlobalization.exe
 
UtilityGlobalization.exe: UtilityGlobalization.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityGlobalizationDocumentation.xml /main:WordEngineering.UtilityGlobalization /out:UtilityGlobalization.exe /target:exe UtilityGlobalization.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 