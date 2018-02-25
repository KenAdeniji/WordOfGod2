all: bin\UtilityProtocol.dll

bin\UtilityProtocol.dll: UtilityProtocol.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityProtocolDocumentation.xml /out:bin\UtilityProtocol.dll /target:library /unsafe UtilityProtocol.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation