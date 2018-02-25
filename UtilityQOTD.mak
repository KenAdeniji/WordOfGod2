all: UtilityQOTDServer.exe UtilityQOTDClient.exe

UtilityQOTDServer.exe: UtilityQOTDServer.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityQOTDServerDocumentation.xml /main:WordEngineering.UtilityQOTDServer /out:UtilityQOTDServer.exe /target:exe UtilityQOTDServer.cs

UtilityQOTDClient.exe: UtilityQOTDClient.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityQOTDClientDocumentation.xml /main:WordEngineering.UtilityQOTDClient /out:UtilityQOTDClient.exe /target:exe UtilityQOTDClient.cs