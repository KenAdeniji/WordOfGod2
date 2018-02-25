all: UtilityNetSession.exe

UtilityNetSession.exe: UtilityNetSession.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityNetSessionDocumentation.xml /main:WordEngineering.UtilityNetSession /out:UtilityNetSession.exe /target:exe UtilityNetSession.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
