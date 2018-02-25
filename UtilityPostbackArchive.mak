all: UtilityPostback.dll

UtilityPostback.dll: UtilityPostback.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityPostbackDocumentation.xml /out:bin\UtilityPostback.dll /target:library /unsafe UtilityPostback.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation