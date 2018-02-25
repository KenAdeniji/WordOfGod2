all: UtilityWebControl.dll

UtilityWebControl.dll: UtilityWebControl.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWebControlDocumentation.xml /out:bin\UtilityWebControl.dll /target:library UtilityWebControl.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation