all: bin\UtilitySQLCLR.dll UtilitySQLCLR.exe

bin\UtilitySQLCLR.dll: UtilitySQLCLR.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySQLCLRDocumentation.xml /keyFile:WordEngineering.snk /out:bin\UtilitySQLCLR.dll /target:library UtilitySQLCLR.cs

UtilitySQLCLR.exe: UtilitySQLCLR.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySQLCLRDocumentation.xml /main:WordEngineering.UtilitySQLCLR /out:UtilitySQLCLR.exe /target:exe UtilitySQLCLR.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
