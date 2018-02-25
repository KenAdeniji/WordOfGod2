all: UtilityKernel32.exe

UtilityKernel32.exe: UtilityKernel32.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityKernel32.xml /main:WordEngineering.UtilityKernel32 /out:UtilityKernel32.exe /target:exe /unsafe UtilityKernel32.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation