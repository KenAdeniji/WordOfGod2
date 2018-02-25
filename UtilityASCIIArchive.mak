all: UtilityASCII.exe

UtilityASCII.exe: UtilityASCII.cs 
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityASCIIDocumentation.xml /out:UtilityASCII.exe /reference:Microsoft.VisualBasic.dll /target:exe UtilityASCII.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation