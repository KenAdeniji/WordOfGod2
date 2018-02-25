all: UtilityRequest.dll

UtilityRequest.dll: UtilityRequest.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityRequestDocumentation.xml /out:bin\UtilityRequest.dll /target:library /unsafe UtilityRequest.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation