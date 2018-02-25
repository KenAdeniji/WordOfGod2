all: UtilityNetworkNewsTransferProtocolNNTP.exe NetworkNewsTransferProtocolNNTPPage.aspx.cs.dll

NetworkNewsTransferProtocolNNTPPage.aspx.cs.dll: NetworkNewsTransferProtocolNNTPPage.aspx.cs UtilityNetworkNewsTransferProtocolNNTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\NetworkNewsTransferProtocolNNTPPageDocumentation.xml /out:bin\NetworkNewsTransferProtocolNNTPPage.aspx.cs.dll /target:library NetworkNewsTransferProtocolNNTPPage.aspx.cs UtilityNetworkNewsTransferProtocolNNTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

UtilityNetworkNewsTransferProtocolNNTP.exe: UtilityNetworkNewsTransferProtocolNNTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityNetworkNewsTransferProtocolNNTP.xml /main:WordEngineering.UtilityNetworkNewsTransferProtocolNNTP /out:UtilityNetworkNewsTransferProtocolNNTP.exe /target:exe UtilityNetworkNewsTransferProtocolNNTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation