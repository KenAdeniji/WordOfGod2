all: UtilityProcess.exe

UtilityProcess.exe: UtilityProcess.cs UtilityDebug.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityProcessDocumentation.xml /main:WordEngineering.UtilityProcess /out:UtilityProcess.exe /target:exe UtilityProcess.cs UtilityDebug.cs
 
Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation 