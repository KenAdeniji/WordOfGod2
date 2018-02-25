all: UtilitySQLServer.exe

UtilitySQLServer.exe: UtilitySQLServer.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySQLServerDocumentation.xml /main:WordEngineering.UtilitySQLServer /out:UtilitySQLServer.exe /target:exe UtilitySQLServer.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation