all: UtilitySNMP.exe

UtilitySNMP.exe: UtilitySNMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySNMPDocumentation.xml /main:WordEngineering.UtilitySNMP /out:UtilitySNMP.exe /target:exe UtilitySNMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation