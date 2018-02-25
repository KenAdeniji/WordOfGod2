all: bin\UtilitySQLCLR.dll UtilitySQLCLR.exe bin\TabularEventLog.dll

bin\UtilitySQLCLR.dll: UtilitySQLCLR.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySQLCLRDocumentation.xml /keyFile:WordEngineering.snk /out:bin\UtilitySQLCLR.dll /target:library UtilitySQLCLR.cs

UtilitySQLCLR.exe: UtilitySQLCLR.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySQLCLRDocumentation.xml /main:WordEngineering.UtilitySQLCLR /out:UtilitySQLCLR.exe /target:exe UtilitySQLCLR.cs

bin\TabularEventLog.dll: TabularEventLog.cs
 csc /define:DEBUG /debug:full /keyFile:D:\GacKey\WordEngineering.snk /out:bin\TabularEventLog.dll /target:library TabularEventLog.cs

Clean: