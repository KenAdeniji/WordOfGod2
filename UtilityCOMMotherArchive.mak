all: UtilityCOMMother.dll

UtilityCOMMother.dll: UtilityCOMMother.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityCOMMother.xml /out:UtilityCOMMother.dll /target:library UtilityCOMMother.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation