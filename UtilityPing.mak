all: PingPage.aspx.cs.dll UtilityPing.exe

PingPage.aspx.cs.dll: PingPage.aspx.cs UtilityCollection.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityPing.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\PingPageDocumentation.xml /out:bin\PingPage.aspx.cs.dll /target:library PingPage.aspx.cs UtilityCollection.cs UtilityDebug.cs UtilityDirectory.cs UtilityFile.cs UtilityPing.cs UtilityXml.cs

UtilityPing.exe: UtilityPing.cs
 csc /doc:XmlDocumentation\UtilityPingDocumentation.xml /main:WordEngineering.UtilityPing /out:UtilityPing.exe /target:exe UtilityPing.cs
 
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 