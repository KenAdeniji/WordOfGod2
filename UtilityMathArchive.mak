all: UtilityMath.exe

UtilityMath.exe: UtilityMath.cs 
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityMathDocumentation.xml /out:UtilityMath.exe /target:exe UtilityMath.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation